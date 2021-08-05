using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote.DBSource;

namespace _7302AccountingNote
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //---------取得顯示資料的變數---------//
            DataTable accessTime = GetTime();   // 取得時間的DataTable
            var accCount = GetTotalAcc();       // 取得流水帳資料的數量
            var idCount = GetUserCount();       // 取得使用者的數量

            //-------------顯示資料--------------//
            this.ltFstRec.Text = accessTime.Rows[0][0].ToString(); // 初次記帳
            this.ltLstRec.Text = accessTime.Rows[accessTime.Rows.Count-1][0].ToString(); // 最後記帳
            this.ltTotalAcc.Text = accCount.ToString(); // 記帳數量
            this.ltUserNum.Text = idCount.ToString(); // 會員數
        }

        /// <summary> 取得時間 </summary>
        /// <returns></returns>
        public static DataTable GetTime()
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString = 
                $@" SELECT 
                    [CreateDate]
                FROM [AccountingNote]
                ORDER BY [CreateDate] ASC
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            //list.Add(new SqlParameter("@id", id));
            //list.Add(new SqlParameter("@userID", userID));
            try
            {
                return DBHelper.ReadDataTable(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary> 取得流水帳總數量 </summary>
        /// <returns></returns>
        public static string GetTotalAcc()
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString = 
                $@" SELECT 
                        Count ([ID]) as Cnt
                    FROM [AccountingNote]
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            try
            {
                var dr = DBHelper.ReadDataRow(connectionString, dbCommandString, list);
                //int ans = Int32.Parse(dt.Rows[0]);
                string ans = dr[0].ToString();
                return ans;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary> 取得使用者總數量 </summary>
        /// <returns></returns>
        public static string GetUserCount()
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString = 
                $@" SELECT
                        Count ([ID]) 
                    FROM [UserInfo]
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            try
            {
                var dr = DBHelper.ReadDataRow(connectionString, dbCommandString, list);
                string ans = dr[0].ToString();
                return ans;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary> 導至登入頁面 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Login.aspx");
        }
    }
}