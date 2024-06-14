using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;

namespace SFPOS.DAL.MasterDataClasses
{
    public class PostedPurchaseLineDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public PostedPurchaseLineMasterModel AddPostedPurchaseLine(PostedPurchaseLineMasterModel objPostedPurchaseLineMasterModel)
        {
            try
            {
                tbl_PostedPurchaseLine objtbl_ProductPurchaseLine = new tbl_PostedPurchaseLine();
                objPostedPurchaseLineMasterModel.PostedPurchaseLineID = objPostedPurchaseLineMasterModel.PostedPurchaseLineID;
                objtbl_ProductPurchaseLine.PurchaseLineID = objPostedPurchaseLineMasterModel.PurchaseLineID;
                objtbl_ProductPurchaseLine.PostedPurchaseHeaderID = objPostedPurchaseLineMasterModel.PostedPurchaseHeaderID;
                objtbl_ProductPurchaseLine.ProductID = objPostedPurchaseLineMasterModel.ProductID;
                objtbl_ProductPurchaseLine.Quantity = objPostedPurchaseLineMasterModel.Quantity;
                objtbl_ProductPurchaseLine.UnitCost = objPostedPurchaseLineMasterModel.UnitCost;
                objtbl_ProductPurchaseLine.Tax = objPostedPurchaseLineMasterModel.Tax;
                objtbl_ProductPurchaseLine.TaxAmount = objPostedPurchaseLineMasterModel.TaxAmount;
                objtbl_ProductPurchaseLine.LineAmtExclTax = objPostedPurchaseLineMasterModel.LineAmtExclTax;
                objtbl_ProductPurchaseLine.LineAmtInclTax = objPostedPurchaseLineMasterModel.LineAmtInclTax;
                objtbl_ProductPurchaseLine.PurchaseType = objPostedPurchaseLineMasterModel.PurchaseType;
                objtbl_ProductPurchaseLine.ItemCode = objPostedPurchaseLineMasterModel.ItemCode;
                objtbl_ProductPurchaseLine.CreatedDate = DateTime.Now;
                objtbl_ProductPurchaseLine.CreatedBy = LoginInfo.UserId;
                objPostedPurchaseLineMasterModel.PostedPurchaseLineID = objtbl_ProductPurchaseLine.PostedPurchaseLineID;
                _db.tbl_PostedPurchaseLine.Add(objtbl_ProductPurchaseLine);
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objPostedPurchaseLineMasterModel;
        }

    }
}
