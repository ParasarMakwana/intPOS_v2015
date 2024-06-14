using MetroFramework.Forms;
using SFPOS.Common;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SFPOSWindows
{
    public partial class frmDBConnections : MetroForm
    {
        public frmDBConnections()
        {
            InitializeComponent();
            txtDatabaseServerName.CharacterCasing = CharacterCasing.Upper;
            txtDatabaseServerName.Focus();
        }
        public void XMLUpdate(string Key)
        {
            string installationPath = Application.StartupPath + "\\DataSource";
            XDocument doc =
                         new XDocument(
                           new XElement("DataBaseServers",
                             new XElement("DataBaseServer", new XAttribute("id", "1")),
                             new XElement("DataBases",
                                 new XElement("Database", new XAttribute("id", "1"), new XAttribute("TYPE", LoginInfo.ScreenID), new XAttribute("POSStatus", true)),
                                 new XElement("Scanner", new XAttribute("Port", "COM1"), new XAttribute("BaudRate", "9600"), new XAttribute("Databit", "7"), new XAttribute("Stopbit", "1"), new XAttribute("Parity", "None"), new XAttribute("HandShaking", "None")),
                                 new XElement("POSKey", new XAttribute("Key", Key))
                             )));

            var currentDirectory = Path.Combine(installationPath);
            bool IsExists = System.IO.Directory.Exists(currentDirectory);
            if (!IsExists)
            {
                System.IO.Directory.CreateDirectory(currentDirectory);
            }
            var fileName = Path.Combine(currentDirectory, "DatabaseServers.xml");
            doc.Save(fileName);
            ClsCommon.MsgBox("Information","XML Updated", false);
        }
        private void btnTestConnections_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDatabaseServerName.Text.Trim()))
            {
                ClsCommon.MsgBox("Information","Please enter database server name", false);
            }
            else if (String.IsNullOrEmpty(txtDatabaseName.Text.Trim()))
            {
                ClsCommon.MsgBox("Information","Please enter database name", false);
            }
            else if (String.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                ClsCommon.MsgBox("Information","Please enter user name", false);
            }
            else if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                ClsCommon.MsgBox("Information","Please enter password", false);
            }
            else
            {
                ClsCommon.MsgBox("Information","Validate !", false);
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {

        }
    }
}
