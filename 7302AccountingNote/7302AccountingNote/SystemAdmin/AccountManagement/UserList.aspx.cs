using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7302AccountingNote.SystemAdmin.AccountManagement
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check is logined
            //if (this.Session["UserLoginInfo"] == null)
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

            //-------抓到user info，做資料連結-----

            var userInfo = UserInfoManager.GetUserInfoForUserList();
            this.gvUserList.DataSource = userInfo; // 資料繫結
            this.gvUserList.DataBind();
            //Response.Write(userInfo.Rows[0][0].ToString());//驗證

            //-------抓到user info，做資料連結end------
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountManagement/UserCreate.aspx");
        }

    }
}