using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLonHSK
{
    public partial class QLSV : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public QLSV()
        {
            InitializeComponent();
        }
        private DataTable HienDSSV(string constr)
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

        private DataTable HienLopHanhChinh(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from LopHanhChinh", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable("LopHanhChinh"))
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
            view.Sort = "TenLop";
            tbMaLop.DataSource = view;
            tbMaLop.DisplayMember = "TenLop";
            tbMaLop.ValueMember = "MaLop";

        }
  
        private void Form1_Load(object sender, EventArgs e)
        {

            tbMaSV.Enabled = false;
            tbTenSV.Enabled = false;
            rdoNu.Enabled = false;
            rdoNam.Enabled = false;
            tbNgaySinh.Enabled = false;
            tbEmail.Enabled = false;
            tbCCCD.Enabled = false;
            tbMaLop.Enabled = false;

            btBoQua.Enabled = false;
            btXoa.Enabled = false;
            btThemSV.Enabled = false;
            btSua.Enabled = false;
            btTimKiem.Enabled = false;
           // btSapXep.Enabled = false;

            tbMaSV.Text = "";
            tbTenSV.Text = "";
            tbEmail.Text = "";
            tbCCCD.Text = "";
            hienGirdView();
            HienMaLop();
            ViewHocSinh();
           // ChonGioiTinh();
        }
        private void hienGirdView()
        {
            DataTable table = HienDSSV(constr);
            dtgvSinhVien.DataSource = table;
            dtgvSinhVien.AutoResizeColumns();
        }
        //private void ChonGioiTinh()
        //{
        //    tbGioiTinh.Items.Insert(0, "Nam");
        //    tbGioiTinh.Items.Insert(1, "Nữ");
        //    tbGioiTinh.Text = tbGioiTinh.Items[0].ToString();
            
        //}
        //Thêm Sinh Viên --------------------------------
        private static bool insertSinhVien(string constr, String MaSV, String TenSV, String GioiTinh, DateTime NgaySinh, String Email, String CCCD, String MaLop)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Insert_SV";
                    cmd.Parameters.AddWithValue("@MaSV", MaSV);
                    cmd.Parameters.AddWithValue("@TenSV", TenSV);
                    cmd.Parameters.AddWithValue("@GioiTinh", GioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@CCCD", CCCD);
                    cmd.Parameters.AddWithValue("@MaLop", MaLop);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                    return i > 0;
                }
            }
        }
        public static bool Check_MSV(string constr, string MaSV)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from SinhVien", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaSV"].Equals(MaSV))
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }


        public static bool Check_MSV2(string constr, string MaLop)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select * from SinhVien", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd["MaLop"].Equals(MaLop))
                                return false;
                        }
                        rd.Close();
                    }
                    cnn.Close();
                }
            }
            return true;
        }

        private void btThemSV_Click(object sender, EventArgs e)
        {
            String MaSV = tbMaSV.Text;
            String TenSV = tbTenSV.Text;
            String GioiTinh;
            //  DateTime ngaysinh = Convert.ToDateTime(Console.ReadLine());
            DateTime NgaySinh = DateTime.Parse(tbNgaySinh.Text);
            String Email = tbEmail.Text;
            String CCCD = tbCCCD.Text;
            // String malop = btmalop.Text;
            String MaLop = tbMaLop.SelectedValue.ToString();



           // DateTime NgayHienTai =  DateTime.Now();
            
           // if(NgayHienTai - NgaySinh <= 18)
            {

            }
                if (MaSV != "" || TenSV != "" || CCCD != "" || Email != "")
            {
               
                if (Check_MSV(constr, MaSV))
                {
                    if (rdoNam.Checked == false && rdoNu.Checked == false)
                        MessageBox.Show("Vui Lòng Chọn Giới Tính", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (rdoNam.Checked == true)
                            insertSinhVien(constr, MaSV, TenSV, rdoNam.Text, NgaySinh, Email, CCCD, MaLop);
                        else if (rdoNu.Checked == true)
                            insertSinhVien(constr, MaSV, TenSV, rdoNu.Text, NgaySinh, Email, CCCD, MaLop);
                        MessageBox.Show("Thêm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbCCCD.Text = "";
                        tbEmail.Text = "";
                        tbMaSV.Text = "";
                        tbTenSV.Text = "";
                        Form1_Load(sender, e);
                    }
                }
                else
                    MessageBox.Show("Thêm thất bại do trùng MaSV", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Vui Lòng Nhập Hết Các Dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //-------------------------------------
        //Xóa ----------------------------------
        private bool deleteSV(String MaSV)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Delete_SV";
                    cmd.Parameters.AddWithValue("@MaSV", MaSV);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();
                    return i > 0;
                }
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            String maSV = tbMaSV.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (deleteSV(maSV))
                {
                    MessageBox.Show("Xóa SV thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                   // btBoQua_Click(sender, e);
                    tbMaSV.Text = "";
                    tbTenSV.Text = "";
                    tbEmail.Text = "";
                    tbCCCD.Text = "";

                    hienGirdView();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //--------------------------------
        //Sửa ---------------------------
        private void btSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    if (tbMaSV.Text != "" && tbTenSV.Text != "" && tbEmail.Text != "" && tbCCCD.Text != "" && tbMaLop.Text != "")
                    {
                        cnn.Open();
                        if (rdoNam.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@GioiTinh", rdoNam.Text);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@GioiTinh", rdoNu.Text);
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Update_SV";
                        cmd.Parameters.AddWithValue("@MaSV", tbMaSV.Text);
                        if (Check_MSV(constr, tbMaSV.Text))
                            MessageBox.Show("Không Được Sửa Mã Sinh Viên", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        else
                        {
                            cmd.Parameters.AddWithValue("@TenSV", tbTenSV.Text);
                            // cmd.Parameters.AddWithValue("@GioiTinh", rdoNu.Text);
                            //cmd.Parameters.AddWithValue("@GioiTinh", rdoNam.Text);
                            cmd.Parameters.AddWithValue("@NgaySinh", tbNgaySinh.Text);
                            cmd.Parameters.AddWithValue("@Email", tbEmail.Text);
                            cmd.Parameters.AddWithValue("@CCCD", tbCCCD.Text);
                            cmd.Parameters.AddWithValue("@MaLop", tbMaLop.SelectedValue.ToString());
                            int i = cmd.ExecuteNonQuery();
                            //tbMaSV.Enabled = false;
                            cnn.Close();
                            hienGirdView();
                        }


                    }
                    else
                        MessageBox.Show("Không Được Để Trống", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                }
            }
        }
        private void dtgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbMaSV.Text = dtgvSinhVien.CurrentRow.Cells[0].Value.ToString();
            tbTenSV.Text = dtgvSinhVien.CurrentRow.Cells[1].Value.ToString();
            tbNgaySinh.Text = dtgvSinhVien.CurrentRow.Cells[3].Value.ToString();
            tbEmail.Text = dtgvSinhVien.CurrentRow.Cells[4].Value.ToString();
            tbCCCD.Text = dtgvSinhVien.CurrentRow.Cells[5].Value.ToString();
            tbMaLop.Text = dtgvSinhVien.CurrentRow.Cells[6].Value.ToString();
        }
        private void ViewHocSinh()
        {
            dtgvSinhVien.ReadOnly = true;
            dtgvSinhVien.Columns[0].HeaderText = "Mã Sinh Viên";
            dtgvSinhVien.Columns[1].HeaderText = "Tên Sinh Viên";
            dtgvSinhVien.Columns[2].HeaderText = "Giới Tính";
            dtgvSinhVien.Columns[3].HeaderText = "Ngày Sinh";
            dtgvSinhVien.Columns[4].HeaderText = "Email";
            dtgvSinhVien.Columns[5].HeaderText = "CCCD";
            dtgvSinhVien.Columns[6].HeaderText = "Lớp Hành Chính";
            dtgvSinhVien.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            
            tbMaSV.Enabled = true;
            tbTenSV.Enabled = true;
            rdoNu.Enabled = true;
            rdoNam.Enabled = true;
            tbNgaySinh.Enabled = true;
            tbEmail.Enabled = true;
            tbCCCD.Enabled = true;
            tbMaLop.Enabled = true;

            btBoQua.Enabled = true;
            btXoa.Enabled = true;
            btThemSV.Enabled = true;
            btSua.Enabled = true;
            btTimKiem.Enabled = true;
           // btSapXep.Enabled = true;
            tbMaSV.Focus();
        }
        private void btBoQua_Click(object sender, EventArgs e)
        {
            tbMaSV.Enabled = false;
            tbTenSV.Enabled = false;
            rdoNu.Enabled = false;
            rdoNam.Enabled = false;
            tbNgaySinh.Enabled = false;
            tbEmail.Enabled = false;
            tbCCCD.Enabled = false;
            tbMaLop.Enabled = false;

            btBoQua.Enabled = false;
            btXoa.Enabled = false;
            btThemSV.Enabled = false;
            btSua.Enabled = false;
            btTimKiem.Enabled = false;
            //btSapXep.Enabled = false;

            tbMaSV.Text = "";
            tbTenSV.Text = "";
            tbEmail.Text = "";
            tbCCCD.Text = "";
        }
        //-----------SEACH------------------------------------
        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string TenSV = tbTenSV.Text;
            string MaSV = tbMaSV.Text;
            string CCCD = tbCCCD.Text;
            string Email = tbEmail.Text;
            string LopHC = tbMaLop.Text;
            //
            if (rdoNam.Checked == true)
                loadDataSV(Timkiem_SV(TenSV, MaSV, CCCD, Email, LopHC, rdoNam.Text));
            else if (rdoNu.Checked == true)
                loadDataSV(Timkiem_SV(TenSV, MaSV, CCCD, Email, LopHC, rdoNu.Text));
            else
                loadDataSV(Timkiem_SV(TenSV, MaSV, CCCD, Email, LopHC, ""));
        }

        //--------------------------------------------------------
        //private void btnTimKiem_Click(object sender, EventArgs e)
        //{
        //    string TenSV = txtTenSV.Text;
        //    string MaSV = txtMaSV.Text;
        //    string CCCD = txtCCCD.Text;
        //    string Email = txtEmail.Text;
        //    string LopHC = txtLopHC.Text;
        //    //
        //    if (rdbtNam.Checked == true)
        //        loadDataSV(Timkiem_SV(TenSV, MaSV, CCCD, Email, LopHC, rdbtNam.Text));
        //    else if (rdbtNu.Checked == true)
        //        loadDataSV(Timkiem_SV(TenSV, MaSV, CCCD, Email, LopHC, rdbtNu.Text));
        //    else
        //        loadDataSV(Timkiem_SV(TenSV, MaSV, CCCD, Email, LopHC, ""));
        //}
        private DataTable Timkiem_SV(String TenSV, string MaSV, string CCCD, string Email, string MaLop, string GioiTinh)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                String sqlGT = "";
                if (GioiTinh != "")
                {
                    sqlGT = "and[Giới Tính] = N'" + GioiTinh + "'";
                }
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM showViewAllSV WHERE [Tên Sinh Viên] LIKE N'%" + TenSV + "%' and [Mã Sinh Viên] LIKE '%" + MaSV + "%' and [CCCD] LIKE '%" + CCCD + "%' and [Email] LIKE '%" + Email + "%' and [Lớp Hành Chính] LIKE '%" + MaLop + "%' " + sqlGT, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable("showViewAllSV"))
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
            dtgvSinhVien.DataSource = dsTimkiem;
        }

        //    private void txtLopHC_Validating(object sender, CancelEventArgs e)
        //    {
        //        if (txtLopHC.SelectedIndex == 0)
        //        {
        //            errorProvider1.SetError(txtLopHC, "Khong được để trống");
        //        }
        //        else
        //            errorProvider1.SetError(txtLopHC, "");
        //    }
        //}

        //--------------------------------------Check--------------ư
        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đóng ứng dụng không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                e.Cancel = false;

            else e.Cancel = true;
        }

        private void tbMaSV_Validating(object sender, CancelEventArgs e)
        {
            if (tbMaSV.Text == "")
            {
                errorProvider1.SetError(tbMaSV, "Không được để trống !");
            }
            else
                errorProvider1.SetError(tbMaSV, "");
        }

        private void tbTenSV_Validating(object sender, CancelEventArgs e)
        {
            if (tbTenSV.Text == "")
            {
                errorProvider1.SetError(tbTenSV, "Không được để trống !");
            }
            else
                errorProvider1.SetError(tbTenSV, "");
        }

        //private void tbEmail_Validating(object sender, CancelEventArgs e)
        //{
        //    string pattern = @"\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        //    if (tbEmail.Text == "")
        //    {
        //        errorProvider1.SetError(tbEmail, "Không được để trống !");
        //    }
        //    else
        //    {
        //        if (Regex.IsMatch(tbEmail.Text, pattern))
        //        {
                    
        //            errorProvider1.SetError(tbEmail, "");
        //        }
        //        else
        //            errorProvider1.SetError(tbEmail, "Không phải Email");
        //    }
                
        //}


        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            if (tbEmail.Text == "")
            {
                errorProvider1.SetError(tbEmail, "Không được để trống !");
            }
            else
            {
                if (Regex.IsMatch(tbEmail.Text, pattern))
                {
                    errorProvider1.SetError(tbEmail, "");
                }
                else
                    errorProvider1.SetError(tbEmail, "Không phải Email");
            }
        }

        private void tbCCCD_Validating(object sender, CancelEventArgs e)
        {

            if (tbCCCD.Text == "")
            {
                errorProvider1.SetError(tbCCCD, "Không được để trống !");
            }
            else if (tbCCCD.Text.Length ==12)
                errorProvider1.SetError(tbCCCD, "");
        else
                errorProvider1.SetError(tbCCCD, "CCCC gồm 12 số!");
        }

        private void tbCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btTaiLai_Click(object sender, EventArgs e)
        {
            tbMaSV.Text = "";
            tbTenSV.Text = "";
            tbEmail.Text = "";
            tbCCCD.Text = "";
            hienGirdView();
        }

        private void điểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrangChu f1 = new TrangChu();
            f1.MdiParent = this;
            f1.Show();
        }

        private void btInDS_Click(object sender, EventArgs e)
        {
            Form_Crystall f2 = new Form_Crystall();
            //f2.MdiParent = this;
            f2.Show();
        }



//        Regex isValidInput = new Regex(@"^\d{9,11}$");
//        string strPhone = Console.ReadLine();
//if(!isValidInput.IsMatch(strPhone)) Console.WriteLine("Nhầm hàng rồi");


    }
}

