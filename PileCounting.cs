﻿using System;
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

        
        List<PileData> PileData = new List<PileData>();

        List<outPileCap> outPileCap = new List<outPileCap>();
        List<outDiaPile> outDiaPile = new List<outDiaPile>();
        List<sheetPile> sheetPile = new List<sheetPile>();
        List<ExcaVolume> ExcaVolume = new List<ExcaVolume>();
                
        public void Process(string function)
        {
            appGlobal.state = "數量計算尚未完成";
            XSSFWorkbook wb;
            ISheet ws;
            ISheet writeWS;
            ISheet strutWS;

            FileStream file;
            try { file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite); }
            catch { appGlobal.state = "讀取失敗，請確認檔案是否為開啟狀態"; return; }

            try { wb = new XSSFWorkbook(file); }
            catch { appGlobal.state = "讀取失敗，請確認檔案是否為xlsx檔"; return; }

            ws = wb.GetSheet("基礎型式總表");
            writeWS = wb.GetSheet("數量計算");
            strutWS = wb.GetSheet("支撐計算");

            if(ws == null || writeWS == null || strutWS == null)
            {
                appGlobal.state = "讀取失敗，請確認工作表名稱或選擇檔案是否正確";
                return;
            }
                                    

            List<string> capDistinct = new List<string>();
            List<string> diaDistinct = new List<string>();

            #region 讀取資料
            //appGlobal.pileData.Clear(); //在form1程式完成後統一清除
            int lastRow;
            try
            {
                lastRow = ws.LastRowNum;
                for (int i = 1; i < ws.LastRowNum; i++)
                {
                    if (ws.GetRow(i + 1).GetCell(2).ToString() == "") { lastRow = i; break; }
                }
                lastRow++;
                
                for (int i = 1; i < lastRow; i++) //將資料讀取寫入Data
                {
                    //string pileStation = ws.GetRow(0).GetCell(2).ToString();
                    //string ID = pileStation + int.Parse(ws.GetRow(i).GetCell(2).ToString()).ToString("D2"); //基礎編號                                        
                    string ID = ws.GetRow(i).GetCell(2).ToString();
                    double L = double.Parse(ws.GetRow(i).GetCell(12).ToString()); //樁帽軸長
                    double w = double.Parse(ws.GetRow(i).GetCell(11).ToString()); //樁帽橫寬
                    double Df = double.Parse(ws.GetRow(i).GetCell(5).ToString()); //Df
                    double d = double.Parse(ws.GetRow(i).GetCell(6).ToString()); //版厚
                    string capCon = L + "*" + w + "*" + d; //樁帽混凝土

                    string Dia = ws.GetRow(i).GetCell(7).ToString(); //樁徑

                    int pileXno = int.Parse(ws.GetRow(i).GetCell(3).ToString());
                    int pileYno = int.Parse(ws.GetRow(i).GetCell(4).ToString());
                    int pileNo = pileXno * pileYno; //基樁數量
                    string pileLength = ws.GetRow(i).GetCell(8).ToString(); //樁長

                    double PierX = double.Parse(ws.GetRow(i).GetCell(0).ToString());
                    double PierY = double.Parse(ws.GetRow(i).GetCell(1).ToString());

                    //PileData.Add(new PileData()                    
                    appGlobal.pileData.Add(new PileData()
                    {
                        ID = ID,
                        Length = L,
                        Width = w,
                        Df = Df,
                        d = d,
                        capCon = capCon,
                        Dia = Dia,
                        pileNo = pileNo,
                        pileXno = pileXno,
                        pileYno = pileYno,
                        pileLength = pileLength,
                        pierX = PierX,
                        pierY = PierY,
                    });
                    capDistinct.Add(capCon);
                    diaDistinct.Add(Dia);
                }
            }
            catch { appGlobal.state = "讀取失敗，請確認檔案資料是否有誤"; return; }
            PileData = appGlobal.pileData;

            appGlobal.IsWriteStrut = true;
            switch (function.ToUpper()) //讀取斜撐資料
            {
                case ("COUNTING"):
                    try
                    {
                        for (int i = 0; i < PileData.Count; i++)
                        {
                            string ID = PileData[i].ID;
                            string strBracing2m = strutWS.GetRow(i + 1).GetCell(10).ToString();
                            string strStrut2m = strutWS.GetRow(i + 1).GetCell(11).ToString();
                            double total2m = double.Parse(strutWS.GetRow(i + 1).GetCell(12).ToString());

                            string strBracing2p5m = strutWS.GetRow(i + 1).GetCell(13).ToString();
                            string strStrut2p5m = strutWS.GetRow(i + 1).GetCell(14).ToString();
                            double total2p5m = double.Parse(strutWS.GetRow(i + 1).GetCell(15).ToString());

                            int stage = int.Parse(strutWS.GetRow(i + 1).GetCell(16).ToString());
                            string designStrut = strutWS.GetRow(i + 1).GetCell(17).ToString();

                            if (designStrut == "") //無選擇斜撐
                            {
                                appGlobal.IsWriteStrut = false;
                                break;
                            }

                            appGlobal.strutData.Add(new strutData()
                            {
                                ID = ID,
                                strBracing2m = strBracing2m,
                                strStrut2m = strStrut2m,
                                total2m = total2m,

                                strBracing2p5m = strBracing2p5m,
                                strStrut2p5m = strStrut2p5m,
                                total2p5m = total2p5m,

                                stage = stage,
                                designStrut = designStrut
                            });
                        }
                    }
                    catch
                    {
                        appGlobal.IsWriteStrut = false; //無點選斜撐計算
                    }
                    
                    
                    break;
            }
            #endregion

            //string done = "";
            switch(function.ToUpper())
            {
                case ("COUNTING"):
                    capDistinct = capDistinct.Distinct().ToList(); //依據樁帽尺寸分類
                    CapCount(ref outPileCap, capDistinct);

                    //List<PileData> find = PileData.FindAll(x => x.capCon.Contains("6.25*10.41*2.5"));

                    diaDistinct = diaDistinct.Distinct().ToList(); //依據基樁直徑分類
                    DiaCount(ref outDiaPile, diaDistinct);

                    List<bool> sheetPileCheck = new List<bool>(); //確認鋼板樁使用尺寸(6, 9, 16m) 依據深度(<=3, <=5.5, <=9), >9m須依據土層設計，因此另外標示由使用者自行設計
                    sheetPileCheck.Add(PileData.Exists(x => x.Df <= 3));
                    sheetPileCheck.Add(PileData.Exists(x => x.Df > 3 && x.Df <= 5.5));
                    sheetPileCheck.Add(PileData.Exists(x => x.Df > 5.5 && x.Df <= 9));
                    sheetPileCheck.Add(PileData.Exists(x => x.Df > 9));

                    SheetPileCount(ref sheetPile); //鋼板樁行進米計算

                    ExcaVolumeCount(ref ExcaVolume); //構造物開挖體積計算

                    WriteToSheet(writeWS); //寫入數量計算書                  

                    if (appGlobal.IsWriteStrut) { appGlobal.state = "數量計算寫入完成"; }
                    else { appGlobal.state = "斜撐資料有誤，數量計算將不會計算\n9.斜撐、直撐、橫擋\n10.中間柱"; }
                    
                    //done = "數量計算寫入完成";
                    break;

                case ("STRUT"):

                    strutCal(strutWS);
                    
                    fStrutDesign f = new fStrutDesign();
                    f.ShowDialog();

                    if (appGlobal.IsWriteStrut) { ExcelStrut(strutWS, lastRow); }                    
                    
                    //done = "支撐計算與寫入完成";
                    break;
            }

            //FileStream fileSave = new FileStream(filePath, FileMode.Create);
            //wb.Write(fileSave);
            //fileSave.Close();
            FileSave(wb, filePath);



            //return done;
        }

        void FileSave(IWorkbook wb,string filePath)
        {
            FileStream fileSave = new FileStream(filePath, FileMode.Create);
            wb.Write(fileSave);
            fileSave.Close();
        }

        #region 樁帽混凝土體積、樁帽底板PC體積、模板面積計算
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outPileCap"></param> 輸出樁帽尺寸計算
        /// <param name="capDistinct"></param> 判斷樁帽尺寸種類
        void CapCount(ref List<outPileCap> outPileCap, List<string> capDistinct)
        {            
            foreach (var item in capDistinct)
            {
                List<PileData> each = PileData.FindAll(x => x.capCon.Equals(item));

                //List<PileData> each = PileData.FindAll(x => x.capCon.Contains(item));
                List<string> eachID = new List<string>();
                for (int i = 0; i < each.Count; i++)
                {
                    eachID.Add(each[i].ID);
                }
                int no = each.Count;

                double Volume = each[0].Length * each[0].Width * each[0].d * no;
                string strVolume = item + "*" + no;

                double PCPlateV = each[0].Length * each[0].Width * 0.1 * no;
                string strPCPlateV = each[0].Length + "*" + each[0].Width + "*0.1*" + no; //底板混凝土PC
                                
                double moldArea = (each[0].Length + each[0].Width) * 2 * each[0].d * no;
                string strMoldArea = "(" + each[0].Length + "+" + each[0].Width + ")*2*" + each[0].d + "*" + no; //免拆模板面積

                outPileCap.Add(new outPileCap()
                {
                    size = item,
                    ID = eachID,
                    no = no,
                    Volume = Volume,
                    strVolume = strVolume,
                    PCPlateV = PCPlateV,
                    strPCPlateV = strPCPlateV,
                    moldArea = moldArea,
                    strMoldArea = strMoldArea

                });
            }
        }
        #endregion

        #region 基樁尺寸、混凝土體積、長度、支數等計算
        void DiaCount(ref List<outDiaPile> outDiaPile, List<string> diaDistinct)
        {
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
                string strTestPVC = "";
                double testPVC = 0;
                string strBackFill = "";
                double backFill = 0;

                double dia = double.Parse(item);
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
                        string temp = $"1*{dia}*{dia}*pi()/4*";
                        strHead = temp + "(" + strHead + ")";
                    }
                    else strHead += "+";                                        
                }
                int no = each.Count;

                int pvcDia = 0;
                if (item == "1.5") pvcDia = 4;
                else if (item == "2") pvcDia = 6;
                else pvcDia = 4;

                testPVC = drillL * pvcDia + pileNo * pvcDia * 0.2; //計算完整性試驗PVC管
                strTestPVC = $"{drillL}*{pvcDia}+{pileNo}*{pvcDia}*0.2";

                backFill = (drillL - pileL - pileNo) * dia * dia * Math.PI * 0.25; //劣質混凝土直接取基樁支數，因為長度為1m
                strBackFill = $"({drillL}-{pileL}-{pileNo})*{dia}*{dia}*pi()/4";

                outDiaPile.Add(new outDiaPile()
                {
                    name = $"全套管基樁{item}m樁徑",
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
                    strHead = strHead,
                    testPVC = testPVC,
                    pvcDia = pvcDia,
                    strTestPVC = strTestPVC,
                    backFill = backFill,
                    strBackFill = strBackFill,
                });
            }
        }
        #endregion

        #region 鋼板樁行進米計算
        void SheetPileCount(ref List<sheetPile> sheetPile)
        {
            sheetPile.Add(new sheetPile() { name = "6m鋼板樁", ID = new List<string>() });
            sheetPile.Add(new sheetPile() { name = "9m鋼板樁", ID = new List<string>() });
            sheetPile.Add(new sheetPile() { name = "13m鋼板樁", ID = new List<string>() });
            sheetPile.Add(new sheetPile() { name = "16m鋼板樁", ID = new List<string>() });
            sheetPile.Add(new sheetPile() { name = "自行設計", ID = new List<string>() });

            List<bool> addCheck = new List<bool> { false, false, false, false, false };
            //List<PileData> orderSheet = PileData.OrderBy(x => x.Df).ToList();
            for (int i = 0; i < PileData.Count; i++)
            {
                double Df = PileData[i].Df;
                double L = PileData[i].Length;
                double W = PileData[i].Width;
                double marchL = (L + W) * 2;
                string strMarchL = $"({L}+{W})*2";

                bool addPlus;

                int check = 4;
                if (Df <= 3) { check = 0; sheetPile[check].ID.Add(PileData[i].ID);
                    addPlus = addCheck[0]; addCheck[0] = true; }
                else if (Df > 3 && Df <= 5.5) { check = 1; sheetPile[check].ID.Add(PileData[i].ID);
                    addPlus = addCheck[1]; addCheck[1] = true; }
                else if (Df > 5.5 && Df <= 9) { check = 2; sheetPile[check].ID.Add(PileData[i].ID);
                    addPlus = addCheck[2]; addCheck[2] = true; }
                else { check = 4; sheetPile[check].ID.Add(PileData[i].ID);
                    addPlus = addCheck[4]; addCheck[4] = true; }

                if (addPlus) sheetPile[check].strMarchL += "+";

                sheetPile[check].strMarchL += strMarchL;
                sheetPile[check].marchL += marchL;

                
            }
        }
        #endregion

        #region 構造物開挖體積計算
        void ExcaVolumeCount(ref List<ExcaVolume> ExcaVolume)
        {
            ExcaVolume.Add(new ExcaVolume() { name = "構造物開挖 深度<0.5m", ID = new List<string>() });
            ExcaVolume.Add(new ExcaVolume() { name = "構造物開挖 深度0.5~5m", ID = new List<string>() });
            ExcaVolume.Add(new ExcaVolume() { name = "構造物開挖 深度5~10m", ID = new List<string>() });
            ExcaVolume.Add(new ExcaVolume() { name = "構造物開挖 深度10~15m", ID = new List<string>() });
            ExcaVolume.Add(new ExcaVolume() { name = "構造物開挖 深度15~20m", ID = new List<string>() });
            ExcaVolume.Add(new ExcaVolume() { name = "構造物開挖 深度>20m", ID = new List<string>() });

            List<string> ID05 = new List<string>();
            List<string> ID05_5 = new List<string>();
            List<string> ID5_10 = new List<string>();
            List<string> ID10_15 = new List<string>();
            List<string> ID15_20 = new List<string>();
            List<string> ID20 = new List<string>();

            List<bool> check = new List<bool> { false, false, false, false, false, false };
            for (int i = 0; i < PileData.Count; i++)
            {
                double L = PileData[i].Length;
                double W = PileData[i].Width;
                double Df = PileData[i].Df;
                                                
                bool addPlus;
                //if (i == 0 ) { for (int j = 0; j < check.Count; j++) check[j] = false; }
                                
                if (Df > 0.5)
                {
                    addPlus = check[0];
                    ExcaVolumeInput(L, W, 0.5, 0, addPlus, ref ExcaVolume[0].volume, ref ExcaVolume[0].strVolume);
                    ID05.Add(PileData[i].ID);
                    check[0] = true;
                }

                
                if (Df > 0.5 && Df <= 5)
                {
                    addPlus = check[1];
                    ExcaVolumeInput(L, W, Df, 0.5, addPlus, ref ExcaVolume[1].volume, ref ExcaVolume[1].strVolume);
                    ID05_5.Add(PileData[i].ID);
                    check[1] = true;
                }
                else if (Df > 5)
                {
                    addPlus = check[1];
                    ExcaVolumeInput(L, W, 5, 0.5, addPlus, ref ExcaVolume[1].volume, ref ExcaVolume[1].strVolume);
                    ID05_5.Add(PileData[i].ID);
                    check[1] = true;
           
                }

                
                if (Df > 5 && Df <= 10)
                {
                    addPlus = check[2];
                    ExcaVolumeInput(L, W, Df, 5, addPlus, ref ExcaVolume[2].volume, ref ExcaVolume[2].strVolume);
                    ID5_10.Add(PileData[i].ID);
                    check[2] = true;
                }
                else if (Df > 10)
                {
                    addPlus = check[2];
                    ExcaVolumeInput(L, W, 10, 5, addPlus, ref ExcaVolume[2].volume, ref ExcaVolume[2].strVolume);
                    ID5_10.Add(PileData[i].ID);
                    check[2] = true;
                }

                
                if (Df > 10 && Df <= 15)
                {
                    addPlus = check[3];
                    ExcaVolumeInput(L, W, Df, 10, addPlus, ref ExcaVolume[3].volume, ref ExcaVolume[3].strVolume);
                    ID10_15.Add(PileData[i].ID);
                    check[3] = true;
                }
                else if (Df > 15)
                {
                    addPlus = check[3];
                    ExcaVolumeInput(L, W, 15, 10, addPlus, ref ExcaVolume[3].volume, ref ExcaVolume[3].strVolume);
                    ID10_15.Add(PileData[i].ID);
                    check[3] = true;
                }

                
                if (Df > 15 && Df <= 20)
                {
                    addPlus = check[4];
                    ExcaVolumeInput(L, W, Df, 15, addPlus, ref ExcaVolume[4].volume, ref ExcaVolume[4].strVolume);
                    ID15_20.Add(PileData[i].ID);
                    check[4] = true;
                }
                else if (Df > 20)
                {
                    addPlus = check[4];
                    ExcaVolumeInput(L, W, Df, 15, addPlus, ref ExcaVolume[4].volume, ref ExcaVolume[4].strVolume);
                    ID15_20.Add(PileData[i].ID);
                    check[4] = true;

                    addPlus = check[5];
                    ExcaVolumeInput(L, W, Df, 20, addPlus, ref ExcaVolume[5].volume, ref ExcaVolume[5].strVolume);
                    ID20.Add(PileData[i].ID);
                    check[5] = true;
                }

            }
            ExcaVolume[0].ID = ID05;
            ExcaVolume[1].ID = ID05_5;
            ExcaVolume[2].ID = ID5_10;
            ExcaVolume[3].ID = ID10_15;
            ExcaVolume[4].ID = ID15_20;
            ExcaVolume[5].ID = ID20;
        }

        void ExcaVolumeInput(double L, double W, double DN, double UP, bool addPlus, ref double excaVolume, ref string strExcaVolume)
        {
            if (addPlus) strExcaVolume += "+";
            excaVolume += (L * W) * (DN - UP);
            strExcaVolume += $"({L}*{W})*{DN - UP}";
        }
        #endregion

        #region 寫入計算書
        void WriteToSheet(ISheet writeWS)
        {
            string clearExcel = ",,,,,,";
            for (int i = 0; i < 300; i++) SetRowData(writeWS, 6 + i, clearExcel);

            List<string> capCon = new List<string>();
            List<string> PCCon = new List<string>();
            List<string> mold = new List<string>();
            capCon.Add($"樁基礎");
            capCon.Add("");
            PCCon.Add("");
            mold.Add("");

            double totalcapConV = 0;
            double totalPCConV = 0;
            double totalmoldA = 0;

            for (int i = 0; i < outPileCap.Count; i++)
            {
                string ID = "";
                double V = Math.Round(outPileCap[i].Volume, 2);
                totalcapConV += V;
                for (int j = 0; j < outPileCap[i].ID.Count; j++) { ID += $"{outPileCap[i].ID[j]}、"; }
                capCon.Add($",,,{V},{outPileCap[i].strVolume},{outPileCap[i].size}樁帽,{ID}");

                V = Math.Round(outPileCap[i].PCPlateV, 2);
                totalPCConV += V;
                PCCon.Add($",,,{V},{outPileCap[i].strPCPlateV},{outPileCap[i].size}樁帽,{ID}");

                double A = Math.Round(outPileCap[i].moldArea, 2);
                totalmoldA += A;
                mold.Add($",,,{A},{outPileCap[i].strMoldArea},{outPileCap[i].size}樁帽,{ID}");

            }

            capCon[1] = $"1,fc' = 280kg/cm2 TYPE II 混凝土,m3,{Math.Round(totalcapConV,0)}";
            PCCon[0] = $"2,f'c=140kg/cm2 TYPE I 混凝土,m3,{Math.Round(totalPCConV,0)}";
            mold[0] = $"4,免拆模板,m2,{Math.Round(totalmoldA,0)}";

            int row = 6;
            for (int i = 0; i < capCon.Count; i++) { SetRowData(writeWS, row, capCon[i]); row++; }
            for (int i = 0; i < PCCon.Count; i++) { SetRowData(writeWS, row, PCCon[i]); row++; }
            SetRowData(writeWS, row, ""); row++;
            SetRowData(writeWS, row, "3,鋼筋SD420W,T"); row++;
            SetRowData(writeWS, row, ",樁帽鋼筋,T,,,各墩總和"); row++;
            SetRowData(writeWS, row, ",搭接筋,T,"); row++;
            SetRowData(writeWS, row, ",工作筋,T,,,樁帽混凝土*5.2kg/m3"); row++;
            SetRowData(writeWS, row, ""); row++;
            for (int i = 0; i < mold.Count; i++) { SetRowData(writeWS, row, mold[i]); row++; }
                        
            for (int i = 0; i < outDiaPile.Count; i++)
            {
                string dia = outDiaPile[i].Dia;
                string name = outDiaPile[i].name;
                double L = Math.Round(outDiaPile[i].pileL, 0);
                string strL = outDiaPile[i].strPileL;
                int no = outDiaPile[i].pileNo;
                string strNo = outDiaPile[i].strPileNo;
                double drillL = Math.Round(outDiaPile[i].drillL, 0);
                string strDrillL = outDiaPile[i].strDrillL;
                double V = Math.Round(outDiaPile[i].Volume, 0);
                string strV = outDiaPile[i].strVolume;
                double head = Math.Round(outDiaPile[i].Head, 0);
                string strHead = outDiaPile[i].strHead;
                double testPVC = Math.Round(outDiaPile[i].testPVC, 0);
                double pvcDia = outDiaPile[i].pvcDia;
                string strTestPVC = outDiaPile[i].strTestPVC;
                double backfill = Math.Round(outDiaPile[i].backFill, 0);
                string strBackFill = outDiaPile[i].strBackFill;

                string ID = "";
                for (int j = 0; j < outDiaPile[i].ID.Count; j++) { ID += $"{outDiaPile[i].ID[j]}、"; }
                List<string> pileSheet = new List<string>();
                pileSheet.Add($"5.{i+1},{name},m,{L},{strL},Σ各墩樁數*各墩樁長,{ID}");
                pileSheet.Add($"(1),基樁支數,支,{no},{strNo},Σ各墩墩數");
                pileSheet.Add($"(2),鑽掘長度,m,{drillL},{strDrillL},含空打段，土層、岩層各約佔50%");
                pileSheet.Add($"(3),f'c=280kg/cm2 TYPE II 混凝土,m3,{V},{strV}");
                pileSheet.Add($"(4),鋼筋SD420W,T,,,樁徑{dia}m全套管樁(樁基礎_---鋼筋，SD420W(含搭接，不含耗損)");
                pileSheet.Add($"(5),基樁樁頭敲除,m3,{head},{strHead},每支樁敲除體積(1m)*Σ(各墩樁數*墩數)");
                pileSheet.Add($"(6),完整性試驗PVC管,m,{testPVC},{strTestPVC},鑽掘長度*{pvcDia}+基樁支數*{pvcDia}*0.2");
                pileSheet.Add($"(7),空打回填,m3,{backfill},{strBackFill},(鑽掘長度-總樁長-劣質混凝土)*基樁面積");
                SetRowData(writeWS, row, ""); row++;
                for (int j = 0; j < pileSheet.Count; j++) { SetRowData(writeWS, row, pileSheet[j]); row++; }
            }


            //List<string> excelSheetPile = new List<string>();
            SetRowData(writeWS, row, ""); row++;
            SetRowData(writeWS, row, $"6,鋼板樁");
            row++;
            for (int i = 0; i < sheetPile.Count; i++)
            {
                string ID = "";
                for (int j = 0; j < sheetPile[i].ID.Count; j++) { ID += $"{sheetPile[i].ID[j]}、"; }
                string name = sheetPile[i].name;
                double marchL = sheetPile[i].marchL;
                string strMarchL = sheetPile[i].strMarchL;

                SetRowData(writeWS,row,$"({i + 1}),{name},m,{Math.Round(marchL,0)},{strMarchL},行進米,{ID}");
                row++;
                //excelSheetPile.Add($"({i + 1}),{name},m,{marchL},{strMarchL},行進米,{ID}");
            }            


            List<string> strut = new List<string>();
            SetRowData(writeWS, row, ""); row++;
            string strbracing = "";
            double bracingTotal = 0;
            List<strutData> strutData = appGlobal.strutData;
            string strWales = ""; //圍令
            double walesTotal = 0;
            string strKingPost = ""; //中間柱
            double kingPostTotal = 0;
            if (appGlobal.IsWriteStrut)
            {
                strbracing += "0.1365*(";
                strWales += "0.1365*(";
                strKingPost += "0.1365*(";
                bool king1st = true;
                for(int i = 0; i < PileData.Count; i++)
                {
                    double Df = PileData[i].Df;
                    if(strutData[i].designStrut == "2")
                    {
                        strbracing += $"({strutData[i].strBracing2m})*{strutData[i].stage}";
                        if(strutData[i].strStrut2m != "")
                        {
                            strbracing += $"+{strutData[i].strStrut2m}*{strutData[i].stage}";
                            if (!king1st) { strKingPost += "+"; }
                            else { king1st = false; }
                            strKingPost += $"{Df+2.5}";
                            kingPostTotal += Df + 2.5;
                            
                        }
                        bracingTotal += strutData[i].total2m;
                    }
                    else if(strutData[i].designStrut == "2.5")
                    {
                        strbracing += $"({strutData[i].strBracing2p5m})*{strutData[i].stage}";
                        if (strutData[i].strStrut2p5m != "")
                        {
                            strbracing += $"+{strutData[i].strStrut2p5m}*{strutData[i].stage}";
                            if (!king1st) strKingPost += "+";
                            else { king1st = false; }
                            strKingPost += $"{Df + 2.5}";
                            kingPostTotal += Df + 2.5;
                        }
                        bracingTotal += strutData[i].total2p5m;
                    }

                    double L = PileData[i].Length;
                    double W = PileData[i].Width;
                    strWales += $"({L}+{W})*2*{strutData[i].stage}";
                    walesTotal += (L + W) * 2 * strutData[i].stage;

                    if(i != PileData.Count - 1) { strbracing += "+"; strWales += "+"; }
                }
                bracingTotal *= 0.1365;
                walesTotal *= 0.1365;
                kingPostTotal *= 0.1365;
                strbracing += ")";
                strWales += ")";
                strKingPost += ")";
            }
            strut.Add($"7,斜撐、直撐,T,{Math.Round(bracingTotal,0)},{strbracing},H350型鋼－斜撐長度*單層支數*階數*墩數+直撐");
            strut.Add($",橫擋,T,{Math.Round(walesTotal,0)},{strWales},H350型鋼－樁帽周長*階數*墩數");
            strut.Add($"8,中間柱,T,{Math.Round(kingPostTotal,0)},{strKingPost},,有直撐之墩，其Df+2.5m");
            for(int i = 0; i < strut.Count; i++) { SetRowData(writeWS, row, strut[i]); row++; }

            List<string> excelExcaV = new List<string>();
            SetRowData(writeWS, row, ""); row++;
            SetRowData(writeWS, row, $"9,構造物開挖");
            row++;
            for(int i = 0; i < ExcaVolume.Count; i++)
            {
                string name = ExcaVolume[i].name;
                string ID = "";
                for (int j = 0; j < ExcaVolume[i].ID.Count; j++) { ID += $"{ExcaVolume[i].ID[j]}、"; }
                double V = Math.Round(ExcaVolume[i].volume, 0);
                string strV = ExcaVolume[i].strVolume;

                SetRowData(writeWS, row, $"({i+1}),{name},m3,{V},{strV},,{ID}");
                row++;
            }

            double backFillV = 0;
            string strBackFillV = "";
            backFillV -= (totalcapConV + totalPCConV);
            for(int i = 0; i < ExcaVolume.Count; i++)
            {
                backFillV += Math.Round(ExcaVolume[i].volume,0);
                strBackFillV += $"{Math.Round(ExcaVolume[i].volume,0)}";
                if (i != ExcaVolume.Count - 1) strBackFillV += "+";
            }
            strBackFillV += $"-{totalcapConV}-{totalPCConV}";

            for(int i = 0; i < PileData.Count; i++)
            {
                double Df_b = PileData[i].Df - PileData[i].d;
                double L = PileData[i].pierX;
                double W = PileData[i].pierY;

                backFillV -= L * W * Df_b;
                strBackFillV += $"-{L}*{W}*{Df_b}";
            }

            SetRowData(writeWS, row, $"10,構造物回填，第I類材料,m3,{Math.Round(backFillV,0)},{strBackFillV}," +
                $"構造物開挖-樁帽280混凝土-樁帽打底140混凝土-回填深度之柱體積");
            row++;

            double surplusV = 0;
            string strSurplusV = "";
            for(int i = 0; i < ExcaVolume.Count; i ++)
            {
                surplusV += Math.Round(ExcaVolume[i].volume,0);
                strSurplusV += $"{Math.Round(ExcaVolume[i].volume,0)}";
                if (i != ExcaVolume.Count - 1) strSurplusV += "+";
            }
            surplusV -= Math.Round(backFillV,0);
            strSurplusV += $"-{Math.Round(backFillV,0)}";

            SetRowData(writeWS, row, $"11,餘方,m3,{surplusV},{strSurplusV},挖方-填方");
            row++;

        }        
        #endregion

        #region 斜撐計算
        void strutCal(ISheet strutWs)
        {
            //appGlobal.strutData.Clear(); //在form1統一在程式完成後清除
            for (int i = 0; i < PileData.Count; i++)
            {
                for(int j = 10; j < 18 + 1; j++) { SetValue(strutWs, i + 1, j, ""); } //先確認斜撐計算的項目為空的

                string ID = PileData[i].ID;
                double L = PileData[i].Length;
                double W = PileData[i].Width;
                double Df = PileData[i].Df;

                int stage = 1;
                if (Df > 5) stage = 2;

                List<double> spacing = new List<double> { 2, 2.5 };

                string strBracing2m = "";
                string strStrut2m = "";
                double total2m = 0;

                string strBracing2p5m = "";
                string strStrut2p5m = "";
                double total2p5m = 0;

                //長、寬兩側扣掉6m的距離(6m為設置直撐的最小距離
                double Lside = (L - 6) / 2;
                double Wside = (W - 6) / 2;

                for(int k = 0; k < spacing.Count; k++)
                {
                    //分別計算以2m與2.5m為間距的斜撐在長及寬側所需的支數
                    int LNo = (int)(Lside / spacing[k]) + 1;
                    int WNo = (int)(Wside / spacing[k]) + 1;

                    //先取支數較多的計算可行與否
                    bool check = true;
                    int braNo = Math.Max(LNo, WNo);
                    //double ch = L - braNo * spacing[k] * 2;
                    if ((L - braNo * spacing[k] * 2) < 0) check = false;
                    if ((W - braNo * spacing[k] * 2) < 0) check = false;

                    string strBracing = "";
                    string strStrut = "";
                    double total = 0;

                    if (!check) braNo = Math.Min(LNo, WNo);

                    for (int j = 1; j < braNo + 1; j++)
                    {
                        if (j != 1) strBracing += "+";
                        strBracing += $"{j * spacing[k]}*2^(1/2)*4";
                        total += j * spacing[k] * Math.Sqrt(2) * 4;
                    }

                    if (!check)
                    {
                        int strut = (int)((Math.Max(L, W) - 2 * braNo * 2) / 6);
                        strStrut += $"{strut}*{Math.Min(L, W)}";
                        total += strut * Math.Min(L, W);
                    }

                    total *= stage;

                    if(k == 0)
                    {
                        strBracing2m = strBracing;
                        strStrut2m = strStrut;
                        total2m = total;
                    }
                    else if (k == 1)
                    {
                        strBracing2p5m = strBracing;
                        strStrut2p5m = strStrut;
                        total2p5m = total;
                    }

                    
                }
                                
                appGlobal.strutData.Add(new strutData()
                {
                    ID = ID,
                    strBracing2m = strBracing2m,
                    strStrut2m = strStrut2m,
                    total2m = total2m,

                    strBracing2p5m = strBracing2p5m,
                    strStrut2p5m = strStrut2p5m,
                    total2p5m = total2p5m,
                    stage = stage
                });
                                
            }           
            
        }
        #endregion

        #region 支撐寫入Excel
        void ExcelStrut(ISheet strutWS, int lastRow)
        {
            List<strutData> strutData = appGlobal.strutData;

            for(int i = 0; i < strutData.Count; i++)
            {
                //string strInput = "";

                List<string> setData = new List<string>();
                setData.Add(strutData[i].strBracing2m);
                setData.Add(strutData[i].strStrut2m);
                setData.Add(Math.Round(strutData[i].total2m,2).ToString());

                setData.Add(strutData[i].strBracing2p5m);
                setData.Add(strutData[i].strStrut2p5m);
                setData.Add(Math.Round(strutData[i].total2p5m,2).ToString());

                setData.Add(strutData[i].stage.ToString());
                setData.Add(appGlobal.strutData[i].designStrut);
                                
                //strInput += $"{strutData[i].strBracing2m},{strutData[i].strStrut2m},{strutData[i].total2m}," +
                //    $"{strutData[i].strBracing2p5m},{strutData[i].strStrut2p5m},{strutData[i].total2p5m}," +
                //    $"{strutData[i].stage}";

                int k = 0;
                for(int j = 10; j < 17 + 1; j++)
                {
                    //SetValue(strutWS, i + 1, j, "");
                    SetValue(strutWS, i + 1, j, setData[k]);
                    k++;
                }
                
                
            }
            appGlobal.state = "支撐計算與寫入Excel完成";

        }
        #endregion

 

        #region Excel寫入
        /// <summary>
        /// 資料置入EXCEL CELL
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rid">ROW</param>
        /// <param name="data"></param>
        void SetRowData(ISheet ws, int rid, string data)
        {
            string[] values = data.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                SetValue(ws, rid, i, values[i], false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rid">ROW</param>
        /// <param name="cid">COLUMN 欄位</param>
        /// <param name="value"></param>
        /// <param name="SetText"></param>
        void SetValue(ISheet ws, int rid, int cid, string value, bool SetText = false)
        {
            IRow row = ws.GetRow(rid);
            if (row == null)
                row = ws.CreateRow(rid);
            ICell cell = row.GetCell(cid);
            if (cell == null)
                cell = row.CreateCell(cid);

            if (SetText)
            {
                //強制文字輸出
                cell.SetCellType(CellType.String);
                cell.SetCellValue(value);
            }
            else
            {
                double output;
                if (double.TryParse(value, out output))
                    cell.SetCellValue(output);
                else
                {
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue(value);
                }
            }
        }
        #endregion
    }
}
