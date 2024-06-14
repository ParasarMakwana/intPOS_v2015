using MetroFramework.Forms;
using SFPOS.BAL;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Link;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmScaleSync : MetroForm
    {
        #region Properties
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        List<DepartmentMasterModel> LstDepartmentMasterModel = new List<DepartmentMasterModel>();
        List<ProductMasterModel> lstProductMasterModel = new List<ProductMasterModel>();
        List<UoMMasterModel> lstUoMMasterModel = new List<UoMMasterModel>();
        private DepartmentService _DepartmentService = new DepartmentService();
        ProductService _ProductService = new ProductService();
        UoMService _UoMService = new UoMService();
        frmSyncDataToLink sd = new frmSyncDataToLink();
        #endregion

        public FrmScaleSync()
        {
            InitializeComponent();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(loader));
                thread.Start();

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

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

        private void loader()
        {
            try
            {
                if (chkBoxList.CheckedItems.Count > 0)
                {
                    if (picLoader.InvokeRequired)
                    {
                        picLoader.Invoke(new MethodInvoker(delegate { picLoader.Visible = true; }));
                    }
                    for (int i = 0; i < chkBoxList.CheckedItems.Count; i++)
                    {
                        if (chkBoxList.CheckedItems[i].ToString() == "Department")
                        {
                            DataTable departmentDT = new DataTable();
                            LstDepartmentMasterModel = _DepartmentService.GetAllDepartment();
                            departmentDT = ToDataTable(LstDepartmentMasterModel.Where(p => p.IsActive == true && p.IsDelete == false).ToList());
                            LogMessage(departmentDT.ToString());
                            sd.SyncDepartment(departmentDT);

                        }
                        if (chkBoxList.CheckedItems[i].ToString() == "Products")
                        {
                            lstProductMasterModel = _ProductService.GetAllProduct("",false);
                            DataTable productDT = new DataTable();
                            productDT = ToDataTable(lstProductMasterModel.Where(p => p.LabeledPrice == true).ToList());
                            LogMessage(productDT.ToString());
                            sd.SyncProduct(productDT);
                        }
                        if (chkBoxList.CheckedItems[i].ToString() == "Unit Of Measures")
                        {
                             lstUoMMasterModel = _UoMService.GetAllUoM();
                            DataTable uomDT = new DataTable();
                            uomDT = ToDataTable(lstUoMMasterModel.Where(p => p.IsActive == true && p.IsDelete == false).ToList());
                            LogMessage(uomDT.ToString());
                            sd.SyncUOM(uomDT);
                        }
                    }
                    if (picLoader.InvokeRequired)
                    {
                        LogMessage(picLoader.InvokeRequired.ToString());
                        picLoader.Invoke(new MethodInvoker(delegate { picLoader.Visible = false; }));
                    }
                }
                else
                {
                    MessageBox.Show("Select At Least One Checkbox");
                }
            }
            catch (Exception ex)
            {
                LogMessage(ex.ToString());
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }


        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        
    }
}
