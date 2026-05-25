using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CommonLibs;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using static YoloSharp.YoloWrapper;

namespace YoloSharp
{
    public class YoloGPU
    {
        YoloWrapper _detection_net;
        Mat _img;
        Bitmap _bmp;
        int _nDetectWidth;
        int _nDetectHeight;
        double _dConf;
        public YoloGPU(string configurationFilename, string weightsFilename, int nDetectWidth, int nDetectHeight, double dConf = 0.7)
        {
            try
            {
                _detection_net = new YoloWrapper(configurationFilename, weightsFilename, 0);
                _img = new Mat();
                _nDetectWidth = nDetectWidth;
                _nDetectHeight = nDetectHeight;
                _dConf = dConf;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        public List<bbox_t> Detect(Mat imgInput)
        {
            List<bbox_t> _boxReturn = new List<bbox_t>();
            if (_nDetectWidth == 0 || _nDetectHeight == 0)
            {
                MessageBox.Show("Lỗi/Error", "Load thông tin job bị lỗi, vui lòng kiềm tra lại");
                return _boxReturn;
            }
            double dRatioX = (double)imgInput.Width / (double)_nDetectWidth;
            double dRatioY = (double)imgInput.Height / (double)_nDetectHeight;
            if (imgInput.Width > _nDetectWidth || imgInput.Height > _nDetectHeight)
            {
                _img = imgInput.Resize(new OpenCvSharp.Size(_nDetectWidth, _nDetectHeight));
                _bmp = _img.ToBitmap();
            }
            else
            {
                _bmp = imgInput.ToBitmap();
                dRatioX = 1; dRatioY = 1;
            }
            bbox_t[] _box = _detection_net.Detect(_bmp);

            for (int i = 0; i < _box.Length; i++)
            {
                if (_box[i].prob > _dConf)
                {
                    bbox_t newItem = new bbox_t();
                    newItem.frames_counter = _box[i].frames_counter;
                    newItem.x = (uint)(_box[i].x * dRatioX);
                    newItem.y = (uint)(_box[i].y * dRatioY);
                    newItem.w = (uint)(_box[i].w * dRatioX);
                    newItem.h = (uint)(_box[i].h * dRatioY);
                    newItem.track_id = _box[i].track_id;
                    newItem.obj_id = _box[i].obj_id;
                    newItem.prob = _box[i].prob;
                    newItem.x_3d = _box[i].x_3d;
                    newItem.y_3d = _box[i].y_3d;
                    newItem.z_3d = _box[i].z_3d;
                    _boxReturn.Add(newItem);
                }
            }
            return _boxReturn;
        }
    }
}
