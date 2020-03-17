using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pile_Counting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
            this.Text = "樁基礎設計數量計算 " + Application.ProductVersion;
            lbl_UserName.Text = $"{appGlobal.UserName}, {appGlobal.UserID} \n {appGlobal.UserEmail}";
        }

        private void MovePanel(Control c)
        {
            pnl_choose.Height = c.Height;
            pnl_choose.Top = c.Top;
        }

        string filePath;
        private void btn_FileChoosing_Click(object sender, EventArgs e)
        {
            MovePanel(btn_FileChoosing);

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
                            
            if(openFile.FileName == "")
            {
                filePath = null;
                lbl_FilePath.Text = "選擇檔案路徑";
            }
            else
            {
                filePath = openFile.FileName;
                lbl_FilePath.Text = openFile.FileName;
            }
            
        }

        private void btn_PileCounting_Click(object sender, EventArgs e)
        {
            MovePanel(btn_PileCounting);
            //filePath = @"E:\2019_DQ126\!Count\20200224_P16.xlsx"; //暫時用，最後請刪除                       

            GeneralProcess("Counting");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MovePanel(btn_Close);
            this.Close();
        }

        private void btn_StrutCal_Click(object sender, EventArgs e)
        {
            MovePanel(btn_StrutCal);
            GeneralProcess("Strut");
        }

        void GeneralProcess(string function)
        {
            if (filePath == null) { MessageBox.Show("請選擇檔案路徑", "Error"); return; }

            PileCounting pileCounting = new PileCounting(filePath);
            pileCounting.Process(function);
            appGlobal.pileData.Clear();
            appGlobal.strutData.Clear();

            MessageBox.Show(appGlobal.state, "Pile Founction");
        }
    }
}
