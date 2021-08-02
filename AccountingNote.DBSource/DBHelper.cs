using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class DBHelper
    {
        public static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }


        // 讀取清單的Method，抽到共用方法中
        public static DataTable ReadDataTable(string connStr, string dbCommand, List<SqlParameter> list)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    //comm.Parameters.AddWithValue("@userID", userID);
                    comm.Parameters.AddRange(list.ToArray());


                    conn.Open();
                    var reader = comm.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return dt;
                }
            }
        }

        public static DataRow ReadDataRow(string connStr, string dbCommand, List<SqlParameter> list) // AccountingManager的
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddRange(list.ToArray());

                    conn.Open();
                    var reader = comm.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    if (dt.Rows.Count == 0)
                        return null;

                    DataRow dr = dt.Rows[0]; // 舊的搬過來
                    return dt.Rows[0];

                }
            }
        }
        // UpdateAccounting 與 DeleteAccounting 的重構
        public static int ModifyData(string connectionString, string dbCommandString, List<SqlParameter> paramList)
        {
            // connect db & execute
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand(dbCommandString, conn))
                {

                    //comm.Parameters.AddWithValue("@id", ID);
                    comm.Parameters.AddRange(paramList.ToArray());

                    conn.Open();
                    int effectRowsCount = comm.ExecuteNonQuery();
                    return effectRowsCount;
                }
            }
        }
        // CreateAccounting 的重構
        public static void CreatData(string connStr, string dbCommand, List<SqlParameter> paramList)
        {
            // connect db & execute
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    //comm.Parameters.AddWithValue("@userID", userID);
                    //comm.Parameters.AddWithValue("@caption", caption);
                    //comm.Parameters.AddWithValue("@amount", amount);
                    //comm.Parameters.AddWithValue("@actType", actType);
                    //comm.Parameters.AddWithValue("@createDate", DateTime.Now);
                    //comm.Parameters.AddWithValue("@body", body);
                    comm.Parameters.AddRange(paramList.ToArray());

                    conn.Open();
                    comm.ExecuteNonQuery();
                }
            }
        }
    }
}
