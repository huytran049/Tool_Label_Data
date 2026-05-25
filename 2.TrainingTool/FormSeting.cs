using DirectShowLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibs;

namespace TrainingTool
{
    public partial class FormSeting : Form
    {
        string _sLocal_folder;
        public FormSeting()
        {
            InitializeComponent();
            _sLocal_folder = Directory.GetCurrentDirectory();
            //SystemConfig.OnApplicationStart(_sLocal_folder,"");
            String jobConfiguration = _sLocal_folder + "\\Data\\job.config";
            JobConfig.OpenJob(jobConfiguration);
            tbTrainWidth.Text = JobConfig.GetTrainWidth();
            tbTrainHeight.Text = JobConfig.GetTrainHeight();
            EnumerateCameras();
        }
        public bool EnumerateCameras()
        {
            DsDevice[] captureDevices;

            // Get the set of directshow devices that are video inputs.
            captureDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            for (int idx = 0; idx < captureDevices.Length; idx++)
            {
                cbbCamlist.Items.Add(captureDevices[idx].Name);
            }
            return true;
        }
        private void btSaveSetting_Click(object sender, EventArgs e)
        {
            if (cbbCamlist.SelectedItem == null) return;
            JobConfig.SetCamID(cbbCamlist.SelectedItem.ToString());
            JobConfig.SetTrainWidth(tbTrainWidth.Text);
            JobConfig.SetTrainHeight(tbTrainHeight.Text);
            MessageBox.Show("Setting thành công, vui lòng khởi động lại phần mềm để thay đổi");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
