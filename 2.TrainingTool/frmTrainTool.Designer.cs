namespace TrainingTool
{
    partial class frmTrainTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrainTool));
            this.dgvListImage = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbWorkingDir = new System.Windows.Forms.TextBox();
            this.timerWebcam = new System.Windows.Forms.Timer(this.components);
            this.lbTime = new System.Windows.Forms.Label();
            this.timerOutput = new System.Windows.Forms.Timer(this.components);
            this.btNewJob = new System.Windows.Forms.Button();
            this.btOpenJob = new System.Windows.Forms.Button();
            this.btSetting = new System.Windows.Forms.Button();
            this.btSaveJob = new System.Windows.Forms.Button();
            this.trackPlaying = new System.Windows.Forms.TrackBar();
            this.timerVideo = new System.Windows.Forms.Timer(this.components);
            this.lbStatus = new System.Windows.Forms.Label();
            this.btFramePrev = new System.Windows.Forms.Button();
            this.btFrameNext = new System.Windows.Forms.Button();
            this.tbTimeFrame = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabLabeling = new System.Windows.Forms.TabPage();
            this.cbShowName = new System.Windows.Forms.CheckBox();
            this.btDelSelectBound = new System.Windows.Forms.Button();
            this.gbObjectClass = new System.Windows.Forms.GroupBox();
            this.btSaveLabel = new System.Windows.Forms.Button();
            this.btDeleteAllBounding = new System.Windows.Forms.Button();
            this.btAutoLabel = new System.Windows.Forms.Button();
            this.dgvListObject = new System.Windows.Forms.DataGridView();
            this.tabPageDetection = new System.Windows.Forms.TabPage();
            this.btOpenDetect = new System.Windows.Forms.Button();
            this.cbShowChecking = new System.Windows.Forms.CheckBox();
            this.cbShowAll = new System.Windows.Forms.CheckBox();
            this.dgvDetecResult = new System.Windows.Forms.DataGridView();
            this.btDetect = new System.Windows.Forms.Button();
            this.tabPageTraining = new System.Windows.Forms.TabPage();
            this.listBoxOut = new System.Windows.Forms.ListBox();
            this.btStopTraining = new System.Windows.Forms.Button();
            this.btDelteAll = new System.Windows.Forms.Button();
            this.lbSelectedModel = new System.Windows.Forms.Label();
            this.btUseThisModel = new System.Windows.Forms.Button();
            this.btStartTrain = new System.Windows.Forms.Button();
            this.tabPageDataAgument = new System.Windows.Forms.TabPage();
            this.btSharpenImage = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbCropShift = new System.Windows.Forms.TextBox();
            this.cbCropShift = new System.Windows.Forms.CheckBox();
            this.cbResize = new System.Windows.Forms.CheckBox();
            this.cbResizeCrop = new System.Windows.Forms.CheckBox();
            this.tabExtractData = new System.Windows.Forms.TabPage();
            this.btAddExist = new System.Windows.Forms.Button();
            this.btAddAll = new System.Windows.Forms.Button();
            this.btListAllTrainfile = new System.Windows.Forms.Button();
            this.btDeleteTrain = new System.Windows.Forms.Button();
            this.btAutoVideo = new System.Windows.Forms.Button();
            this.btCropImage = new System.Windows.Forms.Button();
            this.btAdjustROI = new System.Windows.Forms.Button();
            this.btStopVideo = new System.Windows.Forms.Button();
            this.btAddTrain = new System.Windows.Forms.Button();
            this.btCapture = new System.Windows.Forms.Button();
            this.btOpenImage = new System.Windows.Forms.Button();
            this.btPlayvideo = new System.Windows.Forms.Button();
            this.btStartPreview = new System.Windows.Forms.Button();
            this.btOpenVideo = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pbMainTrain = new System.Windows.Forms.PictureBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvImport = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPlaying)).BeginInit();
            this.tabLabeling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListObject)).BeginInit();
            this.tabPageDetection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetecResult)).BeginInit();
            this.tabPageTraining.SuspendLayout();
            this.tabPageDataAgument.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabExtractData.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainTrain)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImport)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvListImage
            // 
            this.dgvListImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListImage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListImage.Location = new System.Drawing.Point(3, 3);
            this.dgvListImage.MultiSelect = false;
            this.dgvListImage.Name = "dgvListImage";
            this.dgvListImage.ReadOnly = true;
            this.dgvListImage.RowHeadersVisible = false;
            this.dgvListImage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListImage.Size = new System.Drawing.Size(460, 198);
            this.dgvListImage.TabIndex = 1;
            this.dgvListImage.SelectionChanged += new System.EventHandler(this.dgvListImage_SelectionChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(854, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Working Directory";
            // 
            // tbWorkingDir
            // 
            this.tbWorkingDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWorkingDir.Location = new System.Drawing.Point(857, 25);
            this.tbWorkingDir.Name = "tbWorkingDir";
            this.tbWorkingDir.ReadOnly = true;
            this.tbWorkingDir.Size = new System.Drawing.Size(380, 20);
            this.tbWorkingDir.TabIndex = 3;
            // 
            // timerWebcam
            // 
            this.timerWebcam.Tick += new System.EventHandler(this.timerWebcam_Tick);
            // 
            // lbTime
            // 
            this.lbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.Location = new System.Drawing.Point(851, 680);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(45, 18);
            this.lbTime.TabIndex = 13;
            this.lbTime.Text = "Time:";
            // 
            // timerOutput
            // 
            this.timerOutput.Interval = 1000;
            this.timerOutput.Tick += new System.EventHandler(this.timerOutput_Tick);
            // 
            // btNewJob
            // 
            this.btNewJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btNewJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btNewJob.Location = new System.Drawing.Point(857, 51);
            this.btNewJob.Name = "btNewJob";
            this.btNewJob.Size = new System.Drawing.Size(118, 31);
            this.btNewJob.TabIndex = 28;
            this.btNewJob.Text = "New Job";
            this.btNewJob.UseVisualStyleBackColor = true;
            this.btNewJob.Click += new System.EventHandler(this.btNewJob_Click);
            // 
            // btOpenJob
            // 
            this.btOpenJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOpenJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOpenJob.Location = new System.Drawing.Point(987, 51);
            this.btOpenJob.Name = "btOpenJob";
            this.btOpenJob.Size = new System.Drawing.Size(118, 31);
            this.btOpenJob.TabIndex = 29;
            this.btOpenJob.Text = "Open Job";
            this.btOpenJob.UseVisualStyleBackColor = true;
            this.btOpenJob.Click += new System.EventHandler(this.btOpenJob_Click);
            // 
            // btSetting
            // 
            this.btSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSetting.Location = new System.Drawing.Point(1243, 9);
            this.btSetting.Name = "btSetting";
            this.btSetting.Size = new System.Drawing.Size(95, 73);
            this.btSetting.TabIndex = 31;
            this.btSetting.Text = "Setting";
            this.btSetting.UseVisualStyleBackColor = true;
            this.btSetting.Click += new System.EventHandler(this.btSetting_Click);
            // 
            // btSaveJob
            // 
            this.btSaveJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSaveJob.Location = new System.Drawing.Point(1117, 51);
            this.btSaveJob.Name = "btSaveJob";
            this.btSaveJob.Size = new System.Drawing.Size(118, 31);
            this.btSaveJob.TabIndex = 32;
            this.btSaveJob.Text = "Save Job";
            this.btSaveJob.UseVisualStyleBackColor = true;
            this.btSaveJob.Click += new System.EventHandler(this.btSaveJob_Click);
            // 
            // trackPlaying
            // 
            this.trackPlaying.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackPlaying.Location = new System.Drawing.Point(33, 626);
            this.trackPlaying.Maximum = 100;
            this.trackPlaying.Name = "trackPlaying";
            this.trackPlaying.Size = new System.Drawing.Size(782, 45);
            this.trackPlaying.TabIndex = 35;
            this.trackPlaying.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackPlaying.Scroll += new System.EventHandler(this.trackPlaying_Scroll);
            // 
            // timerVideo
            // 
            this.timerVideo.Enabled = true;
            this.timerVideo.Interval = 50;
            this.timerVideo.Tick += new System.EventHandler(this.timerVideo_Tick);
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(6, 679);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(95, 18);
            this.lbStatus.TabIndex = 37;
            this.lbStatus.Text = "Video Status:";
            // 
            // btFramePrev
            // 
            this.btFramePrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btFramePrev.Location = new System.Drawing.Point(2, 626);
            this.btFramePrev.Name = "btFramePrev";
            this.btFramePrev.Size = new System.Drawing.Size(32, 32);
            this.btFramePrev.TabIndex = 40;
            this.btFramePrev.Text = "-";
            this.btFramePrev.UseVisualStyleBackColor = true;
            this.btFramePrev.Click += new System.EventHandler(this.btFramePrev_Click);
            // 
            // btFrameNext
            // 
            this.btFrameNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btFrameNext.Location = new System.Drawing.Point(814, 626);
            this.btFrameNext.Name = "btFrameNext";
            this.btFrameNext.Size = new System.Drawing.Size(32, 32);
            this.btFrameNext.TabIndex = 41;
            this.btFrameNext.Text = "+";
            this.btFrameNext.UseVisualStyleBackColor = true;
            this.btFrameNext.Click += new System.EventHandler(this.btFrameNext_Click);
            // 
            // tbTimeFrame
            // 
            this.tbTimeFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTimeFrame.Location = new System.Drawing.Point(734, 677);
            this.tbTimeFrame.Name = "tbTimeFrame";
            this.tbTimeFrame.Size = new System.Drawing.Size(67, 20);
            this.tbTimeFrame.TabIndex = 42;
            this.tbTimeFrame.Text = "100";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(804, 678);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 18);
            this.label4.TabIndex = 43;
            this.label4.Text = "ms";
            // 
            // tabLabeling
            // 
            this.tabLabeling.Controls.Add(this.cbShowName);
            this.tabLabeling.Controls.Add(this.btDelSelectBound);
            this.tabLabeling.Controls.Add(this.gbObjectClass);
            this.tabLabeling.Controls.Add(this.btSaveLabel);
            this.tabLabeling.Controls.Add(this.btDeleteAllBounding);
            this.tabLabeling.Controls.Add(this.btAutoLabel);
            this.tabLabeling.Controls.Add(this.dgvListObject);
            this.tabLabeling.Location = new System.Drawing.Point(4, 22);
            this.tabLabeling.Name = "tabLabeling";
            this.tabLabeling.Size = new System.Drawing.Size(478, 325);
            this.tabLabeling.TabIndex = 4;
            this.tabLabeling.Text = "Gắn Nhãn";
            this.tabLabeling.UseVisualStyleBackColor = true;
            // 
            // cbShowName
            // 
            this.cbShowName.AutoSize = true;
            this.cbShowName.Location = new System.Drawing.Point(149, 196);
            this.cbShowName.Name = "cbShowName";
            this.cbShowName.Size = new System.Drawing.Size(84, 17);
            this.cbShowName.TabIndex = 38;
            this.cbShowName.Text = "Show Name";
            this.cbShowName.UseVisualStyleBackColor = true;
            // 
            // btDelSelectBound
            // 
            this.btDelSelectBound.Location = new System.Drawing.Point(261, 196);
            this.btDelSelectBound.Name = "btDelSelectBound";
            this.btDelSelectBound.Size = new System.Drawing.Size(100, 46);
            this.btDelSelectBound.TabIndex = 37;
            this.btDelSelectBound.Text = "Delete Select Box";
            this.btDelSelectBound.UseVisualStyleBackColor = true;
            this.btDelSelectBound.Click += new System.EventHandler(this.btDelSelectBound_Click);
            // 
            // gbObjectClass
            // 
            this.gbObjectClass.Location = new System.Drawing.Point(3, 6);
            this.gbObjectClass.Name = "gbObjectClass";
            this.gbObjectClass.Size = new System.Drawing.Size(138, 314);
            this.gbObjectClass.TabIndex = 30;
            this.gbObjectClass.TabStop = false;
            this.gbObjectClass.Text = "Object Class";
            // 
            // btSaveLabel
            // 
            this.btSaveLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveLabel.Location = new System.Drawing.Point(363, 247);
            this.btSaveLabel.Name = "btSaveLabel";
            this.btSaveLabel.Size = new System.Drawing.Size(100, 46);
            this.btSaveLabel.TabIndex = 4;
            this.btSaveLabel.Text = "Save Bounding Box";
            this.btSaveLabel.UseVisualStyleBackColor = true;
            this.btSaveLabel.Click += new System.EventHandler(this.btSaveLabel_Click);
            // 
            // btDeleteAllBounding
            // 
            this.btDeleteAllBounding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteAllBounding.Location = new System.Drawing.Point(363, 196);
            this.btDeleteAllBounding.Name = "btDeleteAllBounding";
            this.btDeleteAllBounding.Size = new System.Drawing.Size(100, 46);
            this.btDeleteAllBounding.TabIndex = 32;
            this.btDeleteAllBounding.Text = "Delete All Bounding Box";
            this.btDeleteAllBounding.UseVisualStyleBackColor = true;
            this.btDeleteAllBounding.Click += new System.EventHandler(this.btDeleteAllBounding_Click);
            // 
            // btAutoLabel
            // 
            this.btAutoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAutoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAutoLabel.Location = new System.Drawing.Point(261, 248);
            this.btAutoLabel.Name = "btAutoLabel";
            this.btAutoLabel.Size = new System.Drawing.Size(100, 46);
            this.btAutoLabel.TabIndex = 36;
            this.btAutoLabel.Text = "Auto Draw Bound";
            this.btAutoLabel.UseVisualStyleBackColor = true;
            this.btAutoLabel.Click += new System.EventHandler(this.btAutoLabel_Click);
            // 
            // dgvListObject
            // 
            this.dgvListObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListObject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListObject.Location = new System.Drawing.Point(149, 10);
            this.dgvListObject.MultiSelect = false;
            this.dgvListObject.Name = "dgvListObject";
            this.dgvListObject.ReadOnly = true;
            this.dgvListObject.RowHeadersVisible = false;
            this.dgvListObject.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListObject.Size = new System.Drawing.Size(314, 172);
            this.dgvListObject.TabIndex = 33;
            this.dgvListObject.SelectionChanged += new System.EventHandler(this.dgvListObject_SelectionChanged);
            // 
            // tabPageDetection
            // 
            this.tabPageDetection.BackColor = System.Drawing.Color.CornflowerBlue;
            this.tabPageDetection.Controls.Add(this.btOpenDetect);
            this.tabPageDetection.Controls.Add(this.cbShowChecking);
            this.tabPageDetection.Controls.Add(this.cbShowAll);
            this.tabPageDetection.Controls.Add(this.dgvDetecResult);
            this.tabPageDetection.Controls.Add(this.btDetect);
            this.tabPageDetection.Location = new System.Drawing.Point(4, 22);
            this.tabPageDetection.Name = "tabPageDetection";
            this.tabPageDetection.Size = new System.Drawing.Size(478, 325);
            this.tabPageDetection.TabIndex = 2;
            this.tabPageDetection.Text = "Detection";
            // 
            // btOpenDetect
            // 
            this.btOpenDetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOpenDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOpenDetect.Location = new System.Drawing.Point(151, 261);
            this.btOpenDetect.Name = "btOpenDetect";
            this.btOpenDetect.Size = new System.Drawing.Size(159, 53);
            this.btOpenDetect.TabIndex = 42;
            this.btOpenDetect.Text = "Open";
            this.btOpenDetect.UseVisualStyleBackColor = true;
            this.btOpenDetect.Click += new System.EventHandler(this.btOpenDetect_Click);
            // 
            // cbShowChecking
            // 
            this.cbShowChecking.AutoSize = true;
            this.cbShowChecking.Location = new System.Drawing.Point(3, 276);
            this.cbShowChecking.Name = "cbShowChecking";
            this.cbShowChecking.Size = new System.Drawing.Size(123, 17);
            this.cbShowChecking.TabIndex = 41;
            this.cbShowChecking.Text = "Show only Checking";
            this.cbShowChecking.UseVisualStyleBackColor = true;
            // 
            // cbShowAll
            // 
            this.cbShowAll.AutoSize = true;
            this.cbShowAll.Location = new System.Drawing.Point(3, 299);
            this.cbShowAll.Name = "cbShowAll";
            this.cbShowAll.Size = new System.Drawing.Size(67, 17);
            this.cbShowAll.TabIndex = 40;
            this.cbShowAll.Text = "Show All";
            this.cbShowAll.UseVisualStyleBackColor = true;
            // 
            // dgvDetecResult
            // 
            this.dgvDetecResult.AllowUserToAddRows = false;
            this.dgvDetecResult.AllowUserToResizeColumns = false;
            this.dgvDetecResult.AllowUserToResizeRows = false;
            this.dgvDetecResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetecResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetecResult.Location = new System.Drawing.Point(3, 3);
            this.dgvDetecResult.MultiSelect = false;
            this.dgvDetecResult.Name = "dgvDetecResult";
            this.dgvDetecResult.ReadOnly = true;
            this.dgvDetecResult.RowHeadersVisible = false;
            this.dgvDetecResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetecResult.Size = new System.Drawing.Size(472, 254);
            this.dgvDetecResult.TabIndex = 39;
            this.dgvDetecResult.SelectionChanged += new System.EventHandler(this.dgvDetecResult_SelectionChanged);
            // 
            // btDetect
            // 
            this.btDetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDetect.Location = new System.Drawing.Point(316, 261);
            this.btDetect.Name = "btDetect";
            this.btDetect.Size = new System.Drawing.Size(159, 53);
            this.btDetect.TabIndex = 7;
            this.btDetect.Text = "Detect";
            this.btDetect.UseVisualStyleBackColor = true;
            this.btDetect.Click += new System.EventHandler(this.btDetect_Click);
            // 
            // tabPageTraining
            // 
            this.tabPageTraining.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabPageTraining.Controls.Add(this.listBoxOut);
            this.tabPageTraining.Controls.Add(this.btStopTraining);
            this.tabPageTraining.Controls.Add(this.btDelteAll);
            this.tabPageTraining.Controls.Add(this.lbSelectedModel);
            this.tabPageTraining.Controls.Add(this.btUseThisModel);
            this.tabPageTraining.Controls.Add(this.btStartTrain);
            this.tabPageTraining.Location = new System.Drawing.Point(4, 22);
            this.tabPageTraining.Name = "tabPageTraining";
            this.tabPageTraining.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTraining.Size = new System.Drawing.Size(478, 325);
            this.tabPageTraining.TabIndex = 1;
            this.tabPageTraining.Text = "Training";
            // 
            // listBoxOut
            // 
            this.listBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxOut.FormattingEnabled = true;
            this.listBoxOut.Location = new System.Drawing.Point(6, 23);
            this.listBoxOut.Name = "listBoxOut";
            this.listBoxOut.Size = new System.Drawing.Size(260, 290);
            this.listBoxOut.TabIndex = 20;
            this.listBoxOut.SelectedIndexChanged += new System.EventHandler(this.listBoxOut_SelectedIndexChanged);
            // 
            // btStopTraining
            // 
            this.btStopTraining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btStopTraining.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStopTraining.Location = new System.Drawing.Point(272, 146);
            this.btStopTraining.Name = "btStopTraining";
            this.btStopTraining.Size = new System.Drawing.Size(200, 69);
            this.btStopTraining.TabIndex = 5;
            this.btStopTraining.Text = "Stop Training";
            this.btStopTraining.UseVisualStyleBackColor = true;
            this.btStopTraining.Click += new System.EventHandler(this.btStopTraining_Click);
            // 
            // btDelteAll
            // 
            this.btDelteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelteAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDelteAll.Location = new System.Drawing.Point(272, 7);
            this.btDelteAll.Name = "btDelteAll";
            this.btDelteAll.Size = new System.Drawing.Size(200, 68);
            this.btDelteAll.TabIndex = 3;
            this.btDelteAll.Text = "Delete All Training Data";
            this.btDelteAll.UseVisualStyleBackColor = true;
            this.btDelteAll.Click += new System.EventHandler(this.btDelteAll_Click);
            // 
            // lbSelectedModel
            // 
            this.lbSelectedModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSelectedModel.AutoSize = true;
            this.lbSelectedModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelectedModel.Location = new System.Drawing.Point(6, 7);
            this.lbSelectedModel.Name = "lbSelectedModel";
            this.lbSelectedModel.Size = new System.Drawing.Size(108, 16);
            this.lbSelectedModel.TabIndex = 22;
            this.lbSelectedModel.Text = "List output weight";
            // 
            // btUseThisModel
            // 
            this.btUseThisModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btUseThisModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUseThisModel.Location = new System.Drawing.Point(272, 216);
            this.btUseThisModel.Name = "btUseThisModel";
            this.btUseThisModel.Size = new System.Drawing.Size(200, 69);
            this.btUseThisModel.TabIndex = 6;
            this.btUseThisModel.Text = "Use this model";
            this.btUseThisModel.UseVisualStyleBackColor = true;
            this.btUseThisModel.Click += new System.EventHandler(this.btUseThisModel_Click);
            // 
            // btStartTrain
            // 
            this.btStartTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btStartTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStartTrain.Location = new System.Drawing.Point(272, 76);
            this.btStartTrain.Name = "btStartTrain";
            this.btStartTrain.Size = new System.Drawing.Size(200, 69);
            this.btStartTrain.TabIndex = 4;
            this.btStartTrain.Text = "Start Training";
            this.btStartTrain.UseVisualStyleBackColor = true;
            this.btStartTrain.Click += new System.EventHandler(this.btStartTrain_Click);
            // 
            // tabPageDataAgument
            // 
            this.tabPageDataAgument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabPageDataAgument.Controls.Add(this.btSharpenImage);
            this.tabPageDataAgument.Location = new System.Drawing.Point(4, 22);
            this.tabPageDataAgument.Name = "tabPageDataAgument";
            this.tabPageDataAgument.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDataAgument.Size = new System.Drawing.Size(478, 325);
            this.tabPageDataAgument.TabIndex = 0;
            this.tabPageDataAgument.Text = "Data Agumentation";
            // 
            // btSharpenImage
            // 
            this.btSharpenImage.Location = new System.Drawing.Point(6, 6);
            this.btSharpenImage.Name = "btSharpenImage";
            this.btSharpenImage.Size = new System.Drawing.Size(142, 57);
            this.btSharpenImage.TabIndex = 0;
            this.btSharpenImage.Text = "Sharpen Image";
            this.btSharpenImage.UseVisualStyleBackColor = true;
            this.btSharpenImage.Click += new System.EventHandler(this.btSharpenImage_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbCropShift);
            this.groupBox3.Controls.Add(this.cbCropShift);
            this.groupBox3.Controls.Add(this.cbResize);
            this.groupBox3.Controls.Add(this.cbResizeCrop);
            this.groupBox3.Location = new System.Drawing.Point(6, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(238, 60);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Image Resize mode";
            // 
            // tbCropShift
            // 
            this.tbCropShift.Location = new System.Drawing.Point(173, 16);
            this.tbCropShift.Name = "tbCropShift";
            this.tbCropShift.Size = new System.Drawing.Size(46, 20);
            this.tbCropShift.TabIndex = 30;
            this.tbCropShift.Text = "15";
            // 
            // cbCropShift
            // 
            this.cbCropShift.AutoSize = true;
            this.cbCropShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCropShift.Location = new System.Drawing.Point(120, 19);
            this.cbCropShift.Name = "cbCropShift";
            this.cbCropShift.Size = new System.Drawing.Size(47, 17);
            this.cbCropShift.TabIndex = 29;
            this.cbCropShift.Text = "Shift";
            this.cbCropShift.UseVisualStyleBackColor = true;
            // 
            // cbResize
            // 
            this.cbResize.AutoSize = true;
            this.cbResize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbResize.Location = new System.Drawing.Point(9, 38);
            this.cbResize.Name = "cbResize";
            this.cbResize.Size = new System.Drawing.Size(81, 17);
            this.cbResize.TabIndex = 28;
            this.cbResize.Text = "Resize to fit";
            this.cbResize.UseVisualStyleBackColor = true;
            this.cbResize.CheckedChanged += new System.EventHandler(this.cbResize_CheckedChanged);
            // 
            // cbResizeCrop
            // 
            this.cbResizeCrop.AutoSize = true;
            this.cbResizeCrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbResizeCrop.Location = new System.Drawing.Point(9, 19);
            this.cbResizeCrop.Name = "cbResizeCrop";
            this.cbResizeCrop.Size = new System.Drawing.Size(70, 17);
            this.cbResizeCrop.TabIndex = 27;
            this.cbResizeCrop.Text = "Crop ROI";
            this.cbResizeCrop.UseVisualStyleBackColor = true;
            this.cbResizeCrop.CheckedChanged += new System.EventHandler(this.cbResizeCrop_CheckedChanged);
            // 
            // tabExtractData
            // 
            this.tabExtractData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tabExtractData.Controls.Add(this.btTest);
            this.tabExtractData.Controls.Add(this.btAddExist);
            this.tabExtractData.Controls.Add(this.btAddAll);
            this.tabExtractData.Controls.Add(this.btListAllTrainfile);
            this.tabExtractData.Controls.Add(this.btDeleteTrain);
            this.tabExtractData.Controls.Add(this.btAutoVideo);
            this.tabExtractData.Controls.Add(this.btCropImage);
            this.tabExtractData.Controls.Add(this.groupBox3);
            this.tabExtractData.Controls.Add(this.btAdjustROI);
            this.tabExtractData.Controls.Add(this.btStopVideo);
            this.tabExtractData.Controls.Add(this.btAddTrain);
            this.tabExtractData.Controls.Add(this.btCapture);
            this.tabExtractData.Controls.Add(this.btOpenImage);
            this.tabExtractData.Controls.Add(this.btPlayvideo);
            this.tabExtractData.Controls.Add(this.btStartPreview);
            this.tabExtractData.Controls.Add(this.btOpenVideo);
            this.tabExtractData.Location = new System.Drawing.Point(4, 22);
            this.tabExtractData.Name = "tabExtractData";
            this.tabExtractData.Size = new System.Drawing.Size(478, 325);
            this.tabExtractData.TabIndex = 3;
            this.tabExtractData.Text = "Extract Data";
            // 
            // btAddExist
            // 
            this.btAddExist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddExist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAddExist.Location = new System.Drawing.Point(163, 209);
            this.btAddExist.Name = "btAddExist";
            this.btAddExist.Size = new System.Drawing.Size(153, 56);
            this.btAddExist.TabIndex = 41;
            this.btAddExist.Text = "Thống kê hiện tại";
            this.btAddExist.UseVisualStyleBackColor = true;
            this.btAddExist.Click += new System.EventHandler(this.btAddExist_Click);
            // 
            // btAddAll
            // 
            this.btAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAddAll.Location = new System.Drawing.Point(163, 147);
            this.btAddAll.Name = "btAddAll";
            this.btAddAll.Size = new System.Drawing.Size(153, 56);
            this.btAddAll.TabIndex = 40;
            this.btAddAll.Text = "Add All";
            this.btAddAll.UseVisualStyleBackColor = true;
            this.btAddAll.Click += new System.EventHandler(this.btAddAll_Click);
            // 
            // btListAllTrainfile
            // 
            this.btListAllTrainfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btListAllTrainfile.Location = new System.Drawing.Point(164, 271);
            this.btListAllTrainfile.Name = "btListAllTrainfile";
            this.btListAllTrainfile.Size = new System.Drawing.Size(152, 49);
            this.btListAllTrainfile.TabIndex = 39;
            this.btListAllTrainfile.Text = "Add All Auto";
            this.btListAllTrainfile.UseVisualStyleBackColor = true;
            this.btListAllTrainfile.Click += new System.EventHandler(this.btListAllTrainfile_Click);
            // 
            // btDeleteTrain
            // 
            this.btDeleteTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDeleteTrain.Location = new System.Drawing.Point(322, 147);
            this.btDeleteTrain.Name = "btDeleteTrain";
            this.btDeleteTrain.Size = new System.Drawing.Size(152, 56);
            this.btDeleteTrain.TabIndex = 37;
            this.btDeleteTrain.Text = "Delete from train";
            this.btDeleteTrain.UseVisualStyleBackColor = true;
            this.btDeleteTrain.Click += new System.EventHandler(this.btDeleteTrain_Click);
            // 
            // btAutoVideo
            // 
            this.btAutoVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAutoVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAutoVideo.Location = new System.Drawing.Point(3, 271);
            this.btAutoVideo.Name = "btAutoVideo";
            this.btAutoVideo.Size = new System.Drawing.Size(152, 49);
            this.btAutoVideo.TabIndex = 35;
            this.btAutoVideo.Text = "Auto Extract Image";
            this.btAutoVideo.UseVisualStyleBackColor = true;
            this.btAutoVideo.Click += new System.EventHandler(this.btAutoVideo_Click);
            // 
            // btCropImage
            // 
            this.btCropImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCropImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCropImage.Location = new System.Drawing.Point(3, 209);
            this.btCropImage.Name = "btCropImage";
            this.btCropImage.Size = new System.Drawing.Size(152, 56);
            this.btCropImage.TabIndex = 31;
            this.btCropImage.Text = "Crop Image";
            this.btCropImage.UseVisualStyleBackColor = true;
            this.btCropImage.Click += new System.EventHandler(this.btCropImage_Click);
            // 
            // btAdjustROI
            // 
            this.btAdjustROI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdjustROI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAdjustROI.Location = new System.Drawing.Point(3, 147);
            this.btAdjustROI.Name = "btAdjustROI";
            this.btAdjustROI.Size = new System.Drawing.Size(152, 56);
            this.btAdjustROI.TabIndex = 34;
            this.btAdjustROI.Text = "Adjust ROI";
            this.btAdjustROI.UseVisualStyleBackColor = true;
            this.btAdjustROI.Click += new System.EventHandler(this.btAdjustROI_Click);
            // 
            // btStopVideo
            // 
            this.btStopVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btStopVideo.Location = new System.Drawing.Point(322, 42);
            this.btStopVideo.Name = "btStopVideo";
            this.btStopVideo.Size = new System.Drawing.Size(152, 33);
            this.btStopVideo.TabIndex = 38;
            this.btStopVideo.Text = "Stop Video";
            this.btStopVideo.UseVisualStyleBackColor = true;
            this.btStopVideo.Click += new System.EventHandler(this.btStopVideo_Click);
            // 
            // btAddTrain
            // 
            this.btAddTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAddTrain.Location = new System.Drawing.Point(322, 209);
            this.btAddTrain.Name = "btAddTrain";
            this.btAddTrain.Size = new System.Drawing.Size(153, 56);
            this.btAddTrain.TabIndex = 12;
            this.btAddTrain.Text = "Add to train";
            this.btAddTrain.UseVisualStyleBackColor = true;
            this.btAddTrain.Click += new System.EventHandler(this.btAddTrain_Click);
            // 
            // btCapture
            // 
            this.btCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCapture.Location = new System.Drawing.Point(322, 6);
            this.btCapture.Name = "btCapture";
            this.btCapture.Size = new System.Drawing.Size(152, 33);
            this.btCapture.TabIndex = 11;
            this.btCapture.Text = "Capture image";
            this.btCapture.UseVisualStyleBackColor = true;
            this.btCapture.Click += new System.EventHandler(this.btCapture_Click);
            // 
            // btOpenImage
            // 
            this.btOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOpenImage.Location = new System.Drawing.Point(6, 6);
            this.btOpenImage.Name = "btOpenImage";
            this.btOpenImage.Size = new System.Drawing.Size(152, 33);
            this.btOpenImage.TabIndex = 5;
            this.btOpenImage.Text = "Open Image";
            this.btOpenImage.UseVisualStyleBackColor = true;
            this.btOpenImage.Click += new System.EventHandler(this.btOpenImage_Click);
            // 
            // btPlayvideo
            // 
            this.btPlayvideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPlayvideo.Location = new System.Drawing.Point(164, 42);
            this.btPlayvideo.Name = "btPlayvideo";
            this.btPlayvideo.Size = new System.Drawing.Size(152, 33);
            this.btPlayvideo.TabIndex = 34;
            this.btPlayvideo.Text = "Play Video";
            this.btPlayvideo.UseVisualStyleBackColor = true;
            this.btPlayvideo.Click += new System.EventHandler(this.btPlayvideo_Click);
            // 
            // btStartPreview
            // 
            this.btStartPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btStartPreview.Location = new System.Drawing.Point(164, 6);
            this.btStartPreview.Name = "btStartPreview";
            this.btStartPreview.Size = new System.Drawing.Size(152, 33);
            this.btStartPreview.TabIndex = 10;
            this.btStartPreview.Text = "Start Preview Camera";
            this.btStartPreview.UseVisualStyleBackColor = true;
            this.btStartPreview.Click += new System.EventHandler(this.btStartPreview_Click);
            // 
            // btOpenVideo
            // 
            this.btOpenVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOpenVideo.Location = new System.Drawing.Point(6, 42);
            this.btOpenVideo.Name = "btOpenVideo";
            this.btOpenVideo.Size = new System.Drawing.Size(152, 33);
            this.btOpenVideo.TabIndex = 33;
            this.btOpenVideo.Text = "Open Video";
            this.btOpenVideo.UseVisualStyleBackColor = true;
            this.btOpenVideo.Click += new System.EventHandler(this.btOpenVideo_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabExtractData);
            this.tabControl1.Controls.Add(this.tabPageDataAgument);
            this.tabControl1.Controls.Add(this.tabLabeling);
            this.tabControl1.Controls.Add(this.tabPageTraining);
            this.tabControl1.Controls.Add(this.tabPageDetection);
            this.tabControl1.Location = new System.Drawing.Point(857, 326);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(486, 351);
            this.tabControl1.TabIndex = 30;
            // 
            // pbMainTrain
            // 
            this.pbMainTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMainTrain.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbMainTrain.Location = new System.Drawing.Point(0, 9);
            this.pbMainTrain.Name = "pbMainTrain";
            this.pbMainTrain.Size = new System.Drawing.Size(848, 611);
            this.pbMainTrain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMainTrain.TabIndex = 0;
            this.pbMainTrain.TabStop = false;
            this.pbMainTrain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMainTrain_Paint);
            this.pbMainTrain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMainTrain_MouseDown);
            this.pbMainTrain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMainTrain_MouseMove);
            this.pbMainTrain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMainTrain_MouseUp);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(863, 94);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(474, 230);
            this.tabControl2.TabIndex = 44;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvImport);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(466, 204);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ImportData";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvImport
            // 
            this.dgvImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImport.Location = new System.Drawing.Point(3, 3);
            this.dgvImport.MultiSelect = false;
            this.dgvImport.Name = "dgvImport";
            this.dgvImport.ReadOnly = true;
            this.dgvImport.RowHeadersVisible = false;
            this.dgvImport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImport.Size = new System.Drawing.Size(460, 198);
            this.dgvImport.TabIndex = 2;
            this.dgvImport.SelectionChanged += new System.EventHandler(this.dgvImport_SelectionChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvListImage);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(466, 204);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TrainingData";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btTest
            // 
            this.btTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btTest.Location = new System.Drawing.Point(322, 270);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(152, 49);
            this.btTest.TabIndex = 42;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // frmTrainTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 701);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbTimeFrame);
            this.Controls.Add(this.btFrameNext);
            this.Controls.Add(this.btFramePrev);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.trackPlaying);
            this.Controls.Add(this.btSaveJob);
            this.Controls.Add(this.btSetting);
            this.Controls.Add(this.btOpenJob);
            this.Controls.Add(this.btNewJob);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.tbWorkingDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbMainTrain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmTrainTool";
            this.Text = "AI Training Tool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTrainTool_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPlaying)).EndInit();
            this.tabLabeling.ResumeLayout(false);
            this.tabLabeling.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListObject)).EndInit();
            this.tabPageDetection.ResumeLayout(false);
            this.tabPageDetection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetecResult)).EndInit();
            this.tabPageTraining.ResumeLayout(false);
            this.tabPageTraining.PerformLayout();
            this.tabPageDataAgument.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabExtractData.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMainTrain)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImport)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbMainTrain;
        private System.Windows.Forms.DataGridView dgvListImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbWorkingDir;
        private System.Windows.Forms.Timer timerWebcam;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Timer timerOutput;
        private System.Windows.Forms.Button btNewJob;
        private System.Windows.Forms.Button btOpenJob;
        private System.Windows.Forms.Button btSetting;
        private System.Windows.Forms.Button btSaveJob;
        private System.Windows.Forms.TrackBar trackPlaying;
        private System.Windows.Forms.Timer timerVideo;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btFramePrev;
        private System.Windows.Forms.Button btFrameNext;
        private System.Windows.Forms.TextBox tbTimeFrame;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabLabeling;
        private System.Windows.Forms.TabPage tabPageDetection;
        private System.Windows.Forms.CheckBox cbShowChecking;
        private System.Windows.Forms.CheckBox cbShowAll;
        private System.Windows.Forms.DataGridView dgvDetecResult;
        private System.Windows.Forms.Button btDetect;
        private System.Windows.Forms.TabPage tabPageTraining;
        private System.Windows.Forms.ListBox listBoxOut;
        private System.Windows.Forms.Button btStopTraining;
        private System.Windows.Forms.Button btDelteAll;
        private System.Windows.Forms.Label lbSelectedModel;
        private System.Windows.Forms.Button btUseThisModel;
        private System.Windows.Forms.Button btStartTrain;
        private System.Windows.Forms.TabPage tabPageDataAgument;
        private System.Windows.Forms.CheckBox cbShowName;
        private System.Windows.Forms.Button btDelSelectBound;
        private System.Windows.Forms.Button btDeleteAllBounding;
        private System.Windows.Forms.GroupBox gbObjectClass;
        private System.Windows.Forms.DataGridView dgvListObject;
        private System.Windows.Forms.Button btSaveLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbResize;
        private System.Windows.Forms.CheckBox cbResizeCrop;
        private System.Windows.Forms.TabPage tabExtractData;
        private System.Windows.Forms.Button btListAllTrainfile;
        private System.Windows.Forms.Button btDeleteTrain;
        private System.Windows.Forms.Button btAutoVideo;
        private System.Windows.Forms.Button btAutoLabel;
        private System.Windows.Forms.Button btCropImage;
        private System.Windows.Forms.Button btAdjustROI;
        private System.Windows.Forms.Button btStopVideo;
        private System.Windows.Forms.Button btAddTrain;
        private System.Windows.Forms.Button btCapture;
        private System.Windows.Forms.Button btOpenImage;
        private System.Windows.Forms.Button btPlayvideo;
        private System.Windows.Forms.Button btStartPreview;
        private System.Windows.Forms.Button btOpenVideo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox tbCropShift;
        private System.Windows.Forms.CheckBox cbCropShift;
        private System.Windows.Forms.Button btAddAll;
        private System.Windows.Forms.Button btAddExist;
        private System.Windows.Forms.Button btOpenDetect;
        private System.Windows.Forms.Button btSharpenImage;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvImport;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btTest;
    }
}

