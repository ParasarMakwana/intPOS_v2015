using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.FrontEnd
{
    public class TransSuspendMasterModel
    {
        public long TransSuspendID { get; set; }
        public string TransSuspendCode { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string ProductName { get; set; }
        public string UPCCode { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public Nullable<long> SectionID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<decimal> TotalTaxAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }

        public Nullable<decimal> FinalPrice { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> GrossAmount { get; set; }
        public Nullable<bool> IsScale { get; set; }
        public Nullable<bool> IsFoodStamp { get; set; }
        public Nullable<bool> IsTax { get; set; }
        public Nullable<bool> DiscountApplyed { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<long> StoreID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string StoreName { get; set; }
        public string SMAddress { get; set; }
        public string SAddress2 { get; set; }
        public string SZipCode { get; set; }
        public string SPhone { get; set; }
        public string SFax { get; set; }
        public string FirstName { get; set; }
        public decimal GroupQty { get; set; }
        public decimal GroupPrice { get; set; }
        public decimal CaseQty { get; set; }
        public decimal CasePrice { get; set; }
        public string CouponCode { get; set; }
        public Nullable<decimal> CouponDiscAmt { get; set; }

        public Nullable<bool> CasePriceApplied { get; set; }

        public Nullable<bool> ManWT { get; set; }
        public Nullable<decimal> FSEligibleAmount { get; set; }
    }
    public class TransSuspendMasterModelCont
    {
        public const string TransSuspendID = "TransSuspendID";
        public const string TransSuspendCode = "TransSuspendCode";
        public const string ProductID = "ProductID";
        public const string ProductName = "ProductName";
        public const string TaxAmount = "TaxAmount";

        public const string TotalTaxAmount = "TotalTaxAmount"; 
        public const string UPCCode = "UPCCode";
        public const string Quantity = "Quantity";
        public const string SellPrice = "SellPrice";
        public const string FinalPrice = "FinalPrice";
        public const string TotalAmount = "TotalAmount";
        public const string Tax = "Tax";
        public const string GrossAmount = "GrossAmount";
        public const string IsScale = "IsScale";
        public const string IsFoodStamp = "IsFoodStamp";
        public const string IsTax = "IsTax";
        public const string DiscountApplyed = "DiscountApplyed";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string Status = "Status";
        public const string StoreID = "StoreID";
        public const string StoreName = "StoreName";
        public const string SMAddress = "SMAddress";
        public const string SAddress2 = "SAddress2";
        public const string SZipCode = "SZipCode";
        public const string SPhone = "SPhone";
        public const string SFax = "SFax";
        public const string FirstName = "FirstName";
        public const string DepartmentID = "DepartmentID";
        public const string SectionID = "SectionID";
        public const string GroupQty = "GroupQty";
        public const string GroupPrice = "GroupPrice";
        public const string CaseQty = "CaseQty";
        public const string CasePrice = "CasePrice";
        public const string CouponCode = "CouponCode";
        public const string CouponDiscAmt = "CouponDiscAmt";
        public const string CasePriceApplied = "CasePriceApplied";

       public const string ManWT = "IsManWTRefund";
        public const string FSEligibleAmount = "FSEligibleAmount";
    }
}
