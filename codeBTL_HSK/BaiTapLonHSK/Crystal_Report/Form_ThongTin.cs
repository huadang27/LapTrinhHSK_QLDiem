using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaiTapLonHSK.Class;
using System.Configuration;

namespace BaiTapLonHSK.Crystal_Report
{
    public partial class Form_ThongTin : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public Form_ThongTin()
        {
            InitializeComponent();
        }

        //private void crystalReportViewer1_Load(object sender, EventArgs e)
        //{
        //    ReportDocument crtRpt = new ReportDocument();

        //    crtRpt.Load(@"E:\LaptrinhHSK\BaiTapLonHSK\Crystal_Report\ThongTinGV.rpt");


        //    ParameterFieldDefinition rpd = crtRpt.DataDefinition.ParameterFields["TimGV"];
        //    ParameterValues pv = new ParameterValues();
        //    ParameterDiscreteValue pdv = new ParameterDiscreteValue();
        //    pdv.Value = TruyenDuLieu.MaGV;
        //    pv.Add(pdv);
        //    rpd.CurrentValues.Clear();
        //    rpd.ApplyCurrentValues(pv);

        //    crystalReportViewer1.ReportSource = crtRpt;
        //    crystalReportViewer1.Refresh();
        //    crystalReportViewer1.ReportSource = crtRpt;
        //    crystalReportViewer1.Refresh();
        //    LayTT();
        //}

        public void LayTT()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from GiangVien where MaGV ='" + TruyenDuLieu.MaGV + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaGV"].Equals(TruyenDuLieu.MaGV))
                            {
                                lbTenSV.Text = rd["TenGV"].ToString();
                                lbMaGV.Text = rd["MaGV"].ToString();
                                lbEmail.Text = rd["Email"].ToString();
                                lbCCCD.Text = rd["CCCD"].ToString();
                                lbSDT.Text = rd["SDT"].ToString();
                            }
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
        }

        public void LayTG_DN()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from TaiKhoan where TaiKhoan ='" + TruyenDuLieu.MaGV + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["TaiKhoan"].Equals(TruyenDuLieu.MaGV))
                            {
                                sl_dn.Text = rd["Dem_dn"].ToString();
                            }
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
        }

   

        private DataTable Hien_TT_GV(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.MonHoc.TenMon, dbo.LopHocPhan.MaMon, dbo.LopHocPhan.ThoiGianBatDau, dbo.LopHocPhan.ThoiGianKetThuc FROM dbo.MonHoc INNER JOIN dbo.LopHocPhan ON dbo.MonHoc.MaMon = dbo.LopHocPhan.MaMon INNER JOIN dbo.GiangVien ON dbo.LopHocPhan.MaGV = dbo.GiangVien.MaGV WHERE GiangVien.MaGV = '"+TruyenDuLieu.MaGV+"'", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable("GiangVien"))
                        {
                            ad.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        private void hienGirdView()
        {
            DataTable table = Hien_TT_GV(constr);
           dataGridView1.DataSource = table;
            dataGridView1.AutoResizeColumns();
        }


        private void Form_ThongTin_Load(object sender, EventArgs e)
        {
            hienGirdView();
            LayTT();
            ViewHocTT();
            //LayTG_DN();
            sl_dn.Visible = false;
        }
        private void ViewHocTT()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].HeaderText = "Tên Môn";
            dataGridView1.Columns[1].HeaderText = "Mã Môn";
            dataGridView1.Columns[2].HeaderText = "Thời Gian Bắt Đầu";
            dataGridView1.Columns[3].HeaderText = "Thời Gian Kết Thúc";

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
