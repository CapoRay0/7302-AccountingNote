using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7302AccountingNote.TestPage
{
    public partial class NewAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string newAccount = txtNewAccount.Text;
            string newPWD = txtNewPWD.Text;
            //string newGUID = this.txtGuid.Text;

            var id = Guid.NewGuid();
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

            UserInfoManager.CreateNewUser(id, newAccount, newPWD,  newName, newEmail);
            Response.Redirect("/Default.aspx");
        }
    }
}