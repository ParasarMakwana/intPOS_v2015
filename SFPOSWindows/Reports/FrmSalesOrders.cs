using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Properties;
using SFPOSWindows.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmSalesOrders : Form
    {
        public static long PrimaryId = 0;
        public static long EmployeeId = 0;

        public static string transactionNo = "";
        public static string CashierName = "";
        public static string Datetime = "";

        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        List<OrderMasterModel> objOrderMasterModel = new List<OrderMasterModel>();
        OrderScannerService _OrderScannerService = new OrderScannerService();

        public FrmSalesOrders()
        {
            InitializeComponent();
        }

        public void dataLoadToday()
        {
            try
            {

                objOrderMasterModel = _OrderScannerService.GetSaleOrderDetail(startDate.Value.Date, endDate.Value.Date);
                //SaleOrderGrdView.DataSource = objOrderMasterModel;
                SaleOrderGrdView.DataSource = objOrderMasterModel.Where(c => c.CreatedDate.Value.Date == DateTime.Now.Date)
                                                .Select(o => new
                                                {
                                                    OrderID = o.OrderID,
                                                    OrdNo = o.OrdNo,
                                                    TotalAmount = o.TotalAmount,
                                                    TaxAmount = o.TaxAmount,
                                                    CashAmount = o.CashAmount,
                                                    CheckAmount = o.CheckAmount,
                                                    CreditCardAmount = o.CreditCardAmount,
                                                    FoodStampAmount = o.FoodStampAmount,
                                                    RefundAmount = o.RefundAmount,
                                                    Balance = o.Balance,
                                                    PaymentMethodID = o.PaymentMethodID,
                                                    CreatedDate = o.CreatedDate,
                                                    FirstName = o.FirstName,
                                                    LastName = o.LastName,
                                                    EmployeeID = o.EmployeeID,
                                                    CancelAmount = o.CancelAmount,
                                                    ReturnAmount = o.ReturnAmount
                                                    ,OverrideTotal = o.OverridePrice
                                                }).ToList();

                DataGridViewImageColumn imgSubOrder = new DataGridViewImageColumn();
                var Sub_Order = new Bitmap(Resources.Sub_Category_Management);
                imgSubOrder.Image = Sub_Order;
                SaleOrderGrdView.Columns.Add(imgSubOrder);
                imgSubOrder.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                gridcolumnhide();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmSalesOrders + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadCmbCashierCode()
        {
            try
            {
                long EmployeeID = 0;
                List<EmployeeMasterModel> lstEmployeeMasterModel = new List<EmployeeMasterModel>();
                EmployeeService _EmployeeService = new EmployeeService();
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var onjtbl_ProductUoM = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false)
                                         orderby EM.FirstName
                                         select new
                                         {
                                             FirstName = EM.FirstName + " " + EM.LastName,
                                             EmployeeID = EM.EmployeeID,
                                         }).ToList();
                onjtbl_ProductUoM.Insert(0, new { FirstName = "All", EmployeeID = EmployeeID });
                cmbCashier.DisplayMember = "FirstName";
                cmbCashier.ValueMember = "EmployeeID";
                cmbCashier.DataSource = onjtbl_ProductUoM;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmSalesOrders + ex.StackTrace, ex.LineNumber());
            }
        }

        public void gridcolumnhide()
        {
            try
            {
                SaleOrderGrdView.Columns["OrderID"].Visible = false;
                SaleOrderGrdView.Columns["PaymentMethodID"].Visible = false;
                SaleOrderGrdView.Columns["LastName"].Visible = false;
                SaleOrderGrdView.Columns["EmployeeID"].Visible = false;
                SaleOrderGrdView.Columns["ReturnAmount"].Visible = false;
                SaleOrderGrdView.Columns["Balance"].Visible = false;
                SaleOrderGrdView.Columns["OrdNo"].HeaderText = "Order No";
                SaleOrderGrdView.Columns["TotalAmount"].HeaderText = "Total";
                SaleOrderGrdView.Columns["TaxAmount"].HeaderText = "Tax";
                SaleOrderGrdView.Columns["CashAmount"].HeaderText = "Cash";
                SaleOrderGrdView.Columns["CheckAmount"].HeaderText = "Check";
                SaleOrderGrdView.Columns["CreditCardAmount"].HeaderText = "CreditCard";
                SaleOrderGrdView.Columns["FoodStampAmount"].HeaderText = "FoodStamp";
                SaleOrderGrdView.Columns["RefundAmount"].HeaderText = "Refund";
                SaleOrderGrdView.Columns["OverridePriceTotal"].HeaderText = "OverRide";
                SaleOrderGrdView.Columns["CreatedDate"].HeaderText = "Created Date";
                SaleOrderGrdView.Columns["FirstName"].HeaderText = "Cashier";
                SaleOrderGrdView.Columns["CancelAmount"].HeaderText = "Cancel Amount";
                SaleOrderGrdView.Refresh();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmSalesOrders + ex.StackTrace, ex.LineNumber());
            }

        }

        private void SaleOrderGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt64(SaleOrderGrdView.Rows[e.RowIndex].Cells["OrderID"].Value.ToString());
                    EmployeeId = Convert.ToInt64(SaleOrderGrdView.Rows[e.RowIndex].Cells["EmployeeID"].Value.ToString());
                    transactionNo = SaleOrderGrdView.Rows[e.RowIndex].Cells["OrdNo"].Value.ToString();
                    CashierName = SaleOrderGrdView.Rows[e.RowIndex].Cells["FirstName"].Value.ToString() + SaleOrderGrdView.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
                    Datetime = SaleOrderGrdView.Rows[e.RowIndex].Cells["CreatedDate"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmSalesOrder" + ex.StackTrace, ex.LineNumber());
            }
        }

        private void SaleOrderGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    FrmSalesOrderDetail objFrmSalesOrderDetail = new FrmSalesOrderDetail();
                    objFrmSalesOrderDetail.OrderID = PrimaryId;
                    objFrmSalesOrderDetail.EmployeeId = EmployeeId;
                    objFrmSalesOrderDetail.dataLoad();
                    objFrmSalesOrderDetail.lblTransNo.Text += transactionNo;
                    objFrmSalesOrderDetail.lblCashier.Text += CashierName;
                    objFrmSalesOrderDetail.lblDateTime.Text += Datetime;
                    objFrmSalesOrderDetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmSalesOrder" + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchDepartmentName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchStr = txtSearchDepartmentName.Text;
                if (Convert.ToInt64(cmbCashier.SelectedValue) != 0)
                {
                    objOrderMasterModel = _OrderScannerService.GetSaleOrderDetail(startDate.Value.Date, endDate.Value.Date);
                    SaleOrderGrdView.DataSource = objOrderMasterModel
                        .Where(c => c.OrdNo.Contains(SearchStr.ToLower())
                        && (c.CreatedBy == Convert.ToInt64(cmbCashier.SelectedValue)))
                        //&& (c.CreatedDate.Value.Date <= endDate.Value.Date && c.CreatedDate.Value.Date >= startDate.Value.Date))
                                                    .Select(o => new
                                                    {
                                                        OrderID = o.OrderID,
                                                        OrdNo = o.OrdNo,
                                                        TotalAmount = o.TotalAmount,
                                                        TaxAmount = o.TaxAmount,
                                                        CashAmount = o.CashAmount,
                                                        CheckAmount = o.CheckAmount,
                                                        CreditCardAmount = o.CreditCardAmount,
                                                        FoodStampAmount = o.FoodStampAmount,
                                                        RefundAmount = o.RefundAmount,
                                                        Balance = o.Balance,
                                                        PaymentMethodID = o.PaymentMethodID,
                                                        CreatedDate = o.CreatedDate,
                                                        FirstName = o.FirstName,
                                                        LastName = o.LastName,
                                                        EmployeeID = o.EmployeeID,
                                                        CancelAmount = o.CancelAmount,
                                                        ReturnAmount = o.ReturnAmount
                                                       ,OverrideTotal = o.OverridePrice
                                                    }).ToList();
                    gridcolumnhide();
                }
                else
                {
                    objOrderMasterModel = _OrderScannerService.GetSaleOrderDetail(startDate.Value.Date, endDate.Value.Date);
                    SaleOrderGrdView.DataSource = objOrderMasterModel
                        .Where(c => c.OrdNo.Contains(SearchStr.ToLower())
                        && (c.CreatedDate.Value.Date <= endDate.Value.Date && c.CreatedDate.Value.Date >= startDate.Value.Date))
                                                    .Select(o => new
                                                    {
                                                        OrderID = o.OrderID,
                                                        OrdNo = o.OrdNo,
                                                        TotalAmount = o.TotalAmount,
                                                        TaxAmount = o.TaxAmount,
                                                        CashAmount = o.CashAmount,
                                                        CheckAmount = o.CheckAmount,
                                                        CreditCardAmount = o.CreditCardAmount,
                                                        FoodStampAmount = o.FoodStampAmount,
                                                        RefundAmount = o.RefundAmount,
                                                        Balance = o.Balance,
                                                        PaymentMethodID = o.PaymentMethodID,
                                                        CreatedDate = o.CreatedDate,
                                                        FirstName = o.FirstName,
                                                        LastName = o.LastName,
                                                        EmployeeID = o.EmployeeID,
                                                        CancelAmount = o.CancelAmount,
                                                        ReturnAmount = o.ReturnAmount
                                                        ,OverrideTotal = o.OverridePrice
                                                    }).ToList();
                    gridcolumnhide();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmSalesOrders + ex.StackTrace, ex.LineNumber());
            }
        }


        private void cmbCashier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchDepartmentName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    try
                    {
                        string SearchStr = txtSearchDepartmentName.Text;
                        if (SearchStr != null && SearchStr != CommonModelCont.EmptyString)
                        {
                            objOrderMasterModel = _OrderScannerService.GetSaleOrderDetail(startDate.Value.Date, endDate.Value.Date);
                            SaleOrderGrdView.DataSource = objOrderMasterModel
                                .Where(c => c.OrdNo.Contains(SearchStr.ToLower()) /*&& c.CreatedBy == Convert.ToInt64(cmbCashier.SelectedValue)*/)
                                                            .Select(o => new
                                                            {
                                                                OrderID = o.OrderID,
                                                                OrdNo = o.OrdNo,
                                                                TotalAmount = o.TotalAmount,
                                                                TaxAmount = o.TaxAmount,
                                                                CashAmount = o.CashAmount,
                                                                CheckAmount = o.CheckAmount,
                                                                CreditCardAmount = o.CreditCardAmount,
                                                                FoodStampAmount = o.FoodStampAmount,
                                                                RefundAmount = o.RefundAmount,
                                                                Balance = o.Balance,
                                                                PaymentMethodID = o.PaymentMethodID,
                                                                CreatedDate = o.CreatedDate,
                                                                FirstName = o.FirstName,
                                                                LastName = o.LastName,
                                                                EmployeeID = o.EmployeeID,
                                                                CancelAmount = o.CancelAmount,
                                                                ReturnAmount = o.ReturnAmount
                                                                ,OverrideTotal = o.OverridePrice
                                                            }).ToList();
                            gridcolumnhide();
                        }
                        else
                        {
                            dataLoadToday();
                        }
                    }
                    catch (Exception ex)
                    {
                        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmSalesOrders + ex.StackTrace, ex.LineNumber());
                    }
                }
            }
            catch(Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmSalesOrders + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}

