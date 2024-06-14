using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;

namespace SFPOS.DAL.MasterDataClasses
{
    public class ProductLedgerDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public ProductLedgerMasterModel AddProductLedger(ProductLedgerMasterModel objProductLedgerMasterModel, int TransType)
        {
            try
            {
                tbl_ProductLedger objtbl_ProductLedger = new tbl_ProductLedger();
                if (TransType == 1)
                {
                    objProductLedgerMasterModel.ProductLedgerID = objProductLedgerMasterModel.ProductLedgerID;
                    objtbl_ProductLedger.OrderLineID = objProductLedgerMasterModel.OrderLineID;
                    objtbl_ProductLedger.ProductID = objProductLedgerMasterModel.ProductID;
                    objtbl_ProductLedger.LedgerTypeID = objProductLedgerMasterModel.LedgerTypeID;
                    objtbl_ProductLedger.OrderID = objProductLedgerMasterModel.OrderID;
                    objtbl_ProductLedger.PostedPurchaseHeaderID = objProductLedgerMasterModel.PostedPurchaseHeaderID;
                    objtbl_ProductLedger.TaxGroupCodeID = objProductLedgerMasterModel.TaxGroupCodeID;
                    objtbl_ProductLedger.DepartmentID = objProductLedgerMasterModel.DepartmentID;
                    objtbl_ProductLedger.SectionID = objProductLedgerMasterModel.SectionID;
                    objtbl_ProductLedger.UnitOfMeasureID = objProductLedgerMasterModel.UnitOfMeasureID;
                    objtbl_ProductLedger.UPCCode = objProductLedgerMasterModel.UPCCode;
                    objtbl_ProductLedger.QRCode = objProductLedgerMasterModel.QRCode;
                    objtbl_ProductLedger.Qty = objProductLedgerMasterModel.Qty;
                    objtbl_ProductLedger.PurchasePrice = objProductLedgerMasterModel.PurchasePrice;
                    objtbl_ProductLedger.SellPrice = objProductLedgerMasterModel.SellPrice;
                    objtbl_ProductLedger.FinalPrice = objProductLedgerMasterModel.FinalPrice;
                    objtbl_ProductLedger.DiscountPrice = objProductLedgerMasterModel.DiscountPrice;
                    objtbl_ProductLedger.TaxAmount = objProductLedgerMasterModel.TaxAmount;
                    objtbl_ProductLedger.RemainingQty = objProductLedgerMasterModel.RemainingQty;
                    objtbl_ProductLedger.StoreID = objProductLedgerMasterModel.StoreID;
                    objtbl_ProductLedger.CreatedDate = objProductLedgerMasterModel.CreatedDate;
                    objtbl_ProductLedger.CreatedBy = objProductLedgerMasterModel.CreatedBy;
                    objtbl_ProductLedger.IsForceTax = objProductLedgerMasterModel.IsForceTax;
                    objtbl_ProductLedger.OverridePrice = objProductLedgerMasterModel.OverridePrice;
                    _db.tbl_ProductLedger.Add(objtbl_ProductLedger);
                    _db.SaveChanges();
                    objProductLedgerMasterModel.ProductLedgerID = objtbl_ProductLedger.ProductLedgerID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
                Functions.ErrorLog("ProductLedgerDAL", "AddProductLedger(tbl_ProductLedger)", e);
            }
            return objProductLedgerMasterModel;
        }

    }
}
