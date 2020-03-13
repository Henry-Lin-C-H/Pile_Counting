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
    public partial class fStrutDesign : Form
    {        

        public fStrutDesign()
        {
            InitializeComponent();

            DataGridViewColumn col;

            List<string> colName = new List<string>();
            List<string> colHeader = new List<string>();

            colName.Add("PileID");
            colName.Add("Df");
            colName.Add("CapLength");
            colName.Add("CapWidth");
            colName.Add("Bracing2m");
            colName.Add("Strut2m");
            colName.Add("Total2m");
            colName.Add("Bracing2p5m");
            colName.Add("Strut2p5m");
            colName.Add("Total2p5m");
            colName.Add("Stage");

            colHeader.Add("墩柱編號");
            colHeader.Add("Df");
            colHeader.Add("樁帽軸長");
            colHeader.Add("樁帽橫寬");
            colHeader.Add("斜撐(2m)");
            colHeader.Add("直撐(2m)");
            colHeader.Add("間距2m總長");
            colHeader.Add("斜撐(2.5m)");
            colHeader.Add("直撐(2.5m)");
            colHeader.Add("間距2.5m總長");
            colHeader.Add("階數");

            for (int i = 0; i < colName.Count; i++)
            {
                col = new DataGridViewTextBoxColumn();
                col.Name = colName[i];
                col.DataPropertyName = colName[i];
                col.HeaderText = colHeader[i];
                col.ReadOnly = true;
                dtGrid_Strut.Columns.Add(col);
                                
            }

            col = new DataGridViewTextBoxColumn();
            col.Name = "DesignStrut";
            col.DataPropertyName = "DesignStrut";
            col.HeaderText = "選擇支撐";
            col.ReadOnly = false;
            dtGrid_Strut.Columns.Add(col);

            DataTable dt = new DataTable();
            for (int i = 0; i < colName.Count; i++) { dt.Columns.Add(colName[i]); }
            DataRow row;

            List<PileData> PileData = appGlobal.pileData;
            List<strutData> strutData = appGlobal.strutData;
            for (int i = 0; i < appGlobal.pileData.Count; i++)
            {


                row = dt.NewRow();
                row["PileID"] = PileData[i].ID;
                row["Df"] = PileData[i].Df;
                row["CapLength"] = PileData[i].Length;
                row["CapWidth"] = PileData[i].Width;
                row["Bracing2m"] = strutData[i].strBracing2m;
                row["Strut2m"] = strutData[i].strStrut2m;
                row["Total2m"] = Math.Round(strutData[i].total2m,2);
                row["Bracing2p5m"] = strutData[i].strBracing2p5m;
                row["Strut2p5m"] = strutData[i].strStrut2p5m;
                row["Total2p5m"] = Math.Round(strutData[i].total2p5m,2);
                row["Stage"] = strutData[i].stage;
                //row["DesignStrut"] = "";

                dt.Rows.Add(row);
            }

            dtGrid_Strut.DataSource = dt;
            dtGrid_Strut.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void fStrutDesign_Load(object sender, EventArgs e)
        {
            DataGridViewColumn col;

            List<string> colName = new List<string>();
            List<string> colHeader = new List<string>();

            colName.Add("PileID");
            colName.Add("Df");
            colName.Add("CapLength");
            colName.Add("CapWidth");
            colName.Add("Bracing2m");
            colName.Add("Strut2m");
            colName.Add("Total2m");
            colName.Add("Bracing2p5m");
            colName.Add("Strut2p5m");
            colName.Add("Total2p5m");
            colName.Add("Stage");

            colHeader.Add("墩柱編號");
            colHeader.Add("Df");
            colHeader.Add("樁帽軸長");
            colHeader.Add("樁帽橫寬");
            colHeader.Add("斜撐(2m)");
            colHeader.Add("直撐(2m)");
            colHeader.Add("間距2m總長");
            colHeader.Add("斜撐(2.5m)");
            colHeader.Add("直撐(2.5m)");
            colHeader.Add("間距2.5m總長");
            colHeader.Add("階數");
            
            for(int i = 0; i < colName.Count; i++)
            {
                col = new DataGridViewColumn();
                col.Name = colName[i];
                col.DataPropertyName = colName[i];
                col.HeaderText = colHeader[i];
                col.ReadOnly = true;
                dtGrid_Strut.Columns.Add(col);
            }

            col = new DataGridViewColumn();
            col.Name = "DesignStrut";
            col.DataPropertyName = "DesignStrut";
            col.HeaderText = "選擇支撐";
            col.ReadOnly = false;
            dtGrid_Strut.Columns.Add(col);

            DataTable dt = new DataTable();
            DataRow row;

            List<PileData> PileData = appGlobal.pileData;
            List<strutData> strutData = appGlobal.strutData;
            for (int i = 0; i < appGlobal.pileData.Count; i++)
            {
                

                row = dt.NewRow();
                row["PileID"] = PileData[i].ID;
                row["Df"] = PileData[i].Df;
                row["CapLength"] = PileData[i].Length;
                row["CapWidth"] = PileData[i].Width;
                row["Bracing2m"] = strutData[i].strBracing2m;
                row["Strut2m"] = strutData[i].strStrut2m;
                row["Total2m"] = strutData[i].total2m;
                row["Bracing2p5m"] = strutData[i].strBracing2p5m;
                row["Strut2p5m"] = strutData[i].strStrut2p5m;
                row["Total2p5m"] = strutData[i].total2p5m;
                row["Stage"] = strutData[i].stage;
                row["DesignStrut"] = "";
                
                dt.Rows.Add(row);
            }

            dtGrid_Strut.DataSource = dt;
            
        }

        private void btn_AutoStrut_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < appGlobal.strutData.Count; i++)
            {
                List<strutData> strutData = appGlobal.strutData;
                string designStrut = "";
                if (strutData[i].total2m < strutData[i].total2p5m)
                {
                    designStrut = "2";
                }
                else
                {
                    designStrut = "2.5";
                }

                dtGrid_Strut.Rows[i].Cells["DesignStrut"].Value = designStrut;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < appGlobal.strutData.Count; i++)
            {
                try
                {
                    string designStrut = dtGrid_Strut.Rows[i].Cells["DesignStrut"].Value.ToString();
                    appGlobal.strutData[i].designStrut = designStrut;
                }
                catch
                {
                    MessageBox.Show("GG");
                    break;
                }
                
            }
            this.Close();
        }
    }
}
