using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities;
using SFPOS.BAL;

namespace SFPOSWindows.CustomControl
{
    public partial class UCDepartmentsDynamic : UserControl
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        public string DeptCode = "";

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDepartmentsDynamic));

        //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDepartments));
        DepartmentService _DepartmentService = new DepartmentService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        List<DepartmentMasterModel> _objdepartmentlistFinal = new List<DepartmentMasterModel>();
        public UCDepartmentsDynamic()
        {
            InitializeComponent();
        }
        
        private void UCDepartmentsDynamic_Load(object sender, EventArgs e)
        {
            List<DepartmentMasterModel> objdepartmentlist = new List<DepartmentMasterModel>();
            objdepartmentlist = _DepartmentService.GetAllDepartment();
            _objdepartmentlistFinal = objdepartmentlist.Where(person => person.IsActive == true && person.DepartmentBtn == true).ToList();
            try
            {

                var len = _objdepartmentlistFinal.Count;
                var _nextbtncount = 0;
                var locX_Dim = 0;
                var locY_Dim = 0;
                var countbtn = 0;
                for (int i = 0; i < 6; i++)
                {
                    locX_Dim = 7;
                    if (i == 0)
                    {
                        locY_Dim = 8;
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        Button obj = new Button();

                        if (_nextbtncount < len)
                        {
                            obj.Name = "btn" + _objdepartmentlistFinal[_nextbtncount].DepartmentName.ToString();
                            obj.Text = _objdepartmentlistFinal[_nextbtncount].DepartmentName.ToString();
                            obj.Location = new System.Drawing.Point(locX_Dim, locY_Dim);

                            obj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
                            //obj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGrocery.BackgroundImage")));
                            //obj.Click += (se, ev) => { button_Click(se, ev, _objdepartmentlistFinal[_nextbtncount].DepartmentNo.ToString()); };
                            obj.Click += delegate (object cs, EventArgs ev) { button_Click(cs, ev, ""); };
                            _nextbtncount= _nextbtncount+1;
                        }
                        else
                        {
                            obj.Name = "btn" + countbtn;
                            obj.Text = "-";
                            obj.Location = new System.Drawing.Point(locX_Dim, locY_Dim);
                            obj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
                            countbtn++;
                        }

                        obj.Anchor = System.Windows.Forms.AnchorStyles.None;
                        obj.BackColor = System.Drawing.Color.White;
                        obj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        obj.Cursor = System.Windows.Forms.Cursors.Hand;
                        obj.FlatAppearance.BorderSize = 0;
                        obj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        obj.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        obj.Size = new System.Drawing.Size(160, 45);
                        this.Controls.Add(obj);
                        locX_Dim = locX_Dim + 166;
                    }
                    locY_Dim = locY_Dim + 51;

                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, ex.StackTrace, ex.LineNumber());
            }
        }
        void button_Click(object sender, EventArgs e, string message)
        {
            string _DeptCode = string.Empty;
            var objPictureBox = (sender as Button);
            _objdepartmentlistFinal = _objdepartmentlistFinal.Where(person => person.DepartmentName == objPictureBox.Text).ToList();
            _DeptCode = _objdepartmentlistFinal[0].BtnCode.ToString();
            DeptCode = _DeptCode;
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }
    }
}
