using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaiTapLonHSK.Crystal_Report;
namespace BaiTapLonHSK
{
    public partial class TrangChu : Form
    {
        String constr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        String TaiKhoan = "";
        String MatKhau = "";
        String HoTen = "";
        /*String Quyen = "";*/
        public static string Quyen;
        public TrangChu()
        {
            InitializeComponent();
        }

        public TrangChu(String TaiKhoan, String MatKhau, String HoTen, String Quyen)
        {
            InitializeComponent();
            this.TaiKhoan = TaiKhoan;
            this.MatKhau = MatKhau;
            this.HoTen = HoTen;
/*            this.Quyen = Quyen;*/
        }
        int interval = 100;

        private void mTimer_Tick(object sender, EventArgs e)
        {
                      interval --;
                time_dn.Text = "Thời Gian Còn Lại " + interval.ToString() + "s";
            if (interval == 0)
            {
                this.Hide();
                DangNhap dn = new DangNhap();
                dn.Show();
                
            }

        }
        private void TrangChu_Load(object sender, EventArgs e)
        {
            //Thời gian được đăng nhập

            Timer mTimer = new Timer();
            mTimer.Interval = interval;
            mTimer.Tick += new EventHandler(mTimer_Tick);
            mTimer_Tick(sender, e);
            mTimer.Start();
          //  time_dn.Visible = false;


            if (Quyen == "0")
            {
                btXemDiem.Visible = false;

            }
            else
            {
                 btQuanLiDiem.Visible = false;
                btQLSV.Visible = false;
                btInDS.Visible = false;
                thêmTàiKhoảnToolStripMenuItem.Visible = false;
                sinhViênToolStripMenuItem.Visible = false;
                điểmToolStripMenuItem.Visible = false;
                mônHọcToolStripMenuItem.Visible = false;
                lớpHọcPhầnToolStripMenuItem.Visible = false;

            }
        }

        //------------------Menu-------------------------------
        private void btQLSV_Click(object sender, EventArgs e)
        {
          
            if (Quyen == "0")
            {
               //btXemDiem.Visible = false;
                QLSV qlsv = new QLSV();
                //qlsv.MdiParent = this;
                qlsv.Show();
               // btXemDiem.Visible = false;
                //  btQLSV.Visible = true;

            }
         else
            {
                //btXemDiem.Visible = true;
               MessageBox.Show("Bạn Không Có Quyền Chỉnh Sửa", "Thông báo", MessageBoxButtons.OK);
            }
          //  btQLSV.Visible = true ;
            

        }

        private void btQuanLiDiem_Click(object sender, EventArgs e)
        {
          
            if (Quyen == "0")
            {
           
                Diem diem = new Diem();
                // diem.MdiParent = this;
                diem.Show();
            }
            else
            {
                MessageBox.Show("Bạn Không Có Quyền Chỉnh Sửa", "Thông báo", MessageBoxButtons.OK);
            }

        }


        private void btXemDiem_Click(object sender, EventArgs e)
        {
            

            if (Quyen == "1")
            {
                QLSV_User qlsvu = new QLSV_User();
               // qlsvu.MdiParent = this;
              
                qlsvu.Show();
                 
            }
            else
            {
                MessageBox.Show("Bạn Không Có Quyền Chỉnh Sửa", "Thông báo", MessageBoxButtons.OK);
            }
            //this.Close();
        }
        //-----------------------------------------------------------


        //----------------------Hệ Thống-----------------------------
        private void sinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLSV qlsv = new QLSV();
          //  qlsv.MdiParent = this;
            qlsv.Show();
        }
        private void điểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Diem diem = new Diem();
           // diem.MdiParent = this;
            diem.Show();
        }

        //----------------------------------------

        //--------------Cá Nhân ------------------
        private void đổiMậtKhẩuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoiMatKhau doimk = new DoiMatKhau();
           // doimk.MdiParent = this;
            // btXemDiem.Visible = false;
            doimk.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát khỏi chương trình", "Thông báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                //Close();
            {
                Close();

                DangNhap dn = new DangNhap();
                dn.Show();
                // tt.Close();
            }
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  Form_ThongTin thongtin = new Form_ThongTin();

            if (Quyen == "0")
            {
                Form_ThongTin thongtin = new Form_ThongTin();
                // doimk.MdiParent = this;
                // btXemDiem.Visible = false;
                thongtin.Show();

            }
            else
            {
                Form_ThongTinSV thongtinsv = new Form_ThongTinSV();
                // doimk.MdiParent = this;
                // btXemDiem.Visible = false;
                thongtinsv.Show();
            }
        }
        //-------------------------------------------------


        private void btInDS_Click(object sender, EventArgs e)
        {
            Form_Crystall f2 = new Form_Crystall();
            //f2.MdiParent = this;
            f2.Show();
        }

        private void thêmTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (Quyen == "0")
            {
                Dangky dk = new Dangky();
                // dk.MdiParent = this;
                dk.Show();

            }
            else
            {
                MessageBox.Show("Bạn Không Có Quyền Chỉnh Sửa", "Thông báo", MessageBoxButtons.OK);
               // thêmTàiKhoảnToolStripMenuItem.Visible = true;

            }
        }

        private void mônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Quyen == "0")
            {
                MonHoc mh = new MonHoc();
                // dk.MdiParent = this;
                mh.Show();

            }
            else
            {
                MessageBox.Show("Bạn Không Có Quyền Chỉnh Sửa", "Thông báo", MessageBoxButtons.OK);
                // thêmTàiKhoảnToolStripMenuItem.Visible = true;

            }
        }

        private void lớpHọcPhầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
           LopHocPhan mh = new LopHocPhan();
            // dk.MdiParent = this;
            mh.Show();
        }

        private void abcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
                f1.Show();
        }
    }
}
