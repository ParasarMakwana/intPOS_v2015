using MetroFramework.Forms;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Frontend;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SFPOSWindows
{
    public partial class Loader : MetroForm
    {
        private DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public Loader()
        {
            InitializeComponent();
        }
        //      this.BringToFront();
    }
}
