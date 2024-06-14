using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class InvoiceDetails_ResultModel
    {
        public string Invoice_Number { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string ShippedBy { get; set; }
        public string Adjustment { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string PONumber { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string VCity { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNo { get; set; }
        public string ItemCode { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public string PurchaseType { get; set; }
        public Nullable<decimal> LineAmtExclTax { get; set; }
        public Nullable<decimal> LineAmtInclTax { get; set; }
        public string ProductName { get; set; }
        public string UPCCode { get; set; }
        public string UnitMeasureCode { get; set; }
        public string StoreName { get; set; }
        public string SMAddress { get; set; }
        public string SAddress2 { get; set; }
        public string SCity { get; set; }
        public string SZipCode { get; set; }
        public string SPhone { get; set; }
    }
    public class InvoiceDetails_ResultModelCont
    {
        public const string Qty = "Qty";
        public const string Invoice_Number = "Invoice_Number";
        public const string Date = "Date";
        public const string ShippedBy = "ShippedBy";
        public const string Adjustment = "Adjustment";
        public const string TotalAmount = "TotalAmount";
        public const string PONumber = "PONumber";
        public const string OrderDate = "OrderDate";
        public const string VendorName = "VendorName";
        public const string Address = "Address";
        public const string Address2 = "Address2";
        public const string VCity = "VCity";
        public const string ZipCode = "ZipCode";
        public const string PhoneNo = "PhoneNo";
        public const string ItemCode = "ItemCode";
        public const string Quantity = "Quantity";
        public const string UnitCost = "UnitCost";
        public const string Tax = "Tax";
        public const string TaxAmount = "TaxAmount";
        public const string PurchaseType = "PurchaseType";
        public const string LineAmtExclTax = "LineAmtExclTax";
        public const string LineAmtInclTax = "LineAmtInclTax";
        public const string ProductName = "ProductName";
        public const string UPCCode = "UPCCode";
        public const string UnitMeasureCode = "UnitMeasureCode";
        public const string StoreName = "StoreName";
        public const string SMAddress = "SMAddress";
        public const string SAddress2 = "SAddress2";
        public const string SCity = "SCity";
        public const string SZipCode = "SZipCode";
        public const string SPhone = "SPhone";
    }
}
