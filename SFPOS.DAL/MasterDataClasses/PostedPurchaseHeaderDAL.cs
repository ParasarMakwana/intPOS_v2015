using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;

namespace SFPOS.DAL.MasterDataClasses
{
    public class PostedPurchaseHeaderDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public PostedPurchaseHeaderMasterModel AddPostedPurchaseHeader(PostedPurchaseHeaderMasterModel objPurchaseOrderMasterModel)
        {
            try
            {
                tbl_PostedPurchaseHeader objtbl_PostedPurchaseHeader = new tbl_PostedPurchaseHeader();
                objPurchaseOrderMasterModel.PostedPurchaseHeaderID = objPurchaseOrderMasterModel.PostedPurchaseHeaderID;
                objtbl_PostedPurchaseHeader.PurchaseHeaderID = objPurchaseOrderMasterModel.PurchaseHeaderID;
                objtbl_PostedPurchaseHeader.VendorID = objPurchaseOrderMasterModel.VendorID;
                objtbl_PostedPurchaseHeader.PONumber = objPurchaseOrderMasterModel.PONumber;
                objtbl_PostedPurchaseHeader.OrderDate = objPurchaseOrderMasterModel.OrderDate;
                objtbl_PostedPurchaseHeader.CreatedDate = objPurchaseOrderMasterModel.CreatedDate;
                objtbl_PostedPurchaseHeader.CreatedBy = LoginInfo.UserId;
                objtbl_PostedPurchaseHeader.UpdatedDate = objPurchaseOrderMasterModel.UpdatedDate;
                objtbl_PostedPurchaseHeader.UpdatedBy = LoginInfo.UserId;                
                _db.tbl_PostedPurchaseHeader.Add(objtbl_PostedPurchaseHeader);
                _db.SaveChanges();
                objPurchaseOrderMasterModel.PostedPurchaseHeaderID = objtbl_PostedPurchaseHeader.PostedPurchaseHeaderID;
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objPurchaseOrderMasterModel;
        }
    }
}
