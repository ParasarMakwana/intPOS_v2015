using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
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

namespace SFPOSWindows.Frontend
{
    public partial class FrmOrderHistory : MetroForm
    {
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        public FrmOrderHistory()
        {
            InitializeComponent();
            dataLoad();
        }

        public void dataLoad()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var results = (from pm in _db.tbl_OrderDetail
                               orderby pm.ProductID descending
                               where pm.CreatedBy == LoginInfo.UserId
                               select new
                               {
                                   UPCCode = pm.UPCCode,
                                   ProductName = pm.ProductName,
                                   Quantity = pm.Quantity,
                                   Discount = pm.Discount,
                                   Price = CommonModelCont.AddDollorSign + pm.SellPrice,
                                   ProductID = pm.ProductID
                               }).ToList();

                GridViewProductDetails.DataSource = results;
                GridViewProductDetails.Columns[ProductMasterModelCont.UPCCode].Width = 150;
                GridViewProductDetails.Columns[ProductMasterModelCont.ProductName].Width = 250;
                GridViewProductDetails.Columns[ProductMasterModelCont.Price].Width = 97;
                GridViewProductDetails.Columns["ProductID"].Visible = false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmOrderHistory + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchStr = txtSearchProductName.Text;
                if (searchStr != CommonModelCont.EmptyString && searchStr != null && searchStr != AlertMessages.ProductDetailSearch)
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    var results = (from pm in _db.tbl_OrderDetail
                                   orderby pm.ProductID descending
                                   where pm.CreatedBy == LoginInfo.UserId
                                   && (pm.ProductName.ToLower().StartsWith(searchStr.ToLower().ToString())
                                   || pm.UPCCode.StartsWith(searchStr.ToString())
                                   || pm.SellPrice.ToString().StartsWith(searchStr.ToString()))
                                   select new
                                   {
                                       UPCCode = pm.UPCCode,
                                       ProductName = pm.ProductName,
                                       Quantity = pm.Quantity,
                                       Discount = pm.Discount,
                                       Price = CommonModelCont.AddDollorSign + pm.SellPrice,
                                       ProductID = pm.ProductID
                                   }).ToList();


                    GridViewProductDetails.DataSource = results;
                    GridViewProductDetails.Columns[ProductMasterModelCont.UPCCode].Width = 150;
                    GridViewProductDetails.Columns[ProductMasterModelCont.ProductName].Width = 250;
                    GridViewProductDetails.Columns[ProductMasterModelCont.Price].Width = 97;
                    GridViewProductDetails.Columns["ProductID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmOrderHistory + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
