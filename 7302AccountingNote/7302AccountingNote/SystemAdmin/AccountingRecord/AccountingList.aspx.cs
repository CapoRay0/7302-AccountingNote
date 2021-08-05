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
        /// <summary> 登入檢查 / 金額小計 / 資料繫結 </summary>
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
                var dtPaged = this.GetPagedDataTable(dt);

                this.Pager.TotalSize = dt.Rows.Count;
                this.Pager.Bind();

                this.gvAccountingList.DataSource = dtPaged; // 資料繫結
                this.gvAccountingList.DataBind();
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;
                this.TotalAccount.Visible = false;
                this.Pager.Visible = false;

            }

        }

        /// <summary> 取得頁數 </summary>
        /// <returns></returns>
        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText)) // 空的時候，給第一頁
                return 1;

            int intPage;
            if (!int.TryParse(pageText, out intPage)) // (錯誤) 數字轉換失敗，給第一頁
                return 1;

            if (intPage <= 0) // (錯誤) 0或以下，也給第一頁
                return 1;
            return intPage;
        }

        /// <summary> 分頁結構 </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetPagedDataTable(DataTable dt)
        {



            DataTable dtPaged = dt.Clone(); //只拿結構
            //dt.Copy(); // 除了結構還拿資料，但0筆的話會出錯

            int pageSize = this.Pager.PageSize;
            int startIndex = (this.GetCurrentPage() - 1) * pageSize;
            int endIndex = this.GetCurrentPage() * pageSize;

            //驗證頁數不大於最大頁數
            string pageText = Request.QueryString["Page"];
            int intPage = Convert.ToInt32(pageText);
            if (((intPage - 1) * pageSize) >= dt.Rows.Count || intPage < 0)
            {
                this.ltmsg.Text = "不正確的頁數";
                this.Pager.Visible = false;
                return null;
            }

            if (endIndex > dt.Rows.Count)
                endIndex = dt.Rows.Count;

            for (var i = startIndex; i < endIndex; i++)
            {
                DataRow dr = dt.Rows[i];
                var drNew = dtPaged.NewRow();

                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }

                dtPaged.Rows.Add(drNew);
            }
            this.Pager.Visible = true;
            return dtPaged;
        }

        /// <summary> 新增流水帳 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingRecord/AccountingDetail.aspx");
        }

        /// <summary> ActType(支出 / 收入) </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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