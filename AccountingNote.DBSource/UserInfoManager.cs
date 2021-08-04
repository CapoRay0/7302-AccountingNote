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
    public class UserInfoManager
    {
        public static DataRow GetUserInfoByAccount(string account) //帶參數進來
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                @"SELECT [ID], [Account], [PWD], [Name], [Email], [UserLevel], [CreateDate]
                    FROM UserInfo
                    WHERE [Account] = @account
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@account", account));

            try
            {
                return DBHelper.ReadDataRow(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                //Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public static DataTable GetUserInfoForUserList() //帶參數進來
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                @"SELECT
                      [ID] as UID
                    , [Account]
                    , [Name]
                    , [Email]
                    , [UserLevel]
                    , [CreateDate]
                    FROM UserInfo
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            //list.Add(new SqlParameter("@account", account));

            try
            {
                return DBHelper.ReadDataTable(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                //Console.WriteLine(ex.ToString());
                return null;
            }
        }


        public static DataRow GetUserListForUserDetail(string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT 
                        ID,
                        Account,
                        Name,
                        Email,
                        UserLevel,
                        CreateDate
                    FROM [UserInfo]
                    WHERE ID = @userID
                "; // userID = 防止偷看其他使用者的資料

            List<SqlParameter> list = new List<SqlParameter>();
            
            list.Add(new SqlParameter("@userID", userID));

            try
            {
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static bool UpdateUserInfo(string name, string email, string uid)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" UPDATE [UserInfo]
                    SET
                       [Name]      = @name
                       ,[Email]    = @email
                    WHERE
                        ID = @uid ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@name", name));
            paramList.Add(new SqlParameter("@email", email));
            paramList.Add(new SqlParameter("@uid", uid));

            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);

                if (effectRows == 1)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        public static void DeleteUser(string uid)
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                @" DELETE [UserInfo]
                    WHERE ID = @uid ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@uid", uid));

            try
            {
                DBHelper.ModifyData(connectionString, dbCommandString, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }


    }
}
