using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.FrontEnd
{
    public class CouponModel
    {
        public long CouponID { get; set; }
        public string CoupenCode { get; set; }
        public string CouponName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> MinPurchaseAmt { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<long> AvailableCount { get; set; }
        public Nullable<long> UsedCount { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
