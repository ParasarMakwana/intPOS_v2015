using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SFPOS.BAL;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Link;
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
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmDepartment : Form
    {
        #region Properties
        private DepartmentService _DepartmentService = new DepartmentService();
        public static long PrimaryId = 0;
        List<DepartmentMasterModel> LstDepartmentMasterModel = new List<DepartmentMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        #endregion

        #region Events

        private void DepartmentGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt64(DepartmentGrdView.Rows[e.RowIndex].Cells[DepartmentMasterModelCont.DepartmentID].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }
        }

        private void DepartmentGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    FrmSection objFrmSection = new FrmSection();
                    objFrmSection.DepartmentId = Convert.ToInt32(PrimaryId);
                    objFrmSection.dataLoad();

                    //DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    //var bmpEdit = new Bitmap(Resources.edit);
                    //imgEdit.Image = bmpEdit;
                    //imgEdit.ToolTipText = "Edit";
                    //objFrmSection.SectionGrdView.Columns.Add(imgEdit);
                    //imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    //DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    //var bmp = new Bitmap(Resources.delete);
                    //imgDelete.Image = bmp;
                    //imgDelete.ToolTipText = "Delete";
                    //objFrmSection.SectionGrdView.Columns.Add(imgDelete);
                    //imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    objFrmSection.Text += DepartmentGrdView.Rows[e.RowIndex].Cells[DepartmentMasterModelCont.DepartmentName].Value.ToString().Trim();
                    objFrmSection.ShowDialog();
                }
                if (e.ColumnIndex == 1)
                {
                    UpdateData(e.RowIndex);
                }
                if (e.ColumnIndex == 2)
                {
                    if (PrimaryId > 0)
                    {
                        DialogResult result = MessageBox.Show(this.ParentForm, AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            DepartmentMasterModel objDepartmentMasterModel = new DepartmentMasterModel();

                            objDepartmentMasterModel.DepartmentID = PrimaryId;
                            objDepartmentMasterModel.DepartmentName = txtSearchDepartmentName.Text;
                            var add = _DepartmentService.DeleteAllDepartment(objDepartmentMasterModel);
                            UpdateLog();
                            if (add != null)
                            {
                                DialogResult res = MessageBox.Show(this.ParentForm.ParentForm, AlertMessages.DeleteSuccess, AlertMessages.SuccessAlert, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchDepartmentName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchStr = txtSearchDepartmentName.Text;
                if (SearchStr != null && SearchStr != CommonModelCont.EmptyString)
                {
                    DepartmentGrdView.DataSource = LstDepartmentMasterModel
                        .Where(c => c.DepartmentName.ToLower().Contains(SearchStr.ToLower()))
                        .Select(c => new
                        {
                            DepartmentID = c.DepartmentID,
                            DepartmentNo = c.DepartmentNo,
                            SubNo = c.SubNo,
                            DepartmentName = c.DepartmentName,
                            AgeVarificationAge = c.AgeVarificationAge,
                            IsActive = c.IsActive,
                            IsForceTax = c.IsForceTax,
                            ForcedTaxSection = c.ForcedTaxSection,
                            ForcedTaxSuffix = c.ForcedTaxSuffix,
                            TaxGroupID = c.TaxGroupID,
                            UnitMeasureID = c.UnitMeasureID,
                            IsFoodStamp = c.IsFoodStamp,
                            BtnCode = c.BtnCode,
                            DepartmentBtn = c.DepartmentBtn,
                            DepartmentBtnIndex=c.DepartmentBtnIndex
                        }).ToList();
                    hideColumn();
                }
                else
                {
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }
        }

        private void MetrobtnAdd_Click(object sender, EventArgs e)
        {
            FrmMetro_AddDepartment objFrmMetro_AddDepartment = new FrmMetro_AddDepartment(0);
            objFrmMetro_AddDepartment.ShowDialog();
            dataLoad();
        }

        private void metroBtnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

        #region Functions

        public FrmDepartment()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                LstDepartmentMasterModel = _DepartmentService.GetAllDepartment();
                DepartmentGrdView.DataSource = LstDepartmentMasterModel
                                            .Select(o => new
                                            {
                                                DepartmentId = o.DepartmentID,
                                                DepartmentNo = o.DepartmentNo,
                                                SubNo = o.SubNo,
                                                DepartmentName = o.DepartmentName,
                                                AgeVarificationAge = o.AgeVarificationAge,
                                                IsActive = o.IsActive,
                                                TaxGroupID = o.TaxGroupID,
                                                UnitMeasureID = o.UnitMeasureID,
                                                IsFoodStamp = o.IsFoodStamp,
                                                IsForceTax = o.IsForceTax,
                                                ForcedTaxSuffix = o.ForcedTaxSuffix,
                                                ForcedTaxSection = o.ForcedTaxSection,
                                                BtnCode = o.BtnCode,
                                                DepartmentBtn = o.DepartmentBtn,
                                                DepartmentBtnIndex=o.DepartmentBtnIndex
                                            }).ToList();
                hideColumn();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }
        }

        public void hideColumn()
        {
            DepartmentGrdView.Columns[DepartmentMasterModelCont.DepartmentID].Visible = false;
            DepartmentGrdView.Columns["IsActive"].Visible = false;
            DepartmentGrdView.Columns[DepartmentMasterModelCont.AgeVarificationAge].Visible = false;
            DepartmentGrdView.Columns["IsFoodStamp"].Visible = false;
            DepartmentGrdView.Columns["IsForceTax"].Visible = false;

            DepartmentGrdView.Columns["TaxGroupID"].Visible = false;
            DepartmentGrdView.Columns["UnitMeasureID"].Visible = false;
            DepartmentGrdView.Columns["DepartmentBtn"].Visible = false;
            DepartmentGrdView.Columns["DepartmentBtnIndex"].Visible = false;
            DepartmentGrdView.Columns["BtnCode"].Visible = false;
            DepartmentGrdView.Columns["ForcedTaxSuffix"].Visible = false;
            DepartmentGrdView.Columns["ForcedTaxSection"].Visible = false;
            DepartmentGrdView.Columns[DepartmentMasterModelCont.DepartmentName].HeaderText = "Name";
            DepartmentGrdView.Columns[DepartmentMasterModelCont.DepartmentNo].HeaderText = "Department No";
            DepartmentGrdView.Columns[DepartmentMasterModelCont.SubNo].HeaderText = "Sub No";
        }

        public void Clear()
        {
            dataLoad();
            PrimaryId = 0;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_DepartmentMaster");
        }
        #endregion

        #region Import/Export
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HRD=Yes'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'";
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt = tableLoad();

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "xlsx (*.xlsx)|*.xlsx";
                    sfd.FileName = "Department";
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
                            wb.Worksheets.Add(dt, "Department");
                            wb.Worksheet(1).Columns().AdjustToContents();

                            if (!String.IsNullOrWhiteSpace(sfd.FileName))
                                wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                            ClsCommon.MsgBox("Information","Data will be exported successfully !!!", false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClsCommon.MsgBox("Information","Something went wrong while exporting department !!!", false);
                    _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Something went wrong while exporting department !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add(DepartmentMasterModelCont.DepartmentNo, typeof(string));
                dt.Columns.Add(DepartmentMasterModelCont.SubNo, typeof(string));
                dt.Columns.Add(DepartmentMasterModelCont.Name, typeof(string));
                dt.Columns.Add(DepartmentMasterModelCont.AgeVarificationAge, typeof(string));
                dt.Columns.Add(DepartmentMasterModelCont.IsActive, typeof(string));
                dt.Columns.Add(DepartmentMasterModelCont.IsFoodStamp, typeof(string));
                foreach (var item in LstDepartmentMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr[DepartmentMasterModelCont.DepartmentNo] = item.DepartmentNo;
                    dr[DepartmentMasterModelCont.SubNo] = item.SubNo;
                    dr[DepartmentMasterModelCont.Name] = item.DepartmentName;
                    dr[DepartmentMasterModelCont.AgeVarificationAge] = item.AgeVarificationAge;
                    dr[DepartmentMasterModelCont.IsActive] = item.IsActive;
                    dr[DepartmentMasterModelCont.IsFoodStamp] = item.IsFoodStamp;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void FileOK_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
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


                            for (int row = 0; row < dt.Rows.Count; row++)
                            {
                                bool Isdepartment = _DepartmentService.CheckName(dt.Rows[row]["Name"].ToString());
                                if (!Isdepartment)
                                {
                                    count++;
                                    DepartmentMasterModel objDepartmentMasterModel = new DepartmentMasterModel();
                                    objDepartmentMasterModel.DepartmentName = dt.Rows[row]["Name"].ToString().Trim();
                                    objDepartmentMasterModel.DepartmentNo = Convert.ToInt64(dt.Rows[row][DepartmentMasterModelCont.DepartmentNo].ToString().Trim());
                                    objDepartmentMasterModel.SubNo = Convert.ToInt64(dt.Rows[row][DepartmentMasterModelCont.SubNo].ToString().Trim());
                                    objDepartmentMasterModel.AgeVarificationAge = Convert.ToInt32(String.IsNullOrEmpty(dt.Rows[row][DepartmentMasterModelCont.AgeVarificationAge].ToString()) ? 0 : Convert.ToInt32(dt.Rows[row][DepartmentMasterModelCont.AgeVarificationAge].ToString()));
                                    objDepartmentMasterModel.IsFoodStamp = Convert.ToBoolean(dt.Rows[row][DepartmentMasterModelCont.IsFoodStamp].ToString().Trim().ToLower());
                                    objDepartmentMasterModel.IsActive = Convert.ToBoolean(dt.Rows[row]["IsActive"].ToString().Trim().ToLower());
                                    var add = _DepartmentService.AddDepartment(objDepartmentMasterModel, 1);
                                }
                            }
                            dataLoad();
                        }
                    }
                }
                ClsCommon.MsgBox("Information","Total " + count + " Department imported successfully.!", false);
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            PdfPTable pdfTable = new PdfPTable(DepartmentGrdView.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            foreach (DataGridViewColumn column in DepartmentGrdView.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                //cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            foreach (DataGridViewRow row in DepartmentGrdView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdfTable.AddCell(cell.Value.ToString());
                }
            }

            SaveFileDialog svg = new SaveFileDialog();
            svg.ShowDialog();

            using (FileStream stream = new FileStream(svg.FileName + ".pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A1, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
        }

        private void Department_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ClsCommon.MsgBox("Information","Invalid Excel format !!!", false);
        }



        #endregion

        private void DepartmentGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }

        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddDepartment objFrmMetro_AddDepartment = new FrmMetro_AddDepartment(Convert.ToInt32(PrimaryId));
                objFrmMetro_AddDepartment.DepartmentID = Convert.ToInt32(PrimaryId);

                objFrmMetro_AddDepartment.txtDepartmentName.Text = DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.DepartmentName].Value.ToString().Trim();

                objFrmMetro_AddDepartment.txtAgeVerificationAge.Text = DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.AgeVarificationAge].Value == null ? "" : DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.AgeVarificationAge].Value.ToString().Trim();
                objFrmMetro_AddDepartment.txtDepartmentNo.Text = DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.DepartmentNo].Value == null ? "" : DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.DepartmentNo].Value.ToString().Trim();
                objFrmMetro_AddDepartment.txtSubNo.Text = DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.SubNo].Value == null ? "" : DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.SubNo].Value.ToString().Trim();
                objFrmMetro_AddDepartment.txtDeptBtnCode.Text = DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.BtnCode].Value == null ? "" : DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.BtnCode].Value.ToString().Trim();
                objFrmMetro_AddDepartment.txtDeptBtnCodeIndex.Text = DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.DepartmentBtnIndex].Value == null ? "" : DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.DepartmentBtnIndex].Value.ToString().Trim();
                objFrmMetro_AddDepartment.txtForcedTaxSuffix.Text = DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.ForcedTaxSuffix].Value == null ? "" : DepartmentGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.ForcedTaxSuffix].Value.ToString().Trim();
                //objFrmMetro_AddDepartment.LoadCmbSectionName(Convert.ToInt32(PrimaryId));
                if (DepartmentGrdView.Rows[RowIndex].Cells[UoMMasterModelCont.UnitMeasureID].Value != null)
                    objFrmMetro_AddDepartment.cmbUoM.SelectedValue = DepartmentGrdView.Rows[RowIndex].Cells[UoMMasterModelCont.UnitMeasureID].Value;
                if (DepartmentGrdView.Rows[RowIndex].Cells[TaxGroupMasterModelCont.TaxGroupID].Value != null)
                    objFrmMetro_AddDepartment.cmbTaxGroup.SelectedValue = DepartmentGrdView.Rows[RowIndex].Cells[TaxGroupMasterModelCont.TaxGroupID].Value;
                if (DepartmentGrdView.Rows[RowIndex].Cells["ForcedTaxSection"].Value != null)
                    objFrmMetro_AddDepartment.cmbSection.SelectedValue = DepartmentGrdView.Rows[RowIndex].Cells["ForcedTaxSection"].Value;

                if (DepartmentGrdView.Rows[RowIndex].Cells["IsActive"].Value.ToString().ToLower() == "true")
                    objFrmMetro_AddDepartment.ToggleIsActive.Checked = true;
                else
                    objFrmMetro_AddDepartment.ToggleIsActive.Checked = false;

                if (DepartmentGrdView.Rows[RowIndex].Cells["IsFoodStamp"].Value.ToString().ToLower() == "true")
                    objFrmMetro_AddDepartment.toggleFdStamp.Checked = true;
                else
                    objFrmMetro_AddDepartment.toggleFdStamp.Checked = false;
                if (DepartmentGrdView.Rows[RowIndex].Cells["IsForceTax"].Value.ToString().ToLower() == "true")
                    objFrmMetro_AddDepartment.chkIsForceTax.Checked = true;
                else
                    objFrmMetro_AddDepartment.chkIsForceTax.Checked = false;
                if (DepartmentGrdView.Rows[RowIndex].Cells["DepartmentBtn"].Value!=null)
                {
                    if (DepartmentGrdView.Rows[RowIndex].Cells["DepartmentBtn"].Value.ToString().ToLower() == "true")
                    {
                        objFrmMetro_AddDepartment.toggleBtnActive.CheckedChanged -= objFrmMetro_AddDepartment.toggleBtnActive_CheckedChanged;
                        objFrmMetro_AddDepartment.toggleBtnActive.Checked = true;
                        objFrmMetro_AddDepartment.toggleBtnActive.CheckedChanged += objFrmMetro_AddDepartment.toggleBtnActive_CheckedChanged;
                    }
                    else
                    {
                        objFrmMetro_AddDepartment.toggleBtnActive.Checked = false;
                    }
                }
                else
                {
                    objFrmMetro_AddDepartment.toggleBtnActive.Checked = false;
                }
                objFrmMetro_AddDepartment.ShowDialog();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
