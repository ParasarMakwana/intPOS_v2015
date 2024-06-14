using ClosedXML.Excel;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using SFPOSWindows.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmCoupon : Form
    {

        private CouponService _CouponService = new CouponService();
        public static long PrimaryId = 0;
        List<CouponMasterModel> LstCouponMasterModel = new List<CouponMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();


        public FrmCoupon()
        {
            InitializeComponent();
        }

        private void MetrobtnAdd_Click(object sender, EventArgs e)
        {
            FrmMetro_AddCouponNew objFrmMetro_AddCoupon = new FrmMetro_AddCouponNew();
            objFrmMetro_AddCoupon.ShowDialog();
            dataLoad();
        }

        public void dataLoad()
        {
            try
            {
                LstCouponMasterModel = _CouponService.GetAllCoupon();
                CouponGrdView.DataSource = LstCouponMasterModel
                                            .Select(o => new
                                            {
                                                CouponID = o.CouponID,
                                                CoupenCode = o.CoupenCode,
                                                CouponName = o.CouponName,
                                                StartDate = o.StartDate,
                                                EndDate = o.EndDate,
                                                IsActive = o.IsActive,
                                                AvailableCount = o.AvailableCount,
                                                UsedCount = o.UsedCount,
                                                MinPurchaseAmt = o.MinPurchaseAmt,
                                                Discount = o.Discount,
                                                IsAllowMultipleTime = o.IsAllowMultipleTime,
                                                IsRestricted = o.IsRestricted,
                                                AllowAllDepartment = o.AllowAllDepartment,
                                                SelectedDepartment = o.SelectedDepartment
                                            }).ToList();

                CouponGrdView.Columns[CouponMasterModelCont.CouponID].Visible = false;
                CouponGrdView.Columns["IsActive"].Visible = false;
                CouponGrdView.Columns["CoupenCode"].HeaderText = "Code";
                CouponGrdView.Columns["CouponName"].HeaderText = "Name";
                CouponGrdView.Columns["MinPurchaseAmt"].HeaderText = "Min. Purchase Amt";
                CouponGrdView.Columns["IsAllowMultipleTime"].HeaderText = "Allow Multiple";
                CouponGrdView.Columns["IsRestricted"].HeaderText = "IsRestricted";
                CouponGrdView.Columns["AllowAllDepartment"].Visible = false;
                CouponGrdView.Columns["SelectedDepartment"].Visible = false;
                //CouponGrdView.Columns["IsAllowMultipleTime"].Visible = false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        public void hideColumn()
        {
            CouponGrdView.Columns[CouponMasterModelCont.CouponID].Visible = false;
            CouponGrdView.Columns["IsActive"].Visible = false;
            CouponGrdView.Columns[CouponMasterModelCont.CoupenCode].HeaderText = "Code";
            CouponGrdView.Columns[CouponMasterModelCont.CouponName].HeaderText = "Name";
            CouponGrdView.Columns[CouponMasterModelCont.MinPurchaseAmt].HeaderText = "Min. Purchase Amt";
        }

        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddCouponNew objFrmMetro_AddCoupon = new FrmMetro_AddCouponNew();
                objFrmMetro_AddCoupon.UpdateCouponID = Convert.ToInt32(PrimaryId);
                objFrmMetro_AddCoupon.CouponID = Convert.ToInt32(PrimaryId);
                objFrmMetro_AddCoupon.LoadData = true;
                objFrmMetro_AddCoupon.UpdateSelectedDepartment = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.SelectedDepartment].Value == null ? false : Convert.ToBoolean(Convert.ToString(CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.SelectedDepartment].Value).ToLower());
                objFrmMetro_AddCoupon.txtCouponName.Text = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.CouponName].Value.ToString().Trim();
                objFrmMetro_AddCoupon.txtCouponCode.Text = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.CoupenCode].Value.ToString().Trim();
                objFrmMetro_AddCoupon.datePickerStartDate.Value = Convert.ToDateTime(CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.StartDate].Value.ToString().Trim());
                objFrmMetro_AddCoupon.datePickerEndDate.Value = Convert.ToDateTime(CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.EndDate].Value.ToString().Trim());
                objFrmMetro_AddCoupon.txtMinPurAmt.Text = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.MinPurchaseAmt].Value.ToString().Trim();
                objFrmMetro_AddCoupon.txtDiscount.Text = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.Discount].Value.ToString().Trim();
                //objFrmMetro_AddCoupon.toggleIsMulUser.Text = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.IsAllowMultipleTime].Value.ToString().Trim();
                objFrmMetro_AddCoupon.toggleIsMulUser.Checked = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.IsAllowMultipleTime].Value == null ? false : Convert.ToBoolean(Convert.ToString(CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.IsAllowMultipleTime].Value).ToLower());
                objFrmMetro_AddCoupon.metroCustRest.Checked = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.IsRestricted].Value == null ? false : Convert.ToBoolean(Convert.ToString(CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.IsRestricted].Value).ToLower());
                objFrmMetro_AddCoupon.metroAllDep.Checked = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.AllowAllDepartment].Value == null ? false : Convert.ToBoolean(Convert.ToString(CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.AllowAllDepartment].Value).ToLower());
                objFrmMetro_AddCoupon.metroSelectedDep.Checked = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.SelectedDepartment].Value == null ? false : Convert.ToBoolean(Convert.ToString(CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.SelectedDepartment].Value).ToLower());
                //objFrmMetro_AddCoupon.UpdateSelectedDepartment = objFrmMetro_AddCoupon.metroSelectedDep.Checked;
                //objFrmMetro_AddCoupon.txtCouponFrequency.Text = CouponGrdView.Rows[RowIndex].Cells[CouponMasterModelCont.AvailableCount].Value.ToString().Trim();

                objFrmMetro_AddCoupon.ShowDialog();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
            }
        }
        private void metroBtnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchCouponName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchStr = txtSearchCouponName.Text;
                if (SearchStr != null && SearchStr != CommonModelCont.EmptyString)
                {
                    CouponGrdView.DataSource = LstCouponMasterModel
                        .Where(o => o.CouponName.ToLower().Contains(SearchStr.ToLower()) 
                                 || o.CoupenCode.ToLower().Contains(SearchStr.ToLower()))
                        .Select(o => new
                        {
                            CouponID = o.CouponID,
                            CoupenCode = o.CoupenCode,
                            CouponName = o.CouponName,
                            StartDate = o.StartDate,
                            EndDate = o.EndDate,
                            IsActive = o.IsActive,
                            AvailableCount = o.AvailableCount,
                            UsedCount = o.UsedCount,
                            MinPurchaseAmt = o.MinPurchaseAmt,
                            Discount = o.Discount
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        private void CouponGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt64(CouponGrdView.Rows[e.RowIndex].Cells[CouponMasterModelCont.CouponID].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        private void CouponGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                        DialogResult result = MessageBox.Show(this.ParentForm, AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            CouponMasterModel objCouponMasterModel = new CouponMasterModel();

                            objCouponMasterModel.CouponID = PrimaryId;
                           
                            var add = _CouponService.AddCoupon(objCouponMasterModel,3);
                         
                            if (add != null)
                            {
                                DialogResult res = MessageBox.Show(this.ParentForm.ParentForm, AlertMessages.DeleteSuccess, AlertMessages.SuccessAlert, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            dataLoad();
                            PrimaryId = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        private void CouponGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }

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
                    sfd.FileName = "Coupon";
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
                            wb.Worksheets.Add(dt, "Coupon");
                            wb.Worksheet(1).Columns().AdjustToContents();

                            if (!String.IsNullOrWhiteSpace(sfd.FileName))
                                wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                            ClsCommon.MsgBox("Information", "Data will be exported successfully !!!", false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClsCommon.MsgBox("Information", "Something went wrong while exporting Coupon !!!", false);
                    _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong while exporting Coupon !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add(CouponMasterModelCont.CoupenCode, typeof(string));
                dt.Columns.Add(CouponMasterModelCont.CouponName, typeof(string));
                dt.Columns.Add(CouponMasterModelCont.StartDate, typeof(string));
                dt.Columns.Add(CouponMasterModelCont.EndDate, typeof(string));
                dt.Columns.Add(CouponMasterModelCont.AvailableCount, typeof(string));
                dt.Columns.Add(CouponMasterModelCont.UsedCount, typeof(string));
                dt.Columns.Add(CouponMasterModelCont.MinPurchaseAmt, typeof(string));
                dt.Columns.Add(CouponMasterModelCont.Discount, typeof(string));
                dt.Columns.Add(CouponMasterModelCont.IsActive, typeof(string));
                foreach (var item in LstCouponMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr[CouponMasterModelCont.CoupenCode] = item.CoupenCode;
                    dr[CouponMasterModelCont.CouponName] = item.CouponName;
                    dr[CouponMasterModelCont.StartDate] = item.StartDate;
                    dr[CouponMasterModelCont.EndDate] = item.EndDate;
                    dr[CouponMasterModelCont.IsActive] = item.IsActive;
                    dr[CouponMasterModelCont.AvailableCount] = item.AvailableCount;
                    dr[CouponMasterModelCont.UsedCount] = item.UsedCount;
                    dr[CouponMasterModelCont.MinPurchaseAmt] = item.MinPurchaseAmt;
                    dr[CouponMasterModelCont.Discount] = item.Discount;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }

        private void FrmCoupon_Load(object sender, EventArgs e)
        {
            dataLoad();

            DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
            var bmpEdit = new Bitmap(Resources.edit);
            imgEdit.Image = bmpEdit;
            CouponGrdView.Columns.Add(imgEdit);
            imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
            var bmp = new Bitmap(Resources.delete);
            imgDelete.Image = bmp;
            CouponGrdView.Columns.Add(imgDelete);
            imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        }
    }
}
