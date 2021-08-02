using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    public class AuthManager
    {

        public static bool TryLogin(string account, string pwd, out string errorMsg)
        {
            // check empty
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "Account / PWD is required.";
                return false;
            }

            // read db and check
            var dr = UserInfoManager.GetUserInfoByAccount(account);

            //check null
            if (dr == null)
            {
                errorMsg = $"Account: {account} doesn't exists."; // 查不到的話
                return false;
            }

            // check account / pwd
            if (string.Compare(dr["Account"].ToString(), account, true) == 0 &&
                string.Compare(dr["PWD"].ToString(), pwd, false) == 0) // 因密碼要強制大小寫因此設定為false
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString(); // 正確!!，跳頁至 UserInfo.aspx
                //Response.Redirect("/SystemAdmin/UserInfo.aspx");
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                //this.ltlMsg.Text = "Login failed. Please check PWD.";
                errorMsg = "Login failed. Please check PWD.";
                return false;
            }

        }
    }
}
