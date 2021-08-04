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
    public partial class UserPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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


            if (!this.IsPostBack)
            {
                // Check is create mode or edit mode
                if (this.Request.QueryString["UID"] == null) //參數ID沒有帶到本頁，ID=null >> 新增模式
                {
                    //this.btnDelete.Visible = false;
                }
                else // 編輯模式
                {
                    string GUIDtext = this.Request.QueryString["UID"];

                    var drAccounting = UserInfoManager.GetUserListForUserDetail(GUIDtext);

                    if (drAccounting == null)
                    {
                        this.ltmsg.Text = "資料不存在";
                        this.btnSave.Visible = false;
                    }
                    else // 把原本的資料帶入編輯頁面以供使用者編輯!!
                    {
                        this.ltAccount.Text = drAccounting["Account"].ToString();

                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //如果輸入有誤就跳出
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltmsg.Text = string.Join("<br/>", msgList);
                return;
            }

            //如果沒有登入就回頭
            UserInfoModel currentUser = AuthManager.GetCurrentUser();
            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string txtPwdNew = this.txtPwdNew.Text;
            string uidText = this.Request.QueryString["UID"];

            UserInfoManager.UpdatePwd(txtPwdNew, uidText);

            this.Session["UserLoginInfo"] = null;
            Response.Redirect("/Login.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();
            string uidText = this.Request.QueryString["UID"];

            // 原密碼不可為空
            if (string.IsNullOrWhiteSpace(this.txtPwdOri.Text))
            {
                msgList.Add("原密碼不可為空");
                errorMsgList = msgList;
                return false;
            }

            // 原密碼是否正確
            if (!UserInfoManager.CheckPwdIsCorrect(this.txtPwdOri.Text, uidText))
            {
                msgList.Add("原密碼不正確");
                errorMsgList = msgList;
                return false;
            }

            // 新密碼不可為空
            if (string.IsNullOrWhiteSpace(this.txtPwdNew.Text))
            {
                msgList.Add("新密碼不可為空");
                errorMsgList = msgList;
                return false;
            }

            // 確認密碼不可為空
            if (string.IsNullOrWhiteSpace(this.txtPwdNewConfirm.Text))
            {
                msgList.Add("確認密碼不可為空");
                errorMsgList = msgList;
                return false;
            }

            // 新密碼-確認密碼是否匹配
            if (this.txtPwdNew.Text != this.txtPwdNewConfirm.Text)
            {
                msgList.Add("確認密碼不正確");
                errorMsgList = msgList;
                return false;
            }

            //// 密碼長度限制 (8~16)
            //if (this.txtPwdNew.Text.Length < 8 || this.txtPwdNew.Text.Length > 16)
            //{
            //    msgList.Add("密碼長度限制 (需要8~16碼)");
            //    errorMsgList = msgList;
            //    return false;
            //}

            // 新密碼-原密碼是否匹配
            if (this.txtPwdOri.Text == this.txtPwdNew.Text)
            {
                msgList.Add("新密碼與原密碼不可相同");
                errorMsgList = msgList;
                return false;
            }

            errorMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

    }
}