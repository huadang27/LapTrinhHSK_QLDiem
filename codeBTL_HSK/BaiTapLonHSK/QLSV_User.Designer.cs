
namespace BaiTapLonHSK
{
    partial class QLSV_User
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
            this.View_User = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // View_User
            // 
            this.View_User.ActiveViewIndex = -1;
            this.View_User.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.View_User.Cursor = System.Windows.Forms.Cursors.Default;
            this.View_User.DisplayToolbar = false;
            this.View_User.Location = new System.Drawing.Point(-211, 0);
            this.View_User.Name = "View_User";
            this.View_User.Size = new System.Drawing.Size(1247, 658);
            this.View_User.TabIndex = 0;
            this.View_User.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // QLSV_User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 636);
            this.Controls.Add(this.View_User);
            this.Name = "QLSV_User";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QLSV_User";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer View_User;
    }
}