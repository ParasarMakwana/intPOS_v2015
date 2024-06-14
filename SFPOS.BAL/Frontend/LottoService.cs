using SFPOS.DAL;
using SFPOS.DAL.Frontend;
using SFPOS.Entities.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.Frontend
{
    public class LottoService
    {
        LottoDAL objLottoDAL = new LottoDAL();
        public LottoModel AddLotto(LottoModel objLottoModel)
        {
            return objLottoDAL.AddLotto(objLottoModel);
        }

        public List<LottoTotalTrans_ResultModel> GetLottoSalesAndPayoutTrans(DateTime FromDate, DateTime ToDate)
        {
            return objLottoDAL.GetLottoSalesAndPayoutTrans(FromDate, ToDate);
        }
    }
}
