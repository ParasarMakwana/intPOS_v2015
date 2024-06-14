using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrmReEnterPwd : MetroForm
    {
        // private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmReEnterPwd()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
            var results = (from pm in _db.tbl_EmployeeMaster
                           where pm.IsDelete == false && pm.EmployeeID == LoginInfo.UserId
                           select new { Password = pm.Password }).ToList();
            if (password == results.FirstOrDefault().Password)
            {
                this.Close();
            }
            else
            {
                txtPassword.Text = "";
                ClsCommon.MsgBox("Information","Incorrect password for Resume.!", false);
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtPassword.Text.ToLower().Contains("cl"))
                {
                    txtPassword.Text = "";
                }
                else if (txtPassword.Text.ToLower().Contains("cn"))
                {
                    this.Close();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

