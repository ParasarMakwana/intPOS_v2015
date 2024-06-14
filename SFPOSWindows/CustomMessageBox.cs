using MetroFramework.Forms;
using SFPOS.Common;
using SFPOSWindows.Frontend;
using System.Windows.Forms;

namespace SFPOSWindows
{
    public partial class CustomMessageBox : MetroForm
    {
        public bool IsCancel = false;
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            RetryOrder.isRetry = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            RetryOrder.isRetry = false;
            this.Close();
        }

        private void CustomMessageBox_Load(object sender, System.EventArgs e)
        {
            this.BringToFront();
            if (IsCancel)
            {
                btnCancel.Visible = true;
                if (btnRetry.Visible == true)
                {
                    btnRetry.Location = btnOK.Location;
                }
            }
            else
            {
                btnOK.Location = btnCancel.Location;
            }
        }

        private void btnRetry_Click(object sender, System.EventArgs e)
        {
            RetryOrder.isRetry = true;
            this.Close();
        }

        private void CustomMessageBox_Shown(object sender, System.EventArgs e)
        {
           // this.Location = Screen.AllScreens[0].Bounds.Location;
           //// this.CenterToScreen();
           // this.WindowState = FormWindowState.Normal;
           // //this.StartPosition = FormStartPosition.CenterParent;
           // this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
           // this.Bounds = Screen.AllScreens[0].Bounds;
        }
    }
}
