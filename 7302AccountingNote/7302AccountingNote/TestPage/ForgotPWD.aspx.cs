using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7302AccountingNote.TestPage
{
    public partial class ForgotPWD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            
            string origAcc = txtOrigAccount.Text;
            string origName = txtNameCheck.Text;
            string origEmail = txtEmailCheck.Text;
            int origLevel = 1;
            if (Manager.Selected)
            {
                origLevel = 0;
            }
            else if(Normal.Selected)
            {
                origLevel = 1;
            }

            if(!UserInfoManager.CheckInfoIsCorrect(origAcc, origName, origEmail, origLevel))
            {
                WrongShow.Text = "資料不匹配";
            }

            string newPWD = txtNewPWD.Text;
            string newConfinrmPWD = NewPWDConfirm.Text;

            if(string.Compare(newPWD, newConfinrmPWD, false) == 0)
            {
                UserInfoManager.ChangePWD(newPWD, origAcc);
                Response.Redirect("/Login.aspx");
            }

            WrongShow.Text = "密碼輸入值有誤";
        }
    }
}