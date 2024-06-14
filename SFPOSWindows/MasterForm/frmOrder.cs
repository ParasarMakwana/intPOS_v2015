using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class frmOrder : MetroForm
    {
        long PrimaryID = 0;
        public frmOrder()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            List<OrderMasterModel> objOrderMasterModel = new List<OrderMasterModel>();
            OrderScannerService _OrderScannerService = new OrderScannerService();
            objOrderMasterModel = _OrderScannerService.GetAllOrderDetail();
            OrderMasterGrdView.DataSource = objOrderMasterModel
                                            .Select(o=> new {   Order_No = o.OrderID,
                                                                Total_Amount = o.TotalAmount,
                                                                Tax_Amount = o.TaxAmount,
                                                                Gross_Amount = o.GrossAmount,
                                                                Status = o.Status ,
                                                                Created_Date = o.CreatedDate}).ToList();

            OrderMasterGrdView.Columns["Order_No"].HeaderText = "Order No";
            OrderMasterGrdView.Columns["Total_Amount"].HeaderText = "Total Amount";
            OrderMasterGrdView.Columns["Tax_Amount"].HeaderText = "Tax Amount";
            OrderMasterGrdView.Columns["Gross_Amount"].HeaderText = "Gross Amount";
            OrderMasterGrdView.Columns["Created_Date"].HeaderText = "Created Date";
        }

        private void OrderMasterGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                PrimaryID = Convert.ToInt64(OrderMasterGrdView.Rows[e.RowIndex].Cells["Order_No"].Value);
            }
        }

        private void OrderMasterGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                FrmOrderDetail objFrmOrderDetail = new FrmOrderDetail();
                objFrmOrderDetail.OrderID = PrimaryID;
                objFrmOrderDetail.Text += PrimaryID.ToString();
                objFrmOrderDetail.dataLoad();
                objFrmOrderDetail.ShowDialog();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
