using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using CommonLibs;
using DirectShowLib;
using Point = System.Drawing.Point;
using System.Diagnostics;
using static YoloSharp.YoloWrapper;
using System.Collections.Generic;
using System.Linq;
using YoloSharp;
using System.Reflection;

namespace TrainingTool
{
    public partial class frmTrainTool : Form
    {
        #region declare
        const int c_nClass = 12;
        const string c_sDecimal = "F5";
        const double c_dConf = 0.5;
        Color[] _colorClass = new Color[50];
        Point pCBClassPos = new Point(20,16);
        Point pTextClassPos = new Point(60, 16);
        Point pTextLablePos = new Point(40, 16);
        List<CheckBox> _lClass;
        List<TextBox> _lClassName;
        int _nSpaceClass = 25;
        int _nClass = 0;
        int _nSelectClass = 0;
        string _sRuntime_folder;
        string _sWorking_folder;
        string _sPre_folder;
        string _filename;
        int _nSelectedFile = 0;
        int _nFileTrain = 0;
        Mat _preImage;
        Mat _img;
        Bitmap _bmp;
        Point _pTL = new Point(0, 0);
        Point _pBR = new Point(0, 0);
        Point _pTLROI = new Point(0, 0);
        Point _pBRROI = new Point(0, 0);
        Point _pROIMOVE = new Point(0, 0);
        Point _pROISize = new Point(0, 0);
        int _nTrainWidth;
        int _nTrainHeight;
        int _nDetectWidth;
        int _nDetectHeight;
        //int _boxlabel = 0;
        bool _bCtrl,_bShift,_bAlt;
        bool _bAutoVideo = false;
        Mat _frameCapture = new Mat();
        bool _bCamReady = false;
        bool _bCamRunning = false;
        VideoCapture _capture = new VideoCapture();
        String _sSelectedModel;
        YoloGPU _detector;
        List<bbox_t> _Detectbox;
        int _nSelecteDetectBox =0;
        int _nSelecteTeachBox = 0;
        List<bbox_t> _Teachbox;
        public struct checkingScrewNum
        {
            public int nScrewNum;
            public int nFrameNum;
        };
        List<checkingScrewNum> _checkingScrew = new List<checkingScrewNum>();
        int _nCheckingScrew = 0;
        VideoCapture _videocapture = new VideoCapture();
        int m_nFramCount;
        int m_nFrameTotal;
        bool _bCaptureVideo;
        String _detection_modelConfiguration ;
        String _detection_modelWeights ;
        #endregion
        #region Init
        public frmTrainTool()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            InitColorGrid();
            //
            _sWorking_folder = Directory.GetCurrentDirectory();
            _sRuntime_folder = Directory.GetCurrentDirectory();
            _sPre_folder = _sWorking_folder;
            tbWorkingDir.Text = _sWorking_folder;
            String jobConfiguration = _sWorking_folder + "\\Data\\job.config";
            JobConfig.OpenJob(jobConfiguration);
            _bCamReady = InitCamCapture();
           
            cbResize.Checked = true;
            cbShowAll.Checked = true;
            InitGrid();
            InitGridImport();
            OpenJob(jobConfiguration);
            SearchAllOutput();
            //lbCheckingNum.Text = "";
        }
        private void InitColorGrid()
        {
            for(int i=0; i< _colorClass.Length; i++)
                _colorClass[i] = Color.White;
            //Init color for gridview 
            _colorClass[0] = Color.LightGreen;
            _colorClass[1] = Color.LightGoldenrodYellow;
            _colorClass[2] = Color.LightPink;
            _colorClass[3] = Color.LightSalmon;
            _colorClass[4] = Color.LightSkyBlue;
            _colorClass[5] = Color.LightYellow;
            _colorClass[6] = Color.LightBlue;
        }
        void InitGrid()
        {
            //List Image
            dgvListImage.Columns.Add("Index", "Index");
            dgvListImage.Columns.Add("Image", "Image");
            dgvListImage.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvListImage.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvListImage.AllowUserToAddRows = false;
            dgvListImage.AllowUserToResizeRows = false;
            //list teach Object
            dgvListObject.Columns.Add("Index", "Index");
            dgvListObject.Columns.Add("Class", "Class");
            dgvListObject.Columns.Add("ClassID", "ClassID");
            dgvListObject.Columns.Add("X", "X");
            dgvListObject.Columns.Add("Y", "Y");
            dgvListObject.Columns.Add("W", "W");
            dgvListObject.Columns.Add("H", "H");
            dgvListObject.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvListObject.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvListObject.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvListObject.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvListObject.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvListObject.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvListObject.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvListObject.AllowUserToAddRows = false;
            dgvListObject.AllowUserToResizeRows = false;
            //List detect Object
            dgvDetecResult.Columns.Add("Index", "Index");
            dgvDetecResult.Columns.Add("Class", "Class");
            dgvDetecResult.Columns.Add("ClassID", "ClassID");
            dgvDetecResult.Columns.Add("X", "X");
            dgvDetecResult.Columns.Add("Y", "Y");
            dgvDetecResult.Columns.Add("W", "W");
            dgvDetecResult.Columns.Add("H", "H");
            dgvDetecResult.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvDetecResult.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetecResult.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvDetecResult.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgvDetecResult.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgvDetecResult.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgvDetecResult.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgvDetecResult.AllowUserToAddRows = false;
            dgvDetecResult.AllowUserToResizeRows = false;
            dgvDetecResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           
        }
        void InitGridImport()
        {
            //List Image
            dgvImport.Columns.Add("Index", "Index");
            dgvImport.Columns.Add("Image", "Image");
            dgvImport.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvImport.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvImport.AllowUserToAddRows = false;
            dgvImport.AllowUserToResizeRows = false;
        }
        private String[] GetClassNameAll()
        {
            TextBox cb;
            String[] sclassName = new String[_nClass];
            for (int i = 0; i < _nClass; i++)
            {
                if (i > _lClass.Count)
                {
                    sclassName[i] = JobConfig.GetClassName(i);
                }
                else
                {
                    cb = _lClassName[i];
                    sclassName[i] = cb.Text;
                }
            }
            return sclassName;
        }
        void AddNewClass(string scbName, string sTbName, string sClassName, int nClass)
        {
            if (_nClass < c_nClass - 1)
            {
                CheckBox cbClass1 = new CheckBox();
                cbClass1.Name = scbName;
                cbClass1.Text = " ";
                cbClass1.Width = 15;
                //cbClass1.Enabled = false;
                cbClass1.Location = new Point(pCBClassPos.X, pCBClassPos.Y + nClass * _nSpaceClass);
                cbClass1.CheckedChanged += CbClass1_CheckedChanged;
                gbObjectClass.Controls.Add(cbClass1);
                TextBox tbClass1 = new TextBox();
                tbClass1.Name = sTbName;
                tbClass1.Text = sClassName;
                tbClass1.Width = 70;
                tbClass1.Location = new Point(pTextClassPos.X, pTextClassPos.Y + nClass * _nSpaceClass); ;
                gbObjectClass.Controls.Add(tbClass1);
                _lClass.Add(cbClass1);
                _lClassName.Add(tbClass1);
                Label lbClass1 = new Label();
                lbClass1.Name = sTbName;
                lbClass1.Text = nClass.ToString();
                lbClass1.Width = 20;
                lbClass1.Location = new Point(pTextLablePos.X, pTextLablePos.Y + nClass * _nSpaceClass+5);
                gbObjectClass.Controls.Add(lbClass1);
            }
        }
        void RemoveAllClass()
        {
            _lClass.Clear();
            _lClassName.Clear();
            gbObjectClass.Controls.Clear();
        }
        #endregion
        private void CbClass1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                for(int i=0; i<c_nClass;i++ )
                    if (cb.Checked && cb.Name == "cbClass"+i.ToString()) _nSelectClass = i;
                for (int i = 0; i < _lClass.Count; i++)
                {
                    cb = _lClass[i];
                    if (i == _nSelectClass) cb.Checked = true;
                    else cb.Checked = false;
                }
            }
            else
            {
                bool bFound = false;
                for (int i = 0; i < _lClass.Count; i++)
                {
                    cb = _lClass[i];
                    if (cb.Checked) bFound = true;
                }
                if (!bFound) { _lClass[0].Checked = true; _nSelectClass = 0; }
            }
        }
        private String GetClassName()
        {
            TextBox cb;
            String sclassName = "";
            for (int i = 0; i < _lClassName.Count; i++)
            {
                cb = _lClassName[i];
                if (i == _nSelectClass) sclassName = cb.Text;
            }
            return sclassName;
        }
        
        private void btAddClass_Click(object sender, EventArgs e)
        {
            AddNewClass("cbClass" + _nClass.ToString(), "tbClass" + _nClass.ToString(), "Class" + _nClass.ToString(), _nClass);
            _nClass++;
        }
        #region Camera
        private int CheckCamID()
        {
            int nCamIdx = -1;
            String sCamID = JobConfig.GetCamID().Trim();
            if (String.IsNullOrEmpty(sCamID)) return -1;
            DsDevice[] captureDevices;
            // Get the set of directshow devices that are video inputs.
            captureDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            for (int idx = 0; idx < captureDevices.Length; idx++)
            {
                if (captureDevices[idx].Name == sCamID) nCamIdx = idx;
            }
            if (nCamIdx <= -1)
            {
                lbStatus.Text = "Setting cam lỗi:" + sCamID;
                //MessageBox.Show("Setting Cam lỗi, vui lòng kiểm tra lại");
                return nCamIdx;
            }
            return nCamIdx;
        }
        bool InitCamCapture()
        {
            int nCamIdx = CheckCamID();
            if (nCamIdx <= -1)
            { return false; }
            if (_capture.IsOpened())
                _capture.Release();
            _capture.Open(nCamIdx);
            //_capture.FrameWidth = 1920;
            //_capture.FrameHeight = 1080;
            _capture.Read(_frameCapture);
            return true;
        }
        private void timerWebcam_Tick(object sender, EventArgs e)
        {
            if (_img == null) _img = new Mat();
            if (!_bCamReady) return;
            if (_capture.IsOpened())
            {
                _capture.Read(_img);
                if (_img.Height > 0)
                {
                    _bmp = _img.ToBitmap();
                    //_bmpCam.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    pbMainTrain.Image = _bmp;
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                }
            }
        }
        #endregion
       
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            _bCtrl = e.Control; _bShift = e.Shift; _bAlt = e.Alt;
            int i;
            if(e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                if (int.TryParse(e.KeyValue.ToString(), out i))
                {
                    _nSelectClass = i-49;
                }
                
            }    
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            _bCtrl = e.Control; _bShift = e.Shift; _bAlt = e.Alt;
        }
        private void pbMainTrain_MouseDown(object sender, MouseEventArgs e)
        {
            if (_bCtrl)
            {
                if (_img == null) return;
                _pTL = ViewToPoint(_bmp, e.Location);
            }
            else if (_bShift)
            {
                if (_img == null) return;
                _nTrainWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                if (_img.Width > _nTrainWidth) _pTLROI = ViewToPoint(_bmp, e.Location);
            }
            else if(_bAlt)
            {
                if (_img.Width > _nTrainWidth)
                {   Point ptTemp = ViewToPoint(_bmp, e.Location);
                    Rectangle recrOi = new Rectangle(_pTLROI.X, _pTLROI.Y,_pBRROI.X - _pTLROI.X, _pBRROI.Y - _pTLROI.Y);
                    if (recrOi.Contains(ptTemp))
                    {
                        _pROIMOVE = new Point(ptTemp.X - _pTLROI.X, ptTemp.Y - _pTLROI.Y);
                        _pROISize = new Point(_pBRROI.X - _pTLROI.X, _pBRROI.Y - _pTLROI.Y);
                    }
                    else _pROIMOVE = new Point(0, 0);
                }
            }
            pbMainTrain.Invalidate();
        }

        private void pbMainTrain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_img == null) return;
            if (e.Button == MouseButtons.Left)
            {
                if (_bCtrl)
                {
                    _pBR = ViewToPoint(_bmp, e.Location);
                    pbMainTrain.Invalidate();
                }
                else if (_bShift)
                {
                    _nTrainWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                    if (_img.Width > _nTrainWidth)
                    {
                        _pBRROI = ViewToPoint(_bmp, e.Location);
                        pbMainTrain.Invalidate();
                    }
                }
                else if(_bAlt)
                {
                    if(_pROIMOVE.X >0 && _pROIMOVE.Y>0)
                    {
                        Point ptTemp = ViewToPoint(_bmp, e.Location);
                        _pTLROI.X = (ptTemp.X - _pROIMOVE.X);
                        _pTLROI.Y = (ptTemp.Y - _pROIMOVE.Y);
                        _pBRROI.X = _pTLROI.X + _pROISize.X;
                        _pBRROI.Y = _pTLROI.Y + _pROISize.Y;
                        pbMainTrain.Invalidate();
                    }
                }
            }
            pbMainTrain.Invalidate();
        }
        private void pbMainTrain_MouseUp(object sender, MouseEventArgs e)
        {
            if (_img == null) return;
            if (_bCtrl)
            {
                _pBR = ViewToPoint(_bmp, e.Location);
                pbMainTrain.Invalidate();
            }
            else if (_bShift)
            {
                if (_img.Width > _nTrainWidth)
                {
                    _pBRROI = ViewToPoint(_bmp, e.Location);
                    pbMainTrain.Invalidate();
                }
            }
            _pROIMOVE = new Point(0, 0);
            pbMainTrain.Invalidate();
        }
        
        
        private void LoadLabel()
        {
            try
            {
                if (String.IsNullOrEmpty(_filename)) return;
                string sLabel = FileLocation.GetLocationLabel(_filename); 
                LoadLabel(sLabel);
            }
            catch(Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        private void LoadLabel(string sPath)
        {
            try
            {
                dgvListObject.Rows.Clear();
                _Detectbox = new List<bbox_t>();
                if (!File.Exists(sPath))
                {
                    lbStatus.Text = ("File: " + sPath + " not exist");
                }
                else
                {
                    System.Text.Encoding charset = System.Text.Encoding.ASCII;
                    StreamReader reader = new StreamReader(sPath, charset);
                    try
                    {
                        int nClass;
                        double dCx, dCy, dWx, dWy;
                        String[] sClass = new String[6];
                        sClass = GetClassNameAll();
                        String sLine = "Init";
                        int nCount = 0;
                        _Teachbox = new List<bbox_t>();
                        while (!String.IsNullOrEmpty(sLine))
                        {
                            sLine = reader.ReadLine();
                            if ((!String.IsNullOrEmpty(sLine)))
                            {
                                String[] sSplit = sLine.Split(' ');
                                nClass = Convert.ToInt16(sSplit[0]);
                                dCx = Convert.ToDouble(sSplit[1]);
                                dCy = Convert.ToDouble(sSplit[2]);
                                dWx = Convert.ToDouble(sSplit[3]);
                                dWy = Convert.ToDouble(sSplit[4]);
                                dgvListObject.Rows.Add(nCount.ToString(), sClass[nClass], nClass.ToString(),
                                    dCx.ToString(c_sDecimal), dCy.ToString(c_sDecimal), dWx.ToString(c_sDecimal),
                                    dWy.ToString(c_sDecimal));
                                dgvListObject.Rows[nCount].DefaultCellStyle.BackColor = _colorClass[nClass];
                                nCount++;
                                bbox_t bbox = new bbox_t();
                                //double dCx = (double)((_pTL.X + _pBR.X) / 2) / (double)_img.Width;
                                //double dCy = (double)((_pTL.Y + _pBR.Y) / 2) / (double)_img.Height;
                                //double dWx = (double)(_pBR.X - _pTL.X) / (double)_img.Width;
                                //double dWy = (double)(_pBR.Y - _pTL.Y) / (double)_img.Height;
                                int nTrainWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                                int nTrainHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
                                bbox.x = (uint)((dCx - dWx / 2) * nTrainWidth);
                                bbox.y = (uint)((dCy - dWy / 2) * nTrainHeight);
                                bbox.w = (uint)(dWx * nTrainWidth);
                                bbox.h = (uint)(dWy * nTrainHeight);
                                bbox.obj_id = (uint)nClass;
                                _Teachbox.Add(bbox);
                            }
                        }
                        pbMainTrain.Invalidate();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.StackTrace, ex.Message);
                        ErrorLog.WriteError(ex);
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        private void SaveLabel(string sPath)
        {
            string sDecimal = "F8";
            double dCx = (double)((_pTL.X+ _pBR.X)/2)/ (double)_img.Width;
            double dCy = (double)((_pTL.Y + _pBR.Y) / 2) / (double)_img.Height;
            double dWx = (double)(_pBR.X - _pTL.X) / (double)_img.Width;
            double dWy = (double)(_pBR.Y - _pTL.Y) / (double)_img.Height;
            string sFileContent = _nSelectClass.ToString()+" "+ dCx.ToString(sDecimal) + " "
                + dCy.ToString(sDecimal) + " "+
                dWx.ToString(sDecimal) + " "+ dWy.ToString(sDecimal) ;
            try
            {
                System.Text.Encoding charset = System.Text.Encoding.ASCII;
                if (!File.Exists(sPath))
                {
                    FileStream fileStream = File.Create(sPath);
                    fileStream.Close();
                    StreamWriter oReader = new StreamWriter(sPath,true, charset);
                    oReader.WriteLine(sFileContent);
                    oReader.Close();
                }
                else
                {
                    // Append text in file when file exitsed
                    FileStream fileStream = File.Open(sPath, FileMode.Append, FileAccess.Write,FileShare.ReadWrite);
                    StreamWriter oReader = new StreamWriter(fileStream, charset);
                    oReader.WriteLine(sFileContent);
                    oReader.Close();
                    fileStream.Close();
                }
                lbStatus.Text = ("Save label file to: " + sPath);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
                lbStatus.Text = ("Save label file fail: " + sPath);
            }
        }
        private void AutoAddFrameTraining()
        {
            string sImg=  FileLocation.GetLocationImage(_sWorking_folder) + DateTime.Now.ToString("yyyMMdd_HHmmss")+".jpg";
            while(File.Exists(sImg))
            {
                _nFileTrain++;
                sImg = FileLocation.GetLocationImage(_sWorking_folder) + DateTime.Now.ToString("yyyMMdd_HHmmss") + _nFileTrain + ".jpg";
            }
            _filename = sImg;
            try
            {
                int nWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                int nHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
                if (_img.Width <= nWidth) { MessageBox.Show("Image already crop"); return; }
                _img = _img.Resize(new OpenCvSharp.Size(nWidth, nHeight));
                _bmp = _img.ToBitmap();
                pbMainTrain.Image = _bmp;
                _img.SaveImage(_filename);
                TrainListWriter.WriterTrainList(_sRuntime_folder, _filename);
                dgvListImage.Rows.Add(_nFileTrain.ToString(), _filename);
                SaveAutoLabel();
            }
            catch(Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        private void SaveAutoLabel()
        {
            if (String.IsNullOrEmpty(_filename)) return;
            string sFile;
            string sLabel = FileLocation.GetLocationLabel(_filename);
            try
            {

                System.Text.Encoding charset = System.Text.Encoding.ASCII;
                FileStream fileStream;
                if (!File.Exists(sLabel))
                {
                    fileStream = File.Create(sLabel);
                    fileStream.Close();
                }
                fileStream = File.Open(sLabel, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter oReader = new StreamWriter(fileStream, charset);
                Detect();
                for (int i =0; i< _Detectbox.Count; i++)
                {
                    bbox_t bx = _Detectbox[i];
                    double dCx = (double)(bx.x+ bx.w/2) / (double)_img.Width;
                    double dCy = (double)(bx.y+ bx.h/2) / (double)_img.Height;
                    double dWx = (double)(bx.w) / (double)_img.Width;
                    double dWy = (double)(bx.h) / (double)_img.Height;
                    string sFileContent = bx.obj_id.ToString() + " " + dCx.ToString(c_sDecimal) + " "
                        + dCy.ToString(c_sDecimal) + " " +
                        dWx.ToString(c_sDecimal) + " " + dWy.ToString(c_sDecimal);
                    // Append text in file when file exitsed
                    oReader.WriteLine(sFileContent);
                }
                oReader.Close();
                fileStream.Close();
                lbStatus.Text = ("Save label file to: " + sLabel);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
                lbStatus.Text = ("Save label file fail: " + sLabel);
            }
        }
        private string[] GetBoxPos(string[] sClassName,List<bbox_t> lbox)
        {
            string[] sBoxpos = new string[lbox.Count];
            int nIndex =0;
            int nInexPos = 0;
            float fConfl = 0.0f;
            bool bFound = false;
            for (int i =0;i < lbox.Count; i++)
            {
                if(lbox[i].obj_id > 1)
                {
                    if(fConfl< lbox[i].prob)
                    {
                        nIndex = i;
                        nInexPos = Convert.ToInt16(sClassName[(int)lbox[i].obj_id]);
                        fConfl = lbox[i].prob;
                        bFound = true;
                    }
                }
            }
            for (int i = 0; i < lbox.Count; i++)
            {
                sBoxpos[i] = ((nInexPos - nIndex)+ i).ToString();
                if (lbox[i].obj_id == 1)
                {
                    if (bFound)
                    {
                        //lbCheckingNum.Text = "Checking Number:" + sBoxpos[i];
                        AddChecking(Convert.ToInt16(sBoxpos[i]), m_nFramCount);
                    }
                }
            }
            return sBoxpos;
        }
        private void AddChecking(int nScrew, int nFrameNum1)
        {
            bool bFound = false;
            for(int i =0; i< _checkingScrew.Count; i++)
            {
                if (_checkingScrew[i].nScrewNum == nScrew) bFound = true;
            }
            if (!bFound)
            {
                if (_nCheckingScrew == nScrew)
                {
                    checkingScrewNum checking = new checkingScrewNum
                    {
                        nScrewNum = nScrew,
                        nFrameNum = nFrameNum1
                    };
                    _checkingScrew.Add(checking); }
                else
                {
                    _nCheckingScrew = nScrew;
                }
            }
        }
       
        #region Draw
        private void pbMainTrain_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (_img == null) return;
                if (!(_img.Width > 0)) return;
                Graphics dc = e.Graphics;
                drawRect(dc, _img.ToBitmap(), _pTL, _pBR, GetClassName(), Color.Green);
                _nTrainWidth = SMCLib.SMCNumber.ToInteger(JobConfig.GetTrainWidth());
                _bmp = _img.ToBitmap();
                if (_img.Width > _nTrainWidth) 
                    drawRect(dc, _bmp, _pTLROI, _pBRROI, "ROI", Color.Yellow);
                else
                {
                    if (_Teachbox != null)
                    {
                        if (_Teachbox.Count > 0) drawTeachBox(dc, _bmp, _Teachbox);
                    }
                }
                if (_Detectbox != null) { if (_Detectbox.Count > 0) drawDetectBox(dc, _bmp, _Detectbox); }
            }
            catch(Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        private Point ViewToPoint(Image img, Point cp)
        {
            Point ptRes = new Point();
            int cw = pbMainTrain.Width;
            int ch = pbMainTrain.Height;

            float fx = cw * 1.0f / img.Width;
            float fy = ch * 1.0f / img.Height;
            float f = fx;
            if (f > fy)
                f = fy;
            float dw, dh;
            dw = f * img.Width;
            dh = f * img.Height;
            float dx, dy;
            dx = (cw - dw) / 2;
            dy = (ch - dh) / 2;
            ptRes.X = (int)((cp.X - dx) / f);
            ptRes.Y = (int)((cp.Y - dy) / f);
            return ptRes;
        }
        private Point PointToView(Image img, Point cp)
        {
            Point ptRes = new Point();
            int cw = pbMainTrain.Width;
            int ch = pbMainTrain.Height;

            float fx = cw * 1.0f / img.Width;
            float fy = ch * 1.0f / img.Height;
            float f = fx;
            if (f > fy)
                f = fy;
            float dw, dh;
            dw = f * img.Width;
            dh = f * img.Height;
            float dx, dy;
            dx = (cw - dw) / 2;
            dy = (ch - dh) / 2;
            ptRes.X = (int)(dx + cp.X * f);
            ptRes.Y = (int)(dy + cp.Y * f);
            return ptRes;
        }
        private void drawRect(Graphics dc, Image img, Point tl, Point br, String sLabel, Color color)
        {
            if (Math.Abs(br.X - tl.X) == 0) return;
            if (Math.Abs(br.Y - tl.Y) == 0) return;
            Point viewTL = PointToView(img, tl);
            Point viewBR = PointToView(img, br);
            Pen pen = new Pen(color, 2);
            dc.DrawRectangle(pen, viewTL.X, viewTL.Y, viewBR.X - viewTL.X, viewBR.Y - viewTL.Y);
            dc.DrawString(sLabel, new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
                    new SolidBrush(Color.Green), viewTL.X, viewTL.Y + 2);
        }
        private void drawDetectBox(Graphics dc, Image img, List<bbox_t> lbox)
        {
            //Draw detect 
            //if (!_bDrawImage)
            //    return;
            int cw = pbMainTrain.Width;
            int ch = pbMainTrain.Height;

            float fx = cw * 1.0f / img.Width;
            float fy = ch * 1.0f / img.Height;
            float f = fx;
            if (f > fy)
                f = fy;
            float dw, dh;
            dw = f * img.Width;
            dh = f * img.Height;
            float dx, dy;
            dx = (cw - dw) / 2;
            dy = (ch - dh) / 2;

            int nCnt = lbox.Count;
            //lbCheckingNum.Text = "";
            string[] sClassName = GetClassNameAll();
            string[] sBoxNum = GetBoxPos(sClassName, lbox);
            for (int i = 0; i < nCnt; i++)
            {
                bbox_t b = lbox[i];

                float nX = dx + b.x * f;
                float nY = dy + b.y * f;
                float nWidth = b.w * f;
                float nHeight = b.h * f;
                float fscores = b.prob;
                UInt32 obj_id = b.track_id;
                Pen pen = new Pen(_colorClass[(int)b.obj_id], 2);
                Pen penRed = new Pen(Color.Red, 2);
                if (b.prob > c_dConf)
                {
                    if (cbShowChecking.Checked)
                    {
                        if (b.obj_id == (uint)1)
                        {
                            dc.DrawRectangle(pen, nX, nY, nWidth, nHeight);
                            //dc.DrawString(b.prob.ToString("F2"),
                            //       new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular),
                            //       new SolidBrush(Color.Red), nX, nY + 12);
                            dc.DrawString(sBoxNum[i],
                                 new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular),
                                 new SolidBrush(Color.Red), nX, nY - 22);
                            dc.DrawString(sClassName[(int)b.obj_id],
                                    new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
                                    new SolidBrush(Color.Green), nX, nY + nHeight + 2);
                        }
                    }
                    else if (cbShowAll.Checked)
                    {
                        dc.DrawRectangle(pen, nX, nY, nWidth, nHeight);
                        dc.DrawString(sBoxNum[i],
                               new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular),
                               new SolidBrush(Color.Red), nX, nY - 22);
                        //dc.DrawString(b.prob.ToString("F2"),
                        //          new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular),
                        //          new SolidBrush(Color.Red), nX, nY + 12);
                        if (i == _nSelecteDetectBox)
                            dc.DrawString(sClassName[(int)b.obj_id],
                                new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular),
                                new SolidBrush(Color.Red), nX, nY + nHeight + 2);
                        else
                            dc.DrawString(sClassName[(int)b.obj_id],
                                new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
                                new SolidBrush(Color.Green), nX, nY + nHeight + 2);
                    }
                    else
                    {
                        if (i == _nSelecteDetectBox)
                        {
                            dc.DrawRectangle(pen, nX, nY, nWidth, nHeight);
                            dc.DrawString(sBoxNum[i],
                               new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular),
                               new SolidBrush(Color.Red), nX, nY - 22);
                            dc.DrawString(b.prob.ToString("F2"),
                                  new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular),
                                  new SolidBrush(Color.Red), nX, nY + 12);
                            dc.DrawString(sClassName[(int)b.obj_id],
                                    new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular),
                                    new SolidBrush(Color.Red), nX, nY + nHeight + 2);
                        }
                    }
                }
            }
        }
        private void drawTeachBox(Graphics dc, Image img, List<bbox_t> lbox)
        {
            //if (!_bDrawImage)
            //    return;
            try
            {
                int cw = pbMainTrain.Width;
                int ch = pbMainTrain.Height;

                float fx = cw * 1.0f / img.Width;
                float fy = ch * 1.0f / img.Height;
                float f = fx;
                if (f > fy)
                    f = fy;
                float dw, dh;
                dw = f * img.Width;
                dh = f * img.Height;
                float dx, dy;
                dx = (cw - dw) / 2;
                dy = (ch - dh) / 2;

                int nCnt = lbox.Count;
                string[] sClassName = GetClassNameAll();
                for (int i = 0; i < nCnt; i++)
                {
                    bbox_t b = lbox[i];

                    float nX = dx + b.x * f;
                    float nY = dy + b.y * f;
                    float nWidth = b.w * f;
                    float nHeight = b.h * f;
                    Pen pen = new Pen(Color.LightGreen, 2);
                    Pen penRed = new Pen(Color.Red, 2);
                    if (i == _nSelecteTeachBox)
                        dc.DrawRectangle(penRed, nX, nY, nWidth, nHeight);
                    else
                        dc.DrawRectangle(pen, nX, nY, nWidth, nHeight);
                    if (cbShowName.Checked)
                        dc.DrawString(sClassName[(int)b.obj_id], new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular),
                                    new SolidBrush(Color.Green), nX, nY + nHeight + 2);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        #endregion

        #region detect
        private void Detect()
        {
            _Teachbox = new List<bbox_t>();
            _Detectbox = new List<bbox_t>();
            dgvDetecResult.Rows.Clear();
            if (_detector == null) return;
            Stopwatch stopWatch = Stopwatch.StartNew();
            int nWidth = _nDetectWidth;
            int nHeight = _nDetectHeight;
            _bmp = _img.ToBitmap();
            var _box = _detector.Detect(_img);
            String[] sClass = GetClassNameAll();
            int nCount = 0;
            for (int i = 0; i < _box.Count; i++)
            {
                if (_box[i].prob > c_dConf)
                {
                    _Detectbox.Add(_box[i]);
                }
            }
            _Detectbox = _Detectbox.OrderBy(o => o.x).ToList();
            for (int i = 0; i < _Detectbox.Count; i++)
            {
                int nClass = (int)_Detectbox[i].obj_id;
                double dCx = _Detectbox[i].x;
                double dCy = _Detectbox[i].y;
                double dWx = _Detectbox[i].w;
                double dWy = _Detectbox[i].h;
                dgvDetecResult.Rows.Add(nCount.ToString(), sClass[nClass], nClass.ToString(),
                               dCx.ToString(c_sDecimal), dCy.ToString(c_sDecimal), dWx.ToString(c_sDecimal),
                               dWy.ToString(c_sDecimal));
                dgvDetecResult.Rows[nCount].DefaultCellStyle.BackColor = _colorClass[nClass];
                nCount++;
            }
            stopWatch.Stop();
            float ts = stopWatch.ElapsedMilliseconds;
            lbTime.Invoke((MethodInvoker)(() => lbTime.Text = "Time : " + ts.ToString("F3") + "ms"));
            pbMainTrain.Image = _bmp;
            pbMainTrain.Invalidate();
        }
        private void btSetting_Click(object sender, EventArgs e)
        {
            FormSeting frm = new FormSeting();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                String jobConfiguration = _sWorking_folder + "\\Data\\job.config";
                OpenJob(jobConfiguration);
            }
        }
        private void btDetect_Click(object sender, EventArgs e)
        {
            Detect();
        }
        #endregion
        private void btStartPreview_Click(object sender, EventArgs e)
        {
            if(!_bCamRunning)
            {
                _bCamRunning = true;
                timerWebcam.Enabled = true;
                btStartPreview.Text = "Stop Preview";
            }
            else
            {
                _bCamRunning = false;
                timerWebcam.Enabled = false;
                btStartPreview.Text = "Start Preview Camera";
            }
        }
        void OpenImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "image files |*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            dialog.Title = "Please select an image file.";
            dialog.InitialDirectory = _sPre_folder;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _filename = dialog.FileName;
                try
                {
                    _img = new Mat(_filename);
                    _sPre_folder = Path.GetDirectoryName(_filename);
                    _bmp = _img.ToBitmap();
                    pbMainTrain.Image = _bmp;
                    if(_filename.Contains(_sWorking_folder)) _filename = _filename.Replace(_sWorking_folder + "\\", "");
                    dgvImport.Rows.Add(dgvImport.Rows.Count.ToString(), _filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }
        private void btOpenImage_Click(object sender, EventArgs e)
        {
            OpenImage();
        }
        private void btCapture_Click(object sender, EventArgs e)
        {
            _bCamRunning = false;
            timerWebcam.Enabled = false;
            btStartPreview.Text = "Start Preview Camera";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "image files |*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            dialog.Title = "Please select location to save file.";
            dialog.InitialDirectory = _sPre_folder;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _filename = dialog.FileName;
                try
                {
                    _img.SaveImage(_filename);
                    _sPre_folder = Path.GetDirectoryName(_filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Saving image: " + ex.Message, "AI Training Tool");
                }
            }
        }
        private void DeleteAll(string sFolder)
        {
            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(sFolder);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btDelteAll_Click(object sender, EventArgs e)
        {
            string sLabelFolder = FileLocation.GetLocationLabel(_sWorking_folder);
            DeleteAll(sLabelFolder);
            string sJPEGImageFolder = FileLocation.GetLocationImage(_sWorking_folder);
            DeleteAll(sJPEGImageFolder);
            dgvListImage.Rows.Clear();
            dgvListObject.Rows.Clear();
            _nFileTrain = 0;
        }
        private void btSaveLabel_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_filename)) return;
            string sLabel = FileLocation.GetLocationLabel(_filename);
            SaveLabel(sLabel);
            LoadLabel(sLabel);
        }
        private void SaveListImageToFile()
        {
            string sPath = JobConfig.GetListTrainFile();
            File.Create(sPath).Close();
            try
            {
                System.Text.Encoding charset = System.Text.Encoding.ASCII;
                FileStream fileStream;
                if (!File.Exists(sPath))
                {
                    fileStream = File.Create(sPath);
                    fileStream.Close();
                }
                fileStream = File.Open(sPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter oReader = new StreamWriter(fileStream, charset);
                for (int i = 0; i < dgvListImage.Rows.Count; i++)
                {
                    string sFileContent = dgvListImage.Rows[i].Cells[1].Value.ToString();
                    // Append text in file when file exitsed
                    oReader.WriteLine(sFileContent);
                }
                oReader.Close();
                fileStream.Close();
                lbStatus.Text = ("Save list image to: " + sPath);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
                lbStatus.Text = ("Save list image fail: " + sPath);
            }
        }
        private void LoadTrainListFile(string sPathIn = null)
        {
            string sPath = JobConfig.GetListTrainFile();
            if (!string.IsNullOrEmpty(sPathIn))
                sPath = sPathIn;
            if (!File.Exists(sPath))
            {
                MessageBox.Show("File: " + sPath + " not exist");
            }
            else
            {
                try
                {
                    System.Text.Encoding charset = System.Text.Encoding.ASCII;
                    StreamReader reader = new StreamReader(sPath, charset);
                    dgvListImage.Rows.Clear();
                    String sLine = "Init";
                    int nCount = 0;
                    while (!String.IsNullOrEmpty(sLine))
                    {
                        sLine = reader.ReadLine();
                        if ((!String.IsNullOrEmpty(sLine)))
                        {
                            dgvListImage.Rows.Add(nCount.ToString(), sLine);
                            nCount++;
                            _nFileTrain++;
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
        }
        
        string GenFilesave(string sDir)
        {
            int i = 1;
            string sFile = sDir+"\\" + i.ToString() + ".jpg";
            while (File.Exists(sFile))
            {
                i++;
                sFile = sDir + "\\" + i.ToString() + ".jpg";
            }
            return sFile;
        }
        private void btAddTrain_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "image files |*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            dialog.Title = "Please select location to save file.";
            string saveDir = JobConfig.GetTrainFolder() + "\\";
            dialog.RestoreDirectory = true;
            //dialog.InitialDirectory = saveDir;
            dialog.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            dialog.FileName = GenFilesave(saveDir).Replace(saveDir+"\\",""); 
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _filename = dialog.FileName;
                try
                {
                    int nWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                    int nHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
                    if (_img.Width!= nWidth || _img.Height != nHeight)
                    { _img = _img.Resize(new OpenCvSharp.Size(nWidth, nHeight)); }
                    _img.SaveImage(_filename);
                    _filename = TrainListWriter.WriterTrainList(_sRuntime_folder, _filename);
                    dgvListImage.Rows.Add(_nFileTrain.ToString(), _filename);
                    _nFileTrain++;
                    _bmp = _img.ToBitmap();
                    pbMainTrain.Image = _bmp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Saving image: " + ex.Message, "AI Training Tool");
                }
            }
        }
        private void GenerateBat()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        private void ExcuteBat(string exePath)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo(exePath); //exePath must be full path.
                info.WorkingDirectory = _sWorking_folder;
                info.CreateNoWindow = false;
                info.UseShellExecute = true;
                Process.Start(info);
            }
            catch(Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        private void btStartTrain_Click(object sender, EventArgs e)
        {
            timerOutput.Enabled = true;
            ExcuteBat(_sWorking_folder + "\\train.cmd");
        }
        private void SearchAllOutput()
        {
            try
            {
                string sOuputFolder = _sWorking_folder + "\\Output\\";
                System.IO.DirectoryInfo di = new DirectoryInfo(sOuputFolder);
                listBoxOut.Items.Clear();
                foreach (FileInfo file in di.GetFiles())
                {
                    listBoxOut.Items.Add(file.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void timerOutput_Tick(object sender, EventArgs e)
        {
            SearchAllOutput();
        }
        private void btUseThisModel_Click(object sender, EventArgs e)
        {
            try
            {
                _nDetectWidth = Convert.ToInt16(JobConfig.GetDetectWidth());
                _nDetectHeight = Convert.ToInt16(JobConfig.GetDetectHeight());
                if (String.IsNullOrEmpty(_sSelectedModel))
                {
                    _detector = new YoloGPU(_detection_modelConfiguration, _detection_modelWeights, _nDetectWidth, _nDetectHeight);
                    lbStatus.Text = "Re-init detecion with default model success";
                }
                else
                {
                    String detection_modelConfiguration = _sWorking_folder + "\\model\\running\\model.cfg";
                    String detection_modelWeights = _sWorking_folder + "\\model\\running\\model.weights";
                    String detection_modelSource = _sWorking_folder + "\\Data\\Output\\" + _sSelectedModel;
                    File.Copy(detection_modelSource, detection_modelWeights, true);
                    
                    _detector = new YoloGPU(detection_modelConfiguration, detection_modelWeights, _nDetectWidth, _nDetectHeight);
                    lbStatus.Text = "Re-init detecion model success";
                    _sSelectedModel = "";
                    lbSelectedModel.Text = _sSelectedModel;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btStopTraining_Click(object sender, EventArgs e)
        {
            timerOutput.Enabled = false;
            SearchAllOutput();
        }

        private void listBoxOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxOut.SelectedItem != null)
            {
                _sSelectedModel = listBoxOut.SelectedItem.ToString();
                lbSelectedModel.Text = _sSelectedModel;
            }
            else
            {
                _sSelectedModel = "";
                lbSelectedModel.Text = "Not select";
            }
        }

        private void cbResizeCrop_CheckedChanged(object sender, EventArgs e)
        {
            if (cbResizeCrop.Checked) cbResize.Checked = false;
            else cbResize.Checked = true;
        }
        private void cbResize_CheckedChanged(object sender, EventArgs e)
        {
            if (cbResize.Checked) cbResizeCrop.Checked = false;
            else cbResizeCrop.Checked = true;
        }
        private void CalculateBoxCrop()
        {
            int nWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
            int nHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
            Point ptCenter = new Point((int)(_pTLROI.X + _pBRROI.X)/2, (int)(_pTLROI.Y + _pBRROI.Y) / 2);
            _pTLROI.X = ptCenter.X - (int)(nWidth / 2);
            _pTLROI.Y = ptCenter.Y - (int)(nHeight / 2);
            _pBRROI.X = ptCenter.X + (int)(nWidth/2);
            _pBRROI.Y = ptCenter.Y + (int)(nHeight / 2); 
            pbMainTrain.Invalidate();
        }
        private void CalculateBoxCropResize()
        {
            int nWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
            int nHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
            int nROIWidth = _pBRROI.X - _pTLROI.X;
            int nROIHeight = _pBRROI.Y- _pTLROI.Y;
            int cw = pbMainTrain.Width;
            int ch = pbMainTrain.Height;
            double fx = (double)nWidth / (double)nROIWidth;
            double fy = (double)nHeight / (double)nROIHeight;
            double f = fx;
            if (f > fy)f = fy;
            double dw, dh;
            dw = nWidth/f;
            dh = nHeight/f;
            _pBRROI.X = _pTLROI.X + (int)dw;
            _pBRROI.Y = _pTLROI.Y + (int)dh;
            if (_pTLROI.X < 0 || _pTLROI.Y < 0 || _pBRROI.X > _img.Width || _pBRROI.Y > _img.Height)
            {
                MessageBox.Show("Error in select training ROI. \r\n Please check again ", "AI Training Tool");
            }
        }
        private void btAdjustROI_Click(object sender, EventArgs e)
        {
            if (cbResize.Checked)
            {
                CalculateBoxCropResize();
            }
            else
            {
                CalculateBoxCrop();
            }
            pbMainTrain.Invalidate();
        }
        private void btDeleteAllBounding_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(_filename)) return;
                string sLabel = FileLocation.GetLocationLabel(_filename);
                File.Create(sLabel).Close();
                LoadLabel(sLabel);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void btCropImage_Click(object sender, EventArgs e)
        {
            try
            {
                int nWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                int nHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
                if (_img.Width <= nWidth) { MessageBox.Show("Image already crop"); return; }
                else
                {
                    _img = new Mat(_img, new Rect(_pTLROI.X, _pTLROI.Y, _pBRROI.X - _pTLROI.X, _pBRROI.Y - _pTLROI.Y));
                    _img = _img.Resize(new OpenCvSharp.Size(nWidth, nHeight));
                    _bmp = _img.ToBitmap();
                    pbMainTrain.Image = _bmp;
                    pbMainTrain.Invalidate();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLog.WriteError(ex);
            }
        }
        private void dgvListImage_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvListImage.SelectedRows != null)
            {
                if (dgvListImage.SelectedRows.Count == 0) return;
                if (dgvListImage.SelectedRows[0].Cells["Image"].Value == null) return;
                string sPath = dgvListImage.SelectedRows[0].Cells["Image"].Value.ToString();
                _nSelectedFile = dgvListImage.SelectedRows[0].Index;
                _filename = sPath;
                if (File.Exists(sPath))
                {
                    dgvDetecResult.Rows.Clear();
                    _img = new Mat(sPath);
                    _bmp = _img.ToBitmap();
                    pbMainTrain.Image = _bmp;
                    _Detectbox = new List<bbox_t>();
                    _Teachbox = new List<bbox_t>();
                    LoadLabel();
                }
                else
                {
                    MessageBox.Show("File: "+sPath + " not exist");
                }
            }
        }
        private void dgvDetecResult_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDetecResult.SelectedRows != null)
            {
                if (dgvDetecResult.SelectedRows.Count == 0) return;
                _nSelecteDetectBox = dgvDetecResult.SelectedRows[0].Index;
                pbMainTrain.Invalidate();
            }
        }
        private void dgvListObject_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvListObject.SelectedRows != null)
            {
                if (dgvListObject.SelectedRows.Count == 0) return;
                _nSelecteTeachBox = dgvListObject.SelectedRows[0].Index;
                pbMainTrain.Invalidate();
            }
        }

        private void btNewJob_Click(object sender, EventArgs e)
        {
            frmJobName frm = new frmJobName();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                string sNewJobName = frm.JobName;
               
                
            }    
        }

        private void btOpenJob_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Config Files (*.config)|*.config";
            dlg.InitialDirectory=System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(dlg.FileName)) return;
                OpenJob(dlg.FileName);
            }
        }
        private void OpenJob(string sPath)
        {
            try
            {
                _lClass = new List<CheckBox>();
                _lClassName = new List<TextBox>();
                JobConfig.OpenJob(sPath);
                _sWorking_folder = JobConfig.GetWorkingDir();
                tbWorkingDir.Text = _sWorking_folder;
                _nTrainWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                _nTrainHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
                _nDetectWidth = Convert.ToInt16(JobConfig.GetDetectWidth());
               _nDetectHeight = Convert.ToInt16(JobConfig.GetDetectHeight());
                _pTLROI = new Point(Convert.ToInt16(JobConfig.GetStartROIX()), Convert.ToInt16(JobConfig.GetStartROIY()));
                _pBRROI = new Point(Convert.ToInt16(JobConfig.GetStartROIX()) + Convert.ToInt16(JobConfig.GetDetectWidth()), 
                    Convert.ToInt16(JobConfig.GetStartROIY()) + Convert.ToInt16(JobConfig.GetDetectHeight()));
                _nClass = Convert.ToInt16(JobConfig.GetNumClass());
                string TrainFile = JobConfig.GetListTrainFile();
                RemoveAllClass();
                for (int i = 0; i < _nClass; i++)
                {
                    string s = JobConfig.GetClassName(i);
                    AddNewClass("cbClass" + i.ToString(), "tbClass" + i.ToString(), s, i);
                }
                LoadTrainListFile(TrainFile);
                _detection_modelConfiguration = JobConfig.GetModel();
                _detection_modelWeights = JobConfig.GetWeight();
                var modelConfigurationPlate = _sWorking_folder + "\\Data\\Model\\model.cfg";
                var modelWeightsPlate = _sWorking_folder + "\\Data\\Model\\model.weights";
                _detector = new YoloGPU(_detection_modelConfiguration, _detection_modelWeights, _nDetectWidth, _nDetectHeight, 0.7);
            }
            catch(Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        private void btSaveJob_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Config Files (*.config)|*.config";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(dlg.FileName)) return;
                SaveJob(dlg.FileName);
            }
        }
        private void SaveJob(string sPath)
        {
            try
            {
                JobConfig.OpenJob(sPath);
                JobConfig.SetNumClass(_nClass.ToString());
                string[] sClassName = GetClassNameAll();
                for (int i = 0; i < _nClass; i++)
                {
                   JobConfig.SetClassName(i, sClassName[i]);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }
        private void OpenVideo()
        {
            _checkingScrew = new List<checkingScrewNum>();
            _Detectbox = new List<bbox_t>();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MP4 Files (*.mp4)|*.mp4|AVI Files (*.avi)|*.avi";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string sVideo = dlg.FileName;
                lbTime.Text = sVideo;
                if (!File.Exists(sVideo)) return;
                m_nFramCount = 0;
                trackPlaying.Value = m_nFramCount;
                _videocapture = new VideoCapture();
                _videocapture.Open(sVideo);
                bool b = _videocapture.IsOpened();
                m_nFrameTotal = _videocapture.FrameCount;
                trackPlaying.Maximum = m_nFrameTotal;
                _bCaptureVideo = false;
            }
        }
        private void btOpenVideo_Click(object sender, EventArgs e)
        {
            OpenVideo();
        }

        private void btPlayvideo_Click(object sender, EventArgs e)
        {
            try
            {
                timerVideo.Interval = Convert.ToInt32(tbTimeFrame.Text);
                timerVideo.Start();
                _bCaptureVideo = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Button Play video");
            }
        }
        private void btStopVideo_Click(object sender, EventArgs e)
        {
            timerVideo.Stop();
            _bCaptureVideo = false;
            lbStatus.Text = "Dừng video";
        }
        private void timerVideo_Tick(object sender, EventArgs e)
        {
            if (_bCaptureVideo)
            {
                if (_img == null) _img = new Mat();
                if (_videocapture == null) return;
                if (_img != null)
                {
                     _videocapture.Read(_img); // same as cvQueryFrame
                    if (_img.Empty())
                    { if(_preImage != null) _img = _preImage.Clone(); lbStatus.Text = "Hết Video"; return; }
                    else
                    {
                        _preImage = _img.Clone();
                        _bmp = BitmapConverter.ToBitmap(_img);
                        pbMainTrain.Image = _bmp;
                        m_nFramCount = _videocapture.PosFrames;
                        lbStatus.Text = "Frame :"+ m_nFramCount.ToString()+"/"+ _videocapture.FrameCount.ToString();
                        if (m_nFramCount <= trackPlaying.Maximum)
                            trackPlaying.Value = m_nFramCount;
                        if (_bAutoVideo)
                        {
                            AutoAddFrameTraining();
                        }
                        else
                        {
                            Detect();
                        }
                    }
                }
                //lbStatus.Text = "Đang chạy Video";
            }
        }
        private void btFramePrev_Click(object sender, EventArgs e)
        {
            m_nFramCount--;
            _videocapture.Set(VideoCaptureProperties.PosFrames, m_nFramCount - 1);
            _videocapture.Read(_img);
            if (_img.Width > 0)
            {
                pbMainTrain.Image = BitmapConverter.ToBitmap(_img);
                Detect();
            }
            lbStatus.Text = "Frame :" + m_nFramCount.ToString() + "/" + _videocapture.FrameCount.ToString();
        }

        private void btAutoVideo_Click(object sender, EventArgs e)
        {
            if(!_bAutoVideo)
            {
                _bAutoVideo = true;
                _bCaptureVideo = true;
                timerVideo.Start();
            }
            else
            {
                _bAutoVideo = false;
                timerVideo.Stop();
            }
           
        }

        private void btAutoLabel_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_filename)) return;
            string sLabel = FileLocation.GetLocationLabel(_filename);
            File.Create(sLabel).Close();
            SaveAutoLabel();
            LoadLabel();
            pbMainTrain.Invalidate();
        }
        void SaveListObjectToFile()
        {
            if (String.IsNullOrEmpty(_filename)) return;
            string sLabel = FileLocation.GetLocationLabel(_filename);
            File.Create(sLabel).Close();
            try
            {

                System.Text.Encoding charset = System.Text.Encoding.ASCII;
                FileStream fileStream;
                if (!File.Exists(sLabel))
                {
                    fileStream = File.Create(sLabel);
                    fileStream.Close();
                }
                fileStream = File.Open(sLabel, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter oReader = new StreamWriter(fileStream, charset);
                for (int i=0; i< dgvListObject.Rows.Count; i++)
                {
                    string sClass = dgvListObject.Rows[i].Cells[2].Value.ToString();
                    string sX = dgvListObject.Rows[i].Cells[3].Value.ToString();
                    string sY = dgvListObject.Rows[i].Cells[4].Value.ToString();
                    string sW = dgvListObject.Rows[i].Cells[5].Value.ToString();
                    string sH = dgvListObject.Rows[i].Cells[6].Value.ToString();
                    string sFileContent = sClass + " " + sX + " "+ sY + " " +sW + " " + sH;
                    // Append text in file when file exitsed
                    oReader.WriteLine(sFileContent);
                }
                oReader.Close();
                fileStream.Close();
                lbStatus.Text = ("Save label file to: " + sLabel);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
                lbStatus.Text = ("Save label file fail: " + sLabel);
            }
        }
        private void btDelSelectBound_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListObject == null) return;
                if (dgvListObject.Rows.Count == 0) return;
                if (dgvListObject.Rows.Count < _nSelecteTeachBox) return;
                dgvListObject.Rows.RemoveAt(_nSelecteTeachBox);
                SaveListObjectToFile();
                LoadLabel();
            }
            catch(Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }

        private void btDeleteTrain_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListImage == null) return;
                if (dgvListImage.Rows.Count == 0) return;
                if (dgvListImage.Rows.Count < _nSelectedFile) return;
                if(File.Exists(_filename))File.Delete(_filename);
                string sLabel = FileLocation.GetLocationLabel(_filename);
                if (File.Exists(sLabel)) File.Delete(sLabel);
                dgvListImage.Rows.RemoveAt(_nSelectedFile);
                SaveListImageToFile();
                LoadTrainListFile();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
        }

        private void btListAllTrainfile_Click(object sender, EventArgs e)
        {
            string sImageSaveTo = JobConfig.GetTrainFolder();
            try
            {
                Random rnd = new Random();
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "image files |*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                dialog.Title = "Please select file .";
                dialog.InitialDirectory = FileLocation.GetLocationImage(_sWorking_folder);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string sFolder = Path.GetDirectoryName(dialog.FileName);
                    DirectoryInfo d = new DirectoryInfo(sFolder);
                    FileInfo[] Files = d.GetFiles("*.JPG"); //Getting jpg files
                    string str = "";
                    foreach (FileInfo file in Files)
                    {
                        _filename = file.FullName;
                        try
                        {
                            _img = new Mat(_filename);
                            if (_img.Width > _nTrainWidth)
                            {
                                Mat imgsave = DataAgument.GetRandomCrop(_detector, _filename);
                                _filename = GenFilesave(sImageSaveTo);
                                imgsave.SaveImage(_filename);
                                TrainListWriter.WriterTrainList(_sWorking_folder, _filename);
                                dgvListImage.Rows.Add(_nFileTrain.ToString(), _filename);
                                _nFileTrain++;
                                _bmp = imgsave.ToBitmap();
                                pbMainTrain.Image = _bmp;
                                SaveAutoLabel();
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteError(ex);
                            MessageBox.Show("Error Saving image: " + ex.Message, "AI Training Tool");
                        }
                    }
                    FileInfo[] Files2 = d.GetFiles("*.bmp"); //Getting bmp files
                    foreach (FileInfo file in Files2)
                    {
                        _filename = file.FullName;
                        try
                        {
                            _img = new Mat(_filename);
                            if (_img.Width > _nTrainWidth)
                            {
                                Mat imgsave = DataAgument.GetRandomCrop(_detector, _filename);
                                _filename = GenFilesave(sImageSaveTo);
                                imgsave.SaveImage(_filename);
                                TrainListWriter.WriterTrainList(_sWorking_folder, _filename);
                                dgvListImage.Rows.Add(_nFileTrain.ToString(), _filename);
                                _nFileTrain++;
                                _bmp = imgsave.ToBitmap();
                                pbMainTrain.Image = _bmp;
                                SaveAutoLabel();
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteError(ex);
                            MessageBox.Show("Error Saving image: " + ex.Message, "AI Training Tool");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex);
                lbStatus.Text = ("Save list image fail: " + sImageSaveTo);
            }
        }

        private void btOpenVideo2_Click(object sender, EventArgs e)
        {
            OpenVideo();
        }

        private void btPlayVideo2_Click(object sender, EventArgs e)
        {
            try
            {
                timerVideo.Interval = Convert.ToInt32(tbTimeFrame.Text);
                timerVideo.Start();
                _bCaptureVideo = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Button Play video");
            }
        }

        private void btStopVideo2_Click(object sender, EventArgs e)
        {
            timerVideo.Stop();
            _bCaptureVideo = false;
            lbStatus.Text = "Dừng video";
        }
       
        private void btAddAll_Click(object sender, EventArgs e)
        {
            string sImageSaveTo = JobConfig.GetTrainFolder();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "image files |*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            dialog.Title = "Please select file .";
            dialog.InitialDirectory = FileLocation.GetLocationImage(_sWorking_folder);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string sFolder = Path.GetDirectoryName(dialog.FileName);
                DirectoryInfo d = new DirectoryInfo(sFolder);
                string[] extensions = new string[3] { "jpg", "png", "bmp"};
                var Files = FileCommon.GetFilesByExtensions(d, extensions);
                string str = "";
                foreach (FileInfo file in Files)
                {
                    _filename = file.FullName;
                    try
                    {
                        _img = new Mat(_filename);
                        _img = new Mat(_img, new Rect(_pTLROI.X, _pTLROI.Y, _pBRROI.X - _pTLROI.X, _pBRROI.Y - _pTLROI.Y));
                        int nWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                        int nHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
                        if (_img.Width != nWidth || _img.Height != nHeight)
                        { _img = _img.Resize(new OpenCvSharp.Size(nWidth, nHeight)); }
                        string sFileSave = GenFilesave(sImageSaveTo);
                        _img.SaveImage(sFileSave);
                        TrainListWriter.WriterTrainList(_sWorking_folder, sFileSave);
                        dgvListImage.Rows.Add(_nFileTrain.ToString(), sFileSave);
                        _nFileTrain++;
                        _bmp = _img.ToBitmap();
                        pbMainTrain.Image = _bmp;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving image: " + ex.Message, "AI Training Tool");
                    }
                }
               
            }
        }

        private void frmTrainTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

       
        private void btFrameNext_Click(object sender, EventArgs e)
        {
            m_nFramCount++;
            _videocapture.Set(VideoCaptureProperties.PosFrames, m_nFramCount - 1);
            _videocapture.Read(_img);
            if (_img.Width > 0)
            {
                pbMainTrain.Image = BitmapConverter.ToBitmap(_img);
                Detect();
            }
            lbStatus.Text = "Frame :" + m_nFramCount.ToString() + "/" + _videocapture.FrameCount.ToString();
        }

      

        private void trackPlaying_Scroll(object sender, EventArgs e)
        {
            timerVideo.Stop();
            _bCaptureVideo = false;
            m_nFramCount = trackPlaying.Value;
            if (_videocapture == null) return;
            _videocapture.Set(VideoCaptureProperties.PosFrames, m_nFramCount - 1);
            _videocapture.Read(_img);
            if (_img.Width > 0)
            {
                _bmp = _img.ToBitmap();
                pbMainTrain.Image = _bmp;
                pbMainTrain.Invalidate();
            }
            lbStatus.Text = "Frame :" + m_nFramCount.ToString() + "/" + _videocapture.FrameCount.ToString();
        }

        private void btAddExist_Click(object sender, EventArgs e)
        {
            string sImageSaveTo = _sWorking_folder + "\\Data\\ImageSets";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "image files |*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            dialog.Title = "Please select file .";
            dialog.InitialDirectory = FileLocation.GetLocationImage(_sWorking_folder);
            string strFileName = JobConfig.GetListTrainFile();
            File.Create(strFileName).Close();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string sFolder = Path.GetDirectoryName(dialog.FileName);
                DirectoryInfo d = new DirectoryInfo(sFolder);
                string[] extensions = new string[3] { "jpg", "png", "bmp" };
                var Files = FileCommon.GetFilesByExtensions(d, extensions);
                string str = "";
                foreach (FileInfo file in Files)
                {
                    _filename = file.FullName;
                    try
                    {
                        _img = new Mat(_filename);
                        _img = new Mat(_img, new Rect(_pTLROI.X, _pTLROI.Y, _pBRROI.X - _pTLROI.X, _pBRROI.Y - _pTLROI.Y));
                        int nWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                        int nHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
                        if (_img.Width != nWidth || _img.Height != nHeight)
                        { _img = _img.Resize(new OpenCvSharp.Size(nWidth, nHeight)); }
                        TrainListWriter.WriterTrainList(_sWorking_folder, _filename);
                        _nFileTrain++;
                        _bmp = _img.ToBitmap();
                        _img.SaveImage(_filename);
                        pbMainTrain.Image = _bmp;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving image: " + ex.Message, "AI Training Tool");
                    }
                }

            }
            LoadTrainListFile(strFileName);
        }

        private void btOpenDetect_Click(object sender, EventArgs e)
        {
            OpenImage();
            Detect();
        }
        Mat SharpenImage(Mat img)
        {
            Mat Res = new Mat();
            Mat gaus = new Mat();
            Cv2.GaussianBlur(img , gaus, new OpenCvSharp.Size(0,0), 3);
            Cv2.AddWeighted(img, 1.5, gaus, -0.5, 0, Res);
            return Res;
        }
        private void btSharpenImage_Click(object sender, EventArgs e)
        {
            if (_img == null) return;
            _img = SharpenImage(_img);
            _bmp = _img.ToBitmap();
            pbMainTrain.Image = _bmp;
            pbMainTrain.Invalidate();
        }

        private void dgvImport_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvImport.SelectedRows != null)
            {
                if (dgvImport.SelectedRows.Count == 0) return;
                if (dgvImport.SelectedRows[0].Cells["Image"].Value == null) return;
                string sPath = dgvImport.SelectedRows[0].Cells["Image"].Value.ToString();
                _filename = sPath;
                if (File.Exists(sPath))
                {
                    _Teachbox = new List<bbox_t>();
                    _Detectbox = new List<bbox_t>();
                    dgvDetecResult.Rows.Clear();
                    _img = new Mat(sPath);
                    _bmp = _img.ToBitmap();
                    pbMainTrain.Image = _bmp;
                    _Detectbox = new List<bbox_t>();
                    _Teachbox = new List<bbox_t>();
                }
                else
                {
                    MessageBox.Show("File: " + sPath + " not exist");
                }
            }
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            string sImageSaveTo = JobConfig.GetTrainFolder();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "image files |*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            dialog.Title = "Please select file .";
            dialog.InitialDirectory = FileLocation.GetLocationImage(_sWorking_folder);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string sFolder = Path.GetDirectoryName(dialog.FileName);
                DirectoryInfo d = new DirectoryInfo(sFolder);
                string[] extensions = new string[3] { "jpg", "png", "bmp" };
                var Files = FileCommon.GetFilesByExtensions(d, extensions);
                string str = "";
                foreach (FileInfo file in Files)
                {
                    _filename = file.FullName;
                    try
                    {
                        _img = new Mat(_filename);
                        int BeginWidth =_img.Width;
                        int BeginHeight = _img.Height;
                        _img = new Mat(_img, new Rect(_pTLROI.X, _pTLROI.Y, _pBRROI.X - _pTLROI.X, _pBRROI.Y - _pTLROI.Y));
                        int nWidth = Convert.ToInt16(JobConfig.GetTrainWidth());
                        int nHeight = Convert.ToInt16(JobConfig.GetTrainHeight());
                        if (_img.Width != nWidth || _img.Height != nHeight)
                        { 
                            Cv2.Resize( _img,_img,new OpenCvSharp.Size(nWidth, nHeight)); 
                        }
                        string sFileSave = GenFilesave(sImageSaveTo);
                        _img.SaveImage(sFileSave);
                        TrainListWriter.WriterTrainList(_sWorking_folder, sFileSave);
                        string sPathLabelNew =FileLocation.GetLocationLabel(sFileSave); ;
                        dgvListImage.Rows.Add(_nFileTrain.ToString(), sFileSave);
                        _nFileTrain++;
                        _bmp = _img.ToBitmap();
                        System.Text.Encoding charset = System.Text.Encoding.ASCII;
                        string LabelPatOrg = _filename.Replace("images", "labels").Replace("png", "txt");
                        StreamReader reader = new StreamReader(LabelPatOrg, charset);
                        try
                        {
                            int nClass;
                            double dCx, dCy, dWx, dWy;
                            String[] sClass = new String[6];
                            sClass = GetClassNameAll();
                            String sLine = "Init";
                            int nCount = 0;
                            _Teachbox = new List<bbox_t>();
                            while (!String.IsNullOrEmpty(sLine))
                            {
                                sLine = reader.ReadLine();
                                if ((!String.IsNullOrEmpty(sLine)))
                                {
                                    String[] sSplit = sLine.Split(' ');
                                    nClass = Convert.ToInt16(sSplit[0]);
                                    dCx = Convert.ToDouble(sSplit[1]);
                                    dCy = Convert.ToDouble(sSplit[2]);
                                    dWx = Convert.ToDouble(sSplit[3]);
                                    dWy = Convert.ToDouble(sSplit[4]);
                                    dgvListObject.Rows.Add(nCount.ToString(), sClass[nClass], nClass.ToString(),
                                        dCx.ToString(c_sDecimal), dCy.ToString(c_sDecimal), dWx.ToString(c_sDecimal),
                                        dWy.ToString(c_sDecimal));
                                    dgvListObject.Rows[nCount].DefaultCellStyle.BackColor = _colorClass[nClass];
                                    nCount++;
                                    bbox_t bbox = new bbox_t();
                                    _pTL.X = (int)(dCx*BeginWidth - dWx* BeginWidth / 2 ) - _pTLROI.X;
                                    _pTL.Y = (int)(dCy * BeginHeight - dWy * BeginHeight / 2)- _pTLROI.Y;
                                    _pBR.X = (int)(dCx * BeginWidth + dWx * BeginWidth / 2);
                                    _pBR.Y = (int)(dCy * BeginHeight + dWy * BeginHeight / 2);
                                    
                                    string sDecimal = "F8";
                                    double dCx2 = (double)((_pTL.X + _pBR.X) / 2) / (double)_img.Width;
                                    double dCy2 = (double)((_pTL.Y + _pBR.Y) / 2) / (double)_img.Height;
                                    double dWx2= (double)(_pBR.X - _pTL.X) / (double)_img.Width;
                                    double dWy3 = (double)(_pBR.Y - _pTL.Y) / (double)_img.Height;
                                    string sFileContent = nClass.ToString() + " " + dCx.ToString(sDecimal) + " "
                                        + dCy.ToString(sDecimal) + " " +
                                        dWx.ToString(sDecimal) + " " + dWy.ToString(sDecimal);
                                    try
                                    {
                                        if (!File.Exists(sPathLabelNew))
                                        {
                                            FileStream fileStream = File.Create(sPathLabelNew);
                                            fileStream.Close();
                                            StreamWriter oReader = new StreamWriter(sPathLabelNew, true, charset);
                                            oReader.WriteLine(sFileContent);
                                            oReader.Close();
                                        }
                                        else
                                        {
                                            // Append text in file when file exitsed
                                            FileStream fileStream = File.Open(sPathLabelNew, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                                            StreamWriter oReader = new StreamWriter(fileStream, charset);
                                            oReader.WriteLine(sFileContent);
                                            oReader.Close();
                                            fileStream.Close();
                                        }
                                        lbStatus.Text = ("Save label file to: " + sPathLabelNew);
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorLog.WriteError(ex);
                                        lbStatus.Text = ("Save label file fail: " + sPathLabelNew);
                                    }
                                }
                            }
                        }
                        catch(Exception ex3)
                        {

                        }
                        pbMainTrain.Image = _bmp;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving image: " + ex.Message, "AI Training Tool");
                    }
                }

            }
        }
        #region plate 
        private void btOpenImageRecog_Click(object sender, EventArgs e)
        {

        }
        private void btReadPlate_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
