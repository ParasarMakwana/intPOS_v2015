using ClosedXML.Excel;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Metro_Forms;
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
    public partial class FrmStore : Form
    {
        #region Properties

        private StoreService _StoreService = new StoreService();
        ErrorProvider ep = new ErrorProvider();
        StoreMasterModel objStoreMasterModel = new StoreMasterModel();

        CountryService _CountryService = new CountryService();
        CountryMasterModel objCountryMasterModel = new CountryMasterModel();

        StateService _StateService = new StateService();
        StateMasterModel objStateMasterModel = new StateMasterModel();

        CityService _CityService = new CityService();
        CityMasterModel objCityMasterModel = new CityMasterModel();
        List<StoreMasterModel> lstStoreMasterModel = new List<StoreMasterModel>();
        List<StateMasterModel> lstStateMasterModel = new List<StateMasterModel>();
        List<CityMasterModel> lstCityMasterModel = new List<CityMasterModel>();
        List<CountryMasterModel> lstCountryMasterModel = new List<CountryMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public static long PrimaryId = 0;
        #endregion

        #region Events

        private void StoreGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    UpdateData(e.RowIndex);
                }
                if (e.ColumnIndex == 1)
                {
                    if (PrimaryId > 0)
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            objStoreMasterModel.StoreID = PrimaryId;

                            var add = _StoreService.AddStore(objStoreMasterModel, 3);
                            UpdateLog();
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                            }
                            Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void StoreGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt64(StoreGrdView.Rows[e.RowIndex].Cells[StoreMasterModelCont.StoreID].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchStoreName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchStr = txtSearchStoreName.Text;
                if (SearchStr != null && SearchStr != CommonModelCont.EmptyString && SearchStr != AlertMessages.StoreSearch)
                {
                    StoreGrdView.DataSource = lstStoreMasterModel
                                                               .Where(c => (c.StoreName.ToLower().Contains(SearchStr.ToLower()))
                                                               || c.Address.ToLower().Contains(SearchStr.ToLower()))
                                                               .Select(c => new
                                                               {
                                                                   StoreID = c.StoreID,
                                                                   StoreName = c.StoreName,
                                                                   Address = c.Address,
                                                                   Address2 = c.Address2,
                                                                   City = c.CityName,
                                                                   State = c.StateName,
                                                                   Country = c.CountryName,
                                                                   CityID = c.City,
                                                                   StateID = c.State,
                                                                   CountryID = c.Country,
                                                                   Phone = c.Phone,
                                                                   Fax = c.Fax,
                                                                   ZipCode = c.ZipCode,
                                                                   AgeVarificationAge = c.AgeVarificationAge,
                                                                   Disclaimer = c.Disclaimer,
                                                                   DefaultTax = c.DefaultTax,
                                                                   IsStoreTax = c.IsStoreTax
                                                               }).ToList();

                    StoreGrdView.Columns[StoreMasterModelCont.StoreID].Visible = false;
                    StoreGrdView.Columns[StoreMasterModelCont.Address2].Visible = false;
                    StoreGrdView.Columns[CityMasterModelCont.CityID].Visible = false;
                    StoreGrdView.Columns[StateMasterModelCont.StateID].Visible = false;
                    StoreGrdView.Columns[CountryMasterModelCont.CountryID].Visible = false;

                    StoreGrdView.Columns["ZipCode"].HeaderText = "Zip Code";
                    StoreGrdView.Columns["StoreName"].HeaderText = "Name";
                }
                else { dataLoad(); }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMetro_AddStore objFrmMetro_AddStore = new FrmMetro_AddStore();
                objFrmMetro_AddStore.RefreshCountry();
                objFrmMetro_AddStore.ShowDialog();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

        #region Functions
        public FrmStore()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {

                lstStoreMasterModel = _StoreService.GetAllStore();
                StoreGrdView.DataSource = lstStoreMasterModel.Select(o => new
                {
                    StoreID = o.StoreID,
                    StoreName = o.StoreName,
                    Address = o.Address,
                    Address2 = o.Address2,
                    City = o.CityName, 
                    State = o.StateName, 
                    Country = o.CountryName, 
                    CityID = o.City,
                    StateID = o.State,
                    CountryID = o.Country,
                    Phone = o.Phone,
                    Fax = o.Fax,
                    ZipCode = o.ZipCode,
                    AgeVarificationAge = o.AgeVarificationAge,
                    Disclaimer = o.Disclaimer,
                    DefaultTax = o.DefaultTax,
                    IsStoreTax = o.IsStoreTax
                }).ToList();
                StoreGrdView.Columns[StoreMasterModelCont.StoreID].Visible = false;
                StoreGrdView.Columns[StoreMasterModelCont.Address2].Visible = false;
                StoreGrdView.Columns[CityMasterModelCont.CityID].Visible = false;
                StoreGrdView.Columns[StateMasterModelCont.StateID].Visible = false;
                StoreGrdView.Columns[CountryMasterModelCont.CountryID].Visible = false;

                StoreGrdView.Columns["ZipCode"].HeaderText = "Zip Code";
                StoreGrdView.Columns["StoreName"].HeaderText = "Name";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        public void Clear()
        {
            dataLoad();
            PrimaryId = 0;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_StoreMaster");
        }

        #endregion


        #region IMPORT/EXPORT

        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HRD=Yes'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'";
        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

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
                                        #region Add Store
                                        for (int row = 0; row < dt.Rows.Count; row++)
                                        {
                                            long CountryID = 0;
                                            long StateID = 0;
                                            long CityID = 0;
                                            CountryID = _CountryService.GetCountryID(dt.Rows[row]["CountryName"].ToString());
                                            StateID = _StateService.GetStateID(dt.Rows[row]["StateName"].ToString());
                                            CityID = _CityService.GetCityID(dt.Rows[row]["CityName"].ToString());
                                            if (CountryID != 0 && StateID != 0 && CityID != 0)
                                            {
                                                bool IsStore = _StoreService.CheckName(dt.Rows[row]["StoreName"].ToString());
                                                if (!IsStore)
                                                {
                                                    count++;
                                                    StoreMasterModel objStoreMasterModel = new StoreMasterModel();
                                                    objStoreMasterModel.StoreName = dt.Rows[row]["StoreName"].ToString().Trim();
                                                    objStoreMasterModel.Address = dt.Rows[row]["Address"].ToString().Trim();
                                                    objStoreMasterModel.Address2 = dt.Rows[row]["Address2"].ToString().Trim();
                                                    objStoreMasterModel.Phone = dt.Rows[row]["Phone"].ToString().Trim();
                                                    objStoreMasterModel.Fax = dt.Rows[row]["Fax"].ToString().Trim();
                                                    objStoreMasterModel.ZipCode = dt.Rows[row]["ZipCode"].ToString().Trim();
                                                    objStoreMasterModel.City = CityID;
                                                    objStoreMasterModel.State = StateID;
                                                    objStoreMasterModel.Country = CountryID;
                                                    var add = _StoreService.AddStore(objStoreMasterModel, 1);
                                                }
                                            }
                                        }
                                        ClsCommon.MsgBox("Information","Total " + count + " Store imported successfully.!",false);

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

        private void btnExport_Click(object sender, EventArgs e)
        {
            //EXPORT XLS
            try
            {
                DataTable dt = new DataTable();
                dt = tableLoad();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xlsx (*.xlsx)|*.xlsx";
                sfd.FileName = "Store";
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
                        wb.Worksheets.Add(dt, "Store");

                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information","Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Something went wrong while exporting category !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add(StoreMasterModelCont.StoreName, typeof(string));
                dt.Columns.Add(StoreMasterModelCont.Address, typeof(string));
                dt.Columns.Add(StoreMasterModelCont.Address2, typeof(string));
                dt.Columns.Add(StoreMasterModelCont.CityName, typeof(string));
                dt.Columns.Add(StoreMasterModelCont.StateName, typeof(string));
                dt.Columns.Add(StoreMasterModelCont.CountryName, typeof(string));
                dt.Columns.Add(StoreMasterModelCont.Phone, typeof(string));
                dt.Columns.Add(StoreMasterModelCont.Fax, typeof(string));
                dt.Columns.Add(StoreMasterModelCont.ZipCode, typeof(string));

                foreach (var item in lstStoreMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr[StoreMasterModelCont.StoreName] = item.StoreName;
                    dr[StoreMasterModelCont.Address] = item.Address;
                    dr[StoreMasterModelCont.Address2] = item.Address2;
                    dr[StoreMasterModelCont.CityName] = item.CityName;
                    dr[StoreMasterModelCont.StateName] = item.StateName;
                    dr[StoreMasterModelCont.CountryName] = item.CountryName;
                    dr[StoreMasterModelCont.Phone] = item.Phone;
                    dr[StoreMasterModelCont.Fax] = item.Fax;
                    dr[StoreMasterModelCont.ZipCode] = item.ZipCode;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        #endregion

        private void StoreGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }
        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddStore objFrmMetro_AddStore = new FrmMetro_AddStore();
                objFrmMetro_AddStore.PrimaryId = PrimaryId;
                objFrmMetro_AddStore.RefreshCountry();

                objFrmMetro_AddStore.txtStoreName.Text = StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.StoreName].Value.ToString().Trim();
                objFrmMetro_AddStore.txtAddress.Text = StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.Address].Value.ToString().Trim();
                objFrmMetro_AddStore.txtAddress2.Text = StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.Address2].Value.ToString().Trim();
                objFrmMetro_AddStore.txtPhone.Text = StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.Phone].Value.ToString().Trim();
                objFrmMetro_AddStore.txtFax.Text = StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.Fax].Value.ToString().Trim();
                objFrmMetro_AddStore.txtZipcode.Text = StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.ZipCode].Value.ToString().Trim();
                objFrmMetro_AddStore.txtAgeVerificationAge.Text = StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.AgeVarificationAge].Value.ToString().Trim();
                objFrmMetro_AddStore.txtDefaultTax.Text = (StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.DefaultTax].Value == null ? "0" : StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.DefaultTax].Value.ToString()).Trim();
                objFrmMetro_AddStore.txtDisclaimer.Text = (StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.Disclaimer].Value == null ? "" : StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.Disclaimer].Value.ToString()).Trim();

                if (StoreGrdView.Rows[RowIndex].Cells[CountryMasterModelCont.CountryID].Value != null)
                {
                    objFrmMetro_AddStore.cmbCountry.SelectedValue = StoreGrdView.Rows[RowIndex].Cells[CountryMasterModelCont.CountryID].Value;
                }
                if (StoreGrdView.Rows[RowIndex].Cells[StateMasterModelCont.StateID].Value != null)
                {
                    objFrmMetro_AddStore.cmbState.SelectedValue = StoreGrdView.Rows[RowIndex].Cells[StateMasterModelCont.StateID].Value;
                }
                objFrmMetro_AddStore.ToggleStoreTax.Checked = Convert.ToBoolean(StoreGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.IsStoreTax].Value.ToString().ToLower());
                objFrmMetro_AddStore.cmbCity.SelectedValue = StoreGrdView.Rows[RowIndex].Cells[CityMasterModelCont.CityID].Value;
                objFrmMetro_AddStore.ShowDialog();
                dataLoad();

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }
    }
    
}
