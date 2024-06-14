using MetroFramework.Forms;
using SFPOS.Common;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;


namespace SFPOSWindows
{
    public partial class frmKeyActivation : MetroForm
    {
        public frmKeyActivation()
        {
            InitializeComponent();
            txtKey.CharacterCasing = CharacterCasing.Upper;
            txtKey.Focus();
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            if (txtKey.Text.Length == 4)
            {
                txtKey.Text = txtKey.Text + "-";
            }
            else if (txtKey.Text.Length == 9)
            {
                txtKey.Text = txtKey.Text + "-";
            }
            else if (txtKey.Text.Length == 14)
            {
                txtKey.Text = txtKey.Text + "-";
            }
            txtKey.SelectionStart = txtKey.Text.Length;
            txtKey.SelectionLength = 0;
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            btnActive.Enabled = false;
            KeyResponse _KeyResponse = new KeyResponse();
            APIUtility _APIUtility = new APIUtility();

            _KeyResponse = _APIUtility.ActiveKey(txtKey.Text.Trim());
            if(_KeyResponse.Status==1)
            {
                XMLUpdate(_KeyResponse.KeyDetails[0].GeneratedKey);
            }
            ClsCommon.MsgBox("Information",_KeyResponse.Message, false);
            Application.Exit();
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
    }
}
