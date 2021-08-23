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
        #region 帳務查找
        /// <summary> 以帳號名查到單筆帳號資料 </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataRow GetUserInfoByAccount(string account)
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                $@"SELECT [ID]
                        , [Account]
                        , [PWD]
                        , [Name]
                        , [Email]
                        , [UserLevel]
                        , [CreateDate]
                    FROM [UserInfo]
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
                return null;
            }
        }

        /// <summary> 查找全部帳號資料 </summary>
        /// <returns></returns>
        /// 
        public static DataTable GetUserInfoForUserListAdmin()
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                $@"SELECT [ID]
                        , [Account]
                        , [Name]
                        , [Email]
                        , [UserLevel]
                        , [CreateDate]
                    FROM [UserInfo]
                    ORDER BY [CreateDate] DESC
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            try
            {
                return DBHelper.ReadDataTable(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        public static DataTable GetUserInfoForUserListNormal(Guid UserID)
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                $@"SELECT [ID]
                        , [Account]
                        , [Name]
                        , [Email]
                        , [UserLevel]
                        , [CreateDate]
                    FROM [UserInfo]
                    WHERE UserLevel = 1
                    AND ID = @ID
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@ID", UserID));
            try
            {
                return DBHelper.ReadDataTable(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary> 以ID查到單筆帳號資料 </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataRow GetUserListForUserDetail(string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"SELECT [ID]
                        , [Account]
                        , [Name]
                        , [Email]
                        , [UserLevel]
                        , [CreateDate]
                    FROM [UserInfo]
                    WHERE ID = @userID
                ";

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
        #endregion

        #region 帳務變更(增刪修)
        /// <summary> 建立使用者 </summary>
        /// <param name="newGUID"></param>
        /// <param name="newAccount"></param>
        /// <param name="newPWD"></param>
        /// <param name="newName"></param>
        /// <param name="newEmail"></param>
        /// <param name="newMember"></param>
        public static void CreateNewUser(Guid newGUID, string newAccount, string newPWD, string newName, string newEmail)
        {

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"INSERT INTO [UserInfo]
                    (
                        [ID]
                       ,[Account]
                       ,[PWD]
                       ,[Name]
                       ,[Email]      
                       ,[CreateDate]
                    )
                    VALUES
                    (
                        @ID
                       ,@Account
                       ,@PWD
                       ,@Name
                       ,@Email                    
                       ,@createDate
                    )
                ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@ID", newGUID));
            paramList.Add(new SqlParameter("@Account", newAccount));
            paramList.Add(new SqlParameter("@PWD", newPWD));
            paramList.Add(new SqlParameter("@Name", newName));
            paramList.Add(new SqlParameter("@Email", newEmail));
            //paramList.Add(new SqlParameter("@UserLevel", newMember));
            paramList.Add(new SqlParameter("@CreateDate", DateTime.Now));

            try
            {
                DBHelper.CreatData(connStr, dbCommand, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }

        /// <summary> 修改使用者 </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(string name, string email, string uid)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" UPDATE [UserInfo]
                    SET
                        [Name]   = @name
                       ,[Email]  = @email
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

        /// <summary> 刪除使用者 </summary>
        /// <param name="uid"></param>
        public static void DeleteUser(string uid)
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                $@" DELETE [UserInfo]
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
        #endregion

        #region 使用者驗證
        /// <summary> 原密碼檢查 </summary>
        /// <param name="InpPwd"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool CheckPwdIsCorrect(string InpPwd, string uid)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT [PWD]
                    FROM [UserInfo]
                    WHERE ID = @uid ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@uid", uid));

            try
            {
                var dr = DBHelper.ReadDataRow(connStr, dbCommand, paramList);
                string dbPwd = dr[0].ToString();
                if (dbPwd == InpPwd)
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

        public static bool CheckInfoIsCorrect(string account, string Name, string Email, int UserLevel)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT 
                    [Account]
                    ,[Name]
                    ,[Email]
                    ,[UserLevel]
                    FROM [UserInfo]
                    WHERE Account = @NewAccount ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@NewAccount", account));

            try
            {
                var dr = DBHelper.ReadDataRow(connStr, dbCommand, paramList);

                string OrigAccount = dr[0].ToString();
                string OrigName = dr[1].ToString();
                string OrigEmail = dr[2].ToString();
                int OrigLevel = Convert.ToInt32(dr[3]);

                if (account == OrigAccount && Name == OrigName
                    && Email == OrigEmail && UserLevel == OrigLevel)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        /// <summary> 修改密碼 </summary>
        /// <param name="pwd"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        /// 
        public static bool ChangePWD(string pwd, string acc)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" UPDATE [UserInfo]
                    SET [PWD] = @pwd                     
                    WHERE Account = @acc ";
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@pwd", pwd));
            paramList.Add(new SqlParameter("@acc", acc));

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
        public static bool UpdatePwd(string pwd, string uid)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" UPDATE [UserInfo]
                    SET [PWD] = @pwd                     
                    WHERE ID = @uid ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@pwd", pwd));
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

        /// <summary> 檢查GUID是否重複 </summary>
        /// <param name="InpGUID"></param>
        /// <returns></returns>
        //public static bool CheckGUIDIsCorrect(Guid newGUID)
        //{
        //    string connStr = DBHelper.GetConnectionString();
        //    string dbCommand =
        //        $@" SELECT [ID]
        //            FROM [UserInfo]
        //            WHERE ID = @InpGUID
        //           ";

        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    paramList.Add(new SqlParameter("@InpGUID", newGUID));
        //    try
        //    {
        //        var dr = DBHelper.ReadDataRow(connStr, dbCommand, paramList);

        //        if (dr == null)
        //            return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex);
        //        return false;
        //    }
        //    return false;
        //}
        #endregion

        public static int CheckAccountUserLevel(Guid ID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT [UserLevel]
                    FROM [UserInfo]
                    WHERE ID = @ID
                   ";
            
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@ID", ID));

            try
            {
                var dr = DBHelper.ReadDataRow(connStr, dbCommand, paramList);
                string UserLevelText = dr[0].ToString();
                int UserLevel = Convert.ToInt32(UserLevelText);
                return UserLevel;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return 2;  // 還不確定怎麼寫
            }
        }
    }
}
