using System;
using System.Windows.Forms;
using SFPOS.Common;

namespace SFPOSWindows.CustomControl
{
    public partial class UCOrderSuccess : UserControl
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        public UCOrderSuccess()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Stop();
            OnMyEvent(this, new EventArgs());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                this.Hide();
                OnMyEvent(this, new EventArgs());
                timer1.Stop();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrnOrderSuccess_Load(object sender, EventArgs e)
        {
            timer1.Interval = XMLData.OrderSuccessScreen * 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            OnMyEvent(this, new EventArgs());
            timer1.Stop();
        }
    }
}
