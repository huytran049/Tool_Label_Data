using System.Runtime.InteropServices;
using System.Text;

namespace CommonLibs
{
    public class IniFile
    {
        public IniFile(string filePath)
        {
            FilePath = filePath;
        }
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
            WritePrivateProfileString(section, key, value.ToLower(), FilePath);
        }

        public string Read(string section, string key)
        {
            var sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", sb, 255, FilePath);
            return sb.ToString();
        }
    }
}