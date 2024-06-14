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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmTaxGroup : Form
    {
        #region Properties
        TaxGroupService _TaxGroupService = new TaxGroupService();
        public static long PrimaryId = 0;
        List<TaxGroupMasterModel> lstTaxGroupMasterModel = new List<TaxGroupMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion

        #region Events
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            FrmMetro_AddTaxGroup objFrmMetro_AddTaxGroup = new FrmMetro_AddTaxGroup();
            objFrmMetro_AddTaxGroup.ShowDialog();
            dataLoad();
        }

        private void TaxGroupGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt64(TaxGroupGrdView.Rows[e.RowIndex].Cells[TaxGroupMasterModelCont.TaxGroupID].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
            }
        }

        private void TaxGroupGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                            TaxGroupMasterModel objTaxGroupMasterModel = new TaxGroupMasterModel();

                            objTaxGroupMasterModel.TaxGroupID = PrimaryId;
                            var add = _TaxGroupService.AddTaxGroup(objTaxGroupMasterModel, 3);
                            UpdateLog();
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess,false);
                            }
                            PrimaryId = 0;
                            dataLoad();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchTaxGroupCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchStr = txtSearchTaxGroupCode.Text;
                if (searchStr != null && searchStr != CommonModelCont.EmptyString && searchStr != AlertMessages.TaxGroupSearch)
                {
                    TaxGroupGrdView.DataSource = lstTaxGroupMasterModel
                        .Where(c => c.TaxGroupName.ToLower().StartsWith(searchStr.ToLower()))
                        .Select(c => new { TaxGroupID = c.TaxGroupID, TaxGroupName = c.TaxGroupName }).ToList();
                    TaxGroupGrdView.Columns[TaxGroupMasterModelCont.TaxGroupID].Visible = false;
                    TaxGroupGrdView.Columns[TaxGroupMasterModelCont.TaxGroupName].HeaderText = "Tax Group";
                }
                else
                {
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public FrmTaxGroup()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                lstTaxGroupMasterModel = _TaxGroupService.GetAllTaxGroup();
                TaxGroupGrdView.DataSource = lstTaxGroupMasterModel.Select(o => new { TaxGroupId = o.TaxGroupID, TaxGroupName = o.TaxGroupName }).ToList();
                TaxGroupGrdView.Columns[TaxGroupMasterModelCont.TaxGroupID].Visible = false;
                TaxGroupGrdView.Columns["TaxGroupName"].HeaderText = "Tax";

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
            }
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_TaxGroupMaster");
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
                            #region Add Role
                            for (int row = 0; row < dt.Rows.Count; row++)
                            {
                                TaxGroupMasterModel objTaxGroupMasterModel = new TaxGroupMasterModel();
                                bool IsRole = _TaxGroupService.CheckName(dt.Rows[row]["TaxGroupName"].ToString());
                                if (!IsRole)
                                {
                                    count++;
                                    objTaxGroupMasterModel.TaxGroupName = dt.Rows[row]["TaxGroupName"].ToString().Trim();
                                    var add = _TaxGroupService.AddTaxGroup(objTaxGroupMasterModel, 1);
                                }
                            }
                            ClsCommon.MsgBox("Information","Total " + count + " Tax Type imported successfully.!", false);
                            #endregion
                            dataLoad();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
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
                sfd.FileName = "Tax";
                sfd.Title = "Save an Excel File";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ClsCommon.MsgBox("Information","Data will be exported and you will be notified when it is ready.",false);
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
                        wb.Worksheets.Add(dt, "Tax Group");
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information","Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Something went wrong while exporting Tax Group !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add(TaxGroupMasterModelCont.TaxGroupName, typeof(string));

                foreach (var item in lstTaxGroupMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr[TaxGroupMasterModelCont.TaxGroupName] = item.TaxGroupName;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        #endregion

        private void TaxGroupGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }
        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddTaxGroup objFrmMetro_AddTaxGroup = new FrmMetro_AddTaxGroup();
                objFrmMetro_AddTaxGroup.txtTaxGroupCode.Text = TaxGroupGrdView.Rows[RowIndex].Cells[TaxGroupMasterModelCont.TaxGroupName].Value.ToString().Trim();
                objFrmMetro_AddTaxGroup.PrimaryId = PrimaryId;
                objFrmMetro_AddTaxGroup.ShowDialog();
                dataLoad();

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
