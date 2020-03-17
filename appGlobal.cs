using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pile_Counting
{
    public class appGlobal
    {
        public static string IdentityUser = "";
        public static string UserID = "";
        public static string UserName = "";
        public static string UserEmail = "";
        public static bool IsMembership = false;

        public static List<PileData> pileData = new List<PileData>();
        public static List<strutData> strutData = new List<strutData>();

        public static string state = "計算尚未完成";
    }
}
