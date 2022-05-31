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
    public partial class Diem : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public Diem()
        {
            InitializeComponent();
        }
        private DataTable HienDiem(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UPDATE_DIEM_SV";
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable("Diem"))
                        {
                            ad.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        private DataTable HienMaSV(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from SinhVien", cnn))
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
        private DataTable HienMaLHP(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from LopHocPhan", cnn))
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
        private void HienMaSV()
        {
            DataTable table = HienMaSV(constr);
            DataView view = new DataView(table);

            view.Sort = "MaSV";
            tbMaSV.DataSource = view;
            tbMaSV.DisplayMember = "MaSV";
            tbMaSV.ValueMember = "MaSV";

        }
        private void HienLHP()
        {
            DataTable table = HienMaLHP(constr);
            DataView view = new DataView(table);
            view.Sort = "MaLHP";
            tbMaLHP.DataSource = view;
            tbMaLHP.DisplayMember = "MaLHP";
            tbMaLHP.ValueMember = "MaLHP";

        }

        private void Diem_Load(object sender, EventArgs e)
        {
            HienMaSV();
            HienLHP();
            //HienDiem(constr);
            hienGirdView_Diem();
        }

        private void hienGirdView_Diem()
        {
            DataTable table = HienDiem(constr);
            dtgvDiem.DataSource = table;
            dtgvDiem.AutoResizeColumns();
        }

        //------------------Thêm Điểm------------------------
        public static bool Check_MSV(string constr, string MaSV,string MaLHP)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from Diem", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaSV"].Equals(MaSV)&&rd["MaLHP"].Equals(MaLHP))
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }
        private static bool insert_Diem(string constr, String MaSV, String MaLHP, float DiemCC, float DiemGK, float DiemThi)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Insert_Diem";
                    cmd.Parameters.AddWithValue("@MaSV", MaSV);
                    cmd.Parameters.AddWithValue("@MaLHP", MaLHP);
                    cmd.Parameters.AddWithValue("@DiemCC", DiemCC);
                    cmd.Parameters.AddWithValue("@DiemGK", DiemGK);
                    cmd.Parameters.AddWithValue("@DiemThi", DiemThi);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                    return i > 0;
                }
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
           
            if(tbDCC.Text !="" && tbDGK.Text != "" && tbDThi.Text !="")
            {
                
                String MaSV = tbMaSV.Text;
                String MaLHP = tbMaLHP.Text;
                float DiemCC = float.Parse(tbDCC.Text);
                float DiemGK = float.Parse(tbDGK.Text);
                float DiemThi = float.Parse(tbDThi.Text);
                if (Check_MSV(constr, MaSV, MaLHP))
                {
                    if (DiemCC >= 0 && DiemCC <= 10 && DiemGK >= 0 && DiemGK <= 10 && DiemThi >= 0 && DiemThi <= 10)
                    {
                        if (Check_NgayBatDau(constr, tbMaLHP.Text, tbMaSV.Text, layTenMon(constr, MaLHP)) || Check_NgayKetThuc(constr, tbMaLHP.Text, tbMaSV.Text, layTenMon(constr, MaLHP)))
                        {
                            if (Check_HocLai(constr, MaLHP, MaSV, layTenMon(constr, MaLHP)))
                            {
                                if (insert_Diem(constr, MaSV, MaLHP, DiemCC, DiemGK, DiemThi))
                                {
                                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    hienGirdView_Diem();
                                }
                                else
                                {
                                    MessageBox.Show("Thêm  thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                                MessageBox.Show("SV Đã Qua Môn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        MessageBox.Show("Thêm  thất bại do trùng Thời Gian Học", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("Không Đúng Định Dạng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

                else
                    MessageBox.Show("Thêm  thất bại do trùng Mã SV Và Mã Lớp", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
            }
            else
                MessageBox.Show("Bạn chưa nhập điểm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //---------------Xóa----------------------------
        private bool deleteDiem(String MaSV,String MaLHP)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Delete_Diem";
                    cmd.Parameters.AddWithValue("@MaSV", MaSV);
                    cmd.Parameters.AddWithValue("@MaLHP", MaLHP);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                    return i > 0;
                }
            }
        }
        private void btXoa_Click(object sender, EventArgs e)
        {
            String MaSV = tbMaSV.Text;
            String MaLHP = tbMaLHP.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (deleteDiem(MaSV,MaLHP))
                {
                    MessageBox.Show("Xóa SV thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // btBoQua_Click(sender, e);
                    tbDCC.Text = "";
                    tbDGK.Text = "";
                    tbDThi.Text = "";

                    hienGirdView_Diem();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtgvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbMaSV.Text = dtgvDiem.CurrentRow.Cells[0].Value.ToString();
            tbMaLHP.Text = dtgvDiem.CurrentRow.Cells[1].Value.ToString();
            tbDCC.Text = dtgvDiem.CurrentRow.Cells[2].Value.ToString();
            tbDGK.Text = dtgvDiem.CurrentRow.Cells[3].Value.ToString();
            tbDThi.Text = dtgvDiem.CurrentRow.Cells[4].Value.ToString();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cnn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Update_Diem";
                    cmd.Parameters.AddWithValue("@MaSV", tbMaSV.Text);
                    cmd.Parameters.AddWithValue("@MaLHP", tbMaLHP.Text);
                    cmd.Parameters.AddWithValue("@DiemCC", tbDCC.Text);
                    cmd.Parameters.AddWithValue("@DiemGK", tbDGK.Text);
                    cmd.Parameters.AddWithValue("@DiemThi", tbDThi.Text);
                    int i = cmd.ExecuteNonQuery();
                    //tbMaSV.Enabled = false;
                    cnn.Close();
                    hienGirdView_Diem();
                }
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string MaSV = tbMaSV.Text;
            //
         
                loadDataDiem(Timkiem_SV( MaSV));
        }

     
        private DataTable Timkiem_SV( string MaSV)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
              
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ShowViewAllDiem WHERE[MaSV] LIKE '%" + MaSV + "%'", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable("ShowViewAllDiem"))
                        {
                            ad.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        private void loadDataDiem(DataTable dsTimkiem)
        {
            dtgvDiem.DataSource = dsTimkiem;
        }

        //---check môn--
        public bool Check_HocLai(string constr, string MaLHP, string MaSV, string TenMon)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from check_mon where MaSV='" + MaSV + "' and TenMon=N'" + TenMon + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (double.Parse(rd["TB Môn"].ToString()) > 4)
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }


        public string layTenMon(string constr, string MaLHP)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from LopHocPhan,MonHoc where LopHocPhan.MaMon=MonHoc.MaMon and MaLHP= '" + MaLHP + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaLHP"].Equals(MaLHP))
                                return rd["TenMon"].ToString();
                        }
                        return "";
                    }
                }
            }
        }


        //CHECK NGAY------
        public string layNgayBatDau(string constr, string MaLHP)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from LopHocPhann where LopHocPhan.MaLHP='" + MaLHP + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaLHP"].Equals(MaLHP))
                                return rd["ThoiGianBatDau"].ToString();
                        }
                        return "";
                    }
                }
            }
        }
        public bool Check_NgayKetThuc(string constr, string MaLHP, string MaSV, string TenMon)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from Diem, LopHocPhan,MonHoc where Diem.MaLHP=LopHocPhan.MaLHP and MonHoc.MaMon=LopHocPhan.MaMon and Diem.MaSV='" + MaSV + "' and MonHoc.TenMon=N'" + TenMon + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (DateTime.Compare(DateTime.Parse(layNgayBatDau(constr, MaLHP)), DateTime.Parse(rd["ThoiGianKetThuc"].ToString())) < 0)
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }
        //
        //Kiểm tra khoảng ngày trc
        public string layNgayKetThuc(string constr, string MaLHP)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from LopHocPhan where LopHocPhan.MaLHP='" + MaLHP + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaLHP"].Equals(MaLHP))
                                return rd["ThoiGianKetThuc"].ToString();
                        }
                        return "";
                    }
                }
            }
        }
        public bool Check_NgayBatDau(string constr, string MaLHP, string MaSV, string TenMon)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from Diem, LopHocPhan,MonHoc where Diem.MaLHP=LopHocPhan.MaLHP and MonHoc.MaMon=LopHocPhan.MaMon and Diem.MaSV='" + MaSV + "' and MonHoc.TenMon=N'" + TenMon + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (DateTime.Compare(DateTime.Parse(layNgayKetThuc(constr, MaLHP)), DateTime.Parse(rd["ThoiGianBatDau"].ToString())) > 0)
                                return false;
                        }
                         rd.Close();
                    }
                        cnn.Close();
                }
            }
            return true;
        }

        private void tbDCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbDGK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbDThi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}

