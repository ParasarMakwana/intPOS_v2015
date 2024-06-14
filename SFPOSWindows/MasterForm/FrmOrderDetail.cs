using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmOrderDetail : MetroForm
    {
        public long OrderID = 0;
        public FrmOrderDetail()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            List<OrderDetailmasterModel> objOrderDetailmasterModel = new List<OrderDetailmasterModel>();
            OrderScannerService _OrderScannerService = new OrderScannerService();
            objOrderDetailmasterModel = _OrderScannerService.GetAllSubOrderDetail(OrderID);
            OrderDetailGrdView.DataSource = objOrderDetailmasterModel.Select(o=> new {  Product_Name = o.ProductName ,
                                                                                        UPC_Code = o.UPCCode ,
                                                                                        Quantity = o.Quantity ,
                                                                                        Sale_Price = o.SellPrice ,
                                                                                        Final_Price = o.finalPrice}).ToList();


            OrderDetailGrdView.Columns["UPC_Code"].HeaderText = "UPC Code";
            OrderDetailGrdView.Columns["Sale_Price"].HeaderText = "Sale Price";
            OrderDetailGrdView.Columns["Final_Price"].HeaderText = "Final Price";
            OrderDetailGrdView.Columns["Product_Name"].HeaderText = "Product Name";
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
