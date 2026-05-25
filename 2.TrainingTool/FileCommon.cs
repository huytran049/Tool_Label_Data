using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTool
{
    public static class FileCommon
    {
        public static IEnumerable<FileInfo> GetFiles(DirectoryInfo dinfo, string extensions)
        {
            FileInfo[] files = dinfo.GetFiles()
                             .Where(f => f.Extension.ToLower().Contains(extensions))
                             .ToArray();
            return files.ToList();
        }
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dinfo, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = Enumerable.Empty<FileInfo>();
            foreach (string ext in extensions)
            {
                files = files.Concat(GetFiles(dinfo, ext));
            }
            return files;
        }
    }
}
