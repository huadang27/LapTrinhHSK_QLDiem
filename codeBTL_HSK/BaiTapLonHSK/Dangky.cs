using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BaiTapLonHSK.Class;
using System.Data.SqlClient;
using System.Configuration;

namespace BaiTapLonHSK
{
    public partial class Dangky : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public Dangky()
        {
            InitializeComponent();
        }
        public bool CheckAccount(string ac) //check mk va ten tk
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{6,24}$");
        }

        private void btTroLai_Click(object sender, EventArgs e)
        {
            Close();
        }
        ListTaiKhoan listTK = new ListTaiKhoan();

        
        private void btDangKy_Click(object sender, EventArgs e)
        {
            string HoTen = tbHoTen.Text;
            
            string TaiKhoan = tbDangNhap.Text;
            string SDT = tbSDT.Text;
            string Email = tbEmail.Text;
            string CCCD = tbCCCD.Text;
            string Matkhau = tbMatKhau.Text;
            string NhapLaiMatKhau = tbMatKhauLai.Text;
            //string Quyen = "0";
           // string MaSV = null;
           // string MaGV = tbDangNhap.Text;
            //string MaSV = "";
            
            tbHoTen.Focus();
            //if(!CheckAccount(TaiKhoan))
            //{
            //    MessageBox.Show("Vui lòng nhập cả chữ hoa và thường và 6-24 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //         MessageBox.Show("Vui Lòng Nhập Hết Các Dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //-------------------------------------------------
            
            //----------------------------------------------------

            if (!CheckAccount(Matkhau))
            {
                MessageBox.Show("Vui lòng nhập cả chữ hoa và thường và 6-24 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (NhapLaiMatKhau!=Matkhau)
            {
                MessageBox.Show("Mật khẩu không khớp", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //if (listTK.getUsers("Select * from TaiKhoan where TaiKhoan = '"+TaiKhoan+"'").Count!=0)
            //{
            //    MessageBox.Show("Mã GV đã được đăng ký", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            try
            //{
            //    string sql = "Insert into GiangVien values('" + TaiKhoan + "','" + HoTen + "','" + SDT + "','" + Email + "','" + CCCD + "')";
            //    listTK.ThemTK(sql);
            //     sql = "Insert into TaiKhoan values('" + TaiKhoan + "','" + Matkhau + "','" + HoTen + "','" + Quyen + "','" + TaiKhoan + "','"+ MaSV +"')";
            //    listTK.ThemTK(sql);


            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Insert_GV";
                        cmd.Parameters.AddWithValue("@MaGV", tbDangNhap.Text);
                        cmd.Parameters.AddWithValue("@TenGV", tbHoTen.Text);
                        cmd.Parameters.AddWithValue("@SDT", tbSDT.Text);
                        cmd.Parameters.AddWithValue("@Email", tbEmail.Text);
                        cmd.Parameters.AddWithValue("@CCCD", tbCCCD.Text);
                        cmd.Parameters.AddWithValue("@MatKhau", tbMatKhau.Text);
                        //cmd.Parameters.AddWithValue("@", tbMatKhau);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        cnn.Close();
                        //return i > 0;
                    }
                }
            
                if (MessageBox.Show("Đăng Ký Thành Công", "Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
                {
                    this.Close();
                }

            }
            catch
            {
                MessageBox.Show("Mã GV đã được đăng ký", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
