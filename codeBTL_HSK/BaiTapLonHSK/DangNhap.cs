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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace BaiTapLonHSK
{
    public partial class DangNhap : Form
    {
        private int Dem = 0, i;
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public DangNhap()
        {
            InitializeComponent();

        }
        public DangNhap(String TaiKhoan, String MatKhau, String HoTen, String Quyen)
        {
            InitializeComponent();


        }

        public bool CheckDangNhap()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from TaiKhoan where TaiKhoan= '" + tbDangNhap.Text + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (String.Compare(rd["MatKhau"].ToString(), tbMatKhau.Text, true) == 0)
                            //if (rd["MatKhau"].Equals(txtMatKhau.Text))
                            {
                                TrangChu.Quyen = (string)rd["Quyen"];
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool Check_TaiKhoan(string constr, string TaiKhoan)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from Taikhoan", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["TaiKhoan"].Equals(TaiKhoan))
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }


        Class.Login ub = new Class.Login();
        private void btDangNhap_Click(object sender, EventArgs e)
        {
            // số 1 để chỉnh số phút khóa
            TimeSpan aInterval = new System.TimeSpan(0, 0, 1, 0);
            if (!Check_TaiKhoan(constr, tbDangNhap.Text))
            {
                if (CheckDangNhap())
                {
                    //Khóa 5p
                    int result = DateTime.Compare(DateTime.Now.Subtract(aInterval), DateTime.Parse(layTimelock(constr, tbDangNhap.Text)));

                    if (result >= 0)
                    {
                        
                        TrangChu frmmhc = new TrangChu();
                        demlandn(constr);
                        frmmhc.Show();
                        this.Hide();
                        TruyenDuLieu.MaSV = tbDangNhap.Text;
                        TruyenDuLieu.MaGV = tbDangNhap.Text;

                    }

                    else
                     MessageBox.Show(" Đăng Nhập Thất Bại Tài Khoản Của Bạn Đã Bị Khóa Đến" + DateTime.Parse(layTimelock(constr, tbDangNhap.Text)).Add(aInterval), "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Mật Khẩu Không Chính Xác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     dem++;

                    if (dem > 3)
                    {
                        MessageBox.Show("Tài Khoản Của Bạn Đã Bị Khóa Đến " + DateTime.Parse(layTimelock(constr, tbDangNhap.Text)).Add(aInterval), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        DateTime date = DateTime.Now;
                        KhoaTKtheotime(date);
                    }

                }
            }
            else
                MessageBox.Show("Tài Khoản Không Tồn Tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);



            /*String condition;
                condition = " TaiKhoan ='" + tbDangNhap.Text + "' AND MatKhau ='" + tbMatKhau.Text + "'";
                DataTable dt = new DataTable();
                try
                {
                TimeSpan aInterval = new System.TimeSpan(0, 0, 1, 0);
                int result = DateTime.Compare(DateTime.Now.Subtract(aInterval), DateTime.Parse(layTimelock(constr, tbDangNhap.Text)));

                if (result >= 0)
                {
                    dt = ub.getUser(condition);
                    if (dt.Rows.Count > 0)
                    {
                        TrangChu frmmhc = new TrangChu(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString());
                        frmmhc.Show();
                        this.Hide();
                        TruyenDuLieu.MaSV = tbDangNhap.Text;
                        TruyenDuLieu.MaGV = tbDangNhap.Text;


                    }
                    else
                        MessageBox.Show("Sai TK và MK", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(" Đăng Nhập Thất Bại " + DateTime.Parse(layTimelock(constr, tbDangNhap.Text)).Add(aInterval), "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Dem++;
                    if (Dem == 3)
                    {
                        MessageBox.Show("Tài Khoản Của Bạn Đã Bị Khóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        DateTime date = DateTime.Now;
                        KhoaTKtheotime(date);
                    }

                }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Bạn đã nhập sai cú pháp");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Lỗi kết nối CSDL !");
                }*/

        }


        private void btTroLai_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lbDangKy_Click(object sender, EventArgs e)
        {
            Dangky dk = new Dangky();
            // dk.MdiParent = this;
            dk.Show();
        }


        private void KhoaTK()
        {
            timer1.Enabled = true;
            Time.Show();
            i = 100;
        }


        private void DangNhap_Load(object sender, EventArgs e)
        {
            Time.Hide();
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    i--;
        //    Time.Text = "Thời gian còn lại " + i.ToString() + "Giây";
        //    if (i <= 0)
        //    {
        //        timer1.Enabled = false;
        //        tbDangNhap.Enabled = true;
        //        tbMatKhau.Enabled = true;
        //        btDangNhap.Enabled = true;
        //        Time.Hide();
        //        Dem = 0;
        //    }
        //}

        //private void Login_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    using (SqlConnection cnn = new SqlConnection(constr))
        //    {
        //        try
        //        {
        //            string proecName = "SoLan_Delete";
        //            SqlCommand cmd = new SqlCommand(proecName, cnn);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cnn.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Error!!");
        //            //throw;
        //        }
        //        finally
        //        {
        //            cnn.Close();
        //        }
        //    }
        //}

        //----------------------------------------------------------
        private int dem = 0;

        private void KhoaTKtheotime(DateTime time)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cnn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "update_lastlogin";
                    cmd.Parameters.AddWithValue("@TaiKhoan", tbDangNhap.Text);
                    cmd.Parameters.AddWithValue("@lastlogin", time);
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                    dem = 0;
                }
            }
        }
        public string layTimelock(string constr, string TaiKhoan)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from TaiKhoan where TaiKhoan= '" + TaiKhoan + "'", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["TaiKhoan"].Equals(TaiKhoan))
                                return rd["Time_Login"].ToString();
                        }
                        return "";
                    }
                }
            }
        }

        //-----------------------------------------------------------

        public void demlandn(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cnn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UPDATE_DN";
                    cmd.Parameters.AddWithValue("@TaiKhoan", tbDangNhap.Text);

                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                }
            }
        }
    }
}
   