
namespace BaiTapLonHSK
{
    partial class DoiMatKhau
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btDoiMK = new System.Windows.Forms.Button();
            this.tbTenDN = new System.Windows.Forms.TextBox();
            this.tbMatKhauCu = new System.Windows.Forms.TextBox();
            this.tbMatKhauMoi = new System.Windows.Forms.TextBox();
            this.tbNhapLai = new System.Windows.Forms.TextBox();
            this.btThoat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMK = new System.Windows.Forms.Label();
            this.txtNhapLai = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btDoiMK
            // 
            this.btDoiMK.Location = new System.Drawing.Point(395, 318);
            this.btDoiMK.Name = "btDoiMK";
            this.btDoiMK.Size = new System.Drawing.Size(92, 31);
            this.btDoiMK.TabIndex = 0;
            this.btDoiMK.Text = "Đổi Mật Khẩu";
            this.btDoiMK.UseVisualStyleBackColor = true;
            this.btDoiMK.Click += new System.EventHandler(this.btDoiMK_Click);
            // 
            // tbTenDN
            // 
            this.tbTenDN.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTenDN.Location = new System.Drawing.Point(307, 58);
            this.tbTenDN.Multiline = true;
            this.tbTenDN.Name = "tbTenDN";
            this.tbTenDN.Size = new System.Drawing.Size(144, 28);
            this.tbTenDN.TabIndex = 1;
            // 
            // tbMatKhauCu
            // 
            this.tbMatKhauCu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMatKhauCu.Location = new System.Drawing.Point(307, 116);
            this.tbMatKhauCu.Multiline = true;
            this.tbMatKhauCu.Name = "tbMatKhauCu";
            this.tbMatKhauCu.Size = new System.Drawing.Size(144, 28);
            this.tbMatKhauCu.TabIndex = 2;
            this.tbMatKhauCu.UseSystemPasswordChar = true;
            // 
            // tbMatKhauMoi
            // 
            this.tbMatKhauMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMatKhauMoi.Location = new System.Drawing.Point(307, 172);
            this.tbMatKhauMoi.Multiline = true;
            this.tbMatKhauMoi.Name = "tbMatKhauMoi";
            this.tbMatKhauMoi.Size = new System.Drawing.Size(144, 28);
            this.tbMatKhauMoi.TabIndex = 3;
            this.tbMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // tbNhapLai
            // 
            this.tbNhapLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNhapLai.Location = new System.Drawing.Point(307, 221);
            this.tbNhapLai.Multiline = true;
            this.tbNhapLai.Name = "tbNhapLai";
            this.tbNhapLai.Size = new System.Drawing.Size(144, 28);
            this.tbNhapLai.TabIndex = 4;
            this.tbNhapLai.UseSystemPasswordChar = true;
            // 
            // btThoat
            // 
            this.btThoat.Location = new System.Drawing.Point(269, 318);
            this.btThoat.Name = "btThoat";
            this.btThoat.Size = new System.Drawing.Size(75, 31);
            this.btThoat.TabIndex = 5;
            this.btThoat.Text = "Thoát";
            this.btThoat.UseVisualStyleBackColor = true;
            this.btThoat.Click += new System.EventHandler(this.btThoat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tên Đăng Nhập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mật Khẩu Cũ";
            // 
            // txtMK
            // 
            this.txtMK.AutoSize = true;
            this.txtMK.Location = new System.Drawing.Point(175, 187);
            this.txtMK.Name = "txtMK";
            this.txtMK.Size = new System.Drawing.Size(73, 13);
            this.txtMK.TabIndex = 8;
            this.txtMK.Text = "Mật Khẩu Mới";
            // 
            // txtNhapLai
            // 
            this.txtNhapLai.AutoSize = true;
            this.txtNhapLai.Location = new System.Drawing.Point(175, 236);
            this.txtNhapLai.Name = "txtNhapLai";
            this.txtNhapLai.Size = new System.Drawing.Size(99, 13);
            this.txtNhapLai.TabIndex = 9;
            this.txtNhapLai.Text = "Nhập Lại Mật Khẩu";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(253, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Đổi Mật Khẩu";
            // 
            // DoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNhapLai);
            this.Controls.Add(this.txtMK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btThoat);
            this.Controls.Add(this.tbNhapLai);
            this.Controls.Add(this.tbMatKhauMoi);
            this.Controls.Add(this.tbMatKhauCu);
            this.Controls.Add(this.tbTenDN);
            this.Controls.Add(this.btDoiMK);
            this.Name = "DoiMatKhau";
            this.Text = "Đổi Mật Khẩu";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btDoiMK;
        private System.Windows.Forms.TextBox tbTenDN;
        private System.Windows.Forms.TextBox tbMatKhauCu;
        private System.Windows.Forms.TextBox tbMatKhauMoi;
        private System.Windows.Forms.TextBox tbNhapLai;
        private System.Windows.Forms.Button btThoat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtMK;
        private System.Windows.Forms.Label txtNhapLai;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label3;
    }
}