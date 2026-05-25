using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibs;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using CVPoint = OpenCvSharp.Point;
using System.Drawing;

namespace YoloSharp
{
    public class PlatePosition
    {
        public int x, y;
        List<Rectangle> charPosition;
        public PlatePosition()
        {
            x = 0; y = 0;
            charPosition = new List<Rectangle>();
        }
        public void DrawPosition()
        {

        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct bbox_t
    {
        public UInt32 x, y, w, h;    // (x,y) - top-left corner, (w, h) - width & height of bounded box
        public float prob;                 // confidence - probability that the object was found correctly
        public UInt32 obj_id;        // class of object - from range [0, classes-1]
        public UInt32 track_id;      // tracking id for video (0 - untracked, 1 - inf - tracked object)
        public UInt32 frames_counter;
        public float x_3d, y_3d, z_3d;                 // confidence - probability that the object was found correctly
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct BboxContainer
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)]
        public bbox_t[] candidates;
    }
    public class YoloCPU
    {
        YoloConfig _conf;
        Net _detector;
        int _nDetectWidth;
        int _nDetectHeight;
        double _dConf;
        const float _dnmsThreshold = 0.5f;    //threshold for nms
        public YoloCPU(string Configuration, string Weights, double dConf)
        {
            if (File.Exists(Configuration) && File.Exists(Weights))
            {
                _conf = new YoloConfig(Configuration);
                string width = _conf.GetWidthSetting();
                string Height = _conf.GetHeightSetting();
                _detector = CvDnn.ReadNetFromDarknet(Configuration, Weights);
                _detector.SetPreferableBackend(Backend.OPENCV);
                _detector.SetPreferableTarget(Target.CPU);
                _nDetectWidth = int.Parse(width);
                _nDetectHeight = int.Parse(Height); ;
                _dConf = dConf;
            }
            else
            {
                MessageBox.Show("Missing data file");
            }
        }
        public int GetDetectWidth()
        {
            return _nDetectWidth;
        }
        public int GetDetectHeight()
        {
            return _nDetectHeight;
        }
        public List<bbox_t> Detect(Mat imgInput)
        {
            List<bbox_t> lst = new List<bbox_t>();
            try
            {
                if (imgInput == null) return lst;
                Mat imgProcess;
                imgProcess = imgInput.Clone();
                double dRatioX = (double)imgInput.Width / (double)_nDetectWidth;
                double dRatioY = (double)imgInput.Height / (double)_nDetectHeight;
                if (imgInput.Width > _nDetectWidth || imgInput.Height > _nDetectHeight)
                {
                    imgProcess = imgInput.Resize(new OpenCvSharp.Size(_nDetectWidth, _nDetectHeight));
                }
                else
                {
                    dRatioX = 1; dRatioY = 1;
                }
                var blob = CvDnn.BlobFromImage(imgProcess, 1.0 / 255, new OpenCvSharp.Size(_nDetectWidth, _nDetectHeight), new Scalar(), true, false);
                _detector.SetInput(blob);
                var outNames = _detector.GetUnconnectedOutLayersNames();
                var outs = outNames.Select(_ => new Mat()).ToArray();
                _detector.Forward(outs, outNames);
                GetResult(outs, imgProcess, _dConf, _dnmsThreshold, dRatioX, dRatioY, ref lst);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
            return lst;
        }
        private void GetResult(IEnumerable<Mat> output, Mat image, double threshold, float nmsThreshold, double dRatioX, double dRatioY, ref List<bbox_t> bboxes)
        {
            var classIds = new List<int>();
            var confidences = new List<float>();
            var probabilities = new List<float>();
            var boxes = new List<Rect2d>();
            int nCountOK = 0;
            var w = image.Width;
            var h = image.Height;

            const int prefix = 5;   //skip 0~4

            foreach (var prob in output)
            {
                for (var i = 0; i < prob.Rows; i++)
                {
                    var confidence = prob.At<float>(i, 4);
                    if (confidence > threshold)
                    {
                        //get classes probability
                        nCountOK++;
                        CVPoint max, min;
                        Cv2.MinMaxLoc(prob.Row(i).ColRange(prefix, prob.Cols), out min, out max);
                        var classes = max.X;
                        var probability = prob.At<float>(i, classes + prefix);

                        if (probability > threshold) //more accuracy, you can cancel it
                        {
                            //get center and width/height
                            int centerX = (int)(prob.At<float>(i, 0) * w);
                            int centerY = (int)(prob.At<float>(i, 1) * h);

                            var width = prob.At<float>(i, 2) * w;
                            var height = prob.At<float>(i, 3) * h;

                            //put data to list for NMSBoxes
                            classIds.Add(classes);
                            confidences.Add(confidence);
                            probabilities.Add(probability);
                            boxes.Add(new Rect2d(centerX, centerY, width, height));
                        }
                    }
                }
            }

            //using non-maximum suppression to reduce overlapping low confidence box
            int[] indices;
            CvDnn.NMSBoxes(boxes, confidences, (float)threshold, nmsThreshold, out indices);
            Console.WriteLine("NMSBoxes drop {confidences.Count - indices.Length} overlapping result.");

            foreach (var i in indices)
            {
                bbox_t bboxesitem = new bbox_t();
                var box = boxes[i];
                bboxesitem.x = (uint)(box.X * dRatioX);
                bboxesitem.y = (uint)(box.Y * dRatioY);
                bboxesitem.w = (uint)(box.Width * dRatioX);
                bboxesitem.h = (uint)(box.Height * dRatioY);
                if (bboxesitem.x > bboxesitem.w / 2)
                    bboxesitem.x = (uint)(bboxesitem.x - bboxesitem.w / 2);
                else bboxesitem.x = 0;
                if (bboxesitem.y > bboxesitem.h / 2)
                    bboxesitem.y = (uint)(bboxesitem.y - bboxesitem.h / 2);
                else bboxesitem.y = 0;

                bboxesitem.prob = probabilities[i];
                bboxesitem.obj_id = (uint)classIds[i];
                bboxesitem.track_id = (uint)i;
                bboxes.Add(bboxesitem);
            }
        }
    }
}
