using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pile_Counting
{
    public class PileData
    {
        public string ID;
        public double Length;
        public double Width;
        public double Df;
        public double d;
        public string capCon;

        public string Dia;
        public int pileNo;
        public int pileXno;
        public int pileYno;
        public string pileLength;

        public double pierX;
        public double pierY;
                
    }

    class outPileCap
    {
        public string size;
        public List<string> ID;
        public int no;
        public double Volume;
        public string strVolume;
        public double PCPlateV;
        public string strPCPlateV;
        public double moldArea;
        public string strMoldArea;
    }

    class outDiaPile
    {
        public string name;
        public string Dia;
        public List<string> ID;
        public int no;
        public string strPileL;
        public double pileL;
        public string strPileNo;
        public int pileNo;                
        public string strDrillL;
        public double drillL;
        public string strVolume;
        public double Volume;
        public string strHead;
        public double Head;
    }

    class sheetPile
    {
        public string name;
        public List<string> ID;
        public string strMarchL;
        public double marchL;
    }

    class ExcaVolume
    {
        public string name;
        public List<string> ID;
        public string strVolume;
        public double volume;
    }

    public class strutData
    {
        public string ID;

        public string strBracing2m;
        public string strStrut2m;
        public double total2m;

        public string strBracing2p5m;
        public string strStrut2p5m;
        public double total2p5m;

        public int stage;
        public string designStrut;
    }
}
