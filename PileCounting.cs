using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace Pile_Counting
{
    class PileCounting
    {
        string filePath;
        public PileCounting(string filePath)
        {
            this.filePath = filePath;
        }

        public string Process()
        {
            List<PileData> PileData = new List<PileData>();

            XSSFWorkbook wb;
            ISheet ws;

            FileStream file;
            try { file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite); }
            catch { string error = "讀取失敗，請確認檔案是否為開啟狀態"; return error; }
            
            wb = new XSSFWorkbook(file);
            ws = wb.GetSheet("基礎型式總表");

            List<string> capDistinct = new List<string>();
            List<string> diaDistinct = new List<string>();

            try
            {
                for (int i = 1; i < ws.LastRowNum + 1; i++) //將資料讀取寫入Data
                {
                    string pileStation = ws.GetRow(0).GetCell(1).ToString();
                    string ID = pileStation + int.Parse(ws.GetRow(i).GetCell(1).ToString()).ToString("D2"); //基礎編號                                        
                    double L = double.Parse(ws.GetRow(i).GetCell(10).ToString()); //樁帽軸長
                    double w = double.Parse(ws.GetRow(i).GetCell(9).ToString()); //樁帽橫寬
                    double Df = double.Parse(ws.GetRow(i).GetCell(3).ToString()); //Df
                    double d = double.Parse(ws.GetRow(i).GetCell(4).ToString()); //版厚
                    string capCon = L + "*" + w + "*" + d; //樁帽混凝土

                    string Dia = ws.GetRow(i).GetCell(5).ToString(); //樁徑
                    int pileNo = int.Parse(ws.GetRow(i).GetCell(14).ToString()); //基樁數量
                    string pileLength = ws.GetRow(i).GetCell(6).ToString(); //樁長

                    PileData.Add(new PileData()
                    {
                        ID = ID,
                        Length = L,
                        Width = w,
                        Df = Df,
                        d = d,
                        capCon = capCon,
                        Dia = Dia,
                        pileNo = pileNo,
                        pileLength = pileLength
                    });
                    capDistinct.Add(capCon);
                    diaDistinct.Add(Dia);
                }
            }
            catch { string error = "資料有誤，請確認"; return error; }


            List<outPileCap> outPileCap = new List<outPileCap>();
            capDistinct = capDistinct.Distinct().ToList(); //依據樁帽尺寸分類
            foreach (var item in capDistinct)
            {
                List<PileData> each = PileData.FindAll(x => x.capCon.Contains(item));
                List<string> eachID = new List<string>();
                for (int i = 0; i < each.Count; i++)
                {
                    eachID.Add(each[i].ID);
                }
                int no = each.Count;
                string strAirCon = each[0].Length + "*" + each[0].Width + "*0.1"; //底板混凝土PC
                string strMold = "(" + each[0].Length + "+" + each[0].Width + ")*2*" + each[0].d; //免拆模板面積

                outPileCap.Add(new outPileCap()
                {
                    size = item + "*" + no,
                    ID = eachID,
                    no = no,
                    strAirCon = strAirCon + "*" + no,
                    strMold = strMold + "*" + no
                });

            }

            List<PileData> find = PileData.FindAll(x => x.capCon.Contains("6.25*10.41*2.5"));

            List<outDiaPile> outDiaPile = new List<outDiaPile>();
            diaDistinct = diaDistinct.Distinct().ToList(); //依據基樁直徑分類
            foreach (var item in diaDistinct)
            {
                List<PileData> each = PileData.FindAll(x => x.Dia.Contains(item));
                List<string> eachID = new List<string>();

                string strPileL = "";
                double pileL = 0;
                string strPileNo = "";
                int pileNo = 0;
                string strDrillL = "";
                double drillL = 0;
                string strVolume = "";
                double volume = 0;
                string strHead = "";
                double head = 0;

                for (int i = 0; i < each.Count; i++)
                {
                    eachID.Add(each[i].ID);

                    pileNo += each[i].pileNo; //計算基樁總數量
                    strPileNo += $"{each[i].pileNo}";
                    if (i != each.Count - 1) strPileNo += "+";

                    double L = double.Parse(each[i].pileLength.ToString());

                    pileL += L * each[i].pileNo; //計算基樁總長度
                    strPileL += $"{each[i].pileNo}*{each[i].pileLength}";
                    if (i != each.Count - 1) strPileL += "+";

                    drillL += each[i].pileNo * (L + each[i].Df); //計算鑽掘長度
                    strDrillL += $"{each[i].pileNo}*({L}+{each[i].Df})";
                    if (i != each.Count - 1) strDrillL += "+";

                    double dia = double.Parse(item);

                    volume += each[i].pileNo * (L + 1); //計算體積(混凝土)
                    strVolume += $"{each[i].pileNo}*({L}+1)";
                    if (i == each.Count - 1)
                    {

                        strVolume = "(" + strVolume + $")*{dia}*{dia}*pi()/4";
                        volume *= (dia * dia * Math.PI / 4);
                    }
                    else strVolume += "+";

                    head += each[i].pileNo; //計算樁頭敲除
                    strHead += $"{each[i].pileNo}";
                    if (i == each.Count - 1)
                    {
                        head *= (1 * dia * dia * Math.PI / 4);
                        string temp = $"1*{dia}*{dia}*pi()*";
                        strHead = temp + "(" + strHead + ")";
                    }
                    else strHead += "+";
                }
                int no = each.Count;

                outDiaPile.Add(new outDiaPile()
                {
                    Dia = item,
                    ID = eachID,
                    no = no,
                    pileNo = pileNo,
                    strPileNo = strPileNo,
                    pileL = pileL,
                    strPileL = strPileL,
                    drillL = drillL,
                    strDrillL = strDrillL,
                    Volume = volume,
                    strVolume = strVolume,
                    Head = head,
                    strHead = strHead
                });
            }

            List<bool> sheetPileCheck = new List<bool>(); //確認鋼板樁使用尺寸(6, 9, 16m) 依據深度(<=3, <=5.5, <=9), >9m須依據土層設計，因此另外標示由使用者自行設計
            sheetPileCheck.Add(PileData.Exists(x => x.Df <= 3));
            sheetPileCheck.Add(PileData.Exists(x => x.Df > 3 && x.Df <= 5.5));
            sheetPileCheck.Add(PileData.Exists(x => x.Df > 5.5 && x.Df <= 9));
            sheetPileCheck.Add(PileData.Exists(x => x.Df > 9));

            List<sheetPile> sheetPile = new List<sheetPile>();
            sheetPile.Add(new sheetPile() { name = "6m鋼板樁", ID = new List<string>() });
            sheetPile.Add(new sheetPile() { name = "9m鋼板樁" , ID = new List<string>()});
            sheetPile.Add(new sheetPile() { name = "16m鋼板樁", ID = new List<string>() });
            sheetPile.Add(new sheetPile() { name = "自行設計", ID = new List<string>() });

            //List<PileData> orderSheet = PileData.OrderBy(x => x.Df).ToList();
            for (int i = 0; i < PileData.Count; i++)
            {               
                double Df = PileData[i].Df;
                double L = PileData[i].Length;
                double W = PileData[i].Width;
                double marchL = (L + W) * 2;
                string strMarchL = $"({L}+{W})*2";
                                
                int check = 3;
                if (Df <= 3) { check = 0; sheetPile[check].ID.Add(PileData[i].ID); }
                else if (Df > 3 && Df <= 5.5) { check = 1; sheetPile[check].ID.Add(PileData[i].ID); }
                else if (Df > 5.5 && Df <= 9) { check = 2; sheetPile[check].ID.Add(PileData[i].ID); }
                else { check = 3; sheetPile[check].ID.Add(PileData[i].ID); }
                                
                sheetPile[check].strMarchL += strMarchL;
                sheetPile[check].marchL += marchL;

                if (i != PileData.Count - 1) sheetPile[check].strMarchL += "+";                
            }

            List<double> excaDepth = new List<double> { 0.5, 5, 10, 15 };
            List<string> strExcaDepth = new List<string>();
            for(int i = 0; i < PileData.Count; i++)
            {
                double L = PileData[i].Length;
                double W = PileData[i].Width;
                double Df = PileData[i].Df;

                bool last = false;
                if (i == PileData.Count - 1) last = true;

                double Dep = 0;
                string strDep = "";
                if(Df > 0.5)
                {                    
                    ExcaInput(L, W, 0.5, 0, last, ref Dep, ref strDep);
                    excaDepth[0] += Dep;
                    strExcaDepth[0] += strDep;                    
                }

                if(Df >0.5 && Df <= 5)
                {
                    ExcaInput(L, W, Df, 0.5, last, ref Dep, ref strDep);
                    excaDepth[1] += Dep;
                    strExcaDepth[1] += strDep;
                }
                else if (Df > 5)
                {
                    ExcaInput(L, W, 5, 0.5, last, ref Dep, ref strDep);
                    excaDepth[1] += Dep;
                    strExcaDepth[1] += strDep;
                }

                if (PileData[i].Df > 0.5 && PileData[i].Df <= 5)
                {
                    excaDepth[1] += (L + W) * 0.5;
                    strExcaDepth[1] += $"({L}+{W})*0.5";
                }
                else if (PileData[i].Df > 5)
                {

                }

                
            }


            string done = "計算完成";
            return done;
        }

        void ExcaInput(double L, double W, double DN, double UP, bool last, ref double excaDepth, ref string strExcaDepth)
        {
            excaDepth += (L + W) * (DN - UP);
            strExcaDepth += $"({L}+{W})*{DN - UP}";
            if (!last) strExcaDepth += "+";
        }
    }
}
