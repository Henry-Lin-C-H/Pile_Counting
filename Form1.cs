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
        }

        string filePath;
        private void btn_FileChoosing_Click(object sender, EventArgs e)
        {            
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
                            
            if(openFile.FileName == "")
            {
                filePath = null;
            }
            else
            {
                filePath = openFile.FileName;
                lbl_FilePath.Text = openFile.FileName;
            }
            
        }

        private void btn_PileCounting_Click(object sender, EventArgs e)
        {

            filePath = @"E:\2019_DQ126\!Count\20200224_P16.xlsx"; //暫時用，最後請刪除

            if (filePath == null) { MessageBox.Show("請選擇檔案路徑", "Error"); return; }
            
            PileCounting pileCounting = new PileCounting(filePath);
            string done = pileCounting.Process();

            MessageBox.Show(done);
        }
    }
}
