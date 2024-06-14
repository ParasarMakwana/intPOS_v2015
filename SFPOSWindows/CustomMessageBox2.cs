using MetroFramework.Forms;
using SFPOS.Common;
using SFPOSWindows.Frontend;
using System.Windows.Forms;

namespace SFPOSWindows
{
    public partial class CustomMessageBox2 : MetroForm
    {
        public CustomMessageBox2()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            RetryOrder.UpdateNow = false;
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
        }

        private void btnRetry_Click(object sender, System.EventArgs e)
        {
            RetryOrder.UpdateNow = true;
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
