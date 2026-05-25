
namespace DetectTool
{
    partial class frmDetection
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
            this.pbMainTrain = new System.Windows.Forms.PictureBox();
            this.btOpenImage = new System.Windows.Forms.Button();
            this.lbDetectionTime = new System.Windows.Forms.Label();
            this.btDetect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainTrain)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMainTrain
            // 
            this.pbMainTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMainTrain.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbMainTrain.Location = new System.Drawing.Point(1, 2);
            this.pbMainTrain.Name = "pbMainTrain";
            this.pbMainTrain.Size = new System.Drawing.Size(1043, 534);
            this.pbMainTrain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMainTrain.TabIndex = 1;
            this.pbMainTrain.TabStop = false;
            this.pbMainTrain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMainTrain_Paint);
            // 
            // btOpenImage
            // 
            this.btOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOpenImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOpenImage.Location = new System.Drawing.Point(1, 542);
            this.btOpenImage.Name = "btOpenImage";
            this.btOpenImage.Size = new System.Drawing.Size(125, 53);
            this.btOpenImage.TabIndex = 6;
            this.btOpenImage.Text = "Mở Ảnh";
            this.btOpenImage.UseVisualStyleBackColor = true;
            this.btOpenImage.Click += new System.EventHandler(this.btOpenImage_Click);
            // 
            // lbDetectionTime
            // 
            this.lbDetectionTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDetectionTime.AutoSize = true;
            this.lbDetectionTime.Location = new System.Drawing.Point(12, 666);
            this.lbDetectionTime.Name = "lbDetectionTime";
            this.lbDetectionTime.Size = new System.Drawing.Size(90, 13);
            this.lbDetectionTime.TabIndex = 8;
            this.lbDetectionTime.Text = "Thời gian detect: ";
            // 
            // btDetect
            // 
            this.btDetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDetect.Location = new System.Drawing.Point(1, 601);
            this.btDetect.Name = "btDetect";
            this.btDetect.Size = new System.Drawing.Size(125, 53);
            this.btDetect.TabIndex = 9;
            this.btDetect.Text = "Detect";
            this.btDetect.UseVisualStyleBackColor = true;
            this.btDetect.Click += new System.EventHandler(this.btDetect_Click);
            // 
            // frmDetection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 688);
            this.Controls.Add(this.btDetect);
            this.Controls.Add(this.lbDetectionTime);
            this.Controls.Add(this.btOpenImage);
            this.Controls.Add(this.pbMainTrain);
            this.Name = "frmDetection";
            this.Text = "Detection Tool";
            ((System.ComponentModel.ISupportInitialize)(this.pbMainTrain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMainTrain;
        private System.Windows.Forms.Button btOpenImage;
        private System.Windows.Forms.Label lbDetectionTime;
        private System.Windows.Forms.Button btDetect;
    }
}

