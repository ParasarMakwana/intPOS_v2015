using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace intPOSSetup
{
    public partial class FolderLocation : Form
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        public string installationPath = string.Empty;

        public FolderLocation()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            installationPath = txtInstallPath.Text;
            this.Hide();
            this.Close();
            OnMyEvent(this, new EventArgs());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private FolderBrowserDialog folderBrowserDialog1;
        private void btnOptions_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtInstallPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

       
    }
}
