using MetroFramework.Forms;
using SFPOS.Common;

namespace SFPOSWindows
{
    public partial class SubmittingMessageBox : MetroForm
    {
        public bool IsCancel = false;

        public SubmittingMessageBox()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            LoginInfo.IsVerifiedTillStatus = false;
            this.Close();
        }

        private void CustomMessageBox_Load(object sender, System.EventArgs e)
        {
            this.BringToFront();
            if (IsCancel)
            {
                btnCancel.Visible = true;
            }
            else
            {
                btnOK.Location = btnCancel.Location;
            }
        }

        private void btnRetry_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void checkBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checVarified.Checked == true)
            {
                btnOK.Enabled = true;
                LoginInfo.IsVerifiedTillStatus = true;
            }
            else
            {
                btnOK.Enabled = false;
                LoginInfo.IsVerifiedTillStatus = false;
            }
        }
    }
}
