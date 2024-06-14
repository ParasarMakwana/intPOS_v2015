using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace intPOSSetup
{
    public partial class SelectServerDetails : Form
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        public Boolean dbValid = false;

        public Boolean IsPrev = false;

        public string pathStoreProceduresFile = string.Empty;

        public string DatabaseServer = string.Empty;
        public string DatabaseName = string.Empty;
        public string UserName = string.Empty;
        public string Password = string.Empty;

        public SelectServerDetails()
        {
            InitializeComponent();
        }

       

        private void btnBack_Click(object sender, EventArgs e)
        {
            IsPrev = true;

            this.Hide();
            this.Close();
            OnMyEvent(this, new EventArgs());
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtServerName.Text.Trim()))
                {
                    MessageBox.Show("Please enter database server name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (String.IsNullOrEmpty(txtDataBaseName.Text.Trim()))
                {
                    MessageBox.Show("Please enter database name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (String.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    MessageBox.Show("Please enter user name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("Please enter password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DatabaseServer = txtServerName.Text.Trim();
                    DatabaseName = txtDataBaseName.Text.Trim();
                    UserName = txtUserName.Text.Trim();
                    Password = txtPassword.Text.Trim();

                    string Connstring = "data source = " + txtServerName.Text.Trim() + "; initial catalog = " + txtDataBaseName.Text.Trim() + "; persist security info = True; user id = " + txtUserName.Text.Trim() + "; password = " + txtPassword.Text.Trim() + "; ";

                    btnTestConnection.Text = "Please wait...";
                    dbValid = Db_existCheck(Connstring, "data source = " + txtServerName.Text.Trim() + "; initial catalog = master; persist security info = True; user id = " + txtUserName.Text.Trim() + "; password = " + txtPassword.Text.Trim() + "; ");

                    if (dbValid)
                    {
                        dbValid = false;
                        dbValid = db_Connection(Connstring);
                        if (dbValid)
                        {
                            MessageBox.Show("Database details are validate, Please save details !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Database details are not validate, Please check and try again !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Database details are not validate, Please check and try again !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    btnTestConnection.Text = "Test Connection";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool db_Connection(string Conn)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection MyConn = new SqlConnection(Conn);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = MyConn;
                cmd.CommandText = "select EmployeeID from tbl_EmployeeMaster ";
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                MyConn.Open();

                adp.Fill(dt);

                MyConn.Close();

                if (dt != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        private bool Db_existCheck(string Conn, string MasterConn)
        {
            string sqlCreateDBQuery;
            try
            {
                SqlConnection MyConn = new SqlConnection(MasterConn);

                sqlCreateDBQuery = "SELECT isnull(count(*),0) FROM master.dbo.sysdatabases where name = \'" + DatabaseName + "\'";

                using (MyConn)
                {
                    MyConn.Open();
                    // MyConn.ChangeDatabase("master");

                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, MyConn))
                    {
                        int exists = (int)sqlCmd.ExecuteScalar();

                        if (exists <= 0)
                        {
                            if (MessageBox.Show("Given database not exist in database server, Do you want to create new database?", "intPOS Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                            {
                                return CreateNewDb(Conn, MasterConn);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private FolderBrowserDialog dblocation;
        private bool CreateNewDb(string Conn, string MasterConn)
        {
            try
            {
                dblocation = new FolderBrowserDialog();
                DialogResult result = dblocation.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string DbPath = dblocation.SelectedPath;

                    SqlConnection MyConn = new SqlConnection(MasterConn);
                    ServerConnection con = new ServerConnection(MyConn.DataSource.ToString());
                    Server myServer = new Server(con);

                    Restore sqlRestore = new Restore();

                    BackupDeviceItem deviceItem = new BackupDeviceItem(pathStoreProceduresFile, DeviceType.File);
                    sqlRestore.Devices.Add(deviceItem);
                    sqlRestore.Database = DatabaseName;

                    ServerConnection connection = new ServerConnection(DatabaseServer, UserName, Password);
                    Server sqlServer = new Server(connection);

                    Database db = sqlServer.Databases[DatabaseName];
                    sqlRestore.Action = RestoreActionType.Database;

                    //Create The Restore Database Ldf & Mdf file name
                    String dataFileLocation = DbPath + DatabaseName + ".mdf";
                    String logFileLocation = DbPath + DatabaseName + "_Log.ldf";
                    db = sqlServer.Databases[DatabaseName];
                    RelocateFile rf = new RelocateFile(DatabaseName, dataFileLocation);

                    // Replace ldf, mdf file name of selected Backup file (in that case innboard20151215020030.bak)
                    System.Data.DataTable logicalRestoreFiles = sqlRestore.ReadFileList(sqlServer);
                    sqlRestore.RelocateFiles.Add(new RelocateFile(logicalRestoreFiles.Rows[0][0].ToString(), dataFileLocation));
                    sqlRestore.RelocateFiles.Add(new RelocateFile(logicalRestoreFiles.Rows[1][0].ToString(), logFileLocation));

                    sqlRestore.ReplaceDatabase = true;

                    progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 100;
                    progressBar1.Value = 1;

                    /* Wiring up events for progress monitoring */
                    sqlRestore.PercentComplete += CompletionStatusInPercent;
                    sqlRestore.Complete += Restore_Completed;

                    sqlRestore.SqlRestore(sqlServer);
                    db = sqlServer.Databases[DatabaseName];
                    db.SetOnline();
                    sqlServer.Refresh();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private void CompletionStatusInPercent(object sender, PercentCompleteEventArgs args)
        {
            progressBar1.Value = args.Percent;
            //Console.WriteLine("Percent completed: {0}%.", args.Percent);
        }

        private void Restore_Completed(object sender, ServerMessageEventArgs args)
        {
            progressBar1.Visible = false;
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            if (dbValid == false)
            {
                DatabaseServer = txtServerName.Text.Trim();
                DatabaseName = txtDataBaseName.Text.Trim();
                UserName = txtUserName.Text.Trim();
                Password = txtPassword.Text.Trim();

                string Connstring = "data source = " + txtServerName.Text.Trim() + "; initial catalog = " + txtDataBaseName.Text.Trim() + "; persist security info = True; user id = " + txtUserName.Text.Trim() + "; password = " + txtPassword.Text.Trim() + "; ";

                btnTestConnection.Text = "Please wait...";
                dbValid = Db_existCheck(Connstring, "data source = " + txtServerName.Text.Trim() + "; initial catalog = master; persist security info = True; user id = " + txtUserName.Text.Trim() + "; password = " + txtPassword.Text.Trim() + "; ");

                if (dbValid)
                {
                    dbValid = false;
                    dbValid = db_Connection(Connstring);
                    if (dbValid)
                    {
                        IsPrev = false;

                        this.Hide();
                        this.Close();
                        OnMyEvent(this, new EventArgs());
                    }
                    else
                    {
                        MessageBox.Show("Database details are not validate, Please check and try again !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Database details are not validate, Please check and try again !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                btnTestConnection.Text = "Test Connection";
            }
            else
            {
                IsPrev = false;

                this.Hide();
                this.Close();
                OnMyEvent(this, new EventArgs());
            }
        }
    }
}
