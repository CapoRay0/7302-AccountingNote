using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7302AccountingNote.SystemAdmin.AccountManagement
{
    public partial class UserList : System.Web.UI.Page
    {
        /// <summary> 登入檢查 / 資料繫結 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //---Session存不存在，如果尚未登入，導至登入頁----

            if (!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var CurrentUser = AuthManager.GetCurrentUser();

            if (CurrentUser == null) // 如果帳號不存在，導至登入頁 (有可能被管理者砍帳號)
            {
                this.Session["UserLoginInfo"] = null; // 才不會無限迴圈，導來導去
                Response.Redirect("/Login.aspx");
                return;
            }
            //---Session存不存在，如果尚未登入，導至登入頁end----

            //-------抓到user info，做資料連結-----
            var userInfo = UserInfoManager.GetUserInfoForUserList();
            this.gvUserList.DataSource = userInfo; // 資料繫結
            this.gvUserList.DataBind();
            //Response.Write(userInfo.Rows[0][0].ToString());//驗證
            //-------抓到user info，做資料連結end------

            //-----------做頁數---------------
            if (userInfo.Rows.Count > 0) // 如果DB有資料
            {
                var dtPaged = this.GetPagedDataTable(userInfo);

                this.Pager.TotalSize = userInfo.Rows.Count;
                this.Pager.Bind();

                this.gvUserList.DataSource = dtPaged; // 資料繫結
                this.gvUserList.DataBind();
            }
            else
            {
                this.gvUserList.Visible = false;
                this.plcNoUserData.Visible = true;
            }
            //-----------做頁數end---------------

        }

        /// <summary> 取得頁數 </summary>
        /// <returns></returns>
        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText)) // 空的時候，給第一頁
                return 1;

            int intPage;
            if (!int.TryParse(pageText, out intPage)) // (錯誤) 數字轉換失敗，給第一頁
                return 1;

            if (intPage <= 0) // (錯誤) 0或以下，也給第一頁
                return 1;

            return intPage;
        }

        /// <summary> 分頁結構 </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetPagedDataTable(DataTable dt)
        {
            DataTable dtPaged = dt.Clone(); //只拿結構

            int pageSize = this.Pager.PageSize;
            int startIndex = (this.GetCurrentPage() - 1) * pageSize;
            int endIndex = this.GetCurrentPage() * pageSize;

            if (endIndex > dt.Rows.Count)
                endIndex = dt.Rows.Count;

            for (var i = startIndex; i < endIndex; i++)
            {
                DataRow dr = dt.Rows[i];
                var drNew = dtPaged.NewRow();

                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }

                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }

        /// <summary> 導頁至新增使用者 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountManagement/UserCreate.aspx");
        }


    }
}