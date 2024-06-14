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
    public partial class FrmAddEditVendor : Form
    {
        ErrorProvider ep = new ErrorProvider();
        public long PrimaryId = 0;

        VendorService _VendorService = new VendorService();
        VendorMasterModel objVendorMasterModel = new VendorMasterModel();
        FrmVendor objFrmVendor = new FrmVendor();

        CountryService _CountryService = new CountryService();
        CountryMasterModel objCountryMasterModel = new CountryMasterModel();

        StateService _StateService = new StateService();
        StateMasterModel objStateMasterModel = new StateMasterModel();

        CityService _CityService = new CityService();
        CityMasterModel objCityMasterModel = new CityMasterModel();

        public FrmAddEditVendor()
        {
            InitializeComponent();
            RefreshCountry();
        }

        public void RefreshCountry()
        {
            List<CountryMasterModel> objCountryMasterModel = new List<CountryMasterModel>();
            objCountryMasterModel = _CountryService.GetAllCountry();
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryID";
            cmbCountry.DataSource = objCountryMasterModel;
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCountry.SelectedValue.ToString() != null)
            {
                int countryid = Convert.ToInt32(cmbCountry.SelectedValue.ToString());
                RefreshState(countryid);
            }
        }

        public void RefreshState(int countryid)
        {
            List<StateMasterModel> objStateMasterModel = new List<StateMasterModel>();
            objStateMasterModel = _StateService.GetAllState(countryid);
            cmbState.ValueMember = "StateID";
            cmbState.DisplayMember = "StateName";
            cmbState.DataSource = objStateMasterModel;
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbState.SelectedValue.ToString() != null)
            {
                int stateid = Convert.ToInt32(cmbState.SelectedValue.ToString());
                RefreshCity(stateid);
            }
        }

        public void RefreshCity(int stateid)
        {
            List<CityMasterModel> objCityMasterModel = new List<CityMasterModel>();
            objCityMasterModel = _CityService.GetAllCity(stateid);
            cmbCity.ValueMember = "CityID";
            cmbCity.DisplayMember = "CityName";
            cmbCity.DataSource = objCityMasterModel;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool IsStore = _VendorService.CheckVendorName(txtVendorName.Text.Trim());
            bool IsValidate = validation();
            if (PrimaryId > 0 && IsValidate == false)
            {
                ep.SetError(txtZipcode, string.Empty);
                objVendorMasterModel.VendorID = PrimaryId;
                objVendorMasterModel.VendorName = txtVendorName.Text;
                objVendorMasterModel.Address = txtAddress.Text;
                objVendorMasterModel.Address2 = txtAddress2.Text;
                objVendorMasterModel.City = cmbCity.Text;
                objVendorMasterModel.State = cmbState.Text;
                objVendorMasterModel.ZipCode = txtZipcode.Text.Trim();
                objVendorMasterModel.Country = cmbCountry.Text;
                objVendorMasterModel.PhoneNo = txtPhone.Text.Trim();
                objVendorMasterModel.EmailID = txtEmail.Text.Trim();
                objVendorMasterModel.Fax = txtFax.Text.Trim();
                objVendorMasterModel.ContactPerson = txtContactPerson.Text.Trim();

                var add = _VendorService.AddVendor(objVendorMasterModel, 2);
                PrimaryId = 0;
                lblMessage.Text = "Vendor detail has been updated successfully.";
                txtVendorName.Text = null;
            }
        }

        public bool validation()
        {
            if (string.IsNullOrWhiteSpace(txtVendorName.Text))
            {
                txtVendorName.Focus();
                ep.SetError(txtVendorName, "Vendor name should not be left blank!");
                return true;
            }
            else if (!(Regex.Match(txtPhone.Text.Trim(), "^\\+[9][1][-][\\d]{10}$").Success))
            {
                ep.SetError(txtVendorName, string.Empty);
                txtPhone.Focus();
                ep.SetError(txtPhone, "Wrong phone number. \nPhone number should be 10 digit and Format +91-XXXXXXXXXX");
                return true;
            }
            else if (!(Regex.Match(txtFax.Text.Trim(), "^\\+[0-9]{1,3}-[0-9]{3}-[0-9]{7}$").Success))
            {
                ep.SetError(txtPhone, string.Empty);
                txtFax.Focus();
                ep.SetError(txtFax, "Wrong Fax number. \nFax number should be like - '+(country_code)-(area_code)-(fax_number)'");
                return true;
            }
            else if (!(Regex.Match(txtZipcode.Text.Trim(), @"^\d{3}\s?\d{3}$").Success))
            {
                ep.SetError(txtFax, string.Empty);
                txtZipcode.Focus();
                ep.SetError(txtZipcode, "Wrong Zip Code. \nEnter valid zip code");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool IsStore = _VendorService.CheckVendorName(txtVendorName.Text.Trim());
            bool IsValidate = validation();

            if (IsStore == false && IsValidate == false)
            {
                ep.SetError(txtZipcode, string.Empty);
                objVendorMasterModel.VendorName = txtVendorName.Text;
                objVendorMasterModel.Address = txtAddress.Text;
                objVendorMasterModel.Address2 = txtAddress2.Text;
                objVendorMasterModel.City = cmbCity.Text;
                objVendorMasterModel.State = cmbState.Text;
                objVendorMasterModel.ZipCode = txtZipcode.Text.Trim();
                objVendorMasterModel.Country = cmbCountry.Text;
                objVendorMasterModel.PhoneNo = txtPhone.Text.Trim();
                objVendorMasterModel.Fax = txtFax.Text.Trim();

                var add = _VendorService.AddVendor(objVendorMasterModel, 1);
                lblMessage.Text = "Vendor detail has been added successfully";
                txtVendorName.Text = null;
            }
            
        }
    }
}
