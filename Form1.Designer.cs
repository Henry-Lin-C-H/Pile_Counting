﻿namespace Pile_Counting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_FileChoosing = new System.Windows.Forms.Button();
            this.lbl_FilePath = new System.Windows.Forms.Label();
            this.btn_PileCounting = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_FileChoosing
            // 
            this.btn_FileChoosing.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_FileChoosing.Location = new System.Drawing.Point(12, 107);
            this.btn_FileChoosing.Name = "btn_FileChoosing";
            this.btn_FileChoosing.Size = new System.Drawing.Size(91, 34);
            this.btn_FileChoosing.TabIndex = 0;
            this.btn_FileChoosing.Text = "讀取表單";
            this.btn_FileChoosing.UseVisualStyleBackColor = true;
            this.btn_FileChoosing.Click += new System.EventHandler(this.btn_FileChoosing_Click);
            // 
            // lbl_FilePath
            // 
            this.lbl_FilePath.AutoSize = true;
            this.lbl_FilePath.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FilePath.Font = new System.Drawing.Font("微軟正黑體", 15F);
            this.lbl_FilePath.Location = new System.Drawing.Point(12, 144);
            this.lbl_FilePath.Name = "lbl_FilePath";
            this.lbl_FilePath.Size = new System.Drawing.Size(132, 25);
            this.lbl_FilePath.TabIndex = 1;
            this.lbl_FilePath.Text = "選擇檔案路徑";
            // 
            // btn_PileCounting
            // 
            this.btn_PileCounting.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_PileCounting.Location = new System.Drawing.Point(12, 195);
            this.btn_PileCounting.Name = "btn_PileCounting";
            this.btn_PileCounting.Size = new System.Drawing.Size(91, 37);
            this.btn_PileCounting.TabIndex = 2;
            this.btn_PileCounting.Text = "執行數量計算";
            this.btn_PileCounting.UseVisualStyleBackColor = true;
            this.btn_PileCounting.Click += new System.EventHandler(this.btn_PileCounting_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("華康粗黑體", 25F);
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "樁基礎設計數量計算";
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("Calibri", 9F);
            this.btn_Close.Location = new System.Drawing.Point(12, 247);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(91, 32);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "結束";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(628, 352);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_PileCounting);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_FilePath);
            this.Controls.Add(this.btn_FileChoosing);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_FileChoosing;
        private System.Windows.Forms.Label lbl_FilePath;
        private System.Windows.Forms.Button btn_PileCounting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Close;
    }
}
