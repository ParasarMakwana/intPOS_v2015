using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrmShortcutKey : MetroForm
    {
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public static SqlCeDataAdapter DataAdapter = null;
        public string VersionNo = XMLData.Version;

        public FrmShortcutKey()
        {
            InitializeComponent();
            //dataLoad();
            label2.Text = "Software Information";
            GridViewShortcutKey.ColumnHeadersVisible = false;
            GridViewShortcutKey.Rows.Add(1, "Manufacturer: ", "Intnet Inc.");
            GridViewShortcutKey.Rows.Add(1, "Model: ", "intPOS");
            GridViewShortcutKey.Rows.Add(1, "Ver. No: ", VersionNo);
            GridViewShortcutKey.Rows.Add(1, "NTEP CC: ", "21-102");
            GridViewShortcutKey.Columns["ShortcutKeyID"].Visible = false;
            GridViewShortcutKey.Columns["ShortcutKey"].Width = 330;
            GridViewShortcutKey.Columns["Description"].Width = 530;
        }
        public void dataLoad()
        {
            try
            {
                DataTable dt = new DataTable();
                if (LoginInfo.Connections)
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    var query = from SM in _db.tbl_ShortcutkeyMaster select SM;
                    dt = ClsCommon.LinqToDataTable(query);
                }
                else
                {
                    SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "SELECT ShortcutKeyID,ShortcutKey,Description FROM tbl_ShortcutkeyMaster";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridViewShortcutKey.DataSource = dt;
                    GridViewShortcutKey.Columns["ShortcutKeyID"].Visible = false;
                    GridViewShortcutKey.Columns["ShortcutKey"].Width = 261;
                    GridViewShortcutKey.Columns["Description"].Width = 600;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmShortcutKey + ex.StackTrace, ex.LineNumber());
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                Close();
                return true;
            }
            if (keyData == (Keys.D0))
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
