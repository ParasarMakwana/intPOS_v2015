using AutoMapper;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFPOS.Common;

namespace SFPOS.DAL.MasterDataClasses
{
    public class InvoiceDetailDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public List<InvoiceDetails_ResultModel> GetInvoiceDetails(long PONumber)
        {
            var lstSP_InvoiceDetailMasterModel = new List<InvoiceDetails_ResultModel>();
            var lstInvoiceDetails = _db.SP_InvoiceDetails(PONumber).ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_InvoiceDetails_Result, InvoiceDetails_ResultModel>(); });
            IMapper iMapper = config.CreateMapper();

            InvoiceDetails_ResultModel _SP_InvoiceDetailMasterModel;
            foreach (SP_InvoiceDetails_Result objGetReciptDetails_Result in lstInvoiceDetails)
            {
                _SP_InvoiceDetailMasterModel = iMapper.Map<SP_InvoiceDetails_Result, InvoiceDetails_ResultModel>(objGetReciptDetails_Result);
                lstSP_InvoiceDetailMasterModel.Add(_SP_InvoiceDetailMasterModel);
            }
            return lstSP_InvoiceDetailMasterModel;
        }
    }
}
