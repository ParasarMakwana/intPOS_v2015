using MetroFramework.Forms;
using SFPOS.Common;
using SFPOSWindows.Frontend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SFPOSWindows.MenuForm
{
    public partial class MenuSettings : MetroForm
    {
        SerialPort ComPort = new SerialPort();

        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        private SerialPinChangedEventHandler SerialPinChangedEventHandler1;
        delegate void SetTextCallback(string text);

        public MenuSettings()
        {
            InitializeComponent();
            ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.Write(indata);
            if (indata != String.Empty)
            {
                this.BeginInvoke(new SetTextCallback(SetText), new object[] { indata });
            }
        }
        private void SetText(string text)
        {
            this.rtbIncoming.Text += text;
        }
        private void btnPort_Click(object sender, EventArgs e)
        {
            if (btnConn.Text == "CLOSED")
            {
                btnConn.Text = "Open";
                ComPort.PortName = Convert.ToString(cboPorts.Text);
                ComPort.BaudRate = Convert.ToInt32(cboBaudRate.Text);
                ComPort.DataBits = Convert.ToInt16(cboDataBits.Text);
                ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cboStopBits.Text);
                ComPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cboHandShaking.Text);
                ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), cboParity.Text);
                ComPort.Open();

            }
            else if (btnConn.Text == "Open")
            {
                btnConn.Text = "CLOSED";
                ComPort.Close();
            }
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            SerialPinChangedEventHandler1 = new SerialPinChangedEventHandler(PinChanged);
            ComPort.PinChanged += SerialPinChangedEventHandler1;
         
            ComPort.RtsEnable = true;
            ComPort.DtrEnable = true;
            btnTest.Enabled = false;
        }

        private void btnProductScale_Click(object sender, EventArgs e)
        {
            try
            {
                ComPort.Write("S11" + '\r');
                Thread.Sleep(20);
                ComPort.ReadTimeout = 1000;
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information",ex.Message, false);
            }
        }

        internal void PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SerialPinChange SerialPinChange1 = 0;

            SerialPinChange1 = e.EventType;
        }


        private void btnPort_Click_1(object sender, EventArgs e)
        {
            cboPorts.Items.Clear();
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;

            ArrayComPortsNames = SerialPort.GetPortNames();
            do
            {
                index += 1;
                cboPorts.Items.Add(ArrayComPortsNames[index]);


            } while (!((ArrayComPortsNames[index] == ComPortName) || (index == ArrayComPortsNames.GetUpperBound(0))));
            Array.Sort(ArrayComPortsNames);

            if (index == ArrayComPortsNames.GetUpperBound(0))
            {
                ComPortName = ArrayComPortsNames[0];
            }

            cboBaudRate.Text = "9600";
            cboDataBits.Text = "7";
            cboStopBits.Text = "One";
            cboParity.Text = "Odd";
            cboHandShaking.Text = "RequestToSendXOnXOff";
            cboPorts.Text = ArrayComPortsNames[0];
        }


        private void MenuSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            ComPort.Close();
       }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string path = Path.Combine(exeFolder, "DataSource\\DatabaseServers.xml");
            //string path = Path.Combine("E:\\", "DatabaseServers.xml");
            XDocument xdoc = XDocument.Load(path);
            XElement dbServers = xdoc.Element("DataBaseServers");

            var dbServerDetailResult = (from dbServer in dbServers.Elements("DataBaseServer")
                                        select new
                                        {
                                            ServerId = Convert.ToInt32(dbServer.Attribute("id").Value),
                                        }).ToList()[0];

            var dbDetailResult = (from dbDetail in dbServers.Elements("DataBases")
                                  from x in dbDetail.Elements("Database")
                                  select new
                                  {
                                      DbId = Convert.ToInt32(x.Attribute("id").Value),
                                      TYPE = x.Attribute("TYPE").Value,
                                      POSStatus = x.Attribute("POSStatus").Value
                                  }).ToList()[0];

            XDocument doc =
                          new XDocument(
                            new XElement("DataBaseServers",
                              new XElement("DataBaseServer", new XAttribute("id", (dbServerDetailResult.ServerId).ToString())),
                              new XElement("DataBases",
                                  new XElement("Database", new XAttribute("id", (dbDetailResult.DbId).ToString()), new XAttribute("TYPE", (dbDetailResult.TYPE).ToString()), new XAttribute("POSStatus", Convert.ToBoolean(dbDetailResult.POSStatus))),
                                  new XElement("Scanner", new XAttribute("Port", cboPorts.SelectedItem.ToString()), new XAttribute("BaudRate", cboBaudRate.Text), new XAttribute("Databit", cboDataBits.Text), new XAttribute("Stopbit", cboStopBits.Text), new XAttribute("Parity", cboParity.Text), new XAttribute("HandShaking", cboHandShaking.Text))
                              )));
            doc.Save(path);
            ClsCommon.MsgBox("Information","Scanner/Scale configuration settings successfully Saved.!", false);
            Close();
        }

        private void MenuSettings_Load(object sender, EventArgs e)
        {
            LoadPort();
            TestConne();
            //OpenClosePort();
        }
        public void LoadPort()
        {
            cboPorts.Items.Clear();
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;
            //Com Ports
            ArrayComPortsNames = SerialPort.GetPortNames();
            do
            {
                index += 1;
                cboPorts.Items.Add(ArrayComPortsNames[index]);


            } while (!((ArrayComPortsNames[index] == ComPortName) || (index == ArrayComPortsNames.GetUpperBound(0))));
            Array.Sort(ArrayComPortsNames);

            if (index == ArrayComPortsNames.GetUpperBound(0))
            {
                ComPortName = ArrayComPortsNames[0];
            }
            ComPort.RtsEnable = true;
            ComPort.DtrEnable = true;
            cboBaudRate.Text = "9600";
            cboDataBits.Text = "7";
            cboStopBits.Text = "One";
            cboParity.Text = "Odd";
            cboHandShaking.Text = "RequestToSendXOnXOff";
            cboPorts.Text = ArrayComPortsNames[0];
        }
        public void TestConne()
        {
            SerialPinChangedEventHandler1 = new SerialPinChangedEventHandler(PinChanged);
            ComPort.PinChanged += SerialPinChangedEventHandler1;
            //ComPort.Open();


            btnTest.Enabled = false;
        }
        public void OpenClosePort()
        {
            if (btnConn.Text == "CLOSED")
            {
                btnConn.Text = "Open";
                ComPort.PortName = Convert.ToString(cboPorts.Text);
                ComPort.BaudRate = Convert.ToInt32(cboBaudRate.Text);
                ComPort.DataBits = Convert.ToInt16(cboDataBits.Text);
                ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cboStopBits.Text);
                ComPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cboHandShaking.Text);
                ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), cboParity.Text);
                ComPort.Open();
            }
            else if (btnConn.Text == "Open")
            {
                btnConn.Text = "CLOSED";
                ComPort.Close();
            }
        }

        private void cboPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenClosePort();
            if (btnConn.Text == "CLOSED")
            {
                btnConn.Text = "Open";
                ComPort.PortName = Convert.ToString(cboPorts.Text);
                ComPort.BaudRate = Convert.ToInt32(cboBaudRate.Text);
                ComPort.DataBits = Convert.ToInt16(cboDataBits.Text);
                ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cboStopBits.Text);
                ComPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cboHandShaking.Text);
                ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), cboParity.Text);
                ComPort.Open();
            }
        }
    }
}
