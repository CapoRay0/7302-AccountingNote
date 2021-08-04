using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7302AccountingNote.SystemAdmin.AccountingRecord
{
    public partial class AccountingList : System.Web.UI.Page
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

            // read accounting data
            var dt = AccountingManager.GetAccountingList(CurrentUser.ID);

            //-------小計--------
            int answer = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((int)dt.Rows[i][2] == 0)
                { answer -= (int)dt.Rows[i][3]; }
                else if ((int)dt.Rows[i][2] == 1)
                { answer += (int)dt.Rows[i][3]; }
            }

            TotalAccount.Text = $"小計 {answer:C0} 元";  //驗證

            //-------小計end--------

            if (dt.Rows.Count > 0) // 如果DB有資料
            {
                this.gvAccountingList.DataSource = dt; // 資料繫結
                this.gvAccountingList.DataBind();
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;
            }

        }
        //int totalPages = this.GetTotalPages(dt); // 取得總頁數
        //var dtPaged = this.GetPagedDataTable(dt);

        //this.ucPager.TotalSize = dt.Rows.Count; //總頁數給dt筆數就好
        //this.ucPager.Bind(); // 可以利用 Method 來跟外界(這裡)溝通
        //// 0802--------------------------------------------------------
        //var pages = (dt.Rows.Count / 10); // 計算共幾筆、共幾頁
        //if (dt.Rows.Count % 10 > 0)
        //    pages += 1;

        //this.ltpager.Text = $"共 {dt.Rows.Count} 筆，共 {pages} 頁，目前在第 {this.GetCurrentPage()} 頁<br/>";
        ////--------------------------------------------------------

        //for (var i = 1; i <= totalPages; i++) // 總頁數
        //{
        //    this.ltpager.Text += $"<a href='AccountingList.aspx?page={i}'>{i}</a>&nbsp";
        //}

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingRecord/AccountingDetail.aspx");
        }

        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = row.FindControl("lblActType") as Label;

                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType");

                switch (actType)
                {
                    case 0:
                        lbl.Text = "支出";
                        if (dr.Row.Field<int>("Amount") > 0)
                        {
                            lbl.ForeColor = Color.Red;
                        }
                        break;
                    case 1:
                        lbl.Text = "收入";
                        if (dr.Row.Field<int>("Amount") > 0)
                        {
                            lbl.ForeColor = Color.Blue;
                        }
                        break;
                }
            }
        }
    }
}