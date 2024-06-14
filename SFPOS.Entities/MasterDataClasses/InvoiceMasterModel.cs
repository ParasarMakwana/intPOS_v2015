using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class InvoiceMasterModel
    {
        public long InvoiceID { get; set; }
        public Nullable<long> PostedPurchaseHeaderID { get; set; }
        public Nullable<long> StoreID { get; set; }
        public Nullable<long> PONumber { get; set; }
        public string Invoice_Number { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string ShippedBy { get; set; }
        public string ReceivedBy { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Adjustment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
    }
}
