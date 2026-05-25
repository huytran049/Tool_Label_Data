using CommonLibs;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSharp;

namespace TrainingTool
{
    public static class DataAgument
    {
        public static Mat GetRandomCrop(YoloGPU _detector, string Filename)
        {
            Random rnd = new Random();
            Mat imgReturn = new Mat();
            try
            {
                Mat img = new Mat(Filename);
                int nWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                int nHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
                var rectDectec = _detector.Detect(img);
                if (rectDectec?.Count > 0)
                {
                    int nx = rnd.Next(0, (int)Math.Max(0,rectDectec[0].x - 20));
                    int ny = rnd.Next(0, (int)Math.Max(0, rectDectec[0].y - 20));
                    int nr = rnd.Next((int)Math.Min((int)rectDectec[0].x + (int)rectDectec[0].w + 20, img.Width), img.Width);
                    int nb = rnd.Next((int)Math.Min((int)rectDectec[0].y + (int)rectDectec[0].h + 20, img.Height), img.Height);
                    imgReturn = new Mat(img, new Rect(nx, ny, nr - nx, nb - ny));
                    if (imgReturn.Width != nWidth || imgReturn.Height != nHeight)
                    { imgReturn = imgReturn.Resize(new OpenCvSharp.Size(nWidth, nHeight)); }
                    imgReturn = GetRandomSubtrack(imgReturn);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
            return imgReturn;
        }
        static Mat GetRandomSubtrack(Mat img)
        {
            Random rnd = new Random();
            Mat imgReturn = new Mat();
            try {
                imgReturn = img - Scalar.All((int)rnd.Next(2, 15));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
            return imgReturn;
        }

    }
}
