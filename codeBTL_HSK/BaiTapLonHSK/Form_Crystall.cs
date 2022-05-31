using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;

namespace BaiTapLonHSK
{
    public partial class Form_Crystall : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public Form_Crystall()
        {
            InitializeComponent();
        }

        private void crystalReportViewer2_Load_1(object sender, EventArgs e)
        {
            ReportDocument crtRpt = new ReportDocument();

            crtRpt.Load(@"E:\LaptrinhHSK\BaiTapLonHSK\Crystal_Report\DanhSachSV.rpt");
            //crtRpt.RecordSelectionFormula = "{SinhVien.GioiTinh}='Nam'";

            crystalReportViewer2.ReportSource = crtRpt;
            crystalReportViewer2.Refresh();
            HienMaLop();
        }
        private DataTable HienDSSV2(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM showViewAllSV", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable("SinhVien"))
                        {
                            ad.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ReportDocument crtRpt = new ReportDocument();
            crtRpt.Load(@"E:\LaptrinhHSK\BaiTapLonHSK\Crystal_Report\DanhSachSV.rpt");
            //crtRpt.RecordSelectionFormula = "{SinhVien.GioiTinh}='Nam'";
            ParameterFieldDefinition rpd = crtRpt.DataDefinition.ParameterFields["TimLop"];
            ParameterValues pv = new ParameterValues();
            ParameterDiscreteValue pdv = new ParameterDiscreteValue();
            pdv.Value = tbMaSV.Text;
            pv.Add(pdv);
            rpd.CurrentValues.Clear();
            rpd.ApplyCurrentValues(pv);
            crystalReportViewer2.ReportSource = crtRpt;
            crystalReportViewer2.Refresh();
        }
       

        private void btTimkiem_Click(object sender, EventArgs e)
        {

           // crystalReportViewer2.ReportSource = rpt;
            //crystalReportViewer2.Refresh();
            String txtTimkiem = tbMaSV.Text;
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"E:\LaptrinhHSK\BaiTapLonHSK\Crystal_Report\LocSinhVien.rpt");
            ParameterFieldDefinition rpd = rpt.DataDefinition.ParameterFields["MaSinhVien"];
            ParameterValues pv = new ParameterValues();
            ParameterDiscreteValue pdv = new ParameterDiscreteValue();
            pdv.Value = tbMaSV.Text;
            pv.Add(pdv);
            rpd.CurrentValues.Clear();
            rpd.ApplyCurrentValues(pv);

            crystalReportViewer2.ReportSource = rpt;
            crystalReportViewer2.Refresh();
        }

        private void btInDiem_Click(object sender, EventArgs e)
        {
            String txtLocDiem = cbMaLop.Text;
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"E:\LaptrinhHSK\BaiTapLonHSK\Crystal_Report\InDSDIEM.rpt");
            ParameterFieldDefinition rpd = rpt.DataDefinition.ParameterFields["MaMon"];
            ParameterValues pv = new ParameterValues();
            ParameterDiscreteValue pdv = new ParameterDiscreteValue();
            pdv.Value = cbMaLop.Text;
            pv.Add(pdv);
            rpd.CurrentValues.Clear();
            rpd.ApplyCurrentValues(pv);

            crystalReportViewer2.ReportSource = rpt;
            crystalReportViewer2.Refresh();
        }

        private DataTable HienLopHanhChinh(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from MonHoc", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable("MonHoc"))
                        {
                            ad.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        private void HienMaLop()
        {
            DataTable table = HienLopHanhChinh(constr);
            DataView view = new DataView(table);
            view.Sort = "TenMon";
            cbMaLop.DataSource = view;
            cbMaLop.DisplayMember = "TenMon";
            cbMaLop.ValueMember = "MaMon";

        }
        private void btXemDiem_Click(object sender, EventArgs e)

        {
            Visible = true;
            String txtLocDiem = cbMaLop.Text;
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"E:\LaptrinhHSK\BaiTapLonHSK\Crystal_Report\DanhSachDiem.rpt");
            ParameterFieldDefinition rpd = rpt.DataDefinition.ParameterFields["XemDS_Diem"];
            ParameterValues pv = new ParameterValues();
            ParameterDiscreteValue pdv = new ParameterDiscreteValue();
            pdv.Value = cbMaLop.Text;
            pv.Add(pdv);
            rpd.CurrentValues.Clear();
            rpd.ApplyCurrentValues(pv);

            crystalReportViewer2.ReportSource = rpt;
            crystalReportViewer2.Refresh();

         
        }

        private void Form_Crystall_Load(object sender, EventArgs e)
        {
            btTimkiem.Visible = false;
        }
    }
}
