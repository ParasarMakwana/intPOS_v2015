
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SFPOS.Link
{
    public class frmSyncDataToLink
    {
        public string cs = @"URI=file:C:\Program Files (x86)\Aclas LINK66\Database\Default.db";
        //public string cs = @"URI=file:" + Application.StartupPath + @"\AppData\Default.db";

        #region LogMessage
        static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static string logFileName = "InlinecodeLog.txt";
        static string logfilepath = Path.Combine(desktopPath, logFileName);

        public static void LogMessage(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logfilepath, true))
                {
                    writer.WriteLine($"{message}");
                }
            }
            catch (Exception ex)
            {
                LogMessage("Error writing to log file: " + ex.Message);
            }
        }
        #endregion

        public void SyncDepartment(DataTable dt)
        {
            try
            {
                var conn = new SQLiteConnection(cs);
                conn.Open();
                //int Totalcount = 0;
                //int TotalInsertcount = 0;
                //int TotalUpdatedcount = 0;
                var cmd = new SQLiteCommand(conn);
                if (dt.Rows.Count > 0)
                {
                    // Totalcount = dt.Rows.Count;
                    foreach (DataRow item in dt.Rows)
                    {
                        var checkName = "SELECT COUNT(1) FROM DepartmentTbl WHERE ID= '" + item["DepartmentID"] + "'";
                        cmd.CommandText = checkName;
                        LogMessage(checkName.ToString());
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 0)
                        {
                            var txtQuery = "INSERT INTO DepartmentTbl (ID,Name) values ('" + item["DepartmentID"] + "','" + item["DepartmentName"] + "')";
                            cmd.CommandText = txtQuery;
                            LogMessage(txtQuery.ToString() + "Linenumber = 69");
                            cmd.ExecuteNonQuery();
                            //TotalInsertcount++;
                        }
                        else
                        {
                            var txtQuery = "UPDATE DepartmentTbl SET Name ='" + item["DepartmentName"] + "' WHERE ID = '" + item["DepartmentID"] + "'";
                            cmd.CommandText = txtQuery;
                            LogMessage(txtQuery.ToString() + "Linenumber = 69");
                            cmd.ExecuteNonQuery();
                            //TotalUpdatedcount++;
                        }
                    }

                    //if (TotalInsertcount == Totalcount)
                    //{
                    //    MessageBox.Show(Totalcount + " " + "Records Inserted Successfully. ");
                    //}
                    //else
                    //{
                    //    if (TotalInsertcount > 0)
                    //    {
                    //        MessageBox.Show(TotalInsertcount + " " + "Records Inserted Successfully. ");
                    //    }
                    //    else if (TotalUpdatedcount > 0)
                    //    {
                    //        MessageBox.Show(TotalUpdatedcount + " " + "Records Updated Successfully. ");
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("No New Record Found for Insert/Update. ");
                    //    }
                    //}
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message.ToString());
                LogMessage(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        public void SyncProduct(DataTable dt)
        {
            try
            {
                var conn = new SQLiteConnection(cs);
                conn.Open();
                //int Totalcount = 0;
                //int TotalInsertcount = 0;
                //int TotalUpdatedcount = 0;
                var cmd = new SQLiteCommand(conn);
                if (dt.Rows.Count > 0)
                {
                    // Totalcount = dt.Rows.Count;
                    foreach (DataRow item in dt.Rows)
                    {
                        string plu = item["UPCCode"].ToString();
                        if (plu.EndsWith("00000"))
                        {
                            plu = plu.Substring(2, 6);
                            var checkName = "SELECT COUNT(1) FROM PLUTbl WHERE ItemCode = '" + plu + "'";
                            cmd.CommandText = checkName;
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            if (count == 0)
                            {
                                var txtQuery = "INSERT INTO PLUTbl(ID, ItemCode, DepartmentID, Name1, Price, UnitID, BarcodeType1,TareValue)VALUES('" + plu + "','" + plu + "', '" + item["DepartmentID"] + "','" + item["ProductName"].ToString().Replace("'", "''") + "', '" + item["Price"] + "', '" + item["UnitMeasureID"] + "', '150','" + item["TareWeight"] + "')";
                                cmd.CommandText = txtQuery;
                                cmd.ExecuteNonQuery();
                                // TotalInsertcount++;
                            }
                            else
                            {
                                var txtQuery = "UPDATE PLUTbl SET ID = '" + plu + "',ItemCode = '" + plu + "',DepartmentID = '" + item["DepartmentID"] + "',Name1 = '" + item["ProductName"].ToString().Replace("'", "''") + "',Price = '" + item["Price"] + "',UnitID = '" + item["UnitMeasureID"] + "',BarcodeType1 = '150', TareValue = '" + item["TareWeight"] + "' WHERE ID = '" + plu + "' AND ItemCode = '" + plu + "'";
                                cmd.CommandText = txtQuery;
                                cmd.ExecuteNonQuery();
                                // TotalUpdatedcount++;
                            }
                        }
                    }
                    //if (TotalInsertcount == Totalcount)
                    //{
                    //    MessageBox.Show(Totalcount + " " + "Records Inserted Successfully. ");
                    //}
                    //else
                    //{
                    //    if (TotalInsertcount > 0)
                    //    {
                    //        MessageBox.Show(TotalInsertcount + " " + "Records Inserted Successfully. ");
                    //    }
                    //    else if (TotalUpdatedcount > 0)
                    //    {
                    //        MessageBox.Show(TotalUpdatedcount + " " + "Records Updated Successfully. ");
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("No New Record Found for Insert/Update. ");
                    //    }
                    //}
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SyncUOM(DataTable dt)
        {
           // cs = @"Data Source=C:\Program Files (x86)\Aclas LINK66\Database\Default.db;Version=3;";
            try
            {
                var conn = new SQLiteConnection(cs);
                conn.Open();
                //int Totalcount = 0;
                //int TotalInsertcount = 0;
                //int TotalUpdatedcount = 0;
                var cmd = new SQLiteCommand(conn);
                if (dt.Rows.Count > 0)
                {
                    //Totalcount = dt.Rows.Count;
                    foreach (DataRow item in dt.Rows)
                    {
                        var checkName = "SELECT COUNT(1) FROM UnitTbl WHERE Name = '" + item["Description"] + "'";
                        cmd.CommandText = checkName;
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 0)
                        {
                            var txtQuery = "INSERT INTO UnitTbl(ID,Name)VALUES('" + item["UnitMeasureID"] + "','" + item["Description"] + "')";
                            cmd.CommandText = txtQuery;
                            cmd.ExecuteNonQuery();
                            //TotalInsertcount++;
                        }
                        else
                        {
                            var txtQuery = "UPDATE UnitTbl SET ID = '" + item["UnitMeasureID"] + "',Name ='" + item["Description"] + "' WHERE ID = '" + item["UnitMeasureID"] + "'";
                            cmd.CommandText = txtQuery;
                            cmd.ExecuteNonQuery();
                            //TotalUpdatedcount++;
                        }
                    }
                    //if (TotalInsertcount == Totalcount)
                    //{
                    //    MessageBox.Show(Totalcount + " " + "Records Inserted Successfully. ");
                    //}
                    //else
                    //{
                    //    if (TotalInsertcount > 0)
                    //    {
                    //        MessageBox.Show(TotalInsertcount + " " + "Records Inserted Successfully. ");
                    //    }
                    //    else if (TotalUpdatedcount > 0)
                    //    {
                    //        MessageBox.Show(TotalUpdatedcount + " " + "Records Updated Successfully. ");
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("No New Record Found for Insert/Update. ");
                    //    }
                    //}
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
