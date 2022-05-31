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
    public partial class MonHoc : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public MonHoc()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hienGirdView();
            ViewHocTT();
            cbSoTin.Items.Add("1");
            cbSoTin.Items.Add("2");
            cbSoTin.Items.Add("3");
            cbSoTin.Items.Add("4");

        }


        private DataTable HienDSSV(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM MonHoc", cnn))
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
        private void hienGirdView()
        {
            DataTable table = HienDSSV(constr);
            dataGridView1.DataSource = table;
            dataGridView1.AutoResizeColumns();
        }
        private static bool insertMonHoc(string constr, String MaMon, String TenMon, int SoTin)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Insert_MonHoc";
                    cmd.Parameters.AddWithValue("@MaMon", MaMon);
                    cmd.Parameters.AddWithValue("@TenMon", TenMon);
                    cmd.Parameters.AddWithValue("@SoTinChi", SoTin);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                    return i > 0;
                }
            }
        }

        public static bool Check_MaMon(string constr, string MaMon)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from MonHoc", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaMon"].Equals(MaMon))
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }

        public static bool Check_TenMon(string constr, string TenMon)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from MonHoc", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["TenMon"].Equals(TenMon))
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            string MaMon = tbMaMon.Text;
            string TenMon = tbTenMon.Text;
            int SoTin = int.Parse(cbSoTin.Text);
            if (MaMon != "" && TenMon != "")
            {
                if (Check_TenMon(constr, TenMon))
                {

                    if (Check_MaMon(constr, MaMon))
                    {
                        insertMonHoc(constr, MaMon, TenMon, SoTin);
                        MessageBox.Show("Thêm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbMaMon.Text = "";
                        tbTenMon.Text = "";
                        hienGirdView();


                    }

                    else
                    {
                        MessageBox.Show("Thêm thất bại do trùng Mã Môn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("Thêm thất bại do trùng Tên Môn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Vui Lòng Nhập Hết Các Dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void ViewHocTT()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].HeaderText = "Mã Môn";
            dataGridView1.Columns[1].HeaderText = "Tên Môn";
            dataGridView1.Columns[2].HeaderText = "Số Tín Chỉ";
          

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
       
        }
        private bool deleteMon(String MaMon)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Delete_Mon";
                    cmd.Parameters.AddWithValue("@MaMon", MaMon);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                    return i > 0;
                }
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            String MaMon = tbMaMon.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (deleteMon(MaMon))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // btBoQua_Click(sender, e);
                    tbMaMon.Text = "";
                    tbTenMon.Text = "";

                    hienGirdView();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbMaMon.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tbTenMon.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
       cbSoTin.Text =  dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void Sửa_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    if (tbMaMon.Text != "" && tbTenMon.Text != "")
                    {
                        cnn.Open();
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Update_Mon";
                        cmd.Parameters.AddWithValue("@MaMon", tbMaMon.Text);
                       
                            cmd.Parameters.AddWithValue("@TenMon", tbTenMon.Text);
                     
                            cmd.Parameters.AddWithValue("@SoTin",int.Parse( cbSoTin.Text));
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


        private void btTimKiem_Click(object sender, EventArgs e)
        {

            string MaMon = tbMaMon.Text;
            string TenMon= tbTenMon.Text;
       
            //
            
                loadDataSV(Timkiem_SV(MaMon,TenMon));

        }

        private DataTable Timkiem_SV(String MaMon,  string TenMon)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                
               
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM MonHoc WHERE [TenMon] LIKE N'%" + TenMon+ "%' ", cnn))
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
            dataGridView1.DataSource = dsTimkiem;
        }

        private void btTaiLai_Click(object sender, EventArgs e)
        {
            tbMaMon.Text = "";
            tbTenMon.Text = "";
           
            hienGirdView();
        }

    
    }
}
