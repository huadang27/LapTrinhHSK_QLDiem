
namespace BaiTapLonHSK
{
    partial class Form_Crystall
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
            this.label1 = new System.Windows.Forms.Label();
            this.btTimkiem = new System.Windows.Forms.Button();
            this.tbMaSV = new System.Windows.Forms.TextBox();
            this.crystalReportViewer2 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.btInDiem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMaLop = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nhập Thông Tin";
            // 
            // btTimkiem
            // 
            this.btTimkiem.Location = new System.Drawing.Point(316, 10);
            this.btTimkiem.Name = "btTimkiem";
            this.btTimkiem.Size = new System.Drawing.Size(75, 23);
            this.btTimkiem.TabIndex = 3;
            this.btTimkiem.Text = "Tìm SV";
            this.btTimkiem.UseVisualStyleBackColor = true;
            this.btTimkiem.Click += new System.EventHandler(this.btTimkiem_Click);
            // 
            // tbMaSV
            // 
            this.tbMaSV.Location = new System.Drawing.Point(97, 12);
            this.tbMaSV.Name = "tbMaSV";
            this.tbMaSV.Size = new System.Drawing.Size(120, 20);
            this.tbMaSV.TabIndex = 4;
            // 
            // crystalReportViewer2
            // 
            this.crystalReportViewer2.ActiveViewIndex = -1;
            this.crystalReportViewer2.AutoSize = true;
            this.crystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer2.Location = new System.Drawing.Point(-5, 39);
            this.crystalReportViewer2.Name = "crystalReportViewer2";
            this.crystalReportViewer2.Size = new System.Drawing.Size(1113, 558);
            this.crystalReportViewer2.TabIndex = 5;
            this.crystalReportViewer2.Load += new System.EventHandler(this.crystalReportViewer2_Load_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(235, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "DS Lớp";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btInDiem
            // 
            this.btInDiem.Location = new System.Drawing.Point(775, 15);
            this.btInDiem.Name = "btInDiem";
            this.btInDiem.Size = new System.Drawing.Size(75, 22);
            this.btInDiem.TabIndex = 7;
            this.btInDiem.Text = "In DS Điểm";
            this.btInDiem.UseVisualStyleBackColor = true;
            this.btInDiem.Click += new System.EventHandler(this.btInDiem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(546, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Chọn Môn";
            // 
            // cbMaLop
            // 
            this.cbMaLop.FormattingEnabled = true;
            this.cbMaLop.Location = new System.Drawing.Point(627, 15);
            this.cbMaLop.Name = "cbMaLop";
            this.cbMaLop.Size = new System.Drawing.Size(121, 21);
            this.cbMaLop.TabIndex = 11;
            // 
            // Form_Crystall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 599);
            this.Controls.Add(this.cbMaLop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btInDiem);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.crystalReportViewer2);
            this.Controls.Add(this.tbMaSV);
            this.Controls.Add(this.btTimkiem);
            this.Controls.Add(this.label1);
            this.Name = "Form_Crystall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Crystall";
            this.Load += new System.EventHandler(this.Form_Crystall_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btTimkiem;
        private System.Windows.Forms.TextBox tbMaSV;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btInDiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMaLop;
    }
}