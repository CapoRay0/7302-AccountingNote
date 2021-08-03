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
        /// <summary> 檢查目前是否登入 </summary>
        /// <returns></returns>
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }

        /// <summary> 取得已登入的使用者資訊 (如果沒有登入就回傳 null) </summary>
        /// <returns></returns>
        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)
                return null;

            DataRow dr = UserInfoManager.GetUserInfoByAccount(account);
            //return dr;

            if (dr == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null; // 無限迴圈問題
                return null;
            }

            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();
            model.UserLevel = dr["UserLevel"].ToString();
            //DateTime? CreatDateTemp = dr["CreateDate"] as DateTime?;
            if(dr["CreateDate"] != null)
            {
                model.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
            }

            return model;
        }
        /// <summary> 登出 </summary>
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }
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
