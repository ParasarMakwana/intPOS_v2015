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
    public partial class FrmAddEditStore : Form
    {
        StoreService _StoreService = new StoreService();
        public long PrimaryId = 0;

        ErrorProvider ep = new ErrorProvider();

        StoreMasterModel objStoreMasterModel = new StoreMasterModel();
        
        CountryService _CountryService = new CountryService();
        CountryMasterModel objCountryMasterModel = new CountryMasterModel();

        StateService _StateService = new StateService();
        StateMasterModel objStateMasterModel = new StateMasterModel();

        CityService _CityService = new CityService();
        CityMasterModel objCityMasterModel = new CityMasterModel();

        public FrmAddEditStore()
        {
            InitializeComponent();

            RefreshCountry();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool IsStore = _StoreService.CheckStoreName(txtStoreName.Text.Trim());
            if (string.IsNullOrWhiteSpace(txtStoreName.Text))
            {
                txtStoreName.Focus();
                ep.SetError(txtStoreName, "Store name should not be left blank!");
            }
            else if (!(Regex.Match(txtPhone.Text.Trim(), "^\\+[9][1][-][\\d]{10}$").Success))   
            {
                ep.SetError(txtStoreName,string.Empty);                             
                txtPhone.Focus();
                ep.SetError(txtPhone, "Wrong phone number. \nPhone number should be 10 digit and Format +91-XXXXXXXXXX");
            }
            else if (!(Regex.Match(txtFax.Text.Trim(), "^\\+[0-9]{1,3}-[0-9]{3}-[0-9]{7}$").Success))
            {
                ep.SetError(txtPhone, string.Empty);
                txtFax.Focus();
                ep.SetError(txtFax, "Wrong Fax number. \nFax number should be like - '+(country_code)-(area_code)-(fax_number)'");
            }
            else if (!(Regex.Match(txtZipcode.Text.Trim(), @"^\d{3}\s?\d{3}$").Success))
            {
                ep.SetError(txtFax, string.Empty);
                txtZipcode.Focus();
                ep.SetError(txtZipcode, "Wrong Zip Code. \nEnter valid zip code");
            }
            else if (IsStore == false)
            {
                ep.SetError(txtZipcode, string.Empty);
                objStoreMasterModel.StoreName = txtStoreName.Text;
                objStoreMasterModel.Address = txtAddress.Text;
                objStoreMasterModel.Address2 = txtAddress2.Text;
                objStoreMasterModel.City = cmbCity.Text;
                objStoreMasterModel.State = cmbState.Text;
                objStoreMasterModel.ZipCode = txtZipcode.Text.Trim();
                objStoreMasterModel.Country = cmbCountry.Text;
                objStoreMasterModel.Phone = txtPhone.Text.Trim();
                objStoreMasterModel.Fax = txtFax.Text.Trim();

                var add = _StoreService.AddStore(objStoreMasterModel, 1);
                lblMessage.Text = "Store detail has been added successfully";
                txtStoreName.Text = null;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool IsStore = _StoreService.CheckStoreName(txtStoreName.Text.Trim());
            if (string.IsNullOrWhiteSpace(txtStoreName.Text))
            {
                txtStoreName.Focus();
                ep.SetError(txtStoreName, "Store name should not be left blank!");
            }
            else if(!(Regex.Match(txtPhone.Text.Trim(), "^\\+[9][1][-][\\d]{10}$").Success))
            {
                ep.SetError(txtStoreName, string.Empty);
                txtPhone.Focus();
                ep.SetError(txtPhone, "Wrong phone number.\nPhone number should be 10 digit and Format +91-XXXXXXXXXX");
            }
            else if(!(Regex.Match(txtFax.Text.Trim(), "^\\+[0-9]{1,3}-[0-9]{3}-[0-9]{7}$").Success))
            {
                ep.SetError(txtPhone, string.Empty);
                txtFax.Focus();
                ep.SetError(txtFax, "Wrong Fax number.\nFax number should be like - '+(country_code)-(area_code)-(fax_number)'");
            }
            else if (!(Regex.Match(txtZipcode.Text.Trim(), @"^\d{3}\s?\d{3}$").Success))
            {
                ep.SetError(txtFax, string.Empty);
                txtZipcode.Focus();
                ep.SetError(txtZipcode, "Wrong Zip Code. \nEnter valid zip code");
            }
            else if (PrimaryId > 0)
            {
                ep.SetError(txtZipcode, string.Empty);
                objStoreMasterModel.StoreID = PrimaryId;
                objStoreMasterModel.StoreName = txtStoreName.Text;
                objStoreMasterModel.Address = txtAddress.Text;
                objStoreMasterModel.Address2 = txtAddress2.Text;
                objStoreMasterModel.City = cmbCity.Text;  
                objStoreMasterModel.State = cmbState.Text;
                objStoreMasterModel.ZipCode = txtZipcode.Text.Trim();
                objStoreMasterModel.Country = cmbCountry.Text;
                objStoreMasterModel.Phone = txtPhone.Text.Trim();
                objStoreMasterModel.Fax = txtFax.Text.Trim();

                var add = _StoreService.AddStore(objStoreMasterModel, 2);
                PrimaryId = 0;
                lblMessage.Text = "Store detail has been updated successfully.";
                txtStoreName.Text = null;
            }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
