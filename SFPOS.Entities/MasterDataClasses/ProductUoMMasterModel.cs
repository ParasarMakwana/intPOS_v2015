using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ProductUoMMasterModel
    {
        public long ProductUoMID { get; set; }
        public long ProductID { get; set; }
        public long UnitMeasureID { get; set; }
        public long QtyPerUoM { get; set; }
        public string Discription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
    }
    public class ProductUoMMasterModelCont
    {
        public const string Description = "Description";
        public const string QtyPerUoM = "QtyPerUoM";
        public const string ProductUoMID = "ProductUoMID";
    }
}
