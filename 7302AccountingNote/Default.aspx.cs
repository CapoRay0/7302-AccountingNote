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
            DataTable accessTime = GetTime(); //拿到dt
            var accCount = GetTotalAcc(); //取得流水帳資料的數量
            var idCount = GetUserCount(); //取得使用者的數量

            //---------顯示資料----------//
            this.ltFstRec.Text = accessTime.Rows[0][0].ToString();
            this.ltLstRec.Text = accessTime.Rows[accessTime.Rows.Count-1][0].ToString();
            this.ltTotalAcc.Text = accCount.ToString();
            this.ltUserNum.Text = idCount.ToString();
        }

        public static DataTable GetTime()
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString = 
                $@" SELECT 
                    [CreateDate]
                FROM [7302AccountingNote].[dbo].[AccountingNote]
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
                Logger.WriteLog(ex);   //利用logger存ex而不要用consloe  
                return null;
            }
        }


        public static string GetTotalAcc()
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString = 
                $@" SELECT 
                        Count ([ID]) as Cnt
                    FROM [7302AccountingNote].[dbo].[AccountingNote]
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
                Logger.WriteLog(ex);   //利用logger存ex而不要用consloe  
                return null;
            }
        }

        public static string GetUserCount()
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString = 
                $@" SELECT
                        Count ([ID]) 
                    FROM [7302AccountingNote].[dbo].[UserInfo]
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
                Logger.WriteLog(ex);   //利用logger存ex而不要用consloe  
                return null;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Login.aspx");
        }
    }
}