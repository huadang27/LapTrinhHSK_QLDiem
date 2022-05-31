using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaiTapLonHSK.Class;
namespace BaiTapLonHSK
{
    public partial class QLSV_User : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public QLSV_User()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"E:\LaptrinhHSK\BaiTapLonHSK\Crystal_Report\DanhSachDiem.rpt");
            //crtRpt.RecordSelectionFormula = "{SinhVien.GioiTinh}='Nam'";
            ParameterFieldDefinition rpd = rpt.DataDefinition.ParameterFields["XemDS_Diem"];
            ParameterValues pv = new ParameterValues();
            ParameterDiscreteValue pdv = new ParameterDiscreteValue();
            pdv.Value = TruyenDuLieu.MaSV;
            pv.Add(pdv);
            rpd.CurrentValues.Clear();
            rpd.ApplyCurrentValues(pv);
            View_User.ReportSource = rpt;
            View_User.Refresh();
        }
    
       

        //private void btTimKiem_Click(object sender, EventArgs e)
        //{
          
        //    ReportDocument rpt = new ReportDocument();
        //    rpt.Load(@"E:\LaptrinhHSK\BaiTapLonHSK\Crystal_Report\DanhSachDiem.rpt");
        //    //crtRpt.RecordSelectionFormula = "{SinhVien.GioiTinh}='Nam'";
        //    ParameterFieldDefinition rpd = rpt.DataDefinition.ParameterFields["XemDS_Diem"];
        //    ParameterValues pv = new ParameterValues();
        //    ParameterDiscreteValue pdv = new ParameterDiscreteValue();
        //    pdv.Value = TruyenDuLieu.MaSV;
        //    pv.Add(pdv);
        //    rpd.CurrentValues.Clear();
        //    rpd.ApplyCurrentValues(pv);
        //    View_User.ReportSource = rpt;
        //    View_User.Refresh();
        //}
    }
}
