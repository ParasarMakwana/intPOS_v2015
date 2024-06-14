using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOS.DAL.MasterDataClasses
{
    public class ExceptionLogDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD
        public void AddExceptionLog(string ExceptionName, string description, string PageName, long PageLine)
        {
            try
            {
                bool IsConnet = CheckConnection();

                if (IsConnet)
                {
                    try
                    {
                        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
                        tbl_ExceptionLog objtbl_ExceptionLog = new tbl_ExceptionLog();
                        objtbl_ExceptionLog.ExceptionName = ExceptionName;
                        objtbl_ExceptionLog.Discription = description;
                        objtbl_ExceptionLog.PageName = PageName;
                        objtbl_ExceptionLog.PageLine = PageLine;
                        objtbl_ExceptionLog.CreatedDate = DateTime.Now;
                        objtbl_ExceptionLog.CounterIP = LoginInfo.CounterIP;
                        _db.tbl_ExceptionLog.Add(objtbl_ExceptionLog);
                        objExceptionLogMasterModel.ExceptionID = objtbl_ExceptionLog.ExceptionID;
                    }
                    catch (Exception e)
                    {
                        string ex = e.Message;
                    }
                    _db.SaveChanges();
                }
                else
                {
                    //For Local
                    string SqlCeConn = "Data Source=E:\\Asp_net\\ezPOSPro\\SFPOSWindows\\AppData\\ezPOSPro.sdf;Password ='123456'";
                    //For Live
                    //string SqlCeConn = "Data Source=" + Application.StartupPath + "\\AppData\\ezPOSPro.sdf; Password ='123456'";

                    //SqlCeConn = ClsCommon.SqlCeConn;
                    SqlCeConnection conn = new SqlCeConnection(SqlCeConn);
                    SqlCeDataAdapter DataAdapter = new SqlCeDataAdapter();
                    SqlCeCommand cmd = conn.CreateCommand();
                    cmd = conn.CreateCommand();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
                    objExceptionLogMasterModel.ExceptionName = ExceptionName;
                    objExceptionLogMasterModel.Discription = "Offline - " +description;
                    objExceptionLogMasterModel.PageName = PageName;
                    objExceptionLogMasterModel.PageLine = PageLine;
                    objExceptionLogMasterModel.CreatedDate = DateTime.Now;
                    objExceptionLogMasterModel.CounterIP = LoginInfo.CounterIP;

                    cmd.CommandText = "INSERT INTO tbl_ExceptionLog(ExceptionName,Discription,PageName,PageLine,CreatedDate,CounterIP) " +
                                                        "VALUES(@ExceptionName, @Discription, @PageName, @PageLine, @CreatedDate,@CounterIP)";

                    #region Parameters
                    if (objExceptionLogMasterModel.ExceptionName != null)
                    {
                        cmd.Parameters.AddWithValue("@ExceptionName", objExceptionLogMasterModel.ExceptionName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ExceptionName", DBNull.Value);
                    }
                    if (objExceptionLogMasterModel.Discription != null)
                    {
                        cmd.Parameters.AddWithValue("@Discription", objExceptionLogMasterModel.Discription);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Discription", DBNull.Value);
                    }
                    if (objExceptionLogMasterModel.PageName != null)
                    {
                        cmd.Parameters.AddWithValue("@PageName", objExceptionLogMasterModel.PageName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PageName", DBNull.Value);
                    }
                    //if (objExceptionLogMasterModel.PageLine != null)
                    //{
                    cmd.Parameters.AddWithValue("@PageLine", objExceptionLogMasterModel.PageLine);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@PageLine", DBNull.Value);
                    //}
                    if (objExceptionLogMasterModel.CreatedDate != null)
                    {
                        cmd.Parameters.AddWithValue("@CreatedDate", objExceptionLogMasterModel.CreatedDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                    }
                    if (objExceptionLogMasterModel.CounterIP != null)
                    {
                        cmd.Parameters.AddWithValue("@CounterIP", objExceptionLogMasterModel.CounterIP);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CounterIP", DBNull.Value);
                    }
                    #endregion

                    cmd.ExecuteScalar();
                    cmd.CommandText = "Select @@Identity";
                    long ExceptionID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch(Exception e)
            {

            }
        }
        public bool CheckConnection()
        {
            bool Status;
            var task = Task.Run(() =>
            {
                Status = db_Connection();
            });
            bool isCompletedSuccessfully = task.Wait(TimeSpan.FromMilliseconds(3000));
            if (isCompletedSuccessfully)
            {
                Status = db_Connection();
            }
            else
            {
                Status = false;
            }
            return Status;
        }
        public bool db_Connection()
        {
            try
            {
                var Empl = _db.tbl_EmployeeMaster.FirstOrDefault();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
