using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmAddEditemployee : Form
    {
        ErrorProvider ep = new ErrorProvider();
        EmployeeMasterModel objEmployeeMasterModel = new EmployeeMasterModel();
        EmployeeService _EmployeeService = new EmployeeService();
        StoreService _StoreService = new StoreService();
        public long PrimaryId = 0;


        public FrmAddEditemployee()
        {
            InitializeComponent();
            CmbStore();
        }

        public void CmbStore()
        {
            List<StoreMasterModel> objStoreMasterModel = new List<StoreMasterModel>();
            objStoreMasterModel = _StoreService.GetAllStore();
            cmbStoreName.DisplayMember = "StoreName";
            cmbStoreName.ValueMember = "StoreID";
            cmbStoreName.DataSource = objStoreMasterModel.Select(o => new {
                                                                            StoreID = o.StoreID,
                                                                            StoreName = o.StoreName,
                                                                            }).ToList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Regex regexEmail = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                txtFirstName.Focus();
                ep.SetError(txtFirstName, "Employee first name should not be left blank!");
            }
            else if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                txtLastName.Focus();
                ep.SetError(txtLastName, "Employee Last name should not be left blank!");
            }
            else if (!(Regex.Match(txtPhone.Text.Trim(), "^\\+[9][1][-][\\d]{10}$").Success))
            {
                txtPhone.Focus();
                ep.SetError(txtPhone, "Wrong phone number. \nPhone number should be 10 digit and Format +91-XXXXXXXXXX");
            }
            else if (!(regexEmail.Match(txtEmail.Text)).Success)
            {
                txtEmail.Focus();
                ep.SetError(txtEmail, "Email id should be in correct format.!");
            }
            else
            {
                objEmployeeMasterModel.EmployeeID = PrimaryId;
                objEmployeeMasterModel.FirstName = txtFirstName.Text.Trim();
                objEmployeeMasterModel.LastName = txtLastName.Text.Trim();
                objEmployeeMasterModel.StoreID = Convert.ToInt64(cmbStoreName.SelectedValue);
                objEmployeeMasterModel.RoleID = Convert.ToInt64(cmbRoleName.SelectedValue);
                objEmployeeMasterModel.EmailID = txtEmail.Text.Trim();
                objEmployeeMasterModel.PhoneNo = txtPhone.Text.Trim();

                var add = _EmployeeService.AddEmployee(objEmployeeMasterModel, 2);
                lblMessage.Text = "Employee detail has been updated successfully";
                txtFirstName.Text = null;
                txtLastName.Text = null;
                txtEmail.Text = null;
                txtPhone.Text = null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Regex regexEmail = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            //@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                txtFirstName.Focus();
                ep.SetError(txtFirstName, "Employee first name should not be left blank!");
            }
            else if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                txtLastName.Focus();
                ep.SetError(txtLastName, "Employee Last name should not be left blank!");
            }
            else if (!(Regex.Match(txtPhone.Text.Trim(), "^\\+[9][1][-][\\d]{10}$").Success))
            {
                txtPhone.Focus();
                ep.SetError(txtPhone, "Wrong phone number. \nPhone number should be 10 digit and Format +91-XXXXXXXXXX");
            }
            else if (!(regexEmail.Match(txtEmail.Text)).Success)
            {
                txtEmail.Focus();
                ep.SetError(txtEmail, "Email id should be in correct format.!");
            }
            else
            {
                objEmployeeMasterModel.FirstName = txtFirstName.Text.Trim();
                objEmployeeMasterModel.LastName = txtLastName.Text.Trim();
                objEmployeeMasterModel.StoreID = Convert.ToInt64(cmbStoreName.SelectedValue);
                objEmployeeMasterModel.RoleID = Convert.ToInt64(cmbRoleName.SelectedValue);
                objEmployeeMasterModel.EmailID = txtEmail.Text.Trim();
                objEmployeeMasterModel.PhoneNo = txtPhone.Text.Trim();

                var add = _EmployeeService.AddEmployee(objEmployeeMasterModel, 1);

                lblMessage.Text = "Employee detail has been added successfully";
                txtFirstName.Text = null;
                txtLastName.Text = null;
                txtEmail.Text = null;
                txtPhone.Text = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
