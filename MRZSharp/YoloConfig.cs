using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YoloSharp
{
    public class YoloConfig
    {
        #region "Internal methods"
        public string FilePath { get; set; }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,
            string val,
            string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath);

        public void Write(string section, string key, string value)
        {
            //WritePrivateProfileString(section, key, value.ToLower(), FilePath);
            WritePrivateProfileString(section, key, value, FilePath);
        }

        public string Read(string section, string key)
        {
            var sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", sb, 255, FilePath);
            return sb.ToString();
        }
        #endregion "Internal methods"

        #region "Public methods"
        public YoloConfig(string filePath)
        {
            FilePath = filePath;
        }
        public string GetWidthSetting()
        {
            string section = "net";
            return Read(section, "width");
        }
        public string GetHeightSetting()
        {
            string section = "net";
            return Read(section, "height");
        }
        #endregion "Public methods"
    }
}
