using ClosedXML.Excel;
using MetroFramework.Forms;
using SFPOS.BAL;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.spDataClasses;
using SFPOSWindows.Metro_Forms;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using SFPOSWindows.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmSection : MetroForm
    {
        #region Properties
        SectionService _SectionService = new SectionService();
        public static long PrimaryId = 0;
        public int DepartmentId = 0;
        public bool IsShowAll = false;
        public static bool IsFilterClick = false;
        public static bool isResetFilter = true;
        public static bool IsSearchFilterClick = false;
        public static bool IsFilterColAvailable = true;
        public bool first = false;
        public static int pageIndex = 1;
        IDictionary<string, bool> PreviousColumnFilterList = new Dictionary<string, bool>();
        public List<SectionMasterModel> lstSectionModel = new List<SectionMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ColumnFilterService _ColumnFilterService = new ColumnFilterService();
        IDictionary<string, string> dictGridColumnDisp = new Dictionary<string, string>();
        List<string> lstSearchFilterCols = new List<string>();
        public string OG_SearchFilterText = "";
        string[] EditablecolumnNames;
        public static bool IsEditMode = false;
        List<ClsFilterCheckBox> lstFilterBoxData = new List<ClsFilterCheckBox>();
        BindingSource bs = new BindingSource();


        #endregion

        #region Events

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                IsShowAll = true;
                btnExport.Enabled = true;
                btnImport.Enabled = true;
                btnColumnFilter.Enabled = true;
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void SectionGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (SectionGrdView.Columns[e.ColumnIndex].ToolTipText == "Edit")
                {
                    UpdateData(e.RowIndex);
                }
                if (SectionGrdView.Columns[e.ColumnIndex].ToolTipText == "Delete")
                {
                    SectionMasterModel objSectionMasterModel = new SectionMasterModel();
                    if (PrimaryId > 0)
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            objSectionMasterModel.SectionID = PrimaryId;
                            var add = _SectionService.AddSection(objSectionMasterModel, 3);
                            UpdateLog();
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                            }
                        }
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void SectionGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt64(SectionGrdView.Rows[e.RowIndex].Cells[SectionMasterModelCont.SectionID].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }

        }

        private void txtSearchSectionName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SeachStr = txtSearchSectionName.Text;
                if (SeachStr != null && SeachStr != CommonModelCont.EmptyString)
                {
                    //List<SP_GetSectionList_Result_Model> lstSectionModel = new List<SP_GetSectionList_Result_Model>();
                    //IsShowAll = false;
                    lstSectionModel = _SectionService.SectionDetail(DepartmentId).Select(s => new SectionMasterModel {
                        SectionID = s.SectionID,
                        SectionName = s.SectionName,
                        AgeVarificationAge = s.AgeVarificationAge,
                        TaxGroupID = s.TaxGroupID,
                        TaxGroupName = s.TaxGroupName,
                        DepartmentID = s.DepartmentID,
                        Ramark = s.Ramark,
                        IsActive = s.IsActive,
                        IsDelete = s.IsDelete,
                        CreatedDate = s.CreatedDate,
                        CreatedBy = s.CreatedBy,
                        UpdatedDate = s.UpdatedDate,
                        UpdatedBy = s.UpdatedBy,
                        DepartmentName = s.DepartmentName,
                        IsFoodStamp = s.IsFoodStamp,
                        UnitMeasureID = s.UnitMeasureID
                    } ).ToList();


                    first = false;

                    txtSearchFilter.Text = string.Empty;
                    lstFilterBoxData.Clear();
                    btnExport.Enabled = true;
                    btnImport.Enabled = true;
                    btnColumnFilter.Enabled = true;
                    SectionGrdView.DataSource = lstSectionModel
                        .Where(c => c.SectionName.ToLower().StartsWith(SeachStr.ToLower()))
                        .Select(o => new
                        {
                            SectionID = o.SectionID,
                            SectionName = o.SectionName,
                            AgeVarificationAge = o.AgeVarificationAge,
                            TaxGroupID = o.TaxGroupID,
                            TaxGroupName = o.TaxGroupName,
                            DepartmentID = o.DepartmentID,
                            Ramark = o.Ramark,
                            IsActive = o.IsActive,
                            //IsDelete = o.IsDelete,
                            //CreatedDate = o.CreatedDate,
                            //CreatedBy = o.CreatedBy,
                            //UpdatedDate = o.UpdatedDate,
                            //UpdatedBy = o.UpdatedBy,
                            DepartmentName = o.DepartmentName,
                            IsFoodStamp = o.IsFoodStamp,
                            UnitMeasureID = o.UnitMeasureID
                        }).ToList();

                    if (SectionGrdView.Columns[0].Name == "")
                    {
                        SectionGrdView.Columns.RemoveAt(0);
                    }

                    if (SectionGrdView.Rows.Count > 0 && (first == false))
                    {

                        DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                        var bmpEdit = new Bitmap(Resources.edit);
                        imgEdit.Image = bmpEdit;
                        imgEdit.ToolTipText = "Edit";
                        SectionGrdView.Columns.Add(imgEdit);
                        imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                        if (LoginInfo.RoleType == "Administrator")
                        {
                            if (SectionGrdView.Columns[0].Name == "")
                            {
                                SectionGrdView.Columns.RemoveAt(0);
                            }
                            DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                            var bmp = new Bitmap(Resources.delete);
                            imgDelete.Image = bmp;
                            imgDelete.ToolTipText = "Delete";
                            SectionGrdView.Columns.Add(imgDelete);
                            imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            first = true;
                        }

                        first = true;
                    }
                    grdHideColumn();
                }
                else
                {
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void MetrobtnAdd_Click(object sender, EventArgs e)
        {
            FrmMetro_AddSection objFrmMetro_AddSection = new FrmMetro_AddSection();
            objFrmMetro_AddSection.DepartmentId = DepartmentId;

            objFrmMetro_AddSection.ShowDialog();
            dataLoad();
        }

        #endregion

        #region Functions
        public FrmSection()
        {
            InitializeComponent();
            isResetFilter = true;
            IsFilterClick = false;
            IsSearchFilterClick = false;
            IsEditMode = false;
            bindColumnFiltersChecklistBox();
            getGridColumnType();
            if (LoginInfo.RoleType == "Administrator")
            {
                btnEditList.Visible = true;
            }
        }

        public void dataLoad()
        {
            try
            {
                //List<SP_GetSectionList_Result_Model> lstSectionModel = new List<SP_GetSectionList_Result_Model>();
                
                lstSectionModel = _SectionService.SectionDetail(IsShowAll ? 0 : DepartmentId).Select(s => new SectionMasterModel
                {
                    SectionID = s.SectionID,
                    SectionName = s.SectionName,
                    AgeVarificationAge = s.AgeVarificationAge,
                    TaxGroupID = s.TaxGroupID,
                    TaxGroupName = s.TaxGroupName,
                    DepartmentID = s.DepartmentID,
                    Ramark = s.Ramark,
                    IsActive = s.IsActive,
                    IsDelete = s.IsDelete,
                    CreatedDate = s.CreatedDate,
                    CreatedBy = s.CreatedBy,
                    UpdatedDate = s.UpdatedDate,
                    UpdatedBy = s.UpdatedBy,
                    DepartmentName = s.DepartmentName,
                    IsFoodStamp = s.IsFoodStamp,
                    UnitMeasureID = s.UnitMeasureID
                }).ToList(); 

                first = false;
                SectionGrdView.DataSource = lstSectionModel.Select(o => new
                {
                    SectionID = o.SectionID,
                    SectionName = o.SectionName,
                    AgeVarificationAge = o.AgeVarificationAge,
                    TaxGroupID = o.TaxGroupID,
                    TaxGroupName = o.TaxGroupName,
                    DepartmentID = o.DepartmentID,
                    Ramark = o.Ramark,
                    IsActive = o.IsActive,
                    //IsDelete = o.IsDelete,
                    //CreatedDate = o.CreatedDate,
                    //CreatedBy = o.CreatedBy,
                    //UpdatedDate = o.UpdatedDate,
                    //UpdatedBy = o.UpdatedBy,
                    DepartmentName = o.DepartmentName,
                    IsFoodStamp = o.IsFoodStamp,
                    UnitMeasureID = o.UnitMeasureID
                }).ToList();

                if (SectionGrdView.Columns[0].Name == "")
                {
                    SectionGrdView.Columns.RemoveAt(0);
                }

                if (SectionGrdView.Rows.Count > 0 && (first == false))
                {

                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    imgEdit.ToolTipText = "Edit";
                    SectionGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    if (LoginInfo.RoleType == "Administrator")
                    {
                        if (SectionGrdView.Columns[0].Name == "")
                        {
                            SectionGrdView.Columns.RemoveAt(0);
                        }
                        DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                        var bmp = new Bitmap(Resources.delete);
                        imgDelete.Image = bmp;
                        imgDelete.ToolTipText = "Delete";
                        SectionGrdView.Columns.Add(imgDelete);
                        imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        first = true;
                    }

                    first = true;
                }
                grdHideColumn();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
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
            objFrmMetroMaster.ChangeSyncStatus("tbl_SectionMaster");
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

                            DepartmentService _DepartmentService = new DepartmentService();
                            DataTable uniqueCols = dt.DefaultView.ToTable(true, "DepartmentName");
                            string CName = "";
                            for (int row = 0; row < uniqueCols.Rows.Count; row++)
                            {
                                #region Check Department
                                bool IsCategory = _DepartmentService.CheckName(uniqueCols.Rows[row]["DepartmentName"].ToString());
                                if (!IsCategory)
                                {
                                    if (CName == "")
                                    {
                                        CName = uniqueCols.Rows[row]["DepartmentName"].ToString();
                                    }
                                    else
                                    {
                                        CName = ", " + uniqueCols.Rows[row]["DepartmentName"].ToString();
                                    }
                                }
                                #endregion
                            }
                            if (CName == "")
                            {
                                #region Add Sections
                                for (int row = 0; row < dt.Rows.Count; row++)
                                {
                                    long DepartmentID = 0;
                                    DepartmentID = _DepartmentService.GetDepartmentID(dt.Rows[row]["DepartmentName"].ToString());
                                    if (DepartmentID != 0)
                                    {
                                        SectionMasterModel objSectionModel = new SectionMasterModel();
                                        bool IsSection = _SectionService.CheckSectionName(dt.Rows[row]["SectionName"].ToString());
                                        if (!IsSection)
                                        {
                                            count++;
                                            objSectionModel.SectionName = dt.Rows[row]["SectionName"].ToString().Trim();
                                            objSectionModel.DepartmentID = DepartmentID;
                                            var add = _SectionService.AddSection(objSectionModel, 1);
                                        }
                                    }
                                    else
                                    {
                                        ClsCommon.MsgBox("Information", dt.Rows[row]["DepartmentName"].ToString() + "Department is not available!", false);
                                    }
                                }
                                ClsCommon.MsgBox("Information", "Total " + count + " Department imported successfully.!", false);
                                #endregion
                                dataLoad();
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", CName + " Department is not available!", false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
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
                sfd.FileName = "Section";
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
                        wb.Worksheets.Add(dt, "Section");
                        wb.Worksheet(1).Cells("A1:C1").Style.Fill.BackgroundColor = XLColor.LightBlue;
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information", "Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong while exporting Department !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            Dictionary<string, string> lstcol = new Dictionary<string, string>();
            try
            {
                foreach (var item in SectionGrdView.Columns)
                {
                    if (((System.Windows.Forms.DataGridViewColumn)item).Visible && ((System.Windows.Forms.DataGridViewColumn)item).Name != "")
                    {
                        string colhname = Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).HeaderText);
                        string colname = Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).Name);
                        lstcol.Add(colhname, colname);
                        dt.Columns.Add(colhname, typeof(string));
                    }
                }

                foreach (var item in lstSectionModel)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        
                        if (lstcol.ContainsKey(dt.Columns[i].ColumnName))
                        {
                            string propname = lstcol.Where(w => w.Key == dt.Columns[i].ColumnName).Select(s => s.Value).FirstOrDefault().ToString();
                            dr[dt.Columns[i].ColumnName] = item.GetPropertyValue(propname);
                        }

                        
                    }
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }

        #endregion

        private void SectionGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }

        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddSection objFrmMetro_AddSection = new FrmMetro_AddSection();
                objFrmMetro_AddSection.DepartmentId = DepartmentId;
                objFrmMetro_AddSection.PrimaryId = PrimaryId;
                objFrmMetro_AddSection.txtSectionName.Text = SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.SectionName].Value.ToString().Trim();
                objFrmMetro_AddSection.txtAgeVerificationAge.Text = SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.AgeVarificationAge].Value.ToString().Trim();
                if (SectionGrdView.Columns.Contains("TaxGroupID") && SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.TaxGroupID].Value != null)
                    objFrmMetro_AddSection.cmbTaxGroup.SelectedValue = SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.TaxGroupID].Value;
                objFrmMetro_AddSection.ShowDialog();
                this.first = false;
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        public void grdHideColumn()
        {
            try
            {
                IEnumerable<string> columnnames = null;
                if (IsFilterClick)
                {
                    foreach (var item in SectionGrdView.Columns)
                    {
                        foreach (object chkBoxListitem in chkBoxList.Items)
                        {
                            string FilterGridColMapName = GridColumnnameDispMapping(Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName), 0);

                            if (chkBoxList.Items.Contains(FilterGridColMapName))
                            {
                                if (chkBoxListitem.ToString() == FilterGridColMapName)
                                {
                                    if (chkBoxList.CheckedItems.Contains(chkBoxListitem))
                                    {
                                        SectionGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = true;
                                        ChangeGridColumnHeader(((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString());
                                    }
                                    else
                                    {
                                        if (SectionGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName].ToolTipText.ToString() == "")
                                        {
                                            SectionGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = false;
                                        }
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (SectionGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName].ToolTipText.ToString() == "")
                                {
                                    SectionGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = false;
                                }
                                break;
                            }

                        }
                    }
                    SectionGrdView.Refresh();
                }
                else
                {
                    columnnames = _ColumnFilterService.GetFilterColumnsList("frmSection");

                    if (columnnames != null && columnnames.Count() > 0)
                    {
                        foreach (var item in SectionGrdView.Columns)
                        {
                            string FilterGridColMapName = GridColumnnameDispMapping(Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName), 0);
                            if (columnnames.Contains(FilterGridColMapName))
                            {
                                SectionGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = true;
                                ChangeGridColumnHeader(((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString());
                            }
                            else
                            {
                                if (SectionGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName].ToolTipText.ToString() == "")
                                {
                                    SectionGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = false;
                                }
                            }

                        }
                    }
                    else
                    {
                        IsFilterColAvailable = false;
                        grdHideColumnsOrg();
                    }
                }
                if (SectionGrdView.Columns[0].Name == "")
                {
                    SectionGrdView.Columns.RemoveAt(0);
                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    imgEdit.ToolTipText = "Edit";
                    SectionGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    if (LoginInfo.RoleType == "Administrator")
                    {
                        if (SectionGrdView.Columns[0].Name == "")
                        {
                            SectionGrdView.Columns.RemoveAt(0);
                        }
                        DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                        var bmp = new Bitmap(Resources.delete);
                        imgDelete.Image = bmp;
                        imgDelete.ToolTipText = "Delete";
                        SectionGrdView.Columns.Add(imgDelete);
                        imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                }
                //if (StartIndex == 0)
                //{

                //}
                //else
                //{
                //    ProductGrdView.FirstDisplayedScrollingRowIndex = StartIndex;
                //}
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        public void grdHideColumnsOrg()
        {
            try
            {
                SectionGrdView.Columns[SectionMasterModelCont.SectionID].Visible = false;
                SectionGrdView.Columns[DepartmentMasterModelCont.DepartmentID].Visible = false;
                SectionGrdView.Columns[DepartmentMasterModelCont.TaxGroupID].Visible = false;
                SectionGrdView.Columns[SectionMasterModelCont.Ramark].Visible = false;
                SectionGrdView.Columns[SectionMasterModelCont.AgeVarificationAge].Visible = false;
                SectionGrdView.Columns[SectionMasterModelCont.IsActive].Visible = false;
                SectionGrdView.Columns[DepartmentMasterModelCont.DepartmentName].Visible = false;
                SectionGrdView.Columns[SectionMasterModelCont.IsFoodStamp].Visible = false;
                SectionGrdView.Columns[SectionMasterModelCont.UnitMeasureID].Visible = false;
                SectionGrdView.Columns[SectionMasterModelCont.TaxGroupName].Visible = false;
                SectionGrdView.Columns["SectionName"].HeaderText = "Name";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnColumnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                ColumnFilterpanel.Visible = true;
                ColumnFilterpanel.BringToFront();

                var columnnames = _ColumnFilterService.GetFilterColumnsList("frmSection");
                for (int i = 0; i < chkBoxList.Items.Count; i++)
                {
                    if (PreviousColumnFilterList != null && PreviousColumnFilterList.Count > 0)
                    {
                        foreach (KeyValuePair<string, bool> kvp in PreviousColumnFilterList)
                        {
                            if (chkBoxList.Items[i].ToString() == kvp.Key)
                            {
                                if (isResetFilter)
                                {
                                    if (SectionGrdView != null && SectionGrdView.Rows.Count > 0)
                                    {
                                        if (SectionGrdView.Columns.Contains(chkBoxList.Items[i].ToString()) == true && SectionGrdView.Columns[chkBoxList.Items[i].ToString()].Visible == true)
                                        {
                                            chkBoxList.SetItemChecked(i, kvp.Value);
                                        }
                                    }
                                }
                                else
                                {
                                    if (columnnames.Contains(chkBoxList.Items[i].ToString()))
                                    {
                                        chkBoxList.SetItemChecked(i, kvp.Value);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (columnnames.Contains(chkBoxList.Items[i].ToString()))
                        {
                            chkBoxList.SetItemChecked(i, true);
                        }
                        else
                        {
                            chkBoxList.SetItemChecked(i, false);
                        }
                    }
                }
                bool IsSelected = false;
                for (int i = 0; i < cmbSearchFilter.Items.Count; i++)
                {
                    string ccbcolval = cmbSearchFilter.Items[i].ToString();
                    if (lstSearchFilterCols.Count > 0 && lstSearchFilterCols.Contains(ccbcolval))
                    {
                        cmbSearchFilter.SelectedIndex = i;
                        IsSelected = true;
                    }
                    if (!IsSelected)
                    {
                        cmbSearchFilter.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ColumnFilterpanel.Visible = false;
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAppliedfilter();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void SaveAppliedfilter()
        {
            try
            {
                IsFilterClick = true;
                cmbSearchFilter.Items.Clear();
                cmbSearchFilter.Items.Add("--Please Select--");
                if (SectionGrdView != null && SectionGrdView.Rows.Count > 0)
                {
                    grdHideColumn();
                }
                isResetFilter = false;
                ColumnFilterpanel.Visible = false;
                bool ischkboxitemchecked = false;
                PreviousColumnFilterList = new Dictionary<string, bool>();
                for (int i = 0; i < chkBoxList.Items.Count; i++)
                {
                    string chklistname = chkBoxList.Items[i].ToString();
                    if (chkBoxList.GetItemChecked(i))
                    {
                        if (SectionGrdView != null && SectionGrdView.Columns.Contains(chklistname) == true && SectionGrdView.Columns[chklistname].Visible == true)
                        {
                            chkBoxList.SetItemChecked(i, true);
                        }
                        //CCBoxItem itemcc = new CCBoxItem(chklistname, i);
                        if (chkBoxList.Items[i].ToString() != "UpdatedDate" && chkBoxList.Items[i].ToString() != "CreatedDate")
                        {
                            cmbSearchFilter.Items.Add(chklistname);
                        }
                        PreviousColumnFilterList.Add(chklistname, true);
                        ischkboxitemchecked = true;
                    }
                    else
                    {
                        chkBoxList.SetItemChecked(i, false);
                        PreviousColumnFilterList.Add(chklistname, false);
                    }
                }
                //ccb.MaxDropDownItems = 5;
                //ccb.DisplayMember = "Name";
                //ccb.ValueSeparator = ", ";
                cmbSearchFilter.SelectedIndex = 0;
                if (!ischkboxitemchecked)
                {
                    ClsCommon.MsgBox("Information", "Select atleast one column filter !!!", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void bindColumnFiltersChecklistBox()
        {
            try
            {
                //New 
                chkBoxList.Items.Clear();
                cmbSearchFilter.Items.Clear();
                cmbSearchFilter.Items.Add("--Please Select--");
                IEnumerable<string> columnnames = null;
                IEnumerable<string> SelectedFilterCol = null;
                if (lstFilterBoxData.Count == 0)
                {
                    lstFilterBoxData = _ColumnFilterService.GetFilterMasterColumnsList("frmSection");
                }
                SelectedFilterCol = _ColumnFilterService.GetFilterColumnsList("frmSection");
                columnnames = lstFilterBoxData.Select(s => s.FilterColumnsName).ToList();
                //IEnumerable<string> second = new[] { "DepartmentName", "SectionName", "UnitMeasureCode", "TaxGroupName" };
                //columnnames = columnnames.Concat(second);

                int i = 0;

                foreach (var item in columnnames)
                {
                    if (item.ToString() != "UpdatedDate" && item.ToString() != "CreatedDate")
                    {
                        chkBoxList.Items.Add(item.ToString());
                        if (SelectedFilterCol != null && SelectedFilterCol.Count() > 0)
                        {
                            if (SelectedFilterCol.Contains(item))
                            {
                                chkBoxList.SetItemChecked(i, true);
                                cmbSearchFilter.Items.Add(Convert.ToString(item));
                                i++;
                            }
                            else
                            {
                                chkBoxList.SetItemChecked(i, false);
                            }
                        }
                    }
                }
                //ccb.MaxDropDownItems = 5;
                //ccb.DisplayMember = "Name";
                //ccb.ValueSeparator = ", ";
                cmbSearchFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ccb_DropDownClosed(object sender, EventArgs e)
        {
            txtOut.AppendText("DropdownClosed\r\n");
            //txtOut.AppendText(string.Format("value changed: {0}\r\n", ccb.ValueChanged));
            txtOut.AppendText(string.Format("value: {0}\r\n", cmbSearchFilter.Text));
            // Display all checked items.
            StringBuilder sb = new StringBuilder("Items checked: ");
            //foreach (CCBoxItem item in ccb.CheckedItems)
            //{
            //    sb.Append(item.Name).Append(ccb.ValueSeparator);
            //}
            //sb.Remove(sb.Length - ccb.ValueSeparator.Length, ccb.ValueSeparator.Length);
            txtOut.AppendText(sb.ToString());
            txtOut.AppendText("\r\n");
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchFilter.Text))
                {
                    if (cmbSearchFilter.SelectedIndex != 0)
                    {
                        //if (PreviousColumnFilterList != null && PreviousColumnFilterList.Count > 0)
                        //{
                        //    SaveAppliedfilter();
                        //}
                        bool isNumeric, isBool, isDecimal;
                        int numericval = 0;
                        decimal decimalval = 0;
                        bool booleanval = false;

                        isNumeric = int.TryParse(txtSearchFilter.Text.Trim(), out numericval);
                        isBool = bool.TryParse(txtSearchFilter.Text.Trim(), out booleanval);
                        isDecimal = decimal.TryParse(txtSearchFilter.Text.Trim(), out decimalval);

                        DataTable st = ClsCommon.ListToDataTable(_SectionService.SectionDetail(DepartmentId));
                        DataView dv = new DataView(st);
                        var kd = string.Empty;
                        lstSearchFilterCols.Clear();
                    //foreach (CCBoxItem item in ccb.CheckedItems)
                    //{
                            string selectedname = Convert.ToString(cmbSearchFilter.SelectedItem);
                            string tablecolval = GridColumnnameDispMapping(selectedname, 1);
                            string type = dv.Table.Columns[tablecolval].DataType.ToString();
                            lstSearchFilterCols.Add(selectedname);
                            if (type.ToLower().Contains("int") && isNumeric)
                            {
                                if (kd != "")
                                {
                                    kd += " OR ";
                                }
                                kd += string.Format(tablecolval + "={0}", txtSearchFilter.Text);
                            }
                            else if (type.ToLower().Contains("decimal") && isDecimal)
                            {
                                if (kd != "")
                                {
                                    kd += " OR ";
                                }
                                kd += string.Format(tablecolval + "= {0}", txtSearchFilter.Text);

                            }
                            else if (type.ToLower().Contains("bit") && isBool)
                            {
                                if (kd != "")
                                {
                                    kd += " OR ";
                                }
                                kd += string.Format(tablecolval + "= {0}", txtSearchFilter.Text);
                            }
                            else if (type.ToLower().Contains("string"))
                            {
                                if (kd != "")
                                {
                                    kd += " OR ";
                                }
                                kd += string.Format(tablecolval + " LIKE '%{0}%'", txtSearchFilter.Text);
                            }
                        //}
                        if (!string.IsNullOrEmpty(kd))
                        {
                            dv.RowFilter = kd;
                            first = false;
                            SectionGrdView.DataSource = null;
                            SectionGrdView.DataSource = dv;
                            //txtSearchFilter.Text = string.Empty;
                            ColumnFilterpanel.Visible = false;
                            IsSearchFilterClick = true;
                            grdHideColumn();
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "No result found !!!", false);
                        }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Selete atleast one search filter !!!", false);
                }

            }
                else
                {
                    ClsCommon.MsgBox("Information", "Please enter text search filter !!!", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnSaveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                var columnnames = _ColumnFilterService.GetFilterColumnsList("frmSection");

                for (int i = 0; i < chkBoxList.Items.Count; i++)
                {
                    tbl_FilterColumnsModel Obj = new tbl_FilterColumnsModel();

                    if (columnnames.Contains(chkBoxList.Items[i].ToString()))
                    {
                        Obj.FilterColumnsName = chkBoxList.Items[i].ToString();
                        Obj.FilterPageName = "frmSection";
                        if (chkBoxList.GetItemChecked(i))
                        {
                            Obj.CreatedBy = LoginInfo.UserId;
                            Obj.CreatedDate = DateTime.Now;
                            Obj.IsActive = true;
                        }
                        else
                        {
                            Obj.CreatedBy = LoginInfo.UserId;
                            Obj.UpdatedBy = LoginInfo.UserId;
                            Obj.UpdatedDate = DateTime.Now;
                            Obj.IsActive = false;
                        }
                        Obj.IsMaster = false;
                        if (lstFilterBoxData.Any(w => w.FilterColumnsName == Obj.FilterColumnsName))
                            Obj.SeqNo = lstFilterBoxData.Where(w => w.FilterColumnsName == Obj.FilterColumnsName).Select(s => s.KeySeq).FirstOrDefault();
                        else
                            Obj.SeqNo = 0;
                        _ColumnFilterService.AddProductColumnFilter(Obj);
                    }
                    else
                    {
                        if (chkBoxList.GetItemChecked(i))
                        {
                            Obj.FilterColumnsName = chkBoxList.Items[i].ToString();
                            Obj.FilterPageName = "frmSection";
                            Obj.CreatedBy = LoginInfo.UserId;
                            Obj.CreatedDate = DateTime.Now;
                            Obj.IsActive = true;
                            Obj.IsMaster = false;
                            if (lstFilterBoxData.Any(w => w.FilterColumnsName == Obj.FilterColumnsName))
                                Obj.SeqNo = lstFilterBoxData.Where(w => w.FilterColumnsName == Obj.FilterColumnsName).Select(s => s.KeySeq).FirstOrDefault();
                            else
                                Obj.SeqNo = 0;

                            _ColumnFilterService.AddProductColumnFilter(Obj);
                        }
                    }
                }
                if (SectionGrdView != null && SectionGrdView.Rows.Count > 0)
                {
                    grdHideColumn();
                }
                bindColumnFiltersChecklistBox();
                ColumnFilterpanel.Visible = false;

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ChangeGridColumnHeader(string columnName)
        {
            if (SectionGrdView.Columns.Contains(columnName))
            {
                SectionGrdView.Columns[columnName].HeaderText = GridColumnnameDispMapping(columnName, 0);
            }
        }

        private string GridColumnnameDispMapping(string Searchtext, int SearchBy) //SearchBy 0 : By Key,1 By Value
        {
            string retval = Searchtext;
            try
            {
                if (dictGridColumnDisp.Count == 0)
                {
                    dictGridColumnDisp.Add("SectionName", "Name");
                    dictGridColumnDisp.Add("AgeVarificationAge", "Age Varification Age");
                    dictGridColumnDisp.Add("DepartmentName", "Department");
                    //dictGridColumnDisp.Add("SectionName", "Section");
                    //dictGridColumnDisp.Add("UnitMeasureCode", "Unit");
                    dictGridColumnDisp.Add("TaxGroupName", "Tax");
                }
                if (SearchBy == 0)
                {
                    if (dictGridColumnDisp.ContainsKey(Searchtext))
                    {
                        retval = dictGridColumnDisp.Where(w => w.Key == Searchtext).Select(s => s.Value).FirstOrDefault().ToString();
                    }
                }
                else
                {
                    if (dictGridColumnDisp.Any(w => w.Value == Searchtext))
                    {
                        retval = dictGridColumnDisp.Where(w => w.Value == Searchtext).Select(s => s.Key).FirstOrDefault().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
            return retval;
        }

        private void SectionGrdView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;
                for (int i = 0; i < SectionGrdView.Rows.Count; i++)
                {
                    if (i == e.RowIndex)
                    {
                        saveUpdateGridData(e.RowIndex);
                    }
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
            
        }

        private void saveUpdateGridData(int RowIndex)
        {
            try
            {
                SectionMasterModel objSectionMasterModel = new SectionMasterModel();
                objSectionMasterModel.SectionID = Convert.ToInt64(SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.SectionID].Value.ToString());
                objSectionMasterModel = _SectionService.SectionDetailBySecID(objSectionMasterModel.SectionID);
                foreach (var item in EditablecolumnNames)
                {
                    switch (item)
                    {
                        case SectionMasterModelCont.SectionID:
                            objSectionMasterModel.SectionID = Convert.ToInt64(SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.SectionID].Value.ToString());
                            break;

                        case SectionMasterModelCont.SectionName:

                            if (SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.SectionName].Value != null)
                            {
                                objSectionMasterModel.SectionName = SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.SectionName].Value.ToString().Trim();
                            }
                            else
                            { objSectionMasterModel.SectionName = objSectionMasterModel.SectionName; }

                            break;

                        case SectionMasterModelCont.DepartmentID:
                            if (SectionGrdView.Columns.Contains("DepartmentName"))
                            {
                                List<DepartmentMasterModel> depylist = new List<DepartmentMasterModel>();
                                depylist = (List<DepartmentMasterModel>)((System.Windows.Forms.DataGridViewComboBoxCell)((SectionGrdView.Rows[RowIndex].Cells["DepartmentName"]))).DataSource;
                                var items = (from deptitem in depylist where deptitem.DepartmentName == SectionGrdView.Rows[RowIndex].Cells["DepartmentName"].EditedFormattedValue.ToString() select deptitem.DepartmentID).FirstOrDefault();
                                objSectionMasterModel.DepartmentID = items;
                            }
                            break;
                        case "TaxGroupID":
                            if (SectionGrdView.Columns.Contains("TaxGroupName"))
                            {
                                List<TaxGroupMasterModel> Taxlist = new List<TaxGroupMasterModel>();
                                Taxlist = (List<TaxGroupMasterModel>)((System.Windows.Forms.DataGridViewComboBoxCell)((SectionGrdView.Rows[RowIndex].Cells["TaxGroupName"]))).DataSource;
                                var Taxlistitems = (from deptitem in Taxlist where deptitem.TaxGroupName == SectionGrdView.Rows[RowIndex].Cells["TaxGroupName"].EditedFormattedValue.ToString() select deptitem.TaxGroupID).FirstOrDefault();
                                objSectionMasterModel.TaxGroupID = Taxlistitems;
                            }

                            break;
                        //case ProductMasterModelCont.TaxGroupName:
                        //    objSectionMasterModel.TaxGroupName = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.TaxGroupName].Value.ToString().Trim();
                        //    break;
                        case SectionMasterModelCont.AgeVarificationAge:
                            if (!string.IsNullOrEmpty(Convert.ToString(SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.AgeVarificationAge].Value)))
                            {
                                int result;
                                if (int.TryParse(SectionGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.AgeVarificationAge].Value.ToString(), out result))
                                {
                                    objSectionMasterModel.AgeVarificationAge = result;
                                }
                                else
                                {
                                    objSectionMasterModel.AgeVarificationAge = objSectionMasterModel.AgeVarificationAge;
                                }
                                
                            }
                            break;
                    }
                }

                if (objSectionMasterModel.SectionID > 0)
                {
                    var add = _SectionService.AddSection(objSectionMasterModel, 2);
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void SectionGrdView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (SectionGrdView.CurrentCell is DataGridViewComboBoxCell)
            {
                SectionGrdView.CurrentCell.Value = SectionGrdView.CurrentCell.EditedFormattedValue;
                SectionGrdView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SectionGrdView.EndEdit();
            }
        }

        private void btnEditList_Click(object sender, EventArgs e)
        {
            try
            {
                if (SectionGrdView != null && SectionGrdView.Rows.Count > 0)
                {
                    IsEditMode = true;
                    SectionGrdView.CellValueChanged += SectionGrdView_CellValueChanged;

                    btnExport.Enabled = false;
                    btnImport.Enabled = false;
                    btnColumnFilter.Enabled = false;

                    DataTable dt = new DataTable();
                    for (int i = 0; i < SectionGrdView.ColumnCount; i++)
                    {
                        if (SectionGrdView.Columns[i].Visible == true)
                        {
                            if (!string.IsNullOrEmpty(SectionGrdView.Columns[i].Name.ToString()))
                            {
                                dt.Columns.Add(SectionGrdView.Columns[i].Name);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(SectionGrdView.Columns[i].Name.ToString()) && (SectionGrdView.Columns[i].Name.ToString() == "DepartmentID" || SectionGrdView.Columns[i].Name.ToString() == "SectionID" || SectionGrdView.Columns[i].Name.ToString() == "TaxGroupID" || SectionGrdView.Columns[i].Name.ToString() == "UnitMeasureID"))
                            {
                                dt.Columns.Add(SectionGrdView.Columns[i].Name);
                            }
                        }
                    }

                    EditablecolumnNames = (from dc in dt.Columns.Cast<DataColumn>()
                                           select dc.ColumnName).ToArray();
                    List<string> lstcol = new List<string>() { "DepartmentID", "TaxGroupID", "UnitMeasureID" };
                    List<string> lstcollist = new List<string>() { "DepartmentName", "UnitMeasureCode", "TaxGroupName" };
                    for (int i = 0; i < SectionGrdView.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        foreach (var item in EditablecolumnNames)
                        {
                            if (lstcollist.Contains(item))
                            {
                                switch (item)
                                {
                                    case "DepartmentName":
                                        dr[item] = SectionGrdView.Rows[i].Cells[item].Value == null ? DepartmentMasterModelCont.cmbDepartmentFirst : Convert.ToString(SectionGrdView.Rows[i].Cells[item].Value).Trim();
                                        break;
                                    case "UnitMeasureCode":
                                        dr[item] = SectionGrdView.Rows[i].Cells[item].Value == null ? UoMMasterModelCont.cmbUoMFirst : Convert.ToString(SectionGrdView.Rows[i].Cells[item].Value).Trim();
                                        break;
                                    case "TaxGroupName":
                                        dr[item] = SectionGrdView.Rows[i].Cells[item].Value == null ? TaxGroupMasterModelCont.cmbTaxGroupCodeFirst : Convert.ToString(SectionGrdView.Rows[i].Cells[item].Value).Trim();
                                        break;
                                }
                            }
                            else
                            {
                                dr[item] = SectionGrdView.Rows[i].Cells[item].Value == null && lstcol.Contains(item) ? "0" : Convert.ToString(SectionGrdView.Rows[i].Cells[item].Value).Trim();
                            }
                        }
                        dt.Rows.Add(dr);
                    }

                    SectionGrdView.Columns.Clear();

                    var ProductColCon = getGridColumnType();
                    int index = 0;
                    foreach (var item in ProductColCon)
                    {
                        if (EditablecolumnNames.Contains(item.ColumnName))
                        {

                            switch (item.valueName)
                            {
                                case "Text":
                                    DataGridViewTextBoxColumn curColText = new DataGridViewTextBoxColumn();
                                    curColText.Name = item.ColumnName;
                                    curColText.HeaderText = item.HeaderName;
                                    curColText.DataPropertyName = item.ColumnName;
                                    SectionGrdView.Columns.Insert(index, curColText);
                                    break;
                                case "CheckBox":
                                    DataGridViewCheckBoxColumn curColCheck = new DataGridViewCheckBoxColumn();
                                    curColCheck.Name = item.ColumnName;
                                    curColCheck.HeaderText = item.HeaderName;
                                    curColCheck.DataPropertyName = item.ColumnName;
                                    SectionGrdView.Columns.Insert(index, curColCheck);
                                    break;
                                case "ComboBox":
                                    DataGridViewComboBoxColumn comboBC = new DataGridViewComboBoxColumn();
                                    comboBC.Name = item.ColumnName;
                                    comboBC.HeaderText = item.HeaderName;
                                    comboBC = bindGridViewcombo(comboBC);
                                    SectionGrdView.Columns.Insert(index, comboBC);
                                    break;
                            }
                            index++;
                        }
                    }

                    SectionGrdView.DataSource = dt;
                    
                    SectionGrdView.Columns["DepartmentID"].Visible = false;
                    SectionGrdView.Columns["SectionID"].Visible = false;
                    SectionGrdView.Columns["TaxGroupID"].Visible = false;
                    SectionGrdView.Columns["UnitMeasureID"].Visible = false;

                    if (SectionGrdView.Columns.Contains("UpdatedDate"))
                        SectionGrdView.Columns["UpdatedDate"].Visible = false;
                    if (SectionGrdView.Columns.Contains("CreatedDate"))
                        SectionGrdView.Columns["CreatedDate"].Visible = false;

                    for (int i = 0; i < SectionGrdView.ColumnCount; i++)
                    {
                        SectionGrdView.Columns[i].ReadOnly = false;
                    }
                }
                else
                {
                    ClsCommon.MsgBox(AlertMessages.InformationAlert, "Section cart is empty.", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private List<productMasterModelCount> getGridColumnType()
        {
            List<productMasterModelCount> ps = new List<productMasterModelCount>() {
                
                new productMasterModelCount() { ColumnName = "SectionName" , valueName = "Text" , HeaderName = "Name" },
                //new productMasterModelCount() { ColumnName = "UnitMeasureCode" , valueName = "ComboBox" , HeaderName = "Unit" },
                new productMasterModelCount() { ColumnName = "AgeVarificationAge" , valueName = "Text" , HeaderName = "Age Varification Age" },
                new productMasterModelCount() { ColumnName = "TaxGroupName" , valueName = "ComboBox" , HeaderName = "Tax" },
                new productMasterModelCount() { ColumnName = "DepartmentName" , valueName = "ComboBox" , HeaderName = "Department" },
                new productMasterModelCount() { ColumnName = "IsActive" , valueName = "CheckBox" , HeaderName = "Active" },
                //new productMasterModelCount() { ColumnName = "IsFoodStamp" , valueName = "CheckBox" , HeaderName = "IsFoodStamp" },
        };
            return ps;
        }

        private DataGridViewComboBoxColumn bindGridViewcombo(DataGridViewComboBoxColumn obj)
        {
            try
            {
                switch (obj.Name)
                {
                    case "DepartmentName":

                        List<DepartmentMasterModel> lstDepartmentMasterModel = new List<DepartmentMasterModel>();
                        DepartmentService _DepartmentService = new DepartmentService();

                        lstDepartmentMasterModel = _DepartmentService.GetAllDepartment();
                        lstDepartmentMasterModel.Insert(0, new DepartmentMasterModel { DepartmentID = 0, DepartmentName = DepartmentMasterModelCont.cmbDepartmentFirst });
                        obj.DataSource = lstDepartmentMasterModel;
                        obj.DisplayMember = DepartmentMasterModelCont.DepartmentName;
                        obj.ValueMember = DepartmentMasterModelCont.DepartmentID;
                        obj.DataPropertyName = DepartmentMasterModelCont.DepartmentName;
                        break;

                    case "UnitMeasureCode":

                        List<UoMMasterModel> lstUoMMasterModel = new List<UoMMasterModel>();
                        UoMService _UoMService = new UoMService();

                        lstUoMMasterModel = _UoMService.GetAllUoM();
                        lstUoMMasterModel.Insert(0, new UoMMasterModel { UnitMeasureID = 0, UnitMeasureCode = UoMMasterModelCont.cmbUoMFirst });
                        obj.DataSource = lstUoMMasterModel;
                        obj.DisplayMember = UoMMasterModelCont.UnitMeasureCode;
                        obj.ValueMember = UoMMasterModelCont.UnitMeasureID;
                        obj.DataPropertyName = UoMMasterModelCont.UnitMeasureCode;
                        break;
                    case "TaxGroupName":

                        List<TaxGroupMasterModel> lstTaxGroupMasterModel = new List<TaxGroupMasterModel>();
                        TaxGroupService _TaxGroupService = new TaxGroupService();

                        lstTaxGroupMasterModel = _TaxGroupService.GetAllTaxGroup();
                        lstTaxGroupMasterModel.Insert(0, new TaxGroupMasterModel { TaxGroupID = 0, TaxGroupName = TaxGroupMasterModelCont.cmbTaxGroupCodeFirst });
                        obj.DataSource = lstTaxGroupMasterModel;
                        obj.DisplayMember = TaxGroupMasterModelCont.TaxGroupName;
                        obj.ValueMember = TaxGroupMasterModelCont.TaxGroupID;
                        obj.DataPropertyName = TaxGroupMasterModelCont.TaxGroupName;
                        break;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }

            return obj;
        }

        private void SectionGrdView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < chkBoxList.Items.Count; i++)
            //{
            //    chkBoxList.SetItemChecked(i, false);
            //}
            //for (int i = 0; i < cmbSearchFilter.Items.Count; i++)
            //{
            //    ccb.SetItemChecked(i, false);
            //}
            cmbSearchFilter.SelectedIndex = 0;
            txtSearchFilter.Text = string.Empty;
        }
    }
}
