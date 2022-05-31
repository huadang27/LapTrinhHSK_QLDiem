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
    public partial class DoiMatKhau : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        public static bool Check_Ma(string constr, string TaiKhoan,string MatKhau)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from TaiKhoan", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if(rd["TaiKhoan"].Equals(TaiKhoan) && rd["MatKhau"].Equals(MatKhau))
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }
        private void btDoiMK_Click(object sender, EventArgs e)

        {
            string TaiKhoan = tbTenDN.Text;
            string MatKhau = tbMatKhauCu.Text;
            SqlDataAdapter data = new SqlDataAdapter("Select count (*) from TaiKhoan where TaiKhoan = N'"+tbTenDN.Text+"' and MatKhau = N'"+tbMatKhauCu.Text+"'",constr);
            DataTable dt = new DataTable();
            data.Fill(dt);
            errorProvider1.Clear();

            if (dt.Rows[0][0].ToString()=="1")
               // Check_Ma(constr, TaiKhoan, MatKhau)
            {
                if (tbMatKhauMoi.Text == tbNhapLai.Text)
                {
                    SqlDataAdapter data_1 = new SqlDataAdapter("update TaiKhoan set MatKhau = N'" + tbMatKhauMoi.Text + "' where TaiKhoan = N'" + tbTenDN.Text + "' and MatKhau = N'" + tbMatKhauCu.Text + "'", constr);
                    DataTable dt1 = new DataTable();
                    data_1.Fill(dt1);
                    MessageBox.Show("Đổi Mật Khẩu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    errorProvider1.SetError(tbMatKhauMoi, "Bạn chưa điền mật khẩu");
                    errorProvider1.SetError(tbNhapLai, "Mật khẩu nhập lại chưa đúng");

                }
            }
            else
            {
                MessageBox.Show("Tên Tài Khoản Hoặc Mật Khẩu Không Đúng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
