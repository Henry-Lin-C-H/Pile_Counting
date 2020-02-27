namespace Pile_Counting
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_FileChoosing = new System.Windows.Forms.Button();
            this.lbl_FilePath = new System.Windows.Forms.Label();
            this.btn_PileCounting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_FileChoosing
            // 
            this.btn_FileChoosing.Location = new System.Drawing.Point(70, 96);
            this.btn_FileChoosing.Name = "btn_FileChoosing";
            this.btn_FileChoosing.Size = new System.Drawing.Size(106, 37);
            this.btn_FileChoosing.TabIndex = 0;
            this.btn_FileChoosing.Text = "FileDialog";
            this.btn_FileChoosing.UseVisualStyleBackColor = true;
            this.btn_FileChoosing.Click += new System.EventHandler(this.btn_FileChoosing_Click);
            // 
            // lbl_FilePath
            // 
            this.lbl_FilePath.AutoSize = true;
            this.lbl_FilePath.Font = new System.Drawing.Font("Calibri", 15F);
            this.lbl_FilePath.Location = new System.Drawing.Point(66, 37);
            this.lbl_FilePath.Name = "lbl_FilePath";
            this.lbl_FilePath.Size = new System.Drawing.Size(61, 24);
            this.lbl_FilePath.TabIndex = 1;
            this.lbl_FilePath.Text = "label1";
            // 
            // btn_PileCounting
            // 
            this.btn_PileCounting.Location = new System.Drawing.Point(260, 96);
            this.btn_PileCounting.Name = "btn_PileCounting";
            this.btn_PileCounting.Size = new System.Drawing.Size(112, 37);
            this.btn_PileCounting.TabIndex = 2;
            this.btn_PileCounting.Text = "btn_PileCounting";
            this.btn_PileCounting.UseVisualStyleBackColor = true;
            this.btn_PileCounting.Click += new System.EventHandler(this.btn_PileCounting_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_PileCounting);
            this.Controls.Add(this.lbl_FilePath);
            this.Controls.Add(this.btn_FileChoosing);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_FileChoosing;
        private System.Windows.Forms.Label lbl_FilePath;
        private System.Windows.Forms.Button btn_PileCounting;
    }
}

