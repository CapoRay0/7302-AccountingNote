using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7302AccountingNote.SystemAdmin.UserInfo
{
    public partial class UserInfo : System.Web.UI.Page
    {
        /// <summary> 登入檢查 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack) // 可能是按鈕跳回本頁，所以要判斷 postback
            {
                //---Session存不存在，如果尚未登入，導至登入頁----
                if (!AuthManager.IsLogined()) // Session存不存在，如果尚未登入，導至登入頁
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                var CurrentUser = AuthManager.GetCurrentUser();

                if (CurrentUser == null) // 如果帳號不存在，導至登入頁 (有可能被管理者砍帳號)
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }
                //---Session存不存在，如果尚未登入，導至登入頁end----

                // 帳號存在則印出來
                this.ltAccount.Text = CurrentUser.Account;
                this.ltName.Text = CurrentUser.Name;
                this.ltEmail.Text = CurrentUser.Email;


                // 傳入當前登入帳號的Guid取得帳號的使用者等級
                this.ItUserLevel.Text = CurrentUser.ID;
                Guid UserGuid = Guid.Parse(this.ItUserLevel.Text);

                int UserLevel = UserInfoManager.CheckAccountUserLevel(UserGuid);
                if (UserLevel == 0)
                {
                    this.ItUserLevel.Text = "系統管理者";
                }
                else
                {
                    this.ItUserLevel.Text = "一般使用者";
                }

                HttpContext.Current.Session["UserLoginGuid"] = CurrentUser.ID;
            }
        }

        /// <summary> 使用者登出 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthManager.Logout(); // 登出，並導至登入頁
            Response.Redirect("/Default.aspx");
        }
    }
}