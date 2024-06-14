using ClosedXML.Excel;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmVendor : Form
    {
        #region Properties
        public static long PrimaryId = 0;
        VendorService _VendorService = new VendorService();
        ErrorProvider ep = new ErrorProvider();
        VendorMasterModel objVendorMasterModel = new VendorMasterModel();
        List<VendorMasterModel> lstVendorMasterModel = new List<VendorMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion

        #region Events
        private void VendorGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    UpdateData(e.RowIndex);
                }
                if (e.ColumnIndex == 1)
                {
                    VendorMasterModel objVendorMasterModel = new VendorMasterModel();
                    // bool IsVendor = _VendorService.CheckVendorName(txtVendorName.Text.Trim(), PrimaryId);
                    if (PrimaryId > 0)
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            objVendorMasterModel.VendorID = PrimaryId;
                            var add = _VendorService.AddVendor(objVendorMasterModel, 3);
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess,  false);
                            }
                            PrimaryId = 0;
                        }
                        dataLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void VendorGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    
                    PrimaryId = Convert.ToInt64(VendorGrdView.Rows[e.RowIndex].Cells[VendorMasterModelCont.VendorID].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
        }
        
        private void txtSearchVendorName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchStr = txtSearchVendorName.Text;
                if (SearchStr != null && SearchStr != CommonModelCont.EmptyString && SearchStr != AlertMessages.VendorSearch)
                {
                    VendorGrdView.DataSource = lstVendorMasterModel
                                                               .Where(o => o.VendorName.ToLower().StartsWith(SearchStr.ToLower()))
                                                               .Select(o => new
                                                               {
                                                                   VendorID = o.VendorID,
                                                                   VendorCode = CreateVendorCode(o.VendorID),
                                                                   VendorName = o.VendorName,
                                                                   Address = o.Address,
                                                                   Address2 = o.Address2,
                                                                   City = o.CityName,
                                                                   State = o.StateName,
                                                                   Country = o.CountryName,
                                                                   CityID = o.City,
                                                                   StateID = o.State,
                                                                   CountryID = o.Country,
                                                                   ZipCode = o.ZipCode,
                                                                   EmailID = o.EmailID,
                                                                   PhoneNo = o.PhoneNo,
                                                                   Fax = o.Fax,
                                                                   ContactPerson = o.ContactPerson
                                                               }).ToList();
                    VendorGrdView.Columns[VendorMasterModelCont.VendorID].Visible = false;
                    VendorGrdView.Columns[VendorMasterModelCont.Address].Visible = false;
                    VendorGrdView.Columns[VendorMasterModelCont.Address2].Visible = false;
                    VendorGrdView.Columns[VendorMasterModelCont.State].Visible = false;
                    VendorGrdView.Columns[VendorMasterModelCont.Country].Visible = false;
                    VendorGrdView.Columns[VendorMasterModelCont.EmailID].Visible = false;
                    VendorGrdView.Columns[VendorMasterModelCont.Fax].Visible = false;
                    VendorGrdView.Columns[CityMasterModelCont.CityID].Visible = false;
                    VendorGrdView.Columns[StateMasterModelCont.StateID].Visible = false;
                    VendorGrdView.Columns[CountryMasterModelCont.CountryID].Visible = false;

                    VendorGrdView.Columns["ContactPerson"].HeaderText = "Contact Person";
                    VendorGrdView.Columns["ZipCode"].HeaderText = "Zip Code";

                    VendorGrdView.Columns["PhoneNo"].HeaderText = "Phone";
                    VendorGrdView.Columns["EmailID"].HeaderText = "Email";

                    VendorGrdView.Columns["VendorName"].HeaderText = "Vendor Name";
                    VendorGrdView.Columns["VendorCode"].HeaderText = "Vendor Code";
                }
                else
                {
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            FrmMetro_AddVendor objFrmMetro_AddVendor = new FrmMetro_AddVendor();
            objFrmMetro_AddVendor.RefreshCountry();

            objFrmMetro_AddVendor.ShowDialog();

            dataLoad();
        }
        #endregion

        #region Functions

        public FrmVendor()
        {
            InitializeComponent();
        }

        private string CreateVendorCode(long VendorID)
        {
            string VendorCode = string.Empty;
            VendorCode = VendorID.ToString();
            for (int i = 6; i > VendorID.ToString().Length; i--)
            {
                VendorCode = "0" + VendorCode;
            }
            return VendorCode;
        }
        public void dataLoad()
        {
            try
            {

                lstVendorMasterModel = _VendorService.GetAllVendor();
                VendorGrdView.DataSource = lstVendorMasterModel.Select(o => new
                {
                    VendorID = o.VendorID,
                    VendorCode = CreateVendorCode(o.VendorID),
                    VendorName = o.VendorName,
                    Address = o.Address,
                    Address2 = o.Address2,
                    City = o.CityName, //o.City,
                    State = o.StateName, //o.State,
                    Country = o.CountryName,  //o.Country,
                    CityID = o.City,
                    StateID = o.State,
                    CountryID = o.Country,
                    ZipCode = o.ZipCode,
                    EmailID = o.EmailID,
                    PhoneNo = o.PhoneNo,
                    Fax  =o.Fax,
                    ContactPerson = o.ContactPerson
                }).ToList();

                VendorGrdView.Columns[VendorMasterModelCont.VendorID].Visible = false;
                VendorGrdView.Columns[VendorMasterModelCont.Address].Visible = false;
                VendorGrdView.Columns[VendorMasterModelCont.Address2].Visible = false;
                VendorGrdView.Columns[VendorMasterModelCont.State].Visible = false;
                VendorGrdView.Columns[VendorMasterModelCont.Country].Visible = false;
                VendorGrdView.Columns[VendorMasterModelCont.EmailID].Visible = false;
                VendorGrdView.Columns[VendorMasterModelCont.Fax].Visible = false;
                VendorGrdView.Columns[CityMasterModelCont.CityID].Visible = false;
                VendorGrdView.Columns[StateMasterModelCont.StateID].Visible = false;
                VendorGrdView.Columns[CountryMasterModelCont.CountryID].Visible = false;


                VendorGrdView.Columns["ContactPerson"].HeaderText = "Contact Person";
                VendorGrdView.Columns["ZipCode"].HeaderText = "Zip Code";
                VendorGrdView.Columns["EmailID"].HeaderText = "Email";

                VendorGrdView.Columns["PhoneNo"].HeaderText = "Phone";
                VendorGrdView.Columns["VendorName"].HeaderText = "Vendor Name";
                VendorGrdView.Columns["VendorCode"].HeaderText = "Vendor Code";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

        #region IMPORT/EXPORT

        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HRD=Yes'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'";
        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                string filePath = openFileDialog1.FileName;
                string extension = Path.GetExtension(filePath);
                string conStr, sheetName;
                int count = 0;
                conStr = string.Empty;
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath);
                        break;
                }

                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }

                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.Connection = con;
                            con.Open();
                            oda.SelectCommand = cmd;
                            oda.Fill(dt);
                            con.Close();

                            CountryService _CountryService = new CountryService();
                            DataTable uniqueCountry = dt.DefaultView.ToTable(true, "CountryName");
                            string CountryName = "";
                            for (int row = 0; row < uniqueCountry.Rows.Count; row++)
                            {
                                #region Check Country
                                bool IsCountry = _CountryService.CheckName(uniqueCountry.Rows[row]["CountryName"].ToString());
                                if (!IsCountry)
                                {
                                    if (CountryName == "")
                                    {
                                        CountryName = uniqueCountry.Rows[row]["CountryName"].ToString();
                                    }
                                    else
                                    {
                                        CountryName = ", " + uniqueCountry.Rows[row]["CountryName"].ToString();
                                    }
                                }
                                #endregion
                            }
                            if (CountryName == "")
                            {
                                StateService _StateService = new StateService();
                                DataTable uniqueState = dt.DefaultView.ToTable(true, "StateName");
                                string SName = "";
                                for (int row = 0; row < uniqueState.Rows.Count; row++)
                                {
                                    #region Check State
                                    bool IsState = _StateService.CheckName(uniqueState.Rows[row]["StateName"].ToString());
                                    if (!IsState)
                                    {
                                        if (SName == "")
                                        {
                                            SName = uniqueState.Rows[row]["StateName"].ToString();
                                        }
                                        else
                                        {
                                            SName = ", " + uniqueState.Rows[row]["StateName"].ToString();
                                        }
                                    }
                                    #endregion
                                }

                                if (SName == "")
                                {
                                    CityService _CityService = new CityService();
                                    DataTable uniqueCity = dt.DefaultView.ToTable(true, "CityName");
                                    string CName = "";
                                    for (int row = 0; row < uniqueCity.Rows.Count; row++)
                                    {
                                        #region Check City
                                        bool IsCity = _CityService.CheckName(uniqueCity.Rows[row]["CityName"].ToString());
                                        if (!IsCity)
                                        {
                                            if (CName == "")
                                            {
                                                CName = uniqueCity.Rows[row]["CityName"].ToString();
                                            }
                                            else
                                            {
                                                CName = ", " + uniqueCity.Rows[row]["CityName"].ToString();
                                            }
                                        }
                                        #endregion
                                    }
                                    if (CName == "")
                                    {
                                        #region Add Vendor
                                        for (int row = 0; row < dt.Rows.Count; row++)
                                        {
                                            long CountryID = 0;
                                            long StateID = 0;
                                            long CityID = 0;
                                            CountryID = _CountryService.GetCountryID(dt.Rows[row]["CountryName"].ToString());
                                            StateID = _StateService.GetStateID(dt.Rows[row]["StateName"].ToString());
                                            //CityID = _CityService.GetCityID(dt.Rows[row]["CityName"].ToString());
                                            if (CountryID != 0 && StateID != 0 && CityID != 0)
                                            {
                                                bool IsStore = _VendorService.CheckName(dt.Rows[row]["VendorName"].ToString());
                                                if (!IsStore)
                                                {
                                                    count++;
                                                    VendorMasterModel objVendorMasterModel = new VendorMasterModel();
                                                    objVendorMasterModel.VendorName = dt.Rows[row]["VendorName"].ToString().Trim();
                                                    objVendorMasterModel.Address = dt.Rows[row]["Address"].ToString().Trim();
                                                    objVendorMasterModel.Address2 = dt.Rows[row]["Address2"].ToString().Trim();
                                                    objVendorMasterModel.PhoneNo = dt.Rows[row]["PhoneNo"].ToString().Trim();
                                                    objVendorMasterModel.Fax = dt.Rows[row]["Fax"].ToString().Trim();
                                                    objVendorMasterModel.ZipCode = dt.Rows[row]["ZipCode"].ToString().Trim();
                                                    objVendorMasterModel.City = dt.Rows[row]["CityName"].ToString().Trim();
                                                    objVendorMasterModel.State = StateID;
                                                    objVendorMasterModel.Country = CountryID;
                                                    var add = _VendorService.AddVendor(objVendorMasterModel, 1);
                                                }
                                            }
                                        }
                                        ClsCommon.MsgBox("Information","Total " + count + " Vendor imported successfully.!",false);

                                        dataLoad();
                                        #endregion
                                    }
                                    else
                                    {
                                        ClsCommon.MsgBox("Information",CName + " City is not available!", false);
                                    }
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information",SName + " State is not available!", false);
                                }
                            }
                            else
                            {
                                if (CountryName != "")
                                    ClsCommon.MsgBox("Information",CountryName + " Country is not available!", false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //EXPORT XLS
            try
            {
                DataTable dt = new DataTable();
                dt = tableLoad();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xlsx (*.xlsx)|*.xlsx";
                sfd.FileName = "Vendor";
                sfd.Title = "Save an Excel File";


                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ClsCommon.MsgBox("Information","Data will be exported and you will be notified when it is ready.", false);
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            ClsCommon.MsgBox("Information","It wasn't possible to write the data to the disk." + ex.Message, false);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Vendor");

                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information","Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Something went wrong while exporting Vendor !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add(VendorMasterModelCont.VendorName, typeof(string));
                dt.Columns.Add(VendorMasterModelCont.Address, typeof(string));
                dt.Columns.Add(VendorMasterModelCont.Address2, typeof(string));
                dt.Columns.Add(VendorMasterModelCont.CityName, typeof(string));
                dt.Columns.Add(VendorMasterModelCont.StateName, typeof(string));
                dt.Columns.Add(VendorMasterModelCont.CountryName, typeof(string));
                dt.Columns.Add(VendorMasterModelCont.PhoneNo, typeof(string));
                dt.Columns.Add(VendorMasterModelCont.Fax, typeof(string));
                dt.Columns.Add(VendorMasterModelCont.ZipCode, typeof(string));

                foreach (var item in lstVendorMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr[VendorMasterModelCont.VendorName] = item.VendorName;
                    dr[VendorMasterModelCont.Address] = item.Address;
                    dr[VendorMasterModelCont.Address2] = item.Address2;
                    dr[VendorMasterModelCont.CityName] = item.CityName;
                    dr[VendorMasterModelCont.StateName] = item.StateName;
                    dr[VendorMasterModelCont.CountryName] = item.CountryName;
                    dr[VendorMasterModelCont.PhoneNo] = item.PhoneNo;
                    dr[VendorMasterModelCont.Fax] = item.Fax;
                    dr[VendorMasterModelCont.ZipCode] = item.ZipCode;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        #endregion

        private void VendorGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }

        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddVendor objFrmMetro_AddVendor = new FrmMetro_AddVendor();
                objFrmMetro_AddVendor.PrimaryId = PrimaryId;
                objFrmMetro_AddVendor.RefreshCountry();

                objFrmMetro_AddVendor.txtVendorName.Text = VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.VendorName].Value.ToString().Trim() == null ? CommonModelCont.EmptyString : VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.VendorName].Value.ToString().Trim();
                objFrmMetro_AddVendor.txtAddress.Text = VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.Address].Value.ToString().Trim() == null ? CommonModelCont.EmptyString : VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.Address].Value.ToString().Trim();
                objFrmMetro_AddVendor.txtAddress2.Text = VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.Address2].Value.ToString().Trim() == null ? CommonModelCont.EmptyString : VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.Address2].Value.ToString().Trim();
                objFrmMetro_AddVendor.txtEmail.Text = VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.EmailID].Value == null ? CommonModelCont.EmptyString : VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.EmailID].Value.ToString().Trim();
                objFrmMetro_AddVendor.txtPhone.Text = VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.PhoneNo].Value.ToString().Trim() == null ? CommonModelCont.EmptyString : VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.PhoneNo].Value.ToString().Trim();
                objFrmMetro_AddVendor.txtFax.Text = VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.Fax].Value.ToString().Trim() == null ? CommonModelCont.EmptyString : VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.Fax].Value.ToString().Trim();
                objFrmMetro_AddVendor.txtContactPerson.Text = VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.ContactPerson].Value.ToString().Trim() == null ? CommonModelCont.EmptyString : VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.ContactPerson].Value.ToString().Trim();
                objFrmMetro_AddVendor.txtZipcode.Text = VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.ZipCode].Value.ToString().Trim() == null ? CommonModelCont.EmptyString : VendorGrdView.Rows[RowIndex].Cells[VendorMasterModelCont.ZipCode].Value.ToString().Trim();

                if (VendorGrdView.Rows[RowIndex].Cells[CountryMasterModelCont.CountryID].Value != null)
                {
                    objFrmMetro_AddVendor.cmbCountry.SelectedValue = VendorGrdView.Rows[RowIndex].Cells[CountryMasterModelCont.CountryID].Value;
                }
                if (VendorGrdView.Rows[RowIndex].Cells[StateMasterModelCont.StateID].Value != null)
                {
                    objFrmMetro_AddVendor.cmbState.SelectedValue = VendorGrdView.Rows[RowIndex].Cells[StateMasterModelCont.StateID].Value;
                }
                //objFrmMetro_AddVendor.cmbCity.SelectedValue = VendorGrdView.Rows[RowIndex].Cells[CityMasterModelCont.CityID].Value;
                objFrmMetro_AddVendor.txtcity.Text = Convert.ToString(VendorGrdView.Rows[RowIndex].Cells[CityMasterModelCont.CityID].Value) == null ? CommonModelCont.EmptyString : VendorGrdView.Rows[RowIndex].Cells[CityMasterModelCont.CityID].Value.ToString().Trim(); 
                objFrmMetro_AddVendor.ShowDialog();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
