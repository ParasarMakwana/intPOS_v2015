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
    public partial class FrmRole : Form
    {

        #region Properties
        int PrimaryId = 0;
        RoleService _RoleService = new RoleService();
        List<RoleMasterModel> lstRoleMasterModel = new List<RoleMasterModel>();
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }

        private void RoleGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt32(RoleGrdView.Rows[e.RowIndex].Cells[RoleMasterModelCont.RoleID].Value);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }

        private void RoleGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    UpdateData(e.RowIndex);
                }
                if (e.ColumnIndex == 1)
                {
                    RoleMasterModel objRoleMasterModel = new RoleMasterModel();
                    if (PrimaryId > 0)
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            objRoleMasterModel.RoleID = PrimaryId;
                            var add = _RoleService.AddRole(objRoleMasterModel, 3);
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }



        private void txtSearchRoleType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchStr = txtSearchRoleType.Text;
                if (SearchStr != null && SearchStr != CommonModelCont.EmptyString && SearchStr != AlertMessages.RoleSearch)
                {
                    RoleGrdView.DataSource = lstRoleMasterModel
                        .Where(o => o.RoleType.ToLower().StartsWith(SearchStr.ToLower()))
                        .Select(o => new { RoleID = o.RoleID, RoleType = o.RoleType }).ToList();
                    RoleGrdView.Columns[RoleMasterModelCont.RoleID].Visible = false;
                    RoleGrdView.Columns["RoleType"].HeaderText = "Role";
                    RoleGrdView.Columns["OverrideAmount"].HeaderText = "Override Amount";
                }
                else
                {
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }
        private void btnADD_Click(object sender, EventArgs e)
        {
            FrmMetro_AddRole objFrmMetro_AddRole = new FrmMetro_AddRole();
            objFrmMetro_AddRole.ShowDialog();
            dataLoad();
        }

        #endregion

        #region Functions
        public FrmRole()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                lstRoleMasterModel = _RoleService.GetAllRole();
                RoleGrdView.DataSource = lstRoleMasterModel.Select(o => new { RoleID = o.RoleID, RoleType = o.RoleType, OverrideAmount = o.OverrideAmount }).ToList();
                RoleGrdView.Columns[RoleMasterModelCont.RoleID].Visible = false;
                RoleGrdView.Columns["RoleType"].HeaderText = "Role";
                RoleGrdView.Columns["OverrideAmount"].HeaderText = "Override Amount";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_RoleMaster");
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
                            #region Add Role
                            for (int row = 0; row < dt.Rows.Count; row++)
                            {
                                RoleMasterModel objRoleMasterModel = new RoleMasterModel();
                                bool IsRole = _RoleService.CheckName(dt.Rows[row]["RoleType"].ToString());
                                if (!IsRole)
                                {
                                    count++;
                                    objRoleMasterModel.RoleType = dt.Rows[row]["RoleType"].ToString().Trim();
                                    var add = _RoleService.AddRole(objRoleMasterModel, 1);
                                }
                            }
                            ClsCommon.MsgBox("Information", "Total " + count + " Role imported successfully.!", false);
                            #endregion
                            dataLoad();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
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
                sfd.FileName = "Role";
                sfd.Title = "Save an Excel File";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ClsCommon.MsgBox("Information", "Data will be exported and you will be notified when it is ready.", false);
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            ClsCommon.MsgBox("Information", "It wasn't possible to write the data to the disk." + ex.Message, false);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Role");
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information", "Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong while exporting Roles !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }


        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add(RoleMasterModelCont.RoleType, typeof(string));
                //dt.Columns.Add(RoleMasterModelCont.OverwriteAmount, typeof(string));

                foreach (var item in lstRoleMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr[RoleMasterModelCont.RoleType] = item.RoleType;
                    //dr[RoleMasterModelCont.OverwriteAmount] = item.OverwriteAmount;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        #endregion

        private void RoleGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }
        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddRole objFrmMetro_AddRole = new FrmMetro_AddRole();
                objFrmMetro_AddRole.txtRoleType.Text = RoleGrdView.Rows[RowIndex].Cells[RoleMasterModelCont.RoleType].Value.ToString();
                objFrmMetro_AddRole.txtOverwriteAmount.Text = RoleGrdView.Rows[RowIndex].Cells[RoleMasterModelCont.OverrideAmount].Value.ToString();
                objFrmMetro_AddRole.PrimaryId = Convert.ToInt32(RoleGrdView.Rows[RowIndex].Cells[RoleMasterModelCont.RoleID].Value);
                objFrmMetro_AddRole.ShowDialog();
                this.Refresh();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
