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
namespace BaiTapLonHSK.Crystal_Report
{
    public partial class Form_ThongTinSV : Form
    {

        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public Form_ThongTinSV()
        {
            InitializeComponent();
        }

        private void Form_ThongTinSV_Load(object sender, EventArgs e)
        {
            LayTT();
            hienGirdView();
            ViewHocTT();
            
        }

        public void LayTT()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from SinhVien where MaSV ='" + TruyenDuLieu.MaSV + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaSV"].Equals(TruyenDuLieu.MaSV))
                            {
                                lbTenSV.Text = rd["TenSV"].ToString();
                                lbMaSV.Text = rd["MaSV"].ToString();
                                lbEmail.Text = rd["Email"].ToString();
                                lbCCCD.Text = rd["CCCD"].ToString();
                                lbNgaySinh.Text = String.Format("{0:d}", rd["NgaySinh"]);
                                lbLop.Text = rd["MaLop"].ToString();
                                lbGT.Text = rd["GioiTinh"].ToString();
                            }
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
        }


        private DataTable Hien_TT_SV(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.MonHoc.TenMon, dbo.MonHoc.SoTinChi, dbo.MonHoc.MaMon, dbo.LopHocPhan.MaLHP, dbo.GiangVien.TenGV, dbo.Diem.DiemCC, dbo.Diem.DiemGK, dbo.Diem.DiemThi, dbo.Diem.DiemTB, dbo.Diem.XepLoai FROM dbo.Diem INNER JOIN dbo.LopHocPhan ON dbo.Diem.MaLHP = dbo.LopHocPhan.MaLHP INNER JOIN dbo.MonHoc ON dbo.LopHocPhan.MaMon = dbo.MonHoc.MaMon INNER JOIN dbo.GiangVien ON dbo.LopHocPhan.MaGV = dbo.GiangVien.MaGV INNER JOIN dbo.LopHocPhan AS LopHocPhan_1 ON dbo.Diem.MaLHP = LopHocPhan_1.MaLHP AND dbo.MonHoc.MaMon = LopHocPhan_1.MaMon AND dbo.GiangVien.MaGV = LopHocPhan_1.MaGV where Diem.MaSV ='" + TruyenDuLieu.MaSV + "'", cnn))
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

        private void hienGirdView()
        {
            DataTable dt = Hien_TT_SV(constr);
            //dt.Columns.Add("TB Môn");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //    dt.Rows[i]["TB Môn"] = Math.Round(double.Parse(dt.Rows[i][5].ToString()) * 0.1 + double.Parse(dt.Rows[i][6].ToString()) * 0.2 + double.Parse(dt.Rows[i][7].ToString()) * 0.7, 1);
            
            dataGridView1.DataSource = dt;
            //dataGridView1.Columns["TB Môn"].DisplayIndex = 4;
            dataGridView1.AutoResizeColumns();
            
        }

        private void ViewHocTT()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].HeaderText = "Tên Môn";
            dataGridView1.Columns[1].HeaderText = "Số Tín Chỉ";
            dataGridView1.Columns[2].HeaderText = "Mã Môn";
            dataGridView1.Columns[3].HeaderText = "Mã Lớp Học Phần";
            dataGridView1.Columns[4].HeaderText = "Tên Giảng Viên";
            dataGridView1.Columns[5].HeaderText = "Điểm CC";
            dataGridView1.Columns[6].HeaderText = "Điểm GK";
            dataGridView1.Columns[7].HeaderText = "Điểm Thi";
            dataGridView1.Columns[8].HeaderText = "Điểm TB";
            dataGridView1.Columns[9].HeaderText = "Xếp Loại";

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        //DataTable t = diemDAO.layDS(constr, "select TenMon,SoTinChi,MaLHP,MaGV,TenGV,DiemCC,DiemGK,DiemThi from a where MaSV='" + code + "'", "a");
        //t.Columns.Add("STT");
        //    t.Columns.Add("TB Môn");
        //    for (int i = 0; i<t.Rows.Count; i++)
        //        t.Rows[i]["STT"] = i + 1;
        //    for (int i = 0; i<t.Rows.Count; i++)
        //        t.Rows[i]["TB Môn"] = Math.Round(double.Parse(t.Rows[i][5].ToString())*0.1+ double.Parse(t.Rows[i][6].ToString())*0.2+ double.Parse(t.Rows[i][7].ToString())*0.7,1);
        //    tbCaNhan.DataSource = t;
        //    tbCaNhan.Columns["STT"].DisplayIndex = 0;
        //    tbCaNhan.Columns["TB Môn"].DisplayIndex = 9;

    }
}
