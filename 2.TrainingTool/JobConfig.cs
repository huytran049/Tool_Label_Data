using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMCLib;
using System.Data;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Windows.Forms;
using CommonLibs;

namespace TrainingTool
{
    class JobConfig
    {
        private static string _sPath;
       //----------------------------------------------------------------
        public static void OpenJob(string sPath)
        {
            _sPath = sPath;
        }
        public static string FilePath
        {
            get { return _sPath; }
        }
        private static bool SetConfigurationValue(string section, string key, string value)
        {
            try
            {
                string strXmlFile = string.Empty;
                strXmlFile = _sPath;
                if (string.IsNullOrEmpty(strXmlFile))
                {
                    return false;
                }

                XDocument xmlDoc = XDocument.Load(strXmlFile);
                XElement xElement = xmlDoc.XPathSelectElement("/SYSTEMCONFIG/" + section + "/" + key);
                if (xElement != null)
                {
                    xElement.SetValue(value);
                    xmlDoc.Save(strXmlFile);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
            return false;
        }

        /// <summary>
        /// Modified By Locpv
        /// </summary>
        private static string GetConfigurationValue(string parentKey, string key)
        {
            string values = string.Empty;
            try
            {
                string strXmlFile = string.Empty;
                strXmlFile = _sPath;
                if (string.IsNullOrEmpty(_sPath))
                {
                    //strXmlFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SystemConfig.config");
                    MessageBox.Show("Config file not exist, please check again");
                    return "";
                }
                string xmlContent = default(string);
                if (xmlContent == default(string))
                {
                    xmlContent = File.ReadAllText(strXmlFile);
                }


                XDocument xmlDoc = XDocument.Parse(xmlContent);
                var s = from r in xmlDoc.Descendants(parentKey)
                        select new
                        {
                            values = r.Element(key)?.Value
                        };
                var ks = s.SingleOrDefault();
                if (ks != null)
                {
                    values = ks.values;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }

            return values;
        }

        public static string GetDBServer()
        {
            return GetConfigurationValue("DBCONFIG", "DBSERVER");
        }
        public static string GetImageUrl()
        {
            return GetConfigurationValue("SYSTEM", "ImageBaseURL");
        }
        public static string GetDBUserID()
        {
            return GetConfigurationValue("DBCONFIG", "DBUSER");
        }
        public static string GetDBUserPass()
        {
            return GetConfigurationValue("DBCONFIG", "DBPASSWORD");
        }

        //20140219 Add by TamHM
        public static string GetPortCOM()
        {
            string strPortCOM = "";
            try
            {
                strPortCOM = GetConfigurationValue("MESSENGER", "PortCOM");
            }
            catch (Exception)
            {
                // Cannot find tag "PortCOM"
            }
            return strPortCOM;
        }
        
        public static string GetCamID()
        {
            return  GetConfigurationValue("SYSTEM", "CameraID");
        }
        public static void SetCamID(string Value)
        {
            SetConfigurationValue("SYSTEM", "CameraID", Value);
        }
        public static string GetNumClass()
        {
            return GetConfigurationValue("SYSTEM", "NumClass");
        }
        public static bool SetNumClass(string value)
        {
            if (SetConfigurationValue("SYSTEM", "NumClass", value))
            {
                return true;
            }
            return false;
        }
        public static string GetClassName(int i)
        {
            string extraUrl = "GetClassName"+ i.ToString();
            string dataTable = default(string);
            //string dataTable = SystemHttpRuntimeCache.Get<string>(extraUrl);
            if (dataTable == default(string))
            {
                dataTable = GetConfigurationValue("SYSTEM", "ClassName"+i.ToString());
            }
            return dataTable;
        }
        public static bool SetClassName(int i,string value)
        {
            if (SetConfigurationValue("SYSTEM", "ClassName"+i.ToString(), value))
            {
                return true;
            }
            return false;
        }
        public static string GetWorkingDir()
        {
            string dataTable = GetConfigurationValue("SYSTEM", "BaseDir");
            return dataTable;
        }
        public static string GetTrainWidth()
        {
            string dataTable = GetConfigurationValue("SYSTEM", "TrainWidth");
            return dataTable;
        }
        public static string GetStartROIX()
        {
            string dataTable = GetConfigurationValue("SYSTEM", "StartROIX");
            return dataTable;
        }
        public static string GetStartROIY()
        {
            string dataTable = GetConfigurationValue("SYSTEM", "StartROIY");
            return dataTable;
        }
        public static bool SetTrainWidth(string value)
        {
            if (SetConfigurationValue("SYSTEM", "TrainWidth", value))
            {
                return true;
            }
            return false;
        }
        public static string GetTrainHeight()
        {
            string dataTable = GetConfigurationValue("SYSTEM", "TrainHeight");
            return dataTable;
        }
        public static bool SetTrainHeight(string value)
        {
            if (SetConfigurationValue("SYSTEM", "TrainHeight", value))
            {
                return true;
            }
            return false;
        }
        public static string GetDetectWidth()
        {
            string dataTable = GetConfigurationValue("SYSTEM", "DetectWidth");
            return dataTable;
        }
        public static bool SetDetectWidth(string value)
        {
            if (SetConfigurationValue("SYSTEM", "DetectWidth", value))
            {
                return true;
            }
            return false;
        }
        public static string GetDetectHeight()
        {
            string dataTable = GetConfigurationValue("SYSTEM", "DetectHeight");
            return dataTable;
        }
       
        public static bool SetDetectHeight(string value)
        {
            if (SetConfigurationValue("SYSTEM", "DetectHeight", value))
            {
                return true;
            }
            return false;
        }
        public static string GetListTrainFile()
        {
            string dataTable = GetWorkingDir() + "\\" + GetConfigurationValue("SYSTEM", "TrainList");
            return dataTable;
        }
        public static string GetModel()
        {
            string dataTable = GetWorkingDir() +"\\" + GetConfigurationValue("SYSTEM", "Model");
            return dataTable;
        }
        public static string GetWeight()
        {
            string dataTable = GetWorkingDir() + "\\" + GetConfigurationValue("SYSTEM", "Weight");
            return dataTable;
        }

        internal static string GetTrainFolder()
        {
            string res = string.Empty;
            string dataTable = GetWorkingDir() + "\\" + GetConfigurationValue("SYSTEM", "TrainList");
            if (!File.Exists(dataTable))
            {
                return res;
            }
            else
            {
                try
                {
                    System.Text.Encoding charset = System.Text.Encoding.ASCII;
                    StreamReader reader = new StreamReader(dataTable, charset);
                    String sLine = "Init";
                    while (!String.IsNullOrEmpty(sLine))
                    {
                        sLine = reader.ReadLine();
                        if ((!String.IsNullOrEmpty(sLine)))
                        {
                            res   = Path.GetDirectoryName(sLine);
                            reader.Close();
                            return res;
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
            return res;
        }
    }
}

