namespace Pile_Counting
{
    partial class fStrutDesign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fStrutDesign));
            this.dtGrid_Strut = new System.Windows.Forms.DataGridView();
            this.btn_AutoStrut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_ConfirmClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid_Strut)).BeginInit();
            this.SuspendLayout();
            // 
            // dtGrid_Strut
            // 
            this.dtGrid_Strut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrid_Strut.Location = new System.Drawing.Point(3, 12);
            this.dtGrid_Strut.Name = "dtGrid_Strut";
            this.dtGrid_Strut.RowTemplate.Height = 24;
            this.dtGrid_Strut.Size = new System.Drawing.Size(1214, 450);
            this.dtGrid_Strut.TabIndex = 0;
            // 
            // btn_AutoStrut
            // 
            this.btn_AutoStrut.BackColor = System.Drawing.Color.Transparent;
            this.btn_AutoStrut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AutoStrut.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.btn_AutoStrut.Image = ((System.Drawing.Image)(resources.GetObject("btn_AutoStrut.Image")));
            this.btn_AutoStrut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_AutoStrut.Location = new System.Drawing.Point(916, 480);
            this.btn_AutoStrut.Name = "btn_AutoStrut";
            this.btn_AutoStrut.Size = new System.Drawing.Size(144, 41);
            this.btn_AutoStrut.TabIndex = 1;
            this.btn_AutoStrut.Text = "自動選擇支撐";
            this.btn_AutoStrut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_AutoStrut.UseVisualStyleBackColor = false;
            this.btn_AutoStrut.Click += new System.EventHandler(this.btn_AutoStrut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(913, 524);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "(以長度較少者為主)";
            // 
            // btn_Close
            // 
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Close.Location = new System.Drawing.Point(1084, 515);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(127, 41);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "結束";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_ConfirmClose
            // 
            this.btn_ConfirmClose.BackColor = System.Drawing.Color.Transparent;
            this.btn_ConfirmClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ConfirmClose.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.btn_ConfirmClose.Image = ((System.Drawing.Image)(resources.GetObject("btn_ConfirmClose.Image")));
            this.btn_ConfirmClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ConfirmClose.Location = new System.Drawing.Point(1084, 468);
            this.btn_ConfirmClose.Name = "btn_ConfirmClose";
            this.btn_ConfirmClose.Size = new System.Drawing.Size(127, 41);
            this.btn_ConfirmClose.TabIndex = 4;
            this.btn_ConfirmClose.Text = "確認並結束";
            this.btn_ConfirmClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_ConfirmClose.UseVisualStyleBackColor = false;
            this.btn_ConfirmClose.Click += new System.EventHandler(this.btn_ConfirmClose_Click);
            // 
            // fStrutDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 560);
            this.Controls.Add(this.btn_ConfirmClose);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_AutoStrut);
            this.Controls.Add(this.dtGrid_Strut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "fStrutDesign";
            this.Text = "StrutDesign";
            this.Load += new System.EventHandler(this.fStrutDesign_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid_Strut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtGrid_Strut;
        private System.Windows.Forms.Button btn_AutoStrut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_ConfirmClose;
    }
}