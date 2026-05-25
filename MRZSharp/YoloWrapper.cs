using System;
using System.Drawing;
using System.Drawing.Imaging;
using CommonLibs;
using System.Runtime.InteropServices;

namespace YoloSharp
{
    public class YoloWrapper : IDisposable
    {
        private const string YoloLibraryName = "dark.dll";
        private const int MaxObjects = 1000;
        //for detect
        [DllImport(YoloLibraryName, EntryPoint = "InitializeYolo")]
        private static extern IntPtr InitializeYolo(string configurationFilename, string weightsFilename, int gpu);

        [DllImport(YoloLibraryName, EntryPoint = "DetectImage")]
        private static extern int DetectImage(IntPtr hHandle, string filename, ref BboxContainer container);

        [DllImport(YoloLibraryName, EntryPoint = "DetectImage2")]
        private static extern int DetectImage2(IntPtr hHandle, byte[] szBuff, int nBuff, ref BboxContainer container);

        [DllImport(YoloLibraryName, EntryPoint = "dispose")]
        private static extern int DisposeYolo();
        [DllImport(YoloLibraryName, EntryPoint = "get_device_name")]
        private static extern int get_device_name(int gpu, string weightsFilename);
        [DllImport(YoloLibraryName, EntryPoint = "built_with_cudnn")]
        public static extern bool built_with_cudnn();
        private IntPtr _hHandle = IntPtr.Zero;
        public YoloWrapper(string configurationFilename, string weightsFilename, int gpu)
        {
            _hHandle = InitializeYolo(configurationFilename, weightsFilename, gpu);
        }

        public void Dispose()
        {
            DisposeYolo();
        }
        public String GetDeviceName()
        {
            String sDevice = "Not define";
            get_device_name(0, sDevice);
            return sDevice;
        }

        public bbox_t[] Detect(string filename)
        {
            var container = new BboxContainer();
            var count = DetectImage(_hHandle, filename, ref container);

            return container.candidates;
        }
        public byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        public bbox_t[] Detect(Bitmap img)
        {
            var container = new BboxContainer();
            try
            {
                // Copy the array to unmanaged memory.
                byte[] fileBytes = ImageToByte(img);
                var count = DetectImage2(_hHandle, fileBytes, fileBytes.Length, ref container);
                if (count == -1)
                {
                    throw new NotSupportedException($"{YoloLibraryName} has no OpenCV support");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }

            return container.candidates;
        }
    }
}
