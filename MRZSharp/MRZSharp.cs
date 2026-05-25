using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;

namespace YoloSharp
{
    public class ALPRSharp
    {
        const string sDLL = "EngineALPRx64.dll";
        [DllImport(sDLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Init([MarshalAs(UnmanagedType.LPStr)]String sPath);
        [DllImport(sDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void EngineMRZ_Read(IntPtr img1, int nWidth1, int nHeight1, StringBuilder strL1,bool bSaveChar, int nSize);
        public ALPRSharp(String sPath)
        {
            Init(sPath);
        }
        public String Read(Bitmap bmp)
        {
            String sRes = string.Empty;
            String sRes2 = string.Empty;
            int nsize = 500;
            StringBuilder L1 = new StringBuilder(500);
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr data = bmpData.Scan0;
            EngineMRZ_Read(data,bmp.Width, bmp.Height, L1, true, nsize);
            sRes = L1.ToString();
            bmp.UnlockBits(bmpData);
            return sRes;
        }
    }
}
