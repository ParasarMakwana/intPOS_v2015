
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.Data.Sql;

namespace intPOSSetup
{
    public partial class WelcomeScreen : Form
    {
        #region Declaration
        VersionType _VersionType;
        SelectComponent _SelectComponent;
        SelectServerDetails _SelectServerDetails;
        FolderLocation _FolderLocation;

        private const string Demoexe = "intPOS_Demo.exe";
        private const string Frontendexe = "intPOS_Frontend.exe";
        private const string Backendexe = "intPOS_Backend.exe";
        private const string LabelAppexe = "intPOS_Label.exe";
        private const string SQLExpressexe = "SQL2019-SSEI-Expr.exe";
        private const string dotNetFramework = "prerequisites\\.net Framework 4.6.1\\NDP461-KB3102436-x86-x64-AllOS-ENU.exe";

        private bool FrontOK = false;
        private bool BackOK = false;
        private bool LabelOK = false;
        private bool SQLOK = false;

        private const string SQLScripFile = "intPOS.Bak";

        private string SQLLocalDbName = "";

        string installationPath = string.Empty;
        string PackagePath = string.Empty;

        int intPOSType_ = 2;

        public string DatabaseServer = string.Empty;
        public string DatabaseName = string.Empty;
        public string UserName = string.Empty;
        public string Password = string.Empty;
        string ConnString = string.Empty;

        string SetupVersion = "2.2.1";
        #endregion Declaration

        public WelcomeScreen()
        {
            InitializeComponent();

            //UpdateSetupFiles();

            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = false;

            PackagePath = Application.StartupPath + "\\Install Package\\";
            rtbTerms.Rtf = File.ReadAllText(PackagePath + "\\eula.rtf");

            this.Text = "intPOS - Setup version (" + SetupVersion + ")";

            try
            {
                string logFilePath = Application.StartupPath + "\\LogFilePOS.txt";
                FileInfo fi = new FileInfo(logFilePath);
                if (fi.Exists)
                    fi.Delete();
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
            }

           
        }

        private void NetFramework_Install()
        {
            try
            {
                EnterExceptionMessage(".Net  Framework start");

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.Arguments = "/s /v /qn"; //" / SILENT | /VERYSILENT [/SUPPRESSMSGBOXES]";//psi.Arguments = "/s /v /qn /min";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.ErrorDialog = true;
                psi.FileName = PackagePath + dotNetFramework;
                //psi.UseShellExecute = true;

                Process instproc;
                instproc = Process.Start(psi);
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
            }
        }

        public int LineNumber(Exception ex)
        {
            int linenum = 0;
            try
            {
                //Get a StackTrace object for the exception
                StackTrace st = new StackTrace(ex, true);

                //Get the first stack frame
                StackFrame frame = st.GetFrame(0);

                //Get the file name
                string fileName = frame.GetFileName();

                //Get the method name
                string methodName = frame.GetMethod().Name;

                //Get the line number from the stack frame
                int line = frame.GetFileLineNumber();

                //Get the column number
                //int col = frame.GetFileColumnNumber();
                //linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(' ')));
                linenum = line;
            }
            catch
            {

            }
            return linenum;
        }

        private void EnterExceptionMessage(string exMessage)
        {
            try
            {
                exMessage = "\n\n" + exMessage;
                string logFilePath = Application.StartupPath + "\\LogFilePOS.txt";
                File.AppendAllText(logFilePath, exMessage + Environment.NewLine);
            }
            catch (Exception)
            {
                
            }
        }

        private Boolean CheckFor45DotVersion(int releaseKey)
        {
            Boolean vFound = false;
            //if (releaseKey >= 528040)
            //{
            //    return "4.8 or later";
            //}
            //if (releaseKey >= 461808)
            //{
            //    return "4.7.2 or later";
            //}
            //if (releaseKey >= 461308)
            //{
            //    return "4.7.1 or later";
            //}
            //if (releaseKey >= 460798)
            //{
            //    return "4.7 or later";
            //}
            //if (releaseKey >= 394802)
            //{
            //    return "4.6.2 or later";
            //}
            if (releaseKey >= 394254)
            {
                //return "4.6.1 or later";
                vFound = true;
            }
            //if (releaseKey >= 393295)
            //{
            //    return "4.6 or later";
            //}
            //if (releaseKey >= 393273)
            //{
            //    return "4.6 RC or later";
            //}
            //if ((releaseKey >= 379893))
            //{
            //    return "4.5.2 or later";
            //}
            //if ((releaseKey >= 378675))
            //{
            //    return "4.5.1 or later";
            //}
            //if ((releaseKey >= 378389))
            //{
            //    return "4.5 or later";
            //}
            // This line should never execute. A non-null release key should mean 
            // that 4.5 or later is installed. 
            return vFound;
        }

        private void UpdateSetupFiles()
        {
            try
            {
                string SourcePath = string.Empty;
                string SourcePathFE = string.Empty;
                string SourcePathBE = string.Empty;
                string SourcePathDemo = string.Empty;
                string SourcePathReq = string.Empty;
                string SourcePathLabel = string.Empty;
                string DestinationPath = string.Empty;
                DestinationPath = Application.StartupPath + "\\Install Package\\";
                SourcePath = DestinationPath;

                while (SourcePath.Contains("intPOSSetup"))
                {
                    SourcePath = SourcePath.Substring(0, SourcePath.LastIndexOf("\\"));
                }

                SourcePathFE = SourcePath + "\\ezPOSPro_Frontend\\bin\\ezPOSPro\\ezPOSPro_Frontend.exe";
                SourcePathBE = SourcePath + "\\ezPOSPro_\\bin\\ezPOSPro\\ezPOSPro.exe";
                SourcePathDemo = SourcePath + "\\intPOSEvaluation\\bin\\intPOSEvalution\\intPOSEvaluation.exe";
                SourcePathReq = SourcePath + "\\intPOSEvaluation\\bin\\intPOSEvalution\\prerequisites";

                while (SourcePath.Contains("intPOS"))
                {
                    SourcePath = SourcePath.Substring(0, SourcePath.LastIndexOf("\\"));
                }
                SourcePathLabel = SourcePath + "\\IntPOS_Label_Printing\\ezPOS_Label\\bin\\Debug\\ezPOS_Label.exe";

                FileInfo fi = new FileInfo(SourcePathFE);
                    fi.CopyTo(DestinationPath + Frontendexe, true);
               
                fi = new FileInfo(SourcePathBE);
                  fi.CopyTo(DestinationPath + Backendexe, true);
               
                fi = new FileInfo(SourcePathLabel);
                    fi.CopyTo(DestinationPath + LabelAppexe, true);

                fi = new FileInfo(SourcePathDemo);
                fi.CopyTo(DestinationPath + Demoexe, true);

                string[] filePaths = Directory.GetFiles(SourcePathReq);
                foreach (var filename in filePaths)
                {
                    string file = filename.ToString();

                    string str = DestinationPath + "\\prerequisites" + filename.Substring(filename.LastIndexOf('\\'));
                    fi = new FileInfo(file);
                    fi.CopyTo(str, true);
                }
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            installationPath = "C:\\Program Files (x86)\\Intnet Inc - intPOS";

            _VersionType = new VersionType();
            _VersionType.VersionNo = 0;
            _VersionType.IsPrev = false;
            _VersionType.OnMyEvent += new VersionType.onMyEventHandler(CatchVersionTypeCardEvent_);
            _VersionType.ShowDialog();

            //System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            //DataTable table = instance.GetDataSources();

            //if (table.Rows.Count > 0)
            //{
            //    MessageBox.Show("");
            //}

        }

        private List<string> GetLocalDBInstances()
        {
            List<string> lstInstances = new List<string>();
            try
            {
                // Start the child process.
                Process p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/C sqllocaldb info";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.Start();
                // Do not wait for the child process to exit before
                // reading to the end of its redirected stream.
                // p.WaitForExit();
                // Read the output stream first and then wait.
                string sOutput = p.StandardOutput.ReadToEnd();
                //p.WaitForExit();
                int tryval = 0;
                while (tryval < 5)
                {
                    int maxproce = 30;

                    for (int i = 0; i < 100; i++)
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                        if (progressBar1.Value < maxproce)
                        {
                            progressBar1.Value += 1;
                        }
                    }
                    tryval += 1;
                    if (maxproce < 450)
                        maxproce += 30;
                }
                //If LocalDb is not installed then it will return that 'sqllocaldb' is not recognized as an internal or external command operable program or batch file.
                if (sOutput == null || sOutput.Trim().Length == 0 || sOutput.Contains("not recognized"))
                {
                    EnterExceptionMessage(" - getlocaldbinstance (" + sOutput.ToString());
                    return null;
                }
                string[] instances = sOutput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
               foreach (var item in instances)
                {
                    if (item.Trim().Length > 0)
                    {
                        lstInstances.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + " - getlocaldbinstance (" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
                MessageBox.Show(ex.Message);
            }
            return lstInstances;
        }

        //private List<string> GetDataSources()
        //{
        //    List<string> lstInstances = new List<string>();
        //    try
        //    {
        //       string ServerName = Environment.MachineName;
        //        RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
        //        using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
        //        {
        //            RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
        //            if (instanceKey != null)
        //            {
        //                foreach (var instanceName in instanceKey.GetValueNames())
        //                {
        //                    lstInstances.Add(instanceName);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
        //    }
        //    return lstInstances;
        //}

        void CatchFolderLocationCardEvent_(object sender, EventArgs e)
        {
            if (_FolderLocation.installationPath != string.Empty)
                installationPath = _FolderLocation.installationPath;

            if (_FolderLocation.installationPath != string.Empty)
                installationPath = _FolderLocation.installationPath;

            DirectoryInfo di = new DirectoryInfo(installationPath);
            if (!di.Exists)
                di.Create();

        }

        void CatchVersionTypeCardEvent_(object sender, EventArgs e)
        {
            if (_VersionType == null || intPOSType_ == 0)
            {
                _VersionType = new VersionType();
                _VersionType.VersionNo = 0;
                _VersionType.OnMyEvent += new VersionType.onMyEventHandler(CatchVersionTypeCardEvent_);
                _VersionType.ShowDialog();
            }
            else if (_VersionType.IsPrev == true)
            {
                this.Show();
            }
            else if (_VersionType.VersionNo == 1)
            {
                _SelectComponent = new SelectComponent();
                _SelectComponent.InsFrontend = true;
                _SelectComponent.InsBackend = true;
                _SelectComponent.InsLabelApp = true;
                _SelectComponent.OnMyEvent += new SelectComponent.onMyEventHandler(CatchSelectComponentCardEvent_);
                _SelectComponent.ShowDialog();
            }
            else
            {
                InstallComponents();
            }
        }

        void CatchSelectComponentCardEvent_(object sender, EventArgs e)
        {
            if (_SelectComponent == null)
            {
                _SelectComponent = new SelectComponent();
                _SelectComponent.InsFrontend = true;
                _SelectComponent.InsBackend = true;
                _SelectComponent.InsLabelApp = true;
                _SelectComponent.OnMyEvent += new SelectComponent.onMyEventHandler(CatchSelectComponentCardEvent_);
                _SelectComponent.ShowDialog();
            }
            else if (_SelectComponent.IsPrev == true)
            {
                _VersionType = new VersionType();
                _VersionType.VersionNo = 0;
                _VersionType.OnMyEvent += new VersionType.onMyEventHandler(CatchVersionTypeCardEvent_);
                _VersionType.ShowDialog();
            }
            else
            {
                _SelectServerDetails = new SelectServerDetails();
                _SelectServerDetails.pathStoreProceduresFile = PackagePath + "\\" + SQLScripFile;
                _SelectServerDetails.OnMyEvent += new SelectServerDetails.onMyEventHandler(CatchDabaseServerCardEvent_);
                _SelectServerDetails.ShowDialog();
            }
        }

        void CatchDabaseServerCardEvent_(object sender, EventArgs e)
        {
            if (_SelectServerDetails == null)
            {
                _SelectServerDetails = new SelectServerDetails();
                _SelectServerDetails.OnMyEvent += new SelectServerDetails.onMyEventHandler(CatchDabaseServerCardEvent_);
                _SelectServerDetails.ShowDialog();
            }
            else if (_SelectServerDetails.IsPrev == true)
            {
                _SelectComponent = new SelectComponent();
                _SelectComponent.InsFrontend = true;
                _SelectComponent.InsBackend = true;
                _SelectComponent.InsLabelApp = true;
                _SelectComponent.OnMyEvent += new SelectComponent.onMyEventHandler(CatchSelectComponentCardEvent_);
                _SelectComponent.ShowDialog();
            }
            else
            {
                if (_SelectServerDetails != null)
                {
                    DatabaseServer = _SelectServerDetails.DatabaseServer;
                    DatabaseName = _SelectServerDetails.DatabaseName;
                    UserName = _SelectServerDetails.UserName;
                    Password = _SelectServerDetails.Password;
                }
                InstallComponents();
            }
        }

        private void UpdateIconFiles(string iPath)
        {
            try
            {
                string Mainfile = iPath + "\\favicon.ico";
                FileInfo fi = new FileInfo(Mainfile);

                if (fi.Exists)
                    fi.Delete();

                fi = new FileInfo(iPath + "\\intPOS.ico");
                if (fi.Exists)
                    fi.CopyTo(Mainfile);
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + " - update logo file (" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
            }

            try
            {
                string Mainfile = iPath.Replace("Backend", "Frontend") + "\\favicon.ico";
                FileInfo fi = new FileInfo(Mainfile);

                if (fi.Exists)
                    fi.Delete();

                fi = new FileInfo(iPath + "\\intPOS.ico");
                if (fi.Exists)
                    fi.CopyTo(Mainfile);
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + " - update logo file (" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
            }
        }

        private void InstallComponents()
        {
            this.Show();
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 500;
            progressBar1.Value = 1;
            lblProgressmsg.Text = "Setup Initialization";

            if (_VersionType.VersionNo == 0)
            {
                try
                {
                    Demo_Install();

                    //Frontend_Install();

                    //Backend_Install();

                   // LabelApp_Install();

                    SQLOK = false;
                    FrontOK = false;
                    BackOK = false;
                    LabelOK = false;

                    for (int i = 0; i < 100; i++)
                    {
                        Thread.Sleep(1000);
                        Application.DoEvents();
                        if (progressBar1.Value < 100)
                        {
                            progressBar1.Value += 1;
                        }
                    }

                    lblProgressmsg.Text = "Installing SQLLocalDB";

                    try
                    {
                        while (SQLOK == false)
                        {
                            List<string> dblist;
                            dblist = GetLocalDBInstances();
                            if (dblist !=null && dblist.Count > 0)
                            {
                                SQLLocalDbName = dblist[0];

                                SQLOK = true;
                            }
                            else
                            {
                                EnterExceptionMessage("local db name not found." );
                                SQLLocalDbName = "MSSQLLocalDB";

                                SQLOK = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        EnterExceptionMessage(ex.Message + " - getlocaldbinstance name (" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
                        MessageBox.Show(ex.Message);
                    }


                    lblProgressmsg.Text = "Finalizing Setup";

                    while (FrontOK == false)
                    {
                        Update_XMLFileFB(installationPath + "\\Frontend\\", "Front");
                    }

                    while (BackOK == false)
                    {
                        Update_XMLFileFB(installationPath + "\\Backend\\", "Back");
                    }

                    UpdateIconFiles(installationPath + "\\Backend\\");
                }
                catch (Exception ex)
                {
                   EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
                }
            }
            else
            {
                try
                {
                    if (_SelectComponent.InsFrontend == true)
                    {
                        Frontend_Install();
                    }

                    if (_SelectComponent.InsBackend == true)
                    {
                        Backend_Install();
                    }

                    if (_SelectComponent.InsLabelApp == true)
                    {
                        LabelApp_Install();
                    }

                    SQLOK = false;
                    FrontOK = false;
                    BackOK = false;
                    LabelOK = false;

                    for (int i = 0; i < 100; i++)
                    {
                        Thread.Sleep(1000);
                        if (progressBar1.Value < 100)
                        {
                            progressBar1.Value += 1;
                        }
                    }

                    if (_SelectComponent.InsFrontend == true)
                    {
                        try
                        {
                            while (FrontOK == false)
                            {
                                Update_XMLFileFB(installationPath + "\\Frontend\\", "Front");
                            }
                        }
                        catch (Exception ex)
                        {
                            EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
                        }
                    }

                    if (_SelectComponent.InsBackend == true)
                    {
                        while (BackOK == false)
                        {
                            Update_XMLFileFB(installationPath + "\\Backend\\", "Back");
                        }

                        if (_VersionType.VersionNo == 1)
                        {
                            string mdffiledst = installationPath + "\\Backend\\AppData\\intPOS.mdf";
                            string ldffiledst = installationPath + "\\Backend\\AppData\\intPOS_log.ldf";

                            FileInfo fdsti = new FileInfo(mdffiledst);
                            if (fdsti.Exists)
                                fdsti.Delete();

                            fdsti = new FileInfo(ldffiledst);
                            if (fdsti.Exists)
                                fdsti.Delete();
                        }
                    }

                    if (_SelectComponent.InsLabelApp == true)
                    {
                        try
                        {
                            while (LabelOK == false)
                            {
                                Update_XMLFileLabel(installationPath + "\\Backend\\");
                            }
                        }
                        catch (Exception ex)
                        {
                            EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
                        }
                    }
                   

                    lblProgressmsg.Text = "Finalizing Setup";
                }
                catch (Exception ex)
                {
                    EnterExceptionMessage(ex.Message + " - install component (" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
                }
            }


            this.Close();
        }
        
        private void Demo_Install()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.Arguments = "/s /v /q /norestart /qn REBOOT=ReallySuppress"; 
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.ErrorDialog = true;
                psi.FileName = PackagePath + "prerequisites\\VC_redist.x64.exe";
              
                Process instproc;
                instproc = Process.Start(psi);
                lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
                while (instproc.HasExited == false)
                {
                    Application.DoEvents();
                    Thread.Sleep(250);
                    if (progressBar1.Value < 90)
                    {
                       progressBar1.Value += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + " - demo install (" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
            }

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.Arguments = "/s /v /q /norestart /qn REBOOT=ReallySuppress";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.ErrorDialog = true;
                psi.FileName = PackagePath + "prerequisites\\vc_redist.x86.exe";

                Process instproc;
                instproc = Process.Start(psi);
                lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
                while (instproc.HasExited == false)
                {
                    Application.DoEvents();
                    Thread.Sleep(250);
                    if (progressBar1.Value < 150)
                    {
                        progressBar1.Value += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + " - demo install (" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
            }
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.Arguments = "/s /v ";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.ErrorDialog = true;
                psi.FileName = PackagePath + "prerequisites\\Zebra_CoreScanner_Driver_(32bit)\\Zebra_CoreScanner_Driver_(32bit).exe";

                Process instproc;
                instproc = Process.Start(psi);
                lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
                while (instproc.HasExited == false)
                {
                    Application.DoEvents();
                    Thread.Sleep(250);
                    if (progressBar1.Value < 90)
                    {
                        progressBar1.Value += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + " - demo install (" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
            }
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.Arguments = "/s /v ";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.ErrorDialog = true;
                psi.FileName = PackagePath + "prerequisites\\Zebra_CoreScanner_Driver_(64bit)\\Zebra_CoreScanner_Driver_(64bit).exe";

                Process instproc;
                instproc = Process.Start(psi);
                lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
                while (instproc.HasExited == false)
                {
                    Application.DoEvents();
                    Thread.Sleep(250);
                    if (progressBar1.Value < 90)
                    {
                        progressBar1.Value += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + " - demo install (" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
            }
            string frontendCheck = installationPath + "\\Frontend\\AppData\\intPOS.sdf";
            FileInfo fi = new FileInfo(frontendCheck);
            //if (fi.Exists)
            //{
            //    try
            //    {
            //        ProcessStartInfo psi = new ProcessStartInfo();
            //        psi.Arguments = "/v /x"; //qn" / SILENT | /VERYSILENT [/SUPPRESSMSGBOXES]";//psi.Arguments = "/s /v /qn /min";
            //        psi.CreateNoWindow = true;
            //        psi.WindowStyle = ProcessWindowStyle.Hidden;
            //        psi.ErrorDialog = true;
            //        psi.FileName = PackagePath + Frontendexe;
            //        //psi.UseShellExecute = true;

            //        Process instproc;
            //        instproc = Process.Start(psi);
            //        lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
            //        while (instproc.HasExited == false)
            //        {
            //            Application.DoEvents();
            //            Thread.Sleep(250);
            //            if (progressBar1.Value < 50)
            //            {
            //                progressBar1.Value += 1;
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
            //    }
            //}
            // else
            {
                try
                {
                    //EnterExceptionMessage("Frontend start");
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.Arguments = "/s /v /qn"; //qn" / SILENT | /VERYSILENT [/SUPPRESSMSGBOXES]";//psi.Arguments = "/s /v /qn /min";
                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Hidden;
                    psi.ErrorDialog = true;
                    psi.FileName = PackagePath + Demoexe;
                    //psi.UseShellExecute = true;

                    Process instproc;
                    instproc = Process.Start(psi);
                    lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
                    while (instproc.HasExited == false)
                    {
                        Application.DoEvents();
                        Thread.Sleep(250);
                        if (progressBar1.Value < 300)
                        {
                            progressBar1.Value += 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    EnterExceptionMessage(ex.Message + " - demo install (" + ex.InnerException + ")" + ex.StackTrace + " - " + LineNumber(ex));
                }
            }
        }

        private void Frontend_Install()
        {
            string frontendCheck = installationPath + "\\Frontend\\AppData\\intPOS.sdf";
            FileInfo fi = new FileInfo(frontendCheck);
            //if (fi.Exists)
            //{
            //    try
            //    {
            //        ProcessStartInfo psi = new ProcessStartInfo();
            //        psi.Arguments = "/v /x"; //qn" / SILENT | /VERYSILENT [/SUPPRESSMSGBOXES]";//psi.Arguments = "/s /v /qn /min";
            //        psi.CreateNoWindow = true;
            //        psi.WindowStyle = ProcessWindowStyle.Hidden;
            //        psi.ErrorDialog = true;
            //        psi.FileName = PackagePath + Frontendexe;
            //        //psi.UseShellExecute = true;

            //        Process instproc;
            //        instproc = Process.Start(psi);
            //        lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
            //        while (instproc.HasExited == false)
            //        {
            //            Application.DoEvents();
            //            Thread.Sleep(250);
            //            if (progressBar1.Value < 50)
            //            {
            //                progressBar1.Value += 1;
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
            //    }
            //}
           // else
            {
                try
                {
                    EnterExceptionMessage("Frontend start");
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.Arguments = "/s /v /qn"; //qn" / SILENT | /VERYSILENT [/SUPPRESSMSGBOXES]";//psi.Arguments = "/s /v /qn /min";
                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Hidden;
                    psi.ErrorDialog = true;
                    psi.FileName = PackagePath + Frontendexe;
                    //psi.UseShellExecute = true;

                    Process instproc;
                    instproc = Process.Start(psi);
                    lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
                    while (instproc.HasExited == false)
                    {
                        Application.DoEvents();
                        Thread.Sleep(250);
                        if (progressBar1.Value < 50)
                        {
                            progressBar1.Value += 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    EnterExceptionMessage(ex.Message + " - frontend install (" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
                }
            }
        }

        private void Backend_Install()
        {
            string frontendCheck = installationPath + "\\Backend\\AppData\\intPOS.sdf";
            FileInfo fi = new FileInfo(frontendCheck);
            //if (fi.Exists)
            //{
            //    try
            //    {
            //        ProcessStartInfo psi = new ProcessStartInfo();
            //        psi.Arguments = "/v /x"; //qn" / SILENT | /VERYSILENT [/SUPPRESSMSGBOXES]";//psi.Arguments = "/s /v /qn /min";
            //        psi.CreateNoWindow = true;
            //        psi.WindowStyle = ProcessWindowStyle.Hidden;
            //        psi.ErrorDialog = true;
            //        psi.FileName = PackagePath + Backendexe;
            //        //psi.UseShellExecute = true;
                    
            //        Process instproc;
            //        instproc = Process.Start(psi);
            //        lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
            //        while (instproc.HasExited == false)
            //        {
            //            lblProgressmsg.Text = instproc.StandardOutput.ToString();
            //            lblProgressmsg.Text = instproc.ToString();
            //            Application.DoEvents();
            //            Thread.Sleep(250);
            //             if (progressBar1.Value < 50)
            //            {
            //                progressBar1.Value += 1;
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        EnterExceptionMessage(ex.Message + "(" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
            //    }
            //}
            //else
            {
                try
                {
                    EnterExceptionMessage("Backend start");
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.Arguments = "/s /v /qn"; //" / SILENT | /VERYSILENT [/SUPPRESSMSGBOXES]";//psi.Arguments = "/s /v /qn /min";
                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Hidden;
                    psi.ErrorDialog = true;
                    psi.FileName = PackagePath + Backendexe;
                    //psi.UseShellExecute = true;

                    Process instproc;
                    instproc = Process.Start(psi);
                    lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
                    while (instproc.HasExited == false)
                    {
                        EnterExceptionMessage(instproc.StandardOutput.ToString());
                        EnterExceptionMessage(lblProgressmsg.Text = instproc.ToString());
                        Application.DoEvents();
                        Thread.Sleep(250);
                        if (progressBar1.Value < 50)
                        {
                            progressBar1.Value += 1;
                        }
                    }


                }
                catch (Exception ex)
                {
                    EnterExceptionMessage(ex.Message + " - backend install (" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
                }
            }            
        }

        private void LabelApp_Install()
        {
            try
            {
                EnterExceptionMessage("Label App start");
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.Arguments = "/s /v /a /qn"; //" / SILENT | /VERYSILENT [/SUPPRESSMSGBOXES]";//psi.Arguments = "/s /v /qn /min";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.ErrorDialog = true;
                psi.FileName = PackagePath + LabelAppexe;
                //psi.UseShellExecute = true;

                Process instproc;
                instproc = Process.Start(psi);
                lblProgressmsg.Text = "Installing intPOS version (" + SetupVersion + ")";
                while (instproc.HasExited == false)
                {
                    Application.DoEvents();
                    Thread.Sleep(250);
                    if (progressBar1.Value < 50)
                    {
                        progressBar1.Value += 1;
                    }
                }

               
            }
            catch (Exception ex)
            {
                EnterExceptionMessage(ex.Message + " - label app install (" + ex.InnerException + ")" + ex.StackTrace + " - "+  LineNumber(ex));
            }
        }

        private void CopyLocalDbFile(string installationPath)
        {
            string mdffilesrc = PackagePath + "intPOS.mdf";
            string ldffilesrc = PackagePath + "intPOS_log.ldf";

            string mdffiledst = installationPath + "\\intPOS.mdf";
            string ldffiledst = installationPath + "\\intPOS_log.ldf";

            FileInfo fi = new FileInfo(mdffilesrc);
            FileInfo fdsti = new FileInfo(mdffiledst);
            if (fdsti.Exists)
                fdsti.Delete();
            fi.CopyTo(mdffiledst);

            fi = new FileInfo(ldffilesrc);
            fdsti = new FileInfo(ldffiledst);
            if (fdsti.Exists)
                fdsti.Delete();
            fi.CopyTo(ldffiledst);
        }

        public void Read_XMLFileFB(string VersionName, string POSType)
        {
            try
            {
                string exeFolder = Path.GetDirectoryName(VersionName);
                string path = Path.Combine(exeFolder, "ErrorLog21.xml");

                #region Decrypt_File and Read
                Decrypt_FileFB(VersionName);
                #endregion

                XDocument xdoc = XDocument.Load(path);
                XElement ezPOSProSettings = xdoc.Element("ezPOSProSettings");

                var _POSInfo = (from POSInfo in ezPOSProSettings.Elements("ezPOSPro")
                                from x in POSInfo.Elements("POSInfo")
                                select new
                                {
                                    POSType = x.Attribute("POSType").Value,
                                    InstallationDate = x.Attribute("InstallationDate").Value,
                                    Info = x.Attribute("Info").Value,
                                    Version = x.Attribute("Version").Value
                                }).ToList()[0];

                var _Status = (from Status in ezPOSProSettings.Elements("ezPOSPro")
                               from x in Status.Elements("Status")
                               select new
                               {
                                   Type = (x.Attribute("Type").Value),
                                   POSStatus = (x.Attribute("POSStatus").Value),
                                   SyncStatus = (x.Attribute("SyncStatus").Value)
                               }).ToList()[0];

                var _Scanner = (from Scanner in ezPOSProSettings.Elements("ezPOSPro")
                                from x in Scanner.Elements("Scanner")
                                select new
                                {
                                    ScannerType = (x.Attribute("ScannerType").Value),
                                    ScaleType = (x.Attribute("ScaleType").Value),
                                    ScannerInUsed = (x.Attribute("ScannerInUsed").Value)
                                }).ToList()[0];

                var _POSKey = (from POSKey in ezPOSProSettings.Elements("ezPOSPro")
                               from x in POSKey.Elements("POSKey")
                               select new
                               {
                                   Key = (x.Attribute("Key").Value)
                               }).ToList()[0];

                var _ReciptPrinter = (from ReciptPrinter in ezPOSProSettings.Elements("ezPOSPro")
                                      from x in ReciptPrinter.Elements("ReciptPrinter")
                                      select new
                                      {
                                          PrinterName = (x.Attribute("PrinterName").Value),
                                          Disclaimer = (x.Attribute("Disclaimer").Value)
                                      }).ToList()[0];

                var _DbConnet = (from DbConnet in ezPOSProSettings.Elements("ezPOSPro")
                                 from x in DbConnet.Elements("DbConnet")
                                 select new
                                 {
                                     ServerName = (x.Attribute("ServerName").Value),
                                     DbName = (x.Attribute("DbName").Value),
                                     UserName = (x.Attribute("UserName").Value),
                                     Password = (x.Attribute("Password").Value)
                                 }).ToList()[0];

                var _DbConnection = (from DbConnection in ezPOSProSettings.Elements("ezPOSPro")
                                     from x in DbConnection.Elements("DbConnection")
                                     select new
                                     {
                                         DbConnectionString = (x.Attribute("DbConnectionString").Value),
                                         PriorityCode = (x.Attribute("PriorityCode").Value)
                                     }).ToList()[0];

                var _DataSyncTime = (from DataSyncTime in ezPOSProSettings.Elements("ezPOSPro")
                                     from x in DataSyncTime.Elements("DataSyncTime")
                                     select new
                                     {
                                         LiveToLocal = (x.Attribute("LiveToLocal").Value),
                                         LocalToLive = (x.Attribute("LocalToLive").Value),
                                         OrderSuccessScreen = (x.Attribute("OrderSuccessScreen").Value)
                                     }).ToList()[0];

                // MessageBox.Show("XML read done");
                #region XMLData
                XMLData.POSType = _POSInfo.POSType;
                XMLData.InstallationDate = Convert.ToDateTime(_POSInfo.InstallationDate);
                XMLData.Info = _POSInfo.Info;
                XMLData.Version = _POSInfo.Version;
                XMLData.Type = Convert.ToInt32(_Status.Type);
                //XMLData.Type = 2;
                XMLData.EPXShow = 0;
                XMLData.POSStatus = Convert.ToBoolean(_Status.POSStatus);
                XMLData.SyncStatus = Convert.ToBoolean(_Status.SyncStatus);
                XMLData.Scanner = _Scanner.ScannerType;
                XMLData.Scale = _Scanner.ScaleType;
                XMLData.Key = _POSKey.Key;
                XMLData.PrinterName = _ReciptPrinter.PrinterName;
                XMLData.Disclaimer = _ReciptPrinter.Disclaimer;
                XMLData.ServerName = _DbConnet.ServerName;
                XMLData.DbName = _DbConnet.DbName;
                XMLData.UserName = _DbConnet.UserName;
                XMLData.Password = _DbConnet.Password;
                XMLData.DbConnectionString = _DbConnection.DbConnectionString;
                //XMLData.DbConnectionString = "data source = TBS24; initial catalog = intPOS_Dev; persist security info = True; user id = sa; password = sa@123";
                XMLData.PriorityCode = "";
                XMLData.LiveToLocalTime = !String.IsNullOrEmpty(_DataSyncTime.LiveToLocal) ? Convert.ToInt32(_DataSyncTime.LiveToLocal) : 5;
                if (XMLData.LiveToLocalTime == 0) { XMLData.LiveToLocalTime = 5; }
                XMLData.LocalToLiveTime = !String.IsNullOrEmpty(_DataSyncTime.LocalToLive) ? Convert.ToInt32(_DataSyncTime.LocalToLive) : 1;
                if (XMLData.LocalToLiveTime == 0) { XMLData.LiveToLocalTime = 1; }
                XMLData.OrderSuccessScreen = !String.IsNullOrEmpty(_DataSyncTime.OrderSuccessScreen) ? Convert.ToInt32(_DataSyncTime.OrderSuccessScreen) : 20;
                if (XMLData.OrderSuccessScreen == 0) { XMLData.OrderSuccessScreen = 20; }
                #endregion

                XMLData.POSStatus = Convert.ToBoolean(_Status.POSStatus);

                #region Delete Decrypt_File
                Delete_FileFB(VersionName);
                #endregion

                if (POSType == "Back")
                {
                    if (XMLData.DbConnectionString != string.Empty)
                        BackOK = true;
                }
                else
                {
                    if (XMLData.DbConnectionString != string.Empty)
                        FrontOK = true;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("XML Read Error:  " + e.Message.ToString());
                Delete_FileFB(VersionName);
            }
        }

        public void Update_XMLFileFB(string VersionName, string POSType)
        {
            Read_XMLFileFB(VersionName, POSType);

            try
            {
                EnterExceptionMessage("Update XML File");
                string installPath = VersionName;

                if (_VersionType.VersionNo == 0)
                {
                    var currentDirectorydb = VersionName;
                    var fileNamedb = Path.Combine(currentDirectorydb, "AppData\\intPOS.mdf");
                    fileNamedb = fileNamedb.Replace("\\\\", "\\");
                    fileNamedb = fileNamedb.Replace("Frontend", "Backend");

                    if (POSType == "Back")
                    {
                        XMLData.POSType = "intPOS-Backend";
                        XMLData.Type = 1;
                    }
                    else
                    {
                        XMLData.POSType = "intPOS-Frontend";
                        XMLData.Type = 2;
                    }
                    XMLData.POSStatus = false;
                    XMLData.ServerName = "";
                    XMLData.DbName = "";
                    XMLData.UserName = "";
                    XMLData.Password = "";
                    XMLData.DbConnectionString = "Data Source=(LocalDB)\\" + SQLLocalDbName + ";AttachDbFilename=" + fileNamedb + ";Integrated Security=True;Connect Timeout=30";
                    //XMLData.DbConnectionString = "Data Source=.\\" + SQLLocalDbName + "; AttachDbFilename =" + fileNamedb + ";Integrated Security=True;Connect Timeout=30;User Instance=True";
                    XMLData.IsDemoVersion = 1;
                }
                else
                {
                    if (POSType == "Back")
                    {
                        XMLData.POSType = "intPOS-Backend";
                        XMLData.Type = 1;
                    }
                    else
                    {
                        XMLData.POSType = "intPOS-Frontend";
                        XMLData.Type = 2;
                    }
                    XMLData.POSStatus = false;

                    XMLData.ServerName = DatabaseServer;
                    XMLData.DbName = DatabaseName;
                    XMLData.UserName = UserName;
                    XMLData.Password = Password;
                    XMLData.DbConnectionString = "data source=" + DatabaseServer + ";initial catalog=" + DatabaseName + ";persist security info=True;user id=" + UserName + ";password=" + Password + ";";
                    XMLData.IsDemoVersion = 0;
                }
                XMLData.InstallationDate = DateTime.Now;

                XDocument doc =
                             new XDocument(
                               new XElement("ezPOSProSettings",
                                 new XElement("ezPOSPro",
                                     new XElement("POSInfo", new XAttribute("POSType", XMLData.POSType), new XAttribute("InstallationDate", XMLData.InstallationDate), new XAttribute("Info", XMLData.Info), new XAttribute("Version", SetupVersion)),
                                     new XElement("Status", new XAttribute("Type", XMLData.Type), new XAttribute("POSStatus", XMLData.POSStatus), new XAttribute("SyncStatus", XMLData.SyncStatus)),
                                      new XElement("Scanner", new XAttribute("ScannerType", XMLData.Scanner), new XAttribute("ScaleType", XMLData.Scale), new XAttribute("ScannerInUsed", XMLData.ScannerInUsed)),
                                     new XElement("POSKey", new XAttribute("Key", XMLData.Key)),
                                     new XElement("ReciptPrinter", new XAttribute("PrinterName", XMLData.PrinterName), new XAttribute("Disclaimer", XMLData.Disclaimer)),
                                     new XElement("DbConnet", new XAttribute("ServerName", XMLData.ServerName), new XAttribute("DbName", XMLData.DbName), new XAttribute("UserName", XMLData.UserName), new XAttribute("Password", XMLData.Password)),
                                     new XElement("DbConnection", new XAttribute("DbConnectionString", XMLData.DbConnectionString), new XAttribute("PriorityCode", "")),
                                     new XElement("DataSyncTime", new XAttribute("LiveToLocal", XMLData.LiveToLocalTime), new XAttribute("LocalToLive", XMLData.LocalToLiveTime), new XAttribute("OrderSuccessScreen", XMLData.OrderSuccessScreen)),
                                     new XElement("intPOSVer", new XAttribute("intPOSVer", XMLData.IsDemoVersion))
                                 )));

                var currentDirectory = Path.Combine(VersionName);
                bool IsExists = System.IO.Directory.Exists(currentDirectory);
                if (!IsExists)
                {
                    System.IO.Directory.CreateDirectory(currentDirectory);
                }
                var fileName = Path.Combine(currentDirectory, "ErrorLog21.xml");
                doc.Save(fileName);

                #region Encrypt_File and Delete
                Encrypt_FileFB(VersionName);
                Delete_FileFB(VersionName);
                #endregion


            }
            catch (Exception e)
            {
                Delete_FileFB(VersionName);
                MessageBox.Show("XML Update Error:  " + e.ToString());
            }
        }

        private void Encrypt_FileFB(string VersionName)
        {
            string exeFolder = Path.GetDirectoryName(VersionName + "\\");
            string inputFilePath = Path.Combine(exeFolder, "ErrorLog21.xml");
            string outputfilePath = Path.Combine(exeFolder, "ConfigXMLDataEvl.xml");

            string EncryptionKey = "U0ZBcHA6U0ZAQXBw";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        private void Decrypt_FileFB(string VersionName)
        {
            string exeFolder = Path.GetDirectoryName(VersionName + "\\");
            string inputFilePath = Path.Combine(exeFolder, "ConfigXMLDataEvl.xml");
            string outputfilePath = Path.Combine(exeFolder, "ErrorLog21.xml");

            string EncryptionKey = "U0ZBcHA6U0ZAQXBw";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        private void Delete_FileFB(string VersionName)
        {
            string exeFolder = Path.GetDirectoryName(VersionName);
            string fileName = Path.Combine(exeFolder, "ErrorLog21.xml");
            if ((System.IO.File.Exists(fileName)))
            {
                System.IO.File.Delete(fileName);
            }
        }

        public void Read_XMLFileLabel(string VersionName)
        {
            try
            {
                VersionName = VersionName.Replace("\\\\", "\\");
                VersionName = VersionName.Replace("Backend", "intPOS - Label Printing"); 
                string exeFolder = Path.GetDirectoryName(VersionName);
                string path = Path.Combine(exeFolder, "AppConfigration.xml");

                XDocument xdoc = XDocument.Load(path);
                XElement ezPOSProSettings = xdoc.Element("intPOSSettings");

                var _POSInfo = (from POSInfo in ezPOSProSettings.Elements("intPOS")
                                from x in POSInfo.Elements("POSInfo")
                                select new
                                {
                                    POSType = x.Attribute("POSType").Value,
                                    InstallationDate = x.Attribute("InstallationDate").Value,
                                    Info = x.Attribute("Info").Value,
                                    Version = x.Attribute("Version").Value
                                }).ToList()[0];

                var _Scanner = (from Scanner in ezPOSProSettings.Elements("intPOS")
                                from x in Scanner.Elements("Scanner")
                                select new
                                {
                                    ScannerType = (x.Attribute("ScannerType").Value)
                                }).ToList()[0];

                var _ReciptPrinter = (from ReciptPrinter in ezPOSProSettings.Elements("intPOS")
                                      from x in ReciptPrinter.Elements("ReciptPrinter")
                                      select new
                                      {
                                          PrinterName = (x.Attribute("PrinterName").Value),
                                          Disclaimer = (x.Attribute("Disclaimer").Value)
                                      }).ToList()[0];

                var _SignReciptPrinter = new { SignPrinterName = "" };
                if (ezPOSProSettings.Elements("intPOS").Elements().Where(S => S.Name == "SignReciptPrinter").Any())
                {
                    _SignReciptPrinter = (from SignReciptPrinter in ezPOSProSettings.Elements("intPOS")
                                          from x in SignReciptPrinter.Elements("SignReciptPrinter")
                                          select new
                                          {
                                              SignPrinterName = (x.Attribute("SignPrinterName").Value)
                                          }).ToList()[0];
                }

                var _BarCode = new { Display = "" };
                if (ezPOSProSettings.Elements("intPOS").Elements().Where(S => S.Name == "BarCode").Any())
                {
                    _BarCode = (from SignReciptPrinter in ezPOSProSettings.Elements("intPOS")
                                from x in SignReciptPrinter.Elements("BarCode")
                                select new
                                {
                                    Display = (x.Attribute("Display").Value)
                                }).ToList()[0];
                }

                var _HalfPage = new { Display = "" };
                if (ezPOSProSettings.Elements("intPOS").Elements().Where(S => S.Name == "HalfPage").Any())
                {
                    _HalfPage = (from SignReciptPrinter in ezPOSProSettings.Elements("intPOS")
                                 from x in SignReciptPrinter.Elements("HalfPage")
                                 select new
                                 {
                                     Display = (x.Attribute("Display").Value)
                                 }).ToList()[0];
                }

                var _FullPage = new { Display = "" };
                if (ezPOSProSettings.Elements("intPOS").Elements().Where(S => S.Name == "FullPage").Any())
                {
                    _FullPage = (from SignReciptPrinter in ezPOSProSettings.Elements("intPOS")
                                 from x in SignReciptPrinter.Elements("FullPage")
                                 select new
                                 {
                                     Display = (x.Attribute("Display").Value)
                                 }).ToList()[0];
                }

                var _PotraitFullPage = new { Display = "" };
                if (ezPOSProSettings.Elements("intPOS").Elements().Where(S => S.Name == "PotraitFullPage").Any())
                {
                    _PotraitFullPage = (from SignReciptPrinter in ezPOSProSettings.Elements("intPOS")
                                        from x in SignReciptPrinter.Elements("PotraitFullPage")
                                        select new
                                        {
                                            Display = (x.Attribute("Display").Value)
                                        }).ToList()[0];
                }

                var _SevenSign = new { Display = "" };
                if (ezPOSProSettings.Elements("intPOS").Elements().Where(S => S.Name == "SevenSign").Any())
                {
                    _SevenSign = (from SignReciptPrinter in ezPOSProSettings.Elements("intPOS")
                                  from x in SignReciptPrinter.Elements("SevenSign")
                                  select new
                                  {
                                      Display = (x.Attribute("Display").Value)
                                  }).ToList()[0];
                }

                var _DbConnet = (from DbConnet in ezPOSProSettings.Elements("intPOS")
                                 from x in DbConnet.Elements("DbConnet")
                                 select new
                                 {
                                     ServerName = (x.Attribute("ServerName").Value),
                                     DbName = (x.Attribute("DbName").Value),
                                     UserName = (x.Attribute("UserName").Value),
                                     Password = (x.Attribute("Password").Value)
                                 }).ToList()[0];

                var _DbConnection = (from DbConnection in ezPOSProSettings.Elements("intPOS")
                                     from x in DbConnection.Elements("DbConnection")
                                     select new
                                     {
                                         DbConnectionString = (x.Attribute("DbConnectionString").Value),
                                         PriorityCode = (x.Attribute("PriorityCode").Value)
                                     }).ToList()[0];

                #region XMLData
                XMLDataLabel.POSType = _POSInfo.POSType;
                XMLDataLabel.InstallationDate = Convert.ToDateTime(_POSInfo.InstallationDate);
                XMLDataLabel.Info = _POSInfo.Info;
                XMLDataLabel.Version = _POSInfo.Version;
                XMLDataLabel.Scanner = _Scanner.ScannerType;
                XMLDataLabel.PrinterName = _ReciptPrinter.PrinterName;
                XMLDataLabel.SignPrinterName = _SignReciptPrinter.SignPrinterName;

                XMLDataLabel.BarCode = _BarCode.Display;
                XMLDataLabel.HalfPage = _HalfPage.Display;
                XMLDataLabel.FullPage = _FullPage.Display;
                XMLDataLabel.PotraitFullPage = _PotraitFullPage.Display;
                XMLDataLabel.SevenSign = _SevenSign.Display;

                XMLDataLabel.Disclaimer = _ReciptPrinter.Disclaimer;
                XMLDataLabel.ServerName = _DbConnet.ServerName;
                XMLDataLabel.DbName = _DbConnet.DbName;
                XMLDataLabel.UserName = _DbConnet.UserName;
                XMLDataLabel.Password = _DbConnet.Password;
                XMLDataLabel.DbConnectionString = _DbConnection.DbConnectionString;
                //XMLDataLabel.DbConnectionString = "data source = TBS24; initial catalog = intPOS_Dev; persist security info = True; user id = sa; password = sa@123";
                XMLDataLabel.PriorityCode = "";
                #endregion

                if (XMLDataLabel.DbConnectionString != string.Empty)
                    LabelOK = true;
            }
            catch (Exception e)
            {
                //MessageBox.Show("XML Read Error:  " + e.Message.ToString());

            }
        }

        public void Update_XMLFileLabel(string VersionName)
        {
            Read_XMLFileLabel(VersionName);

            try
            {
                string installPath = VersionName;

                if (_VersionType.VersionNo == 0)
                {
                    var currentDirectorydb = VersionName;
                    var fileNamedb = Path.Combine(currentDirectorydb, "AppData\\intPOS.mdf");
                    fileNamedb = fileNamedb.Replace("\\\\", "\\");
                    fileNamedb = fileNamedb.Replace("Frontend", "Backend");

                    XMLDataLabel.ServerName = "";
                    XMLDataLabel.DbName = "";
                    XMLDataLabel.UserName = "";
                    XMLDataLabel.Password = "";
                    XMLDataLabel.DbConnectionString = "Data Source=(LocalDB)\\" + SQLLocalDbName + ";AttachDbFilename=" + fileNamedb + ";Integrated Security=True;password=123456;Connect Timeout=30";
                    //XMLDataLabel.DbConnectionString = "Data Source=(LocalDB)\\" + SQLLocalDbName + ";AttachDbFilename=" + fileNamedb + ";Integrated Security=True;Connect Timeout=30";
                    //XMLDataLabel.DbConnectionString = "Data Source=.\\" + SQLLocalDbName + "; AttachDbFilename =" + fileNamedb + ";Integrated Security=True;Connect Timeout=30;User Instance=True"; password=123456;

                }
                else
                {
                    XMLDataLabel.ServerName = DatabaseServer;
                    XMLDataLabel.DbName = DatabaseName;
                    XMLDataLabel.UserName = UserName;
                    XMLDataLabel.Password = Password;
                    XMLDataLabel.DbConnectionString = "data source=" + DatabaseServer + ";initial catalog=" + DatabaseName + ";persist security info=True;user id=" + UserName + ";password=" + Password + ";";
                }

                XDocument doc =
                             new XDocument(
                               new XElement("intPOSSettings",
                                 new XElement("intPOS",
                                     new XElement("POSInfo", new XAttribute("POSType", XMLDataLabel.POSType), new XAttribute("InstallationDate", XMLDataLabel.InstallationDate), new XAttribute("Info", XMLDataLabel.Info), new XAttribute("Version", SetupVersion)),
                                     new XElement("Scanner", new XAttribute("ScannerType", XMLDataLabel.Scanner)),
                                     new XElement("ReciptPrinter", new XAttribute("PrinterName", XMLDataLabel.PrinterName), new XAttribute("Disclaimer", XMLDataLabel.Disclaimer)),
                                     new XElement("SignReciptPrinter", new XAttribute("SignPrinterName", XMLDataLabel.SignPrinterName)),
                                     new XElement("BarCode", new XAttribute("Display", XMLDataLabel.BarCode)),
                                     new XElement("HalfPage", new XAttribute("Display", XMLDataLabel.HalfPage)),
                                     new XElement("FullPage", new XAttribute("Display", XMLDataLabel.FullPage)),
                                     new XElement("PotraitFullPage", new XAttribute("Display", XMLDataLabel.PotraitFullPage)),
                                     new XElement("SevenSign", new XAttribute("Display", XMLDataLabel.SevenSign)),
                                     new XElement("DbConnet", new XAttribute("ServerName", XMLDataLabel.ServerName), new XAttribute("DbName", XMLDataLabel.DbName), new XAttribute("UserName", XMLDataLabel.UserName), new XAttribute("Password", XMLDataLabel.Password)),
                                     new XElement("DbConnection", new XAttribute("DbConnectionString", XMLDataLabel.DbConnectionString), new XAttribute("PriorityCode", ""))
                                 )));

                installPath = installPath.Replace("\\\\", "\\");
                installPath = installPath.Replace("Backend", "intPOS - Label Printing");
                var currentDirectory = Path.Combine(installPath);
                bool IsExists = System.IO.Directory.Exists(currentDirectory);
                if (!IsExists)
                {
                    System.IO.Directory.CreateDirectory(currentDirectory);
                }
                var fileName = Path.Combine(currentDirectory, "AppConfigration.xml");
                doc.Save(fileName);
            }
            catch (Exception e)
            {
                //MessageBox.Show("XML Update Error:  " + e.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to cancel?","intPOS Setup",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void chkAgree_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAgree.Checked==true)
            {
                btnInstall.Enabled = true;
            }
            else
            {
                btnInstall.Enabled = false;
            }
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            _FolderLocation = new FolderLocation();
            _FolderLocation.OnMyEvent += new FolderLocation.onMyEventHandler(CatchFolderLocationCardEvent_);
            _FolderLocation.ShowDialog();
        }
    }
}
