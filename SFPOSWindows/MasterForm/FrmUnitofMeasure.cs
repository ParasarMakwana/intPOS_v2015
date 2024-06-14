using ClosedXML.Excel;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Link;
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
    public partial class FrmUnitofMeasure : Form
    {
        #region Properties
        UoMService _UoMService = new UoMService();
        ErrorProvider ep = new ErrorProvider();
        public static long PrimaryId = 0;
        UoMMasterModel objUoMMasterModel = new UoMMasterModel();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        List<UoMMasterModel> lstUoMMasterModel = new List<UoMMasterModel>();
        #endregion

        #region Events
        private void btnADD_Click(object sender, EventArgs e)
        {
            FrmMetro_AddUoM objFrmMetro_AddUoM = new FrmMetro_AddUoM();
            objFrmMetro_AddUoM.ShowDialog();
            dataLoad();
        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
            }
        }
        private void UoMGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    
                    PrimaryId = Convert.ToInt64(UoMGrdView.Rows[e.RowIndex].Cells[UoMMasterModelCont.UnitMeasureID].Value.ToString());
                   
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        private void UoMGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                            objUoMMasterModel.UnitMeasureID = PrimaryId;
                            var add = _UoMService.AddEditDeleteUoM(objUoMMasterModel, 3);
                            UpdateLog();
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                            }
                            PrimaryId = 0;
                            dataLoad();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchUoMName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchStr = txtSearchUoMName.Text;
                if (SearchStr != null && SearchStr != CommonModelCont.EmptyString && SearchStr != AlertMessages.UOMSearch)
                {
                    UoMGrdView.DataSource = lstUoMMasterModel
                        .Where(c => c.UnitMeasureCode.ToLower().StartsWith(SearchStr.ToLower()))
                        .Select(c => new { UnitMeasureID = c.UnitMeasureID, UnitMeasureCode = c.UnitMeasureCode, Description = c.Description }).ToList();
                    UoMGrdView.Columns[UoMMasterModelCont.UnitMeasureID].Visible = false;

                    UoMGrdView.Columns["UnitMeasureCode"].HeaderText = "Unit";
                }
                else
                {
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
            }
        }


        #endregion

        #region Functions
        public FrmUnitofMeasure()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                lstUoMMasterModel = _UoMService.GetAllUoM();
                UoMGrdView.DataSource = lstUoMMasterModel.Select(o => new { UnitMeasureID = o.UnitMeasureID, UnitMeasureCode = o.UnitMeasureCode, Description = o.Description }).ToList();
                UoMGrdView.Columns[UoMMasterModelCont.UnitMeasureID].Visible = false;
                UoMGrdView.Columns[UoMMasterModelCont.UnitMeasureCode].HeaderText = "Unit";

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
            }
        }
        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_UnitMeasureMaster");
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

                    case ".xls":
                        conStr = string.Format(Excel03ConString, filePath);
                        break;

                    case ".xlsx":
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
                            #region Add UoM
                            for (int row = 0; row < dt.Rows.Count; row++)
                            {
                                bool IsRole = _UoMService.CheckName(dt.Rows[row]["UnitMeasureCode"].ToString());
                                if (!IsRole)
                                {
                                    count++;
                                    objUoMMasterModel.Description = dt.Rows[row]["Description"].ToString().Trim();
                                    objUoMMasterModel.UnitMeasureCode = dt.Rows[row]["UnitMeasureCode"].ToString().Trim();

                                    var add = _UoMService.AddEditDeleteUoM(objUoMMasterModel, 1);
                                }
                            }
                            ClsCommon.MsgBox("Information","Total " + count + " UoM imported successfully.!",false);
                            #endregion
                            dataLoad();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = tableLoad();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xlsx (*.xlsx)|*.xlsx";
                sfd.FileName = "UnitOfMeasure";
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
                        wb.Worksheets.Add(dt, "UnitOfMeasure");
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information","Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Something went wrong while exporting UoM !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add(UoMMasterModelCont.Description, typeof(string));
                dt.Columns.Add(UoMMasterModelCont.UnitMeasureCode, typeof(string));
                foreach (var item in lstUoMMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr[UoMMasterModelCont.Description] = item.Description;
                    dr[UoMMasterModelCont.UnitMeasureCode] = item.UnitMeasureCode;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        #endregion

        private void UoMGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }
        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddUoM objFrmMetro_AddUoM = new FrmMetro_AddUoM();
                objFrmMetro_AddUoM.PrimaryId = PrimaryId;
                objFrmMetro_AddUoM.txtUoMName.Text = UoMGrdView.Rows[RowIndex].Cells[UoMMasterModelCont.UnitMeasureCode].Value.ToString().Trim();
                if (UoMGrdView.Rows[RowIndex].Cells[UoMMasterModelCont.Description].Value.ToString().Trim() != null)
                {
                    objFrmMetro_AddUoM.txtDescription.Text = UoMGrdView.Rows[RowIndex].Cells[UoMMasterModelCont.Description].Value.ToString().Trim();
                }
                else
                {
                    objFrmMetro_AddUoM.txtDescription.Text = CommonModelCont.EmptyString;
                }
                objFrmMetro_AddUoM.ShowDialog();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
