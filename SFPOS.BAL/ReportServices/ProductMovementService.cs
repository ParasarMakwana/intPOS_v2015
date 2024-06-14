using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class ProductMovementService
    {
        ProductMovementDAL objProductMovementDAL = new ProductMovementDAL();
        public List<ProductMovement_ResultModel> GetProductDetails(string UPCCode, DateTime STARTDATE, DateTime ENDDATE)
        {
            return objProductMovementDAL.GetProductDetails(UPCCode, STARTDATE, ENDDATE);
        }
        public List<ProductMovement_ResultModel> GetProductDetails(string UPCCode, int flag)
        {
            return objProductMovementDAL.GetProductDetails(UPCCode, flag);
        }
        
    }
}
