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

namespace TrainingTool
{
    public partial class frmJobName : Form
    {
        public string JobName;
        public frmJobName()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtJobName.Text.Trim()))
            {
                MessageBox.Show("Tên Job không được để trống","Cảnh báo");
                txtJobName.Focus();
            }
            else
            {
                JobName = txtJobName.Text.Trim();
                Directory.CreateDirectory(JobName);
                this.DialogResult = DialogResult.OK;
                this.Close();
            } 
                
        }

        private void txtJobName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btOK.PerformClick();
            }    
        }

        private void frmJobName_VisibleChanged(object sender, EventArgs e)
        {
            txtJobName.Focus();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
