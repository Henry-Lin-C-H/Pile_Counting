using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Pile_Counting
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExecuteSQL oExecuteSQL = new ExecuteSQL();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            

            appGlobal.IdentityUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            appGlobal.UserID = appGlobal.IdentityUser.Replace(@"SECLTD\", "");

            DataTable dtable = oExecuteSQL.GetDataBySQL($"SELECT name.UserName,program.PileCalculation,name.UserEmail" +
                $" FROM MTEDept_UserInfo name LEFT OUTER JOIN MTEDept_UserPRogram program" +
                $" ON program.UserName=name.UserName WHERE (name.UserID={appGlobal.UserID})");

            string ID = appGlobal.UserID;

            appGlobal.UserName = dtable.Rows[0]["UserName"].ToString();
            appGlobal.UserEmail = dtable.Rows[0]["UserEmail"].ToString();
            appGlobal.IsMembership = Convert.ToBoolean(dtable.Rows[0]["PileCalculation"]);

            
            DataTable dt = oExecuteSQL.GetDataBySQL("SELECT Version FROM MTEDept_appVersion WHERE (Name = 'PileCalculation') ORDER BY CreateDate DESC");
            string version = dt.Rows[0]["Version"].ToString();

            if (appGlobal.IsMembership)
            {
                if (version == Application.ProductVersion)
                {
                    Application.Run(new Form1());
                }
                else if(appGlobal.UserID == "6989")
                {
                    MessageBox.Show("版本不符:" + Application.ProductVersion + "\n請小心使用", "Watch Out");
                    Application.Run(new Form1());
                }
                else
                {
                    MessageBox.Show("請使用最新版：" + Application.ProductVersion,"Error");
                }
            }
            else
            {
                MessageBox.Show("請洽軌道二部", "Error");
            }
            
            
        }
    }
}
