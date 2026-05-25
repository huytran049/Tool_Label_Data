using CommonLibs;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using OpenCvSharp.Extensions;
using System.IO;
using System.Windows.Forms;

namespace TrainingTool
{
    public class OCRWrapper : IDisposable
    {
        [DllImport("libtesseract.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool tesseract_init([MarshalAs(UnmanagedType.LPStr)] String sPath);
        [DllImport("libtesseract.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int tesseract_setImage(IntPtr img1, int nWidth1, int nHeight1);
        const int MIN_WIDTH_LETTER = 10;
        const int MAX_WIDTH_LETTER = 40;
        const int MIN_HEIGHT_LETTER = 15;
        const int MAX_HEIGHT_LETTER = 60;
        public void Dispose()
        {

        }

        public OCRWrapper(String sPath)
        {
            if(File.Exists(sPath))
                tesseract_init(sPath);
            else
            {
                MessageBox.Show("Tess data not exist");
            } 
                
        }
        //public String Read(Bitmap bmp)
        //{
        //    String sRes = string.Empty;
        //    String sRes2 = string.Empty;
        //    int nsize = 500;
        //    StringBuilder L1 = new StringBuilder(500);
        //    StringBuilder L2 = new StringBuilder(500);
        //    Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        //    BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
        //    IntPtr data = bmpData.Scan0;
        //    EngineMRZ_Read(data, bmp.Width, bmp.Height, L1, L2, nsize);
        //    sRes = L1.ToString() + Environment.NewLine + L2;
        //    bmp.UnlockBits(bmpData);
        //    return sRes;
        //}
        public String ReadSingleOCR(Mat binImage)
        {
            try
            {
                int newWidth = binImage.Width;
                int newheight = binImage.Height;
                Bitmap bmp = binImage.ToBitmap();
                String sRes = string.Empty;
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr data = bmpData.Scan0;
                int nRes = tesseract_setImage(data, newWidth, newheight);
                sRes = Char.ConvertFromUtf32(nRes);
                bmp.UnlockBits(bmpData);
                return sRes;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        static List<Rect> RunTextRecog(Mat img_gray)
        {
            List<Rect> boundRect = new List<Rect>();
            using (Mat img_sobel = new Mat())
            using (Mat img_threshold = new Mat())
            {
                Cv2.Threshold(img_gray, img_threshold, 190, 255, ThresholdTypes.BinaryInv);
                OpenCvSharp.Point[][] edgesArray = img_threshold.Clone().FindContoursAsArray(RetrievalModes.External, ContourApproximationModes.ApproxNone);
                foreach (OpenCvSharp.Point[] edges in edgesArray)
                {
                    OpenCvSharp.Point[] normalizedEdges = Cv2.ApproxPolyDP(edges, 5, true);
                    Rect appRect = Cv2.BoundingRect(normalizedEdges);
                    boundRect.Add(appRect);
                }
            }
            return boundRect;
        }
        public String ReadLine(Mat imginput, bool bSave)
        {
            string sRes = string.Empty;
            try
            {
                if (imginput.Width <= 0) return sRes;
                Mat img = imginput.Resize(new OpenCvSharp.Size(imginput.Width * 4, imginput.Height * 4), 0, 0, InterpolationFlags.Lanczos4);
                if (bSave) img.SaveImage("char_imgs\\" + "img.jpg");
                var gray = new Mat(img.Size(), MatType.CV_8UC1);
                var binaryImage = new Mat(img.Size(), MatType.CV_8UC1);
                if (img.Type() != MatType.CV_8UC1)
                {
                    Cv2.CvtColor(img, gray, ColorConversionCodes.RGB2GRAY);
                }
                else
                    gray = img.Clone();
                gray = gray.Erode(new Mat());
                //gray.SaveImage("char_imgs\\" + "gray.jpg");
                //Cv2.Threshold(img, binaryImage, 190, 255, ThresholdTypes.Binary);
                //binaryImage.SaveImage("char_imgs\\bin.bmp");
                List<Rect> lrect = RunTextRecog(gray);
                List<Rect> SortedList = lrect.OrderBy(o => o.Left).ToList();
                foreach (Rect rect1 in SortedList)
                {
                    int nLeft = rect1.Left - 1;
                    int nTop = rect1.Top - 1;
                    int nWidth = rect1.Width + 2;
                    int nHeight = rect1.Height + 2;
                    if (nLeft < 1) nLeft = 1;
                    if (nTop < 1) nTop = 1;
                    if (nWidth > img.Width - 1 - nLeft) nWidth = img.Width - 1 - nLeft;
                    if (nHeight > img.Height - 1 - nTop) nHeight = img.Height - 1 - nTop;
                    var roi = new Mat(img, new Rect(nLeft, nTop, nWidth, nHeight)); //Crop the image
                    Cv2.Resize(roi, roi, new OpenCvSharp.Size(20, 30)); //resize to 20X20
                    DateTime dt = DateTime.Now;
                    string sName = string.Empty;
                    if (bSave)
                    {
                        sName = "char_imgs\\" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                        while (File.Exists(sName))
                        {
                            dt = DateTime.Now;
                            sName = "char_imgs\\" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                        }
                    }
                    Mat gray2 = new Mat();
                    if (roi.Type() != MatType.CV_8UC1)
                    {
                        Cv2.CvtColor(roi, gray2, ColorConversionCodes.RGB2GRAY);
                    }
                    else
                        gray2 = roi.Clone();
                    Bitmap bmp = gray2.ToBitmap();
                    if (bSave) bmp.Save(sName);
                    Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
                    IntPtr data = bmpData.Scan0;
                    int nRes = tesseract_setImage(data, bmp.Width, bmp.Height);
                    string sTemp = Char.ConvertFromUtf32(nRes);
                    bmp.UnlockBits(bmpData);
                    sRes += sTemp;
                }
                sRes = sRes.Replace("<", ".");
                return sRes;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //public String ReadLine(Mat imginput, bool bSave)
        //{
        //    string sRes = string.Empty;
        //    try
        //    {
        //        if (imginput.Width <= 0) return sRes;
        //        Mat img = imginput.Resize(new OpenCvSharp.Size(imginput.Width* 4, imginput.Height*4),0,0,InterpolationFlags.Lanczos4);
        //        if(bSave) img.SaveImage("char_imgs\\" + "img.jpg");
        //        var gray = new Mat(img.Size(), MatType.CV_8UC1);
        //        var binaryImage = new Mat(img.Size(), MatType.CV_8UC1);
        //        if (img.Type() != MatType.CV_8UC1)
        //        {
        //            Cv2.CvtColor(img, gray, ColorConversionCodes.RGB2GRAY);
        //        }
        //        else
        //            gray = img.Clone();
        //        gray = gray.Erode(new Mat());
        //        //gray.SaveImage("char_imgs\\" + "gray.jpg");
        //        //Cv2.Threshold(img, binaryImage, 190, 255, ThresholdTypes.Binary);
        //        //binaryImage.SaveImage("char_imgs\\bin.bmp");
        //        List<Rect> lrect = RunTextRecog(gray);
        //        List<Rect> SortedList = lrect.OrderBy(o => o.Left).ToList();
        //        foreach (Rect rect1 in SortedList)
        //        {
        //            int nLeft = rect1.Left - 1;
        //            int nTop = rect1.Top - 1;
        //            int nWidth = rect1.Width + 2;
        //            int nHeight = rect1.Height + 2;
        //            if (nLeft < 1) nLeft = 1;
        //            if (nTop < 1) nTop = 1;
        //            if (nWidth > img.Width-1 - nLeft) nWidth = img.Width - 1 - nLeft; 
        //            if (nHeight > img.Height - 1-nTop) nHeight = img.Height - 1 - nTop;
        //            var roi = new Mat(img, new Rect(nLeft , nTop, nWidth, nHeight)); //Crop the image
        //            Cv2.Resize(roi, roi, new OpenCvSharp.Size(20, 30)); //resize to 20X20
        //            DateTime dt = DateTime.Now;
        //            string sName= string.Empty;
        //            if (bSave)
        //            {
        //                sName = "char_imgs\\" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
        //                while (File.Exists(sName))
        //                {
        //                    dt = DateTime.Now;
        //                    sName = "char_imgs\\" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
        //                }
        //            }
        //            Mat gray2 = new Mat();
        //            if (roi.Type() != MatType.CV_8UC1)
        //            {
        //                Cv2.CvtColor(roi, gray2, ColorConversionCodes.RGB2GRAY);
        //            }
        //            else
        //                gray2 = roi.Clone();
        //            Bitmap bmp = gray2.ToBitmap();
        //            if(bSave) bmp.Save(sName);
        //            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        //            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
        //            IntPtr data = bmpData.Scan0;
        //            int nRes = tesseract_setImage(data, bmp.Width, bmp.Height);
        //            string sTemp = Char.ConvertFromUtf32(nRes);
        //            bmp.UnlockBits(bmpData);
        //            sRes += sTemp;
        //        }
        //        //Cv2.Threshold(gray, binaryImage, thresh: 1, maxval: 150, type: ThresholdTypes.Binary);
        //        //gray.SaveImage("char_imgs\\" + "agray.jpg");
        //        //binaryImage.SaveImage("char_imgs\\" + "abin.jpg");
        //        //OpenCvSharp.Point[][] contours;
        //        //HierarchyIndex[] hierarchyIndexes;
        //        //Cv2.FindContours(binaryImage, out contours,
        //        //    out hierarchyIndexes, mode: RetrievalModes.CComp,
        //        //    method: ContourApproximationModes.ApproxSimple);

        //        //if (contours.Length == 0)
        //        //{
        //        //    throw new NotSupportedException("Couldn't find any object in the image.");
        //        //}
        //        //var contourIndex = 0;
        //        //foreach (OpenCvSharp.Point[] cont in contours)
        //        //{
        //        //    var contour = cont;
        //        //    var boundingRect = Cv2.BoundingRect(contour); //Find bounding rect for each contour
        //        //    var roi = new Mat(gray, boundingRect); //Crop the image
        //        //    Cv2.Resize(roi, roi, new OpenCvSharp.Size(20, 20)); //resize to 20X20
        //        //    DateTime dt = DateTime.Now;
        //        //    roi.SaveImage("char_imgs\\" + dt.ToString("yyyyMMddHHmmss") + ".bmp");
        //        //    if (boundingRect.Width > MIN_WIDTH_LETTER && boundingRect.Width < MAX_WIDTH_LETTER
        //        //        && boundingRect.Height > MIN_HEIGHT_LETTER && boundingRect.Height < MAX_HEIGHT_LETTER)
        //        //    { 
        //        //        //Cv2.Rectangle(binaryImage, new OpenCvSharp.Point(boundingRect.X, boundingRect.Y),
        //        //        //new OpenCvSharp.Point(boundingRect.X + boundingRect.Width, boundingRect.Y + boundingRect.Height),
        //        //        //new Scalar(0, 0, 255), 2);
        //        //        var results = new Mat();
        //        //        var neighborResponses = new Mat();
        //        //        var dists = new Mat();
        //        //        Bitmap bmp = roi.ToBitmap();
        //        //        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        //        //        BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
        //        //        IntPtr data = bmpData.Scan0;
        //        //        int nRes = tesseract_setImage(data, bmp.Width, bmp.Height);
        //        //        string sTemp = Char.ConvertFromUtf32(nRes);
        //        //        bmp.UnlockBits(bmpData);
        //        //        sRes += sTemp; 
        //        //    }
        //        //    contourIndex = hierarchyIndexes[contourIndex].Next;
        //        //}

        //        //Cv2.ImShow("Segmented Source", binaryImage);
        //        //Cv2.ImShow("Detected", dst);
        //        //Cv2.ImWrite("dest.jpg", dst);

        //        //Cv2.WaitKey();
        //        sRes= sRes.Replace("<",".");
        //        return sRes;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

        Mat CutRegion(Mat Input, int H_MIN, int S_MIN, int V_MIN, int H_MAX, int S_MAX, int V_MAX, int nSize, bool bSave)
        {
            Mat img1 = Input.InRange(new Mat(1, 1, MatType.CV_8UC3, new Scalar(H_MIN, S_MIN, V_MIN, 0)),
                    new Mat(1, 1, MatType.CV_8UC3, new Scalar(H_MAX, S_MAX, V_MAX, 0)));
            Cv2.Erode(img1, img1, new Mat());
            if (bSave) img1.SaveImage("char_imgs\\" + "Range.jpg");
            Rect boundRect = GetRectMat(img1.Clone());
            //Rect boundRect = new Rect(0, 0, 0, 0);
            //OpenCvSharp.Point[][] edgesArray = img1.Clone().FindContoursAsArray(RetrievalModes.List, ContourApproximationModes.ApproxSimple);
            //foreach (OpenCvSharp.Point[] edges in edgesArray)
            //{
            //    OpenCvSharp.Point[] normalizedEdges = Cv2.ApproxPolyDP(edges, 5, true);
            //    Rect appRect = Cv2.BoundingRect(normalizedEdges);
            //    if (appRect.Width > nSize || appRect.Height > nSize)
            //    {
            //        if (boundRect.Width < 1) boundRect = appRect;
            //        else
            //        {
            //            if (boundRect.Left > appRect.Left)
            //            {
            //                boundRect.Left = appRect.Left;
            //                boundRect.Width += (boundRect.Left - appRect.Left);
            //            }
            //            if (boundRect.Top > appRect.Top)
            //            {
            //                boundRect.Top = appRect.Top;
            //                boundRect.Height += (boundRect.Top - appRect.Top);
            //            }
            //            if (boundRect.Right < appRect.Right)
            //                boundRect.Width = appRect.Right - boundRect.Left;
            //            if (boundRect.Bottom < appRect.Bottom)
            //                boundRect.Height = appRect.Bottom - boundRect.Top;
            //        }
            //    }
            //}
            var roi1 = new Mat(Input, boundRect); //Crop the image
            if (bSave) roi1.SaveImage("char_imgs\\" + "table.jpg");
            return roi1;
        }
        private Rect GetRect(Bitmap b)
        {
            int minX, minY;
            int maxX, maxY;
            minX = minY = int.MaxValue;
            maxX = maxY = int.MinValue;

            // Find the minimum and maximum black coordinates
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    if (b.GetPixel(x, y).ToArgb() == Color.White.ToArgb())
                    {
                        if (x > maxX) maxX = x;
                        if (x < minX) minX = x;
                        if (y > maxY) maxY = y;
                        if (y < minY) minY = y;
                    }
                }
            }
            Rect res = new Rect(minX, minY, maxX - minX, maxY - minY);
            return res;
        }
        private Rect GetRectMat(Mat mat)
        {
            int minX, minY;
            int maxX, maxY;
            minX = minY = int.MaxValue;
            maxX = maxY = int.MinValue;
            
            var indexer = mat.GetGenericIndexer<Vec3b>();

            // Find the minimum and maximum black coordinates
            for (int x = 0; x < mat.Width; x++)
            {
                for (int y = 0; y < mat.Height; y++)
                {
                    Vec3b color = indexer[y, x];
                    int i = color.Item0;
                    if (i == 255)
                    {
                        if (x > maxX) maxX = x;
                        if (x < minX) minX = x;
                        if (y > maxY) maxY = y;
                        if (y < minY) minY = y;
                    }
                }
            }
            Rect res = new Rect(minX, minY, maxX - minX, maxY - minY);
            return res;
        }
        Mat CutRegion2(Mat Input, int H_MIN, int S_MIN, int V_MIN, int H_MAX, int S_MAX, int V_MAX, int nSize, bool bSave)
        {
            Mat img1 = Input.InRange(new Mat(1, 1, MatType.CV_8UC3, new Scalar(H_MIN, S_MIN, V_MIN, 0)),
                    new Mat(1, 1, MatType.CV_8UC3, new Scalar(H_MAX, S_MAX, V_MAX, 0)));
            Cv2.Erode(img1, img1, new Mat());
            if (bSave) img1.SaveImage("char_imgs\\" + "Range2.jpg");
            //Rect boundRect = GetRect(img1.ToBitmap());
            Rect boundRect = new Rect(0, 0, 0, 0);
            OpenCvSharp.Point[][] edgesArray = img1.Clone().FindContoursAsArray(RetrievalModes.External, ContourApproximationModes.ApproxNone);
            foreach (OpenCvSharp.Point[] edges in edgesArray)
            {
                OpenCvSharp.Point[] normalizedEdges = Cv2.ApproxPolyDP(edges, 5, true);
                Rect appRect = Cv2.BoundingRect(normalizedEdges);
                if (appRect.Width > nSize && appRect.Height > nSize)
                {
                    if (boundRect.Width < 1) boundRect = appRect;
                    else
                    {
                        if (boundRect.Left > appRect.Left)
                        {
                            boundRect.Left = appRect.Left;
                            boundRect.Width += (boundRect.Left - appRect.Left);
                        }
                        if (boundRect.Top > appRect.Top)
                        {
                            boundRect.Top = appRect.Top;
                            boundRect.Height += (boundRect.Top - appRect.Top);
                        }
                        if (boundRect.Right < appRect.Right)
                            boundRect.Width = appRect.Right - boundRect.Left;
                        if (boundRect.Bottom < appRect.Bottom)
                            boundRect.Height = appRect.Bottom - boundRect.Top;
                    }
                }
            }
            var roi1 = new Mat(Input, boundRect); //Crop the image
            if (bSave) roi1.SaveImage("char_imgs\\" + "table2.jpg");
            return roi1;
        }
        Mat CutRegionHSV(Mat Input, int H_MIN, int S_MIN, int V_MIN, int H_MAX, int S_MAX, int V_MAX, int nWidth, int nHeight, bool bSave)
        {
            Mat imgHSV = new Mat();
            Cv2.CvtColor(Input, imgHSV, ColorConversionCodes.RGB2HSV);
            Mat img1 = imgHSV.InRange(new Scalar(H_MIN, S_MIN, V_MIN, 0), new Scalar(H_MAX, S_MAX, V_MAX, 0));
            if (bSave) img1.SaveImage("char_imgs\\" + "Range.jpg");
            Cv2.Erode(img1, img1, new Mat());
            Cv2.Erode(img1, img1, new Mat());
            Rect boundRect = new Rect(0, 0, 0, 0);
            OpenCvSharp.Point[][] edgesArray = img1.Clone().FindContoursAsArray(RetrievalModes.External, ContourApproximationModes.ApproxNone);
            foreach (OpenCvSharp.Point[] edges in edgesArray)
            {
                OpenCvSharp.Point[] normalizedEdges = Cv2.ApproxPolyDP(edges, 5, true);
                Rect appRect = Cv2.BoundingRect(normalizedEdges);
                if (appRect.Width > nWidth && appRect.Height > nHeight)
                {
                    if (boundRect.Width < 1) boundRect = appRect;
                    else
                    {
                        if (boundRect.Left > appRect.Left)
                        {
                            //boundRect.Left = appRect.Left;
                            //boundRect.Width += (boundRect.Left - appRect.Left);
                            boundRect = new Rect(Math.Min(boundRect.Left, appRect.Left), Math.Min(boundRect.Top, appRect.Top),
                               Math.Max(boundRect.Right, appRect.Right) - Math.Min(boundRect.Left, appRect.Left),
                               Math.Max(boundRect.Bottom, appRect.Bottom) - Math.Min(boundRect.Top, appRect.Top));
                        }
                        if (boundRect.Top > appRect.Top)
                        {
                            //boundRect.Top = appRect.Top;
                            //boundRect.Height += (boundRect.Top - appRect.Top);
                            boundRect = new Rect(Math.Min(boundRect.Left, appRect.Left), Math.Min(boundRect.Top, appRect.Top),
                                Math.Max(boundRect.Right, appRect.Right) - Math.Min(boundRect.Left, appRect.Left),
                                Math.Max(boundRect.Bottom, appRect.Bottom) - Math.Min(boundRect.Top, appRect.Top));
                        }
                        if (boundRect.Right < appRect.Right)
                        {
                            //boundRect = appRect.Right - boundRect.Right;
                            boundRect = new Rect(Math.Min(boundRect.Left, appRect.Left), Math.Min(boundRect.Top, appRect.Top),
                                Math.Max(boundRect.Right, appRect.Right) - Math.Min(boundRect.Left, appRect.Left),
                                Math.Max(boundRect.Bottom, appRect.Bottom) - Math.Min(boundRect.Top, appRect.Top));
                        }
                        if (boundRect.Bottom < appRect.Bottom)
                        {
                            //boundRect.Height += appRect.Bottom - boundRect.Bottom;
                            boundRect = new Rect(Math.Min(boundRect.Left, appRect.Left), Math.Min(boundRect.Top, appRect.Top),
                                Math.Max(boundRect.Right, appRect.Right) - Math.Min(boundRect.Left, appRect.Left),
                                Math.Max(boundRect.Bottom, appRect.Bottom) - Math.Min(boundRect.Top, appRect.Top));
                        }
                    }
                }
            }
            boundRect = new Rect(boundRect.Left - 10, boundRect.Top - 30, boundRect.Width + 20, boundRect.Height + 80);
            var roi1 = new Mat(Input, boundRect); //Crop the image
            if (bSave) roi1.SaveImage("char_imgs\\" + "table.jpg");
            return roi1;
        }
        static List<Rect> DetectTextKeyence(Mat img_gray)
        {
            List<Rect> boundRect = new List<Rect>();
            //using (Mat img_sobel = new Mat())
            using (Mat img_threshold = new Mat())
            {
                Cv2.Threshold(img_gray, img_threshold, 190, 255, ThresholdTypes.BinaryInv);
                OpenCvSharp.Point[][] edgesArray = img_threshold.Clone().FindContoursAsArray(RetrievalModes.External, ContourApproximationModes.ApproxNone);
                foreach (OpenCvSharp.Point[] edges in edgesArray)
                {
                    OpenCvSharp.Point[] normalizedEdges = Cv2.ApproxPolyDP(edges, 3, true);
                    Rect appRect = Cv2.BoundingRect(normalizedEdges);
                    boundRect.Add(appRect);
                }
            }
            return boundRect;
        }
        string ReadMultiLine(Mat gray, bool bSave)
        {
            string sRes = String.Empty;
            try
            {
                List<Rect> lrect = DetectTextKeyence(gray);
                List<List<Rect>> SortedList = lrect.GroupBy(o => o.Bottom).Select(grp => grp.ToList()).ToList();
                //List<List<Rect>> SortedList = lrect.GroupBy(o => (.top)).Select(grp => grp.ToList()).ToList();
                SortedList.Reverse();
                foreach (List<Rect> Line in SortedList)
                {
                    List<Rect> SortedList2 = Line.OrderBy(o => o.Left).ToList();
                    foreach (Rect rect1 in SortedList2)
                    {
                        int nLeft = rect1.Left - 1;
                        int nTop = rect1.Top - 1;
                        int nWidth = rect1.Width + 2;
                        int nHeight = rect1.Height + 2;
                        if (nLeft < 1) nLeft = 1;
                        if (nTop < 1) nTop = 1;
                        if (nWidth > gray.Width - 1 - nLeft) nWidth = gray.Width - 1 - nLeft;
                        if (nHeight > gray.Height - 1 - nTop) nHeight = gray.Height - 1 - nTop;
                        var roi = new Mat(gray, new Rect(nLeft, nTop, nWidth, nHeight)); //Crop the image
                        Cv2.Resize(roi, roi, new OpenCvSharp.Size(20, 30), 0, 0, InterpolationFlags.Nearest); //resize to 20X30
                        DateTime dt = DateTime.Now;
                        string sName = string.Empty;

                        Mat gray2 = new Mat();
                        if (roi.Type() != MatType.CV_8UC1)
                        {
                            Cv2.CvtColor(roi, gray2, ColorConversionCodes.RGB2GRAY);
                        }
                        else
                            gray2 = roi.Clone();
                        Bitmap bmp = gray2.ToBitmap();

                        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                        BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
                        IntPtr data = bmpData.Scan0;
                        int nRes = tesseract_setImage(data, bmp.Width, bmp.Height);
                        string sTemp = Char.ConvertFromUtf32(nRes);
                        if (nRes != 60)
                        {
                            if (bSave)
                            {
                                sName = "char_imgs\\" + sTemp + "-" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                                while (File.Exists(sName))
                                {
                                    dt = DateTime.Now;
                                    sName = "char_imgs\\" + sTemp + "-" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                                }
                            }
                        }
                        else
                        {
                            if (bSave)
                            {
                                sName = "char_imgs\\" + "10" + "-" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                                while (File.Exists(sName))
                                {
                                    dt = DateTime.Now;
                                    sName = "char_imgs\\" + "10" + "-" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                                }
                            }
                        }
                        bmp.UnlockBits(bmpData);
                        if (bSave) bmp.Save(sName);
                        sRes += sTemp;
                    }
                    sRes += Environment.NewLine;
                }
                Console.WriteLine(sRes);
                return sRes;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
                return sRes;
            }
        }

        string ReadMultiLine2(Mat gray, bool bSave)
        {
            string sRes = String.Empty;
            try
            {
                List<Rect> lrect = DetectTextKeyence(gray);
                List<List<Rect>> SortedList = lrect.GroupBy(o => o.Bottom).Select(grp => grp.ToList()).ToList();
                //List<List<Rect>> Line2 = from n in SortedList  where n.Count > 1 select n;
                List<List<Rect>> Line2 = SortedList.Where(x => x.Count > 1).ToList();
                //
                List<List<Rect>> Line4 = new List<List<Rect>>();
                foreach (List<Rect> Line in Line2)
                {
                    List<Rect> ListBottom = Line.OrderBy(o => o.Bottom).ToList();
                    int MinY = ListBottom[0].Bottom;
                    List<Rect> ListTop = Line.OrderBy(o => o.Top).ToList();
                    int MaxY = ListTop[0].Top;
                    bool bExit = false;
                    foreach (List<Rect> Line6 in Line4)
                    {
                        List<Rect> Line5 = Line6.Where(x => MaxY <= ((x.Top + x.Bottom) / 2) && ((x.Top + x.Bottom) / 2) <= MinY).ToList();
                        if (Line5?.Count > 0) bExit = true;
                    }
                    if (!bExit) Line4.Add(Line);
                }
                //
                List<List<Rect>> SortedListAll = new List<List<Rect>>();
                foreach (List<Rect> Line in Line4)
                {
                    List<Rect> ListBottom = Line.OrderBy(o => o.Bottom).ToList();
                    int MinY = ListBottom[0].Bottom;
                    List<Rect> ListTop = Line.OrderBy(o => o.Top).ToList();
                    int MaxY = ListTop[0].Top;
                    List<Rect> Line3 = lrect.Where(x => MaxY <= ((x.Top + x.Bottom) / 2) && ((x.Top + x.Bottom) / 2) <= MinY).ToList();
                    SortedListAll.Add(Line3);
                }
                SortedListAll.Reverse();
                foreach (List<Rect> Line in SortedListAll)
                {
                    List<Rect> SortedListLine = Line.OrderBy(o => o.Left).ToList();
                    foreach (Rect rect1 in SortedListLine)
                    {
                        int nLeft = rect1.Left - 1;
                        int nTop = rect1.Top - 1;
                        int nWidth = rect1.Width + 2;
                        int nHeight = rect1.Height + 2;
                        if (nLeft < 1) nLeft = 1;
                        if (nTop < 1) nTop = 1;
                        if (nWidth > gray.Width - 1 - nLeft) nWidth = gray.Width - 1 - nLeft;
                        if (nHeight > gray.Height - 1 - nTop) nHeight = gray.Height - 1 - nTop;
                        var roi = new Mat(gray, new Rect(nLeft, nTop, nWidth, nHeight)); //Crop the image
                        Cv2.Resize(roi, roi, new OpenCvSharp.Size(20, 30), 0, 0, InterpolationFlags.Nearest); //resize to 20X30
                        DateTime dt = DateTime.Now;
                        string sName = string.Empty;

                        Mat gray2 = new Mat();
                        if (roi.Type() != MatType.CV_8UC1)
                        {
                            Cv2.CvtColor(roi, gray2, ColorConversionCodes.RGB2GRAY);
                        }
                        else
                            gray2 = roi.Clone();
                        Bitmap bmp = gray2.ToBitmap();

                        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                        BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
                        IntPtr data = bmpData.Scan0;
                        int nRes = tesseract_setImage(data, bmp.Width, bmp.Height);
                        string sTemp = Char.ConvertFromUtf32(nRes);
                        if (nRes != 60)
                        {
                            if (bSave)
                            {
                                sName = "char_imgs\\" + sTemp + "-" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                                while (File.Exists(sName))
                                {
                                    dt = DateTime.Now;
                                    sName = "char_imgs\\" + sTemp + "-" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                                }
                            }
                        }
                        else
                        {
                            if (bSave)
                            {
                                sName = "char_imgs\\" + "10" + "-" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                                while (File.Exists(sName))
                                {
                                    dt = DateTime.Now;
                                    sName = "char_imgs\\" + "10" + "-" + dt.ToString("yyyyMMddHHmmss") + ".bmp";
                                }
                            }
                        }
                        bmp.UnlockBits(bmpData);
                        if (bSave) bmp.Save(sName);
                        sRes += sTemp;
                    }
                    sRes += Environment.NewLine;
                }
                return sRes;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
                return sRes;
            }
        }
        public String ReadLogKeyence(Mat imginput, bool bSave)
        {
            string sRes = string.Empty;
            try
            {
                if (imginput.Width <= 0) return sRes;
                if (bSave) imginput.SaveImage("char_imgs\\" + "aIn.jpg");
                int H1 = 197; int S1 = 195; int V1 = 195;
                Mat imgWindow = CutRegion(imginput, H1 - 10, S1 - 10, V1 - 10, H1 + 10, S1 + 10, V1 + 10, 1, bSave);
                int H2 = 255; int S2 = 255; int V2 = 255;
                Mat imgTextRegion = CutRegion2(imgWindow, H2 - 10, S2 - 10, V2 - 10, H2, S2, V2, 50, bSave);
                Rect rectcrop = new Rect((int)(imgTextRegion.Width / 2), 0, (int)(imgTextRegion.Width / 2), imgTextRegion.Height);
                Mat imgVal = new Mat(imgTextRegion, rectcrop);
                Mat img3 = imgVal.Clone();
                Cv2.Threshold(imgVal, img3, 190, 255, ThresholdTypes.Binary);
                if (bSave) img3.SaveImage("char_imgs\\" + "img3.jpg");
                Mat img4 = img3.Resize(new OpenCvSharp.Size(img3.Width * 4, img3.Height * 4), 0, 0, InterpolationFlags.Nearest);
                if (bSave) img4.SaveImage("char_imgs\\" + "img4.jpg");
                var gray = new Mat(img4.Size(), MatType.CV_8UC1);
                if (img4.Type() != MatType.CV_8UC1)
                {
                    Cv2.CvtColor(img4, gray, ColorConversionCodes.RGB2GRAY);
                }
                else
                    gray = img4.Clone();
                gray = gray.Erode(new Mat());
                if (bSave) gray.SaveImage("char_imgs\\" + "gray.jpg");
                sRes = ReadMultiLine(gray, bSave);
                sRes = sRes.Replace("<", ".");
                sRes = sRes.Replace("U", "");
                sRes = sRes.Replace("M", "");
                return sRes.ToLower();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //read log VHX
        public String ReadLogKeyence2(Mat imginput, bool bSave, int HMin = 25, int SMin = 22,
            int VMin = 36, int HMax = 65, int SMax = 223, int VMax = 255)
        {//25, 22, 36, 65, 223, 255
            string sRes = string.Empty;
            try
            {
                if (imginput.Width <= 0) return sRes;
                if (bSave) imginput.SaveImage("char_imgs\\" + "aIn.jpg");
                Mat imgWindow = CutRegionHSV(imginput, HMin, SMin, VMin, HMax, SMax, VMax, 25, 15, bSave);
                Rect crop = new Rect(140, 0, imgWindow.Width - 140, imgWindow.Height);
                var roi1 = new Mat(imgWindow, crop); //Crop the image
                Mat imgVal = roi1.Clone();
                Cv2.CvtColor(roi1, imgVal, ColorConversionCodes.RGB2GRAY);
                Mat img3 = imgVal.Clone();
                Cv2.Threshold(imgVal, img3, 170, 255, ThresholdTypes.Binary);
                if (bSave) img3.SaveImage("char_imgs\\" + "img3.jpg");
                Mat img4 = img3.Resize(new OpenCvSharp.Size(img3.Width * 4, img3.Height * 4), 0, 0, InterpolationFlags.Nearest);
                if (bSave) img4.SaveImage("char_imgs\\" + "img4.jpg");
                var gray = new Mat(img4.Size(), MatType.CV_8UC1);
                if (img4.Type() != MatType.CV_8UC1)
                {
                    Cv2.CvtColor(img4, gray, ColorConversionCodes.RGB2GRAY);
                }
                else
                    gray = img4.Clone();
                //gray = gray.Erode(new Mat());
                if (bSave) gray.SaveImage("char_imgs\\" + "gray.jpg");
                sRes = ReadMultiLine2(gray, bSave);
                sRes = sRes.Replace("<", ".");
                sRes = sRes.Replace("U", "");
                sRes = sRes.Replace("M", "");
                return sRes.ToLower();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
