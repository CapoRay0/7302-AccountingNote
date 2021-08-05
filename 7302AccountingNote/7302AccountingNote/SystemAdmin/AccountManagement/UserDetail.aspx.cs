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
        /// <summary> 登入檢查 </summary>
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

            if (!this.IsPostBack)
            {
                // Check is create mode or edit mode
                if (this.Request.QueryString["UID"] == null)
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
                        string Level = drAccounting["UserLevel"].ToString();
                        this.ltCreateDate.Text = drAccounting["CreateDate"].ToString();

                        switch (Level)
                        {
                            case "0":
                                this.ltUserLevel.Text = "管理者";
                                break;
                            case "1":
                                this.ltUserLevel.Text = "一般會員";
                                break;
                        }

                    }
                }
            }
        }

        /// <summary> 儲存修改使用者 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(SaveUploadedInfo())
            Response.Redirect("/SystemAdmin/AccountManagement/UserList.aspx");
        }

        /// <summary> 修改使用者並導頁至修改密碼 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnchangePWD_Click(object sender, EventArgs e)
        {
            SaveUploadedInfo();
            string uidText = this.Request.QueryString["UID"];
            Response.Redirect($"/SystemAdmin/AccountManagement/UserPassword.aspx?UID={uidText}");
        }

        /// <summary>
        /// 修改使用者方法
        /// </summary>
        private bool SaveUploadedInfo()
        {
            //如果輸入有誤就跳出
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltmsg.Text = string.Join("<br/>", msgList);
                return false;
            }

            //如果沒有登入就回頭
            UserInfoModel currentUser = AuthManager.GetCurrentUser();
            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return false;
            }

            string name = this.txtName.Text;
            string email = this.txtEmail.Text;

            string uidText = this.Request.QueryString["UID"];
            UserInfoManager.UpdateUserInfo(name, email, uidText);
            return true;

        }

        /// <summary> 驗證修改使用者(錯誤提示) </summary>
        /// <param name="errorMsgList"></param>
        /// <returns></returns>
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
                msgList.Add("姓名長度不可超過50");
                errorMsgList = msgList;
                return false;
            }

            // Email長度限制
            if (this.txtName.Text.Length >= 100)
            {
                msgList.Add("Email長度不可超過100");
                errorMsgList = msgList;
                return false;
            }

            errorMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary> 刪除使用者 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string uidText = this.Request.QueryString["UID"];
            UserInfoManager.DeleteUser(uidText);
            Response.Redirect("/SystemAdmin/AccountManagement/UserList.aspx");
        }


    }
}