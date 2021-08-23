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
    public partial class UserCreate : System.Web.UI.Page
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
        }

        /// <summary> 驗證新增使用者(錯誤提示) </summary>
        /// <param name="errorMsgList"></param>
        /// <returns></returns>
        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();

            // 辨識碼 必填
            //if (string.IsNullOrWhiteSpace(this.txtGuid.Text))
            //{
            //    msgList.Add("請輸入辨識碼");
            //    errorMsgList = msgList;
            //    return false;
            //}

            // 驗證是否為辨識碼 (透過長度)
            //if (this.txtGuid.Text.Length != 36)
            //{
            //    msgList.Add("辨識碼不符合規範、請利用辨識碼產生器");
            //    errorMsgList = msgList;
            //    return false;
            //}

            //辨識碼 不能重複
            //if (!UserInfoManager.CheckGUIDIsCorrect(this.txtGuid.Text))
            //{
            //    msgList.Add("辨識碼不能重複");
            //    errorMsgList = msgList;
            //    return false;
            //}

            // 帳號 必填
            if (string.IsNullOrWhiteSpace(this.txtAccount.Text))
            {
                msgList.Add("請輸入帳號");
                errorMsgList = msgList;
                return false;
            }

            // 帳號長度限制
            if (this.txtAccount.Text.Length >= 50)
            {
                msgList.Add("帳號長度不可超過50字");
                errorMsgList = msgList;
                return false;
            }

            // 密碼 必填
            if (string.IsNullOrWhiteSpace(this.txtPWD.Text))
            {
                msgList.Add("請輸入密碼");
                errorMsgList = msgList;
                return false;
            }

            // 密碼長度限制(8~16)
            if (this.txtPWD.Text.Length < 8 || this.txtPWD.Text.Length>16)
            {
                msgList.Add("密碼長度限制 (需要8~16碼)");
                errorMsgList = msgList;
                return false;
            }
        

            // 請再次確認密碼 必填
            if (string.IsNullOrWhiteSpace(this.txtPWDConfirm.Text))
            {
                msgList.Add("請輸入密碼確認");
                errorMsgList = msgList;
                return false;
            }

            // 密碼與密碼確認匹配
            if (string.Compare(txtPWD.Text, txtPWDConfirm.Text, false) != 0)
            {
                msgList.Add("請確認密碼是否一致");
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
                msgList.Add("姓名長度不可超過50字");
                errorMsgList = msgList;
                return false;
            }

            // email 必填
            if (string.IsNullOrWhiteSpace(this.txtEmail.Text))
            {
                msgList.Add("請輸入Email");
                errorMsgList = msgList;
                return false;
            }

            // email 長度限制
            if (this.txtName.Text.Length >= 100)
            {
                msgList.Add("email長度不可超過100字");
                errorMsgList = msgList;
                return false;
            }

            // 會員等級 必填
            //if (!RadbtnnewManager.Checked && !RadbtnnewUser.Checked)
            //{
            //    msgList.Add("請點選等級");
            //    errorMsgList = msgList;
            //    return false;
            //}

            errorMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary> 新增使用者 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
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

            var id = Guid.NewGuid();

            // Input txt
            //string newGUID = this.txtGuid.Text;
            string newAccount = this.txtAccount.Text;
            string newPWD = this.txtPWD.Text;
            string newName = this.txtName.Text;
            string newEmail = this.txtEmail.Text;
            //int newMember;

            //if (RadbtnnewManager.Checked)
            //{
            //    newMember = 0;
            //}
            //else
            //{
            //    newMember = 1;
            //}

            UserInfoManager.CreateNewUser(id, newAccount, newPWD, newName, newEmail);



            Response.Redirect("/SystemAdmin/AccountManagement/UserList.aspx");
        }

        /// <summary> 驗證密碼修改(錯誤提示) </summary>
        /// <param name="errorMsgList"></param>
        /// <returns></returns>
        
    }
}