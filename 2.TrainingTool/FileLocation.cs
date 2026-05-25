using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTool
{
    static class FileLocation
    {
        static string sImageFolder = "\\ImageSets\\" ;
        static string sTrainlist = "\\ImageSets\\train.txt";
        static string sTrainChar = "\\CharData\\train.txt";
        public static string GetLocationImage(string sBaseFolder)
        {
            return (sBaseFolder + sImageFolder);
        }
        public static string GetLocationLabel(string sFile)
        {
            return  sFile.Replace("jpg","txt").Replace("bmp", "txt").Replace("png", "txt");
        }
        public static string GetLocationTrainList(string sBaseFolder)
        {
            return (sBaseFolder + sTrainlist);
        }
        public static string GetLocationTrainChar(string sBaseFolder)
        {
            return (sBaseFolder + sTrainChar);
        }
    }
}
