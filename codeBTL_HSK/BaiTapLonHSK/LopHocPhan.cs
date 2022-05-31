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

namespace BaiTapLonHSK
{
    public partial class LopHocPhan : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public LopHocPhan()
        {
            InitializeComponent();
        }

        private void LopHocPhan_Load(object sender, EventArgs e)
        {
            HienTenMon();
            HienTenGV();
            hienGirdView();
        }

        private DataTable HienDSLHP(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[View_LHP]", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable("LopHocPhan"))
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
            DataTable table = HienDSLHP(constr);
            dtgvLHP.DataSource = table;
            dtgvLHP.AutoResizeColumns();
        }

        private DataTable HienMon(string constr)
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
        private void HienTenMon()
        {
            DataTable table = HienMon(constr);
            DataView view = new DataView(table);
            view.Sort = "TenMon";
            cbMonHoc.DataSource = view;
            cbMonHoc.DisplayMember = "TenMon";
            cbMonHoc.ValueMember = "MaMon";

        }

        private DataTable HienGV(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from GiangVien", cnn))
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
        private void HienTenGV()
        {
            DataTable table = HienGV(constr);
            DataView view = new DataView(table);
            view.Sort = "MaGV";
            cbMaGV.DataSource = view;
            cbMaGV.DisplayMember = "MaGV";
            cbMaGV.ValueMember = "MaGV";

        }

        public static bool Check_Ma_LHP(string constr, string MaLHP)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from LopHocPhan", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaLHP"].Equals(MaLHP))
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }


        private static bool insertLHP(string constr, String MaLHP, String MonHoc, String MaGV, DateTime ThoiGianBatDau,DateTime ThoiGianKetThuc)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Insert_LHP";
                    cmd.Parameters.AddWithValue("@MaLHP", MaLHP);
                    cmd.Parameters.AddWithValue("@MaMon", MonHoc);
                    cmd.Parameters.AddWithValue("@MaGV", MaGV);
                    cmd.Parameters.AddWithValue("@ThoiGianBatDau", ThoiGianBatDau);
                    cmd.Parameters.AddWithValue("@ThoiGianKetthuc", ThoiGianKetThuc);
                   
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                    return i > 0;
                }
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            String MaLHP = tbMaLHP.Text;
            String MonHoc = cbMonHoc.SelectedValue.ToString();
            String MaGV = cbMaGV.Text;
            //  DateTime ngaysinh = Convert.ToDateTime(Console.ReadLine());
            DateTime ThoiGianBatDau = DateTime.Parse(tbThoiGianBatDau.Text);
            DateTime ThoiGianKetThuc = DateTime.Parse(tbThoiGianKetThuc.Text);

            // String malop = btmalop.Text;

            int result = DateTime.Compare(ThoiGianBatDau, ThoiGianKetThuc);
            if (result <= 0) 
                {
                    if (MaLHP != "")
                    {
                        if (Check_Ma_LHP(constr, MaLHP))
                        {
                            using (SqlConnection cnn = new SqlConnection(constr))
                            {
                                using (SqlCommand cmd = cnn.CreateCommand())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Insert_LHP";
                                    cmd.Parameters.AddWithValue("@MaLHP", MaLHP);
                                    cmd.Parameters.AddWithValue("@MaMon", MonHoc);
                                    cmd.Parameters.AddWithValue("@MaGV", MaGV);
                                    cmd.Parameters.AddWithValue("@ThoiGianBatDau", ThoiGianBatDau);
                                    cmd.Parameters.AddWithValue("@ThoiGianKetthuc", ThoiGianKetThuc);

                                    cnn.Open();
                                    int i = cmd.ExecuteNonQuery();
                                    cnn.Close();
                                    // return i > 0;
                                }
                            }

                            //  insertLHP(constr, MaLHP, MonHoc, MaGV, ThoiGianBatDau, ThoiGianKetThuc);
                            MessageBox.Show("Thêm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbMaLHP.Text = "";

                            LopHocPhan_Load(sender, e);

                        }
                        else
                            MessageBox.Show("Thêm thất bại do trùng MaLHP", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Vui Lòng Nhập Hết Các Dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            else
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 

        private void btXoa_Click(object sender, EventArgs e)
        {
            String MaLHP = tbMaLHP.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (deleteLHP(MaLHP))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // btBoQua_Click(sender, e);
                    tbMaLHP.Text = "";
                    

                    hienGirdView();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool deleteLHP(String MaLHP)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Delete_LHP";
                    cmd.Parameters.AddWithValue("@MaLHP", MaLHP);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                    return i > 0;
                }
            }
        }

  

        private DataTable Timkiem_SV(string TenMon)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {


                using (SqlCommand cmd = new SqlCommand("SELECT * FROM View_LHP WHERE [TenMon] LIKE N'%" + TenMon + "%' ", cnn))
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
        private void loadDataSV(DataTable dsTimkiem)
        {
            dtgvLHP.DataSource = dsTimkiem;
        }

        private void btTaiLai_Click(object sender, EventArgs e)
        {
            tbMaLHP.Text = "";
            //tbTenMon.Text = "";
            hienGirdView();
        }

        private void btTimKiem_Click_1(object sender, EventArgs e)
        {
            string TenMon = cbMonHoc.Text;

            //

            loadDataSV(Timkiem_SV(TenMon));
        }



        private void Sửa_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    if (tbMaLHP.Text != "")
                    {
                        cnn.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Update_LHP";
                        cmd.Parameters.AddWithValue("@MaLHP", tbMaLHP.Text);

                        cmd.Parameters.AddWithValue("@ThoiGianBatDau", tbThoiGianBatDau.Text);

                        cmd.Parameters.AddWithValue("@ThoiGianKetThuc", tbThoiGianKetThuc.Text);
                        int i = cmd.ExecuteNonQuery();
                        //tbMaSV.Enabled = false;
                        cnn.Close();
                        hienGirdView();



                    }
                    else
                        MessageBox.Show("Không Được Để Trống", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                }
            }
        }
    }
}
