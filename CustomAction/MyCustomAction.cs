using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CustomAction
{
    [RunInstaller(true)]
    public partial class MyCustomAction : Installer
    {
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary savedState)
        {
            //System.Diagnostics.Debugger.Launch();
            base.Install(savedState);
            string installationPath = this.Context.Parameters["targetdir"];

            string Type_ = "2";
            //1 BackEnd, 2 Frontend
            string POSType = (Type_ == "1") ? "intPOS-Backend" : "intPOS";

            string intPOSVer = "0";

            XDocument doc =
                             new XDocument(
                               new XElement("ezPOSProSettings",
                                 new XElement("ezPOSPro",
                                     new XElement("POSInfo", new XAttribute("POSType", POSType), new XAttribute("InstallationDate", DateTime.Now), new XAttribute("Info", ""), new XAttribute("Version", "2.2.1")),
                                     new XElement("Status", new XAttribute("Type", Type_), new XAttribute("POSStatus", false), new XAttribute("SyncStatus", false)),
                                     new XElement("Scanner", new XAttribute("ScannerType", ""), new XAttribute("ScaleType", ""), new XAttribute("ScannerInUsed", false)),
                                     new XElement("POSKey", new XAttribute("Key", "")),
                                     new XElement("ReciptPrinter", new XAttribute("PrinterName", ""), new XAttribute("Disclaimer", "")),
                                     new XElement("DbConnet", new XAttribute("ServerName", ""), new XAttribute("DbName", ""), new XAttribute("UserName", ""), new XAttribute("Password", "")),
                                     new XElement("DbConnection", new XAttribute("DbConnectionString", ""), new XAttribute("PriorityCode", "")),
                                     new XElement("DataSyncTime", new XAttribute("LiveToLocal", "1"), new XAttribute("LocalToLive", "5"), new XAttribute("OrderSuccessScreen", "20")),
                                     new XElement("intPOSVer", new XAttribute("intPOSVer", intPOSVer)),
                                     new XElement("ScreenSetting", new XAttribute("POSScreen", "0"), new XAttribute("CustomerScreen", "1"))
                                     )));

            var currentDirectory = Path.Combine(installationPath);
            bool IsExists = System.IO.Directory.Exists(currentDirectory);
            if (!IsExists) System.IO.Directory.CreateDirectory(currentDirectory);

            var fileName = Path.Combine(currentDirectory, "ErrorLog.xml");
            doc.Save(fileName);

            //
            DirectoryInfo dInfo = new DirectoryInfo(currentDirectory);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

            //
            fileName = Path.Combine(currentDirectory, "AppData\\intPOS.sdf");
            dInfo = new DirectoryInfo(fileName);
            dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

            //

            //fileName = Path.Combine(currentDirectory, "AppData\\intPOS.mdf");
            //FileInfo fi = new FileInfo(fileName);
            //if (fi.Exists)
            //{
            //    dInfo = new DirectoryInfo(fileName);
            //    dSecurity = dInfo.GetAccessControl();
            //    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            //    dInfo.SetAccessControl(dSecurity);
            //}

            //fileName = Path.Combine(currentDirectory, "AppData\\intPOS_log.ldf");
            //fi = new FileInfo(fileName);
            //if (fi.Exists)
            //{
            //    dInfo = new DirectoryInfo(fileName);
            //    dSecurity = dInfo.GetAccessControl();
            //    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            //    dInfo.SetAccessControl(dSecurity);
            //}

            #region Encrypt_File
            fileName = Path.Combine(currentDirectory, "ErrorLog.xml");
            string inputFilePath = fileName;
            string outputfilePath = Path.Combine(currentDirectory, "ErrorRobotDelete.xml");
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

            #region Delete Temp File
            if ((System.IO.File.Exists(inputFilePath)))
            {
                System.IO.File.Delete(inputFilePath);
            }
            #endregion
            #endregion
            //


        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

    }
}
