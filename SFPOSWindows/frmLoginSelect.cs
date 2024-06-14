using MetroFramework.Forms;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SFPOSWindows
{
    public partial class frmLoginSelect : MetroForm
    {
        public frmLoginSelect()
        {
            InitializeComponent();
        }

        private void btnOrderScan_Click(object sender, EventArgs e)
        {
            Hide();
            LoginInfo.ScreenID = 2;
            frmLogin _frmLogin = new frmLogin();
            _frmLogin.ShowDialog();
            Application.Exit();
        }

        private void btnBackOfc_Click(object sender, EventArgs e)
        {
            Hide();
            LoginInfo.ScreenID = 1;
            frmLogin _frmLogin = new frmLogin();
            _frmLogin.ShowDialog();
            Application.Exit();
        }


        SqlConnection con = new SqlConnection("Data Source=TBS24;Initial Catalog=ezPOSPro;Persist Security Info=True;User ID=sa;Password=sa@123;");

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();
            try
            {
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("please enter backup file location");
                }
                else
                {
                    string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + textBox1.Text + "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                    using (SqlCommand command = new SqlCommand(cmd, con))
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        command.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("database backup done successefully");
                        btnBackup.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.SelectedPath;
                btnBackup.Enabled = true;
            }
        }
    }
}
