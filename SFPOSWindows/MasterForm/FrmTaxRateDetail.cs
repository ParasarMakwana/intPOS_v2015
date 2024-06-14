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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmTaxRateDetail : Form
    {
        #region Properties
        public static long PrimaryId = 0;
        TaxRateService _TaxRateService = new TaxRateService();
        TaxGroupService _TaxGroupService = new TaxGroupService();
        TaxRateMasterModel objTaxRateMasterModel = new TaxRateMasterModel();
        ErrorProvider ep = new ErrorProvider();
        //private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        List<TaxRateMasterModel> lstTaxRateMasterModel = new List<TaxRateMasterModel>();
        List<TaxGroupMasterModel> lstTaxGroupMasterModel = new List<TaxGroupMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion

        #region Events

        private void btnADD_Click(object sender, EventArgs e)
        {
            FrmMetro_AddTaxRate objFrmMetro_AddTaxRate = new FrmMetro_AddTaxRate();
            objFrmMetro_AddTaxRate.LoadCmbTaxGroupCode();
            objFrmMetro_AddTaxRate.ShowDialog();
            dataLoad();
        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
               
                txtSearchTaxGroupCode.Text = AlertMessages.TaxrateSearch;
                txtSearchTaxGroupCode.ForeColor = Color.Silver;
               dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        private void TaxRateGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    //txtTax.Text = TaxRateGrdView.Rows[e.RowIndex].Cells[TaxRateMasterModelCont.Tax].Value.ToString().Trim();
                    //cmbTaxGroupCode.SelectedValue = TaxRateGrdView.Rows[e.RowIndex].Cells[TaxGroupMasterModelCont.TaxGroupID].Value;
                    //datePickerStartDate.Value = Convert.ToDateTime(TaxRateGrdView.Rows[e.RowIndex].Cells[TaxRateMasterModelCont.StartDate].Value);
                    //datePickerEndDate.Value = Convert.ToDateTime(TaxRateGrdView.Rows[e.RowIndex].Cells[TaxRateMasterModelCont.EndDate].Value);
                    PrimaryId = Convert.ToInt64(TaxRateGrdView.Rows[e.RowIndex].Cells[TaxRateMasterModelCont.TaxRateID].Value.ToString());
                   
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        private void TaxRateGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                            objTaxRateMasterModel.TaxRateID = PrimaryId;
                            var add = _TaxRateService.AddTaxRate(objTaxRateMasterModel, 3);
                            UpdateLog();
                            PrimaryId = 0;
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                            }
                            dataLoad();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchTaxGroupCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchStr = txtSearchTaxGroupCode.Text;
                if (searchStr != null && searchStr != CommonModelCont.EmptyString && searchStr != AlertMessages.TaxrateSearch)
                {
                    TaxRateGrdView.DataSource = lstTaxRateMasterModel
                        .Where(o => o.TaxGroupName.ToLower().StartsWith(searchStr.ToLower()))
                        .Select(o => new
                        {
                            TaxGroupID = o.TaxGroupID,
                            TaxRateID = o.TaxRateID,
                            Tax_Group_Code = o.TaxGroupName,
                            Tax = o.Tax,
                            StartDate = Convert.ToDateTime(o.StartDate).Date,
                            EndDate = Convert.ToDateTime(o.EndDate).Date
                        }).ToList();

                    TaxRateGrdView.Columns[TaxGroupMasterModelCont.TaxGroupID].Visible = false;
                    TaxRateGrdView.Columns[TaxRateMasterModelCont.TaxRateID].Visible = false;
                    TaxRateGrdView.Columns["Tax_Group_Code"].HeaderText = "Tax Group";
                    TaxRateGrdView.Columns["StartDate"].HeaderText = "Start Date";
                    TaxRateGrdView.Columns["EndDate"].HeaderText = "End Date";


                }
                else
                { dataLoad(); }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }


        #endregion

        #region Functions
        public FrmTaxRateDetail()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                lstTaxRateMasterModel = _TaxRateService.GetAllTaxRate();
                TaxRateGrdView.DataSource = lstTaxRateMasterModel
                    .Select(o => new
                    {
                        TaxGroupID = o.TaxGroupID,
                        TaxRateID = o.TaxRateID,
                        Tax_Group_Code = o.TaxGroupName,
                        Tax = o.Tax,
                        StartDate = Convert.ToDateTime(o.StartDate).Date,
                        EndDate = Convert.ToDateTime(o.EndDate).Date
                    }).ToList();
                TaxRateGrdView.Columns[TaxGroupMasterModelCont.TaxGroupID].Visible = false;
                TaxRateGrdView.Columns[TaxRateMasterModelCont.TaxRateID].Visible = false;

                TaxRateGrdView.Columns["Tax_Group_Code"].HeaderText = "Tax Group";
                TaxRateGrdView.Columns["StartDate"].HeaderText = "Start Date";
                TaxRateGrdView.Columns["EndDate"].HeaderText = "End Date";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_TaxRateMaster");
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

                            TaxGroupService _TaxGroupService = new TaxGroupService();
                            DataTable uniqueTaxGroup = dt.DefaultView.ToTable(true, "TaxGroupName");
                            string TName = "";
                            for (int row = 0; row < uniqueTaxGroup.Rows.Count; row++)
                            {
                                #region Check Role
                                bool IsRole = _TaxGroupService.CheckName(uniqueTaxGroup.Rows[row]["TaxGroupName"].ToString());
                                if (!IsRole)
                                {
                                    if (TName == "")
                                    {
                                        TName = uniqueTaxGroup.Rows[row]["TaxGroupName"].ToString();
                                    }
                                    else
                                    {
                                        TName = ", " + uniqueTaxGroup.Rows[row]["TaxGroupName"].ToString();
                                    }
                                }
                                #endregion
                            }

                            if (TName == "")
                            {
                                #region Add Employee
                                for (int row = 0; row < dt.Rows.Count; row++)
                                {
                                    long TaxGroupID = 0;
                                    TaxGroupID = _TaxGroupService.GetTaxGroupID(dt.Rows[row]["TaxGroupName"].ToString());
                                    if (TaxGroupID != 0)
                                    {
                                        bool IsTaxRate = _TaxRateService.CheckName(Convert.ToDecimal(dt.Rows[row]["Tax"].ToString()), Convert.ToDateTime(dt.Rows[row]["StartDate"].ToString()), Convert.ToDateTime(dt.Rows[row]["EndDate"].ToString()));
                                        if (!IsTaxRate)
                                        {
                                            count++;
                                            objTaxRateMasterModel.Tax = Convert.ToDecimal(dt.Rows[row]["Tax"].ToString());
                                            objTaxRateMasterModel.StartDate = Convert.ToDateTime(dt.Rows[row]["StartDate"].ToString());
                                            objTaxRateMasterModel.EndDate = Convert.ToDateTime(dt.Rows[row]["EndDate"].ToString());
                                            objTaxRateMasterModel.TaxGroupID = TaxGroupID;
                                            var add = _TaxRateService.AddTaxRate(objTaxRateMasterModel, 1);
                                        }
                                    }
                                    else
                                    {
                                        ClsCommon.MsgBox("Information",dt.Rows[row]["TaxGroupName"].ToString() + " Tax is not available!",false);
                                    }
                                }
                                ClsCommon.MsgBox("Information","Total " + count + " Tax Rate imported successfully.!", false);
                                #endregion
                                dataLoad();
                            }
                            else
                            {
                                if (TName != "")
                                    ClsCommon.MsgBox("Information",TName + " Tax are not available!", false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = tableLoad();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xlsx (*.xlsx)|*.xlsx";
                sfd.FileName = "TaxRateDetail";
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
                        wb.Worksheets.Add(dt, "TaxRateDetail");
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information","Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Something went wrong while exporting Tax Rate !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add(TaxRateMasterModelCont.TaxGroupName, typeof(string));
                dt.Columns.Add(TaxRateMasterModelCont.Tax, typeof(string));
                dt.Columns.Add(TaxRateMasterModelCont.StartDate, typeof(string));
                dt.Columns.Add(TaxRateMasterModelCont.IsActive, typeof(string));
                dt.Columns.Add(TaxRateMasterModelCont.EndDate, typeof(string));

                foreach (var item in lstTaxRateMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr[TaxRateMasterModelCont.TaxGroupName] = item.TaxGroupName;
                    dr[TaxRateMasterModelCont.Tax] = item.Tax;
                    dr[TaxRateMasterModelCont.StartDate] = item.StartDate;
                    dr[TaxRateMasterModelCont.IsActive] = item.IsActive;
                    dr[TaxRateMasterModelCont.EndDate] = item.EndDate;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        #endregion

        private void TaxRateGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }

        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddTaxRate objFrmMetro_AddTaxRate = new FrmMetro_AddTaxRate();
                objFrmMetro_AddTaxRate.PrimaryId = PrimaryId;
                objFrmMetro_AddTaxRate.txtTax.Text = TaxRateGrdView.Rows[RowIndex].Cells[TaxRateMasterModelCont.Tax].Value.ToString().Trim();
                objFrmMetro_AddTaxRate.cmbTaxGroupCode.SelectedValue = TaxRateGrdView.Rows[RowIndex].Cells[TaxGroupMasterModelCont.TaxGroupID].Value;
                objFrmMetro_AddTaxRate.datePickerStartDate.Value = Convert.ToDateTime(TaxRateGrdView.Rows[RowIndex].Cells[TaxRateMasterModelCont.StartDate].Value != null ? TaxRateGrdView.Rows[RowIndex].Cells[TaxRateMasterModelCont.StartDate].Value : DateTime.Now); 
                objFrmMetro_AddTaxRate.datePickerEndDate.Value = Convert.ToDateTime(TaxRateGrdView.Rows[RowIndex].Cells[TaxRateMasterModelCont.EndDate].Value != null ? TaxRateGrdView.Rows[RowIndex].Cells[TaxRateMasterModelCont.EndDate].Value : DateTime.Now);

                objFrmMetro_AddTaxRate.LoadCmbTaxGroupCode();
                objFrmMetro_AddTaxRate.ShowDialog();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
