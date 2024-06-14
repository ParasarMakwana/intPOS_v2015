using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.Frontend
{
    public class LottoDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public LottoModel AddLotto(LottoModel objLottoModel)
        {
            try
            {
                tbl_LottoTrans objtbl_Lotto = new tbl_LottoTrans();
                objtbl_Lotto.CounterIP = objLottoModel.CounterIP;
                objtbl_Lotto.MacAddress = objLottoModel.MacAddress;
                objtbl_Lotto.LottoPrice = objLottoModel.LottoPrice;
                objtbl_Lotto.LottoType = objLottoModel.LottoType;
                objtbl_Lotto.IsActive = true;
                objtbl_Lotto.IsDelete = false;
                objtbl_Lotto.CreatedDate = DateTime.Now;
                objtbl_Lotto.CreatedBy = objLottoModel.CreatedBy;
                objtbl_Lotto.StoreID = objLottoModel.StoreID;

                _db.tbl_LottoTrans.Add(objtbl_Lotto);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                string e = ex.Message;
            }
            return objLottoModel;
        }
        public List<LottoTotalTrans_ResultModel> GetLottoSalesAndPayoutTrans(DateTime FromDate, DateTime ToDate)
        {
            var lstLottoTotalTrans_ResultModel = new List<LottoTotalTrans_ResultModel>();
            var lstLottoTotalTrans = _db.SP_Rpt_LottoTotalTrans(LoginInfo.StoreID, FromDate, ToDate).ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_LottoTotalTrans_Result, LottoTotalTrans_ResultModel>(); });
            IMapper iMapper = config.CreateMapper();

            LottoTotalTrans_ResultModel _SP_LottoTotalTrans_ResultModel;
            foreach (SP_Rpt_LottoTotalTrans_Result objGetCashierReceipt_Result in lstLottoTotalTrans)
            {
                _SP_LottoTotalTrans_ResultModel = iMapper.Map<SP_Rpt_LottoTotalTrans_Result, LottoTotalTrans_ResultModel>(objGetCashierReceipt_Result);
                lstLottoTotalTrans_ResultModel.Add(_SP_LottoTotalTrans_ResultModel);
            }
            return lstLottoTotalTrans_ResultModel;
        }
    }
}
