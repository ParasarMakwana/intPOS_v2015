using SFPOS.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace SFPOSWindows
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            Boolean IsFileFound = false;
            //FileInfo fi1 = new FileInfo(Path.Combine(Application.StartupPath, "Temp.txt"));
            //// MessageBox.Show(fi1.FullName);
            //if (fi1.Exists)
            //{
            //    IsFileFound = true;
            //    if (ClsCommon.IsUserAdministrator() == false)
            //    {
            //        ClsCommon.MsgBox("Information", "Please start application with run as administrator privileges.", false);
            //        Application.Exit();
            //    }
            //    else
            //    {
                    var currentDirectory = Path.Combine(Application.StartupPath);
                    //MessageBox.Show(currentDirectory);
                    DirectoryInfo dInfo = new DirectoryInfo(currentDirectory);
                    DirectorySecurity dSecurity = dInfo.GetAccessControl();
                    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);
                    //
                    var fileName = Path.Combine(currentDirectory, "AppData\\intPOS.sdf");
                    dInfo = new DirectoryInfo(fileName);
                    dSecurity = dInfo.GetAccessControl();
                    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);
                    //MessageBox.Show("1");
                    //
                    fileName = Path.Combine(currentDirectory, "AppData\\intPOS.mdf");
                    FileInfo fi2 = new FileInfo(fileName);
                    if (fi2.Exists)
                    {
                        dInfo = new DirectoryInfo(fileName);
                        dSecurity = dInfo.GetAccessControl();
                        dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                        dInfo.SetAccessControl(dSecurity);
                    }

                    fileName = Path.Combine(currentDirectory, "AppData\\intPOS_log.ldf");
                    fi2 = new FileInfo(fileName);
                    if (fi2.Exists)
                    {
                        dInfo = new DirectoryInfo(fileName);
                        dSecurity = dInfo.GetAccessControl();
                        dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                        dInfo.SetAccessControl(dSecurity);
                    }
                    IsFileFound = false;
               //}
            //}
            if(IsFileFound == false)
            {
                ClsCommon.GetPhysical_IP_Adress();
                ClsCommon.Read_XMLFile();
                ClsCommon.Read_ExtraSettingXMLFile();
                if (XMLData.Type > 0)
                {
                    //string appGuid = ("{A57C23FD-8A8D-43BD-9436-EED3405B61BE}");

                    //using (Mutex mutex = new Mutex(false, appGuid))
                    //{
                    //    if (!mutex.WaitOne(0, false))
                    //    {
                    //        MessageBox.Show("An instance of the application already running");
                    //        return;
                    //    }
                    Process[] pname = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location));

                    Boolean isapplicationexit = true;
                    if (pname.Length > 0)
                    {


                        foreach (var process in pname)
                        {
                            //MessageBox.Show(process.MainWindowTitle);
                            if (process.MainWindowTitle == "FrmOrderScanner_P2" || process.MainWindowTitle == "frmLogin_P2")
                            {
                                isapplicationexit = false;

                                Application.Exit();
                            }



                        }
                    }
                    if (isapplicationexit == true)
                    {

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new frmSplash());
                    }
                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new frmSplash());
                    //}
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Invalid license key, Please contact a system administrator!", false);
                    Application.Exit();
                }
            }
        }
    }
}
