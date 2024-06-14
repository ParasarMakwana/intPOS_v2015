using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
    public class ExceptionLogService
    {
        ExceptionLogDAL objExceptionLogDAL = new ExceptionLogDAL();
        public void AddExceptionLog(string ExceptionName, string description, string PageName, long Line)
        {
            objExceptionLogDAL.AddExceptionLog(ExceptionName, description, PageName, Line);
        }
    }
}
