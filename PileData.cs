using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pile_Counting
{
    class PileData
    {
        public string ID;
        public double Length;
        public double Width;
        public double Df;
        public double d;
        public string capCon;

        public string Dia;
        public int pileNo;
        public string pileLength;

        public string 
    }

    class outPileCap
    {
        public string size;
        public List<string> ID;
        public int no;
        public string strAirCon;
        public string strMold;
    }

    class outDiaPile
    {
        public string Dia;
        public List<string> ID;
        public int no;
        public int pileNo;
        public string strPileNo;
        public string strPileL;
        public double pileL;
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
}
