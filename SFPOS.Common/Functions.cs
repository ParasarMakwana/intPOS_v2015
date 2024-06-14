using MetroFramework.Forms;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Threading;
using System.Windows.Forms;

namespace SFPOS.Common
{
    public static class Functions
    {
        public static DateTime GetCurrentDateTime()
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone(); //according to current timezone
            culture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            culture.DateTimeFormat.LongTimePattern = "MM/dd/yyyy hh:mm tt";
            culture.DateTimeFormat.LongTimePattern = "hh:mm:ss tt";
            culture.DateTimeFormat.ShortTimePattern = "hh:mm tt";
            Thread.CurrentThread.CurrentCulture = culture;
            return DateTime.Now;
        }
        public static int LineNumber(this Exception e)
        {
            int linenum = 0;
            try
            {
                linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(' ')));
            }
            catch
            {

            }
            return linenum;
        }
        public static string GetDisplayAmount(string Amount)
        {
            //return "$ " + String.Format("{0:0.00}", Amount);
            //return "$" + Convert.ToDouble(Amount).ToString("N2");
            return "$" + (!String.IsNullOrEmpty(Amount) ? Convert.ToDecimal(Amount.ToString()) : 0).ToString("N2");
        }
        public static decimal GetDecimal(string Val)
        {
            return !String.IsNullOrEmpty(Val.ToString()) ? System.Math.Round(Convert.ToDecimal(Val), 4) : 0;
            //return !String.IsNullOrEmpty(Val.ToString()) ? Convert.ToDecimal(Val) : 0;
        }
        public static decimal GetDecimalRound(string Val)
        {
            return !String.IsNullOrEmpty(Val.ToString()) ? System.Math.Round(Convert.ToDecimal(Val), 2) : 0;
            //return !String.IsNullOrEmpty(Val.ToString()) ? Convert.ToDecimal(Val) : 0;
        }
        public static long GetLong(string Val)
        {
            return !String.IsNullOrEmpty(Val.ToString()) ? Convert.ToInt64(Val) : 0;
        }
        public static bool GetBoolean(string Val)
        {
            return !String.IsNullOrEmpty(Val) ? Convert.ToBoolean(Val) : false;
        }
        public static int GetInteger(string Val)
        {
            return !String.IsNullOrEmpty(Val) ? Convert.ToInt32(Val) : 0;
        }
        public static string GetSaperatorValues(string Amount)
        {
            return String.Format("{0:#,0.00}", Amount);
        }
        public static void ErrorMessage(string message, string title, Exception ex)
        {
            string path = Application.StartupPath + "\\AppData\\LogFile.txt";

            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    using (TextWriter tw = new StreamWriter(fs))
                    {
                        tw.WriteLine("Error >> " + DateTime.Now + " >> " + message);
                        tw.WriteLine("=================================================================================");
                    }
                }
            }
            else
            {
                using (FileStream fs = File.Create(path))
                {
                    using (TextWriter tw = new StreamWriter(fs))
                    {
                        tw.WriteLine("Error >> " + DateTime.Now + ">>" + message);
                        tw.WriteLine("=================================================================================");
                    }
                }
            }
        }
        public static string GetUPC_E(string OrignalUPCCode)
        {
            string UPC_E = "";

            if (OrignalUPCCode.Length == 7)
            {
                if (OrignalUPCCode.Length > 6)
                {
                    UPC_E = OrignalUPCCode.Substring(6, 1);

                    switch (UPC_E)
                    {
                        case "0":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 2) + "00000" + OrignalUPCCode.Substring(3, 3); //04963406 = 0004900000634
                            break;
                        case "1":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 2) + "10000" + OrignalUPCCode.Substring(3, 3);
                            break;
                        case "2":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 2) + "20000" + OrignalUPCCode.Substring(3, 3);
                            break;
                        case "3":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 3) + "00000" + OrignalUPCCode.Substring(4, 2);
                            break;
                        case "4":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 4) + "00000" + OrignalUPCCode.Substring(3, 1);
                            break;
                        case "5":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 5) + "00005";
                            break;
                        case "6":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 5) + "00006";
                            break;
                        case "7":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 5) + "00007";
                            break;
                        case "8":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 5) + "00008";
                            break;
                        case "9":
                            UPC_E = "000" + OrignalUPCCode.Substring(1, 5) + "00009";
                            break;
                        default:
                            UPC_E = "";
                            break;
                    }
                }
            }
            return UPC_E;
        }

        public static void ErrorLog(string Page, string title, Exception ex)
        {
            try
            {
                SqlConnection conn = new SqlConnection(XMLData.DbConnectionString);
                SqlDataAdapter DataAdapter = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                cmd = conn.CreateCommand();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
                objExceptionLogMasterModel.ExceptionName = ex.Message;
                objExceptionLogMasterModel.Discription = ex.StackTrace + "(" + ex.InnerException.InnerException.Message.ToString().Replace("'", "") + ")";
                objExceptionLogMasterModel.PageName = Page + "-" + title;
                objExceptionLogMasterModel.PageLine = ex.LineNumber();
                objExceptionLogMasterModel.CreatedDate = DateTime.Now;
                objExceptionLogMasterModel.CounterIP = LoginInfo.CounterIP;

                cmd.CommandText = "INSERT INTO tbl_ExceptionLog(ExceptionName,Discription,PageName,PageLine,CreatedDate,CounterIP) " +
                                                   "VALUES('" + objExceptionLogMasterModel.ExceptionName + "', '" + objExceptionLogMasterModel.Discription + "', '" + objExceptionLogMasterModel.PageName + "', '" + objExceptionLogMasterModel.PageLine + "','" + DateTime.Now + "','" + objExceptionLogMasterModel.CounterIP + "')";

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
        }
        
        public static DataTable ToDataTable(this DataGridView dataGridView)
        {

            DataGridView dgv = dataGridView;
            DataTable table = new DataTable();

            // Crea las columnas 
            for (int iCol = 0; iCol < dgv.Columns.Count; iCol++)
            {
                table.Columns.Add(dgv.Columns[iCol].Name);
            }

            /**
              * THIS DOES NOT WORK
              */
            // Agrega las filas 
            /*for (int i = 0; i < dgv.Rows.Count; i++)
            {
                // Obtiene el DataBound de la fila y copia los valores de la fila 
                DataRowView boundRow = (DataRowView)dgv.Rows[i].DataBoundItem;
                var cells = new object[boundRow.Row.ItemArray.Length];
                for (int iCol = 0; iCol < boundRow.Row.ItemArray.Length; iCol++)
                {
                    cells[iCol] = boundRow.Row.ItemArray[iCol];
                }

                // Agrega la fila clonada                 
                table.Rows.Add(cells);
            }*/

            /* THIS WORKS BUT... */
            foreach (DataGridViewRow row in dgv.Rows)
            {

                DataRow datarw = table.NewRow();

                for (int iCol = 0; iCol < dgv.Columns.Count; iCol++)
                {
                    datarw[iCol] = row.Cells[iCol].Value;
                }

                table.Rows.Add(datarw);
            }

            return table;
        }

        public static object GetPropertyValue(this object T, string PropName)
        {
            return T.GetType().GetProperty(PropName) == null ? null : T.GetType().GetProperty(PropName).GetValue(T, null);
        }
        //public static void SetIcon(MetroForm iForm)
        //{
            //iForm.Icon = new Icon(Application.StartupPath + "\\intPOS.ico");
            //iForm.Refresh();
        //}
        //public static void SetIcon(Form iForm)
        //{
            //iForm.Icon = new Icon(Application.StartupPath + "\\intPOS.ico");
            //iForm.Refresh();
        //}
        public static string ReplaceCharacterInString(string stringDesc)
        {
            string _StringDesc = stringDesc;
            _StringDesc = _StringDesc.Replace("'", "''");
            return _StringDesc;
        }
    }
}
