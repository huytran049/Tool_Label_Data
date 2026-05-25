using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTool
{
    public static class TrainListWriter
    {
        public static string WriterTrainList( string sLocal_folder, string sFile)
        {
            string strTrainFile = JobConfig.GetListTrainFile();
            System.Text.Encoding charset = System.Text.Encoding.ASCII;
            
            var sFullPath = Path.GetFullPath(sLocal_folder);
            string sWrite = sFile;
            if (sFile.Contains(sFullPath))
                sWrite = sFile.Replace(sFullPath+"\\", "");
            if (!File.Exists(strTrainFile))
            {
                FileStream fileStream = File.Create(strTrainFile);
                fileStream.Close();
                StreamWriter oReader = new StreamWriter(strTrainFile, true, charset);
                oReader.WriteLine(sWrite);
                oReader.Close();
            }
            else
            {
                // Append text in file when file exitsed
                FileStream fileStream = File.Open(strTrainFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter oReader = new StreamWriter(fileStream, charset);
                oReader.WriteLine(sWrite);
                oReader.Close();
                fileStream.Close();
            }
            return sWrite;
        }
    }
}
