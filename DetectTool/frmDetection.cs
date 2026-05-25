using CommonLibs;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoloSharp;
using Point = System.Drawing.Point;

namespace DetectTool
{
    public partial class frmDetection : Form
    {
        YoloCPU _yolo;
        string _sPre_folder;
        string _filename;
        string _sModelFile;
        string _sWeightFile;
        List<bbox_t> _Detectbox;
        Bitmap _bmp;
        Mat _img;
        public frmDetection()
        {
            InitializeComponent();
            _sModelFile = "model\\yolo-fastest.cfg";
            _sWeightFile = "model\\yolo-fastest_last.weights";
            _yolo = new YoloCPU(_sModelFile, _sWeightFile, 0.7);
        }

        private void btOpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "image files |*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            dialog.Title = "Please select an image file.";
            dialog.InitialDirectory = _sPre_folder;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _filename = dialog.FileName;
                try
                {
                    _Detectbox = new List<bbox_t>();
                    _img = new Mat(_filename);
                    _sPre_folder = Path.GetDirectoryName(_filename);
                    _bmp =  _img.ToBitmap();
                    pbMainTrain.Image = _bmp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }
         

        private void pbMainTrain_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (_img == null) return;
                if (!(_img.Width > 0)) return;
                Graphics dc = e.Graphics;
                _bmp = _img.ToBitmap();
                if (_Detectbox != null) { if (_Detectbox.Count > 0) drawDetectBox(dc, _bmp, _Detectbox); }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        private Point PointToView(Image img, Point cp)
        {
            Point ptRes = new Point();
            int cw = pbMainTrain.Width;
            int ch = pbMainTrain.Height;

            float fx = cw * 1.0f / img.Width;
            float fy = ch * 1.0f / img.Height;
            float f = fx;
            if (f > fy)
                f = fy;
            float dw, dh;
            dw = f * img.Width;
            dh = f * img.Height;
            float dx, dy;
            dx = (cw - dw) / 2;
            dy = (ch - dh) / 2;
            ptRes.X = (int)(dx + cp.X * f);
            ptRes.Y = (int)(dy + cp.Y * f);
            return ptRes;
        }
        private void drawRect(Graphics dc, Image img, Point tl, Point br, String sLabel, Color color)
        {
            if (Math.Abs(br.X - tl.X) == 0) return;
            if (Math.Abs(br.Y - tl.Y) == 0) return;
            Point viewTL = PointToView(img, tl);
            Point viewBR = PointToView(img, br);
            Pen pen = new Pen(color, 2);
            dc.DrawRectangle(pen, viewTL.X, viewTL.Y, viewBR.X - viewTL.X, viewBR.Y - viewTL.Y);
            dc.DrawString(sLabel, new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
                    new SolidBrush(Color.Green), viewTL.X, viewTL.Y + 2);
        }
        private void drawDetectBox(Graphics dc, Image img, List<bbox_t> lbox)
        {
            //Draw detect 
            //if (!_bDrawImage)
            //    return;
            int cw = pbMainTrain.Width;
            int ch = pbMainTrain.Height;

            float fx = cw * 1.0f / img.Width;
            float fy = ch * 1.0f / img.Height;
            float f = fx;
            if (f > fy)
                f = fy;
            float dw, dh;
            dw = f * img.Width;
            dh = f * img.Height;
            float dx, dy;
            dx = (cw - dw) / 2;
            dy = (ch - dh) / 2;

            int nCnt = lbox.Count;
            for (int i = 0; i < nCnt; i++)
            {
                bbox_t b = lbox[i];

                float nX = dx + b.x * f;
                float nY = dy + b.y * f;
                float nWidth = b.w * f;
                float nHeight = b.h * f;
                float fscores = b.prob;
                UInt32 obj_id = b.track_id;
                Pen pen = new Pen(Color.Green, 2);
                Pen penRed = new Pen(Color.Red, 2);
                if (b.obj_id == 0)
                {
                    dc.DrawRectangle(pen, nX, nY, nWidth, nHeight);
                    dc.DrawString("OK",
                            new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
                            new SolidBrush(Color.Green), nX, nY - 22);
                }
                else
                {
                    dc.DrawRectangle(penRed, nX, nY, nWidth, nHeight);
                    dc.DrawString("NG",
                            new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
                            new SolidBrush(Color.Green), nX, nY - 22);
                }
            }
        }

        private void btDetect_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime t1=  DateTime.Now;

                _Detectbox = _yolo.Detect(_img);
                pbMainTrain.Image = _bmp;
                pbMainTrain.Invalidate();
                DateTime t2 = DateTime.Now;
                lbDetectionTime.Text = "Thời gian detect: " + (t2 - t1).TotalMilliseconds.ToString() + " ms";
            }
            catch(Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
    }
}
