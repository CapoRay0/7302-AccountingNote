using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7302AccountingNote.SystemAdmin.AccountManagement
{
    public partial class UserDetail : System.Web.UI.Page
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
                    this.btnDelete.Visible = false;
                }
                else // 編輯模式
                {
                    this.btnDelete.Visible = true;

                    string GUIDtext = this.Request.QueryString["UID"];

                    var drAccounting = UserInfoManager.GetUserListForUserDetail(GUIDtext);

                    if (drAccounting == null)
                    {
                        this.ltmsg.Text = "資料不存在";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }
                    else // 把原本的資料帶入編輯頁面以供使用者編輯!!
                    {
                        this.ltAccount.Text = drAccounting["Account"].ToString();
                        this.txtName.Text = drAccounting["Name"].ToString();
                        this.txtEmail.Text = drAccounting["Email"].ToString();
                        this.ltUserLevel.Text = drAccounting["UserLevel"].ToString();
                        this.ltCreateDate.Text = drAccounting["CreateDate"].ToString();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveUploadedInfo();
            Response.Redirect("/SystemAdmin/AccountManagement/UserList.aspx");
        }

        private void SaveUploadedInfo()
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

            string name = this.txtName.Text;
            string email = this.txtEmail.Text;

            // Check is create mode or edit mode
            string uidText = this.Request.QueryString["UID"];
            UserInfoManager.UpdateUserInfo(name, email, uidText);

        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();

            // email 必填
            if (string.IsNullOrWhiteSpace(this.txtEmail.Text))
            {
                msgList.Add("請輸入Email");
                errorMsgList = msgList;
                return false;
            }

            // 姓名 必填
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                msgList.Add("請輸入姓名");
                errorMsgList = msgList;
                return false;
            }

            // 姓名長度限制
            if (this.txtName.Text.Length >= 50)
            {
                msgList.Add("長度不可超過50");
                errorMsgList = msgList;
                return false;
            }

            errorMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string uidText = this.Request.QueryString["UID"];
            UserInfoManager.DeleteUser(uidText);
            Response.Redirect("/SystemAdmin/AccountManagement/UserList.aspx");
        }

        protected void btnchangePWD_Click(object sender, EventArgs e)
        {
            SaveUploadedInfo();
            string uidText = this.Request.QueryString["UID"];
            Response.Redirect($"/SystemAdmin/AccountManagement/UserPassword.aspx?UID={uidText}");
        }

    }
}