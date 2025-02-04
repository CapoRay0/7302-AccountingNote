﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class AccountingManager
    {
        #region 流水帳查找
        /// <summary> 查詢流水帳清單 </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetAccountingList(string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT
                        [ID],
                        [CreateDate],
                        [ActType],
                        [Amount],
                        [Caption]
                    FROM [AccountingNote]
                    WHERE UserID = @userID
                ";

            // 用List把Parameter裝起來，再裝到共用參數
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", userID));
            try // 讓錯誤可以被凸顯，因此 TryCatch 不應該重構進 DBHelper
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static DataTable ShowTotalAmount(string userID, int actType)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT
                        [ID],
                        [CreateDate],
                        [ActType],
                        [Amount],
                        [Caption]
                    FROM [AccountingNote]
                    WHERE UserID = @userID
                    AND ActType = @actType
                ";

            // 用List把Parameter裝起來，再裝到共用參數
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", userID));
            list.Add(new SqlParameter("@actType", actType));
            try // 讓錯誤可以被凸顯，因此 TryCatch 不應該重構進 DBHelper
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary> 查詢流水帳 </summary>
        /// <param name="id"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT 
                        [ID],
                        [Caption],
                        [Amount],
                        [ActType],
                        [CreateDate],
                        [Body]
                    FROM [AccountingNote]
                    WHERE id = @id AND UserID = @userID
                "; // userID = 防止偷看其他使用者的資料

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));
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

        #region 流水帳變更(增刪修)

        /// <summary> 建立流水帳 </summary>
        /// <param name="userID"></param>
        /// <param name="caption"></param>
        /// <param name="amount"></param>
        /// <param name="actType"></param>
        /// <param name="body"></param>
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            // <<<<< check input >>>>>
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000.");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1.");
            // <<<<< check input >>>>>

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" INSERT INTO [AccountingNote]
                    (
                        [UserID]
                       ,[Caption]
                       ,[Amount]
                       ,[ActType]
                       ,[CreateDate]
                       ,[Body]
                    )
                    VALUES
                    (
                        @userID
                       ,@caption
                       ,@amount
                       ,@actType
                       ,@createDate
                       ,@body
                    )
                ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@userID", userID));
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@actType", actType));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now));
            paramList.Add(new SqlParameter("@body", body));

            try
            {
                DBHelper.CreatData(connStr, dbCommand, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }

        /// <summary> 編輯流水帳</summary>
        /// <param name="ID"></param>
        /// <param name="userID"></param>
        /// <param name="caption"></param>
        /// <param name="amount"></param>
        /// <param name="actType"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static bool UpdateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {
            // <<<<< check input >>>>>
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000.");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1.");
            // <<<<< check input >>>>>

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" UPDATE [AccountingNote]
                    SET
                        [UserID]     = @userID
                       ,[Caption]    = @caption
                       ,[Amount]     = @amount
                       ,[ActType]    = @actType
                       ,[CreateDate] = @createDate
                       ,[Body]       = @body
                    WHERE
                        ID = @id ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@userID", userID));
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@actType", actType));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now));
            paramList.Add(new SqlParameter("@body", body));
            paramList.Add(new SqlParameter("@id", ID));

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

        /// <summary> 刪除流水帳 </summary>
        /// <param name="ID"></param>
        public static void DeleteAccounting(int ID)
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                @" DELETE [AccountingNote]
                    WHERE ID = @id ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@id", ID));

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
    }
}
