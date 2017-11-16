using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using Dapper;
using System.Diagnostics;

namespace BcSoft.EDC.Surface.Helper
{
    public class DBHelper
    {
        private string m_ConnectionString;
        public static DBHelper Create(string dbPath)
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = dbPath;

            return new DBHelper(builder.ConnectionString);
        }

        private DBHelper(string connectionString)
        {
            m_ConnectionString = connectionString;
        }

        #region Public Methods
        public IEnumerable<T> Select<T>(string sql, object param = null)
        {
            IDbConnection dbConnection = null;

            try
            {
                dbConnection = this.OpenConnection();

                return dbConnection.Query<T>(sql, param);
            }
            catch (Exception ex)
            {
                this.WriteLog("DBHelper.Select<T>", ex.Message);

                return null;
            }
        }

        public T SelectSingle<T>(string sql, object param = null)
        {
            IDbConnection dbConnection = null;

            try
            {
                dbConnection = this.OpenConnection();

                return dbConnection.QuerySingle<T>(sql, param);
            }
            catch (Exception ex)
            {
                this.WriteLog("DBHelper.SelectSingle<T>", ex.Message);

                return default(T);
            }
        }

        public bool Delete(string sql, object param = null)
        {
            IDbConnection dbConnection = null;

            try
            {
                dbConnection = this.OpenConnection();

                var executeResult = dbConnection.Execute(sql, param);
                if (executeResult > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.WriteLog("DBHelper.Delete", ex.Message);
                return false;
            }
        }

        public bool Update(string sql,object param = null)
        {
            IDbConnection dbConnection = null;

            try
            {
                dbConnection = this.OpenConnection();

                var executeResult = dbConnection.Execute(sql, param);
                if (executeResult > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.WriteLog("DBHelper.Update", ex.Message);
                return false;
            }
        }

        public bool Insert(string sql, object param = null)
        {
            IDbConnection dbConnection = null;

            try
            {
                dbConnection = this.OpenConnection();

                var executeResult = dbConnection.Execute(sql, param);
                if (executeResult > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.WriteLog("DBHelper.Insert", ex.Message);
                return false;
            }
        }
        #endregion

        #region Private Methods
        private IDbConnection OpenConnection()
        {
            var dbConnection = new SQLiteConnection(m_ConnectionString);

            return dbConnection;
        }

        private void WriteLog(string functionName,string message)
        {
            LogHelper.WriteErrorLog(functionName, message);
        }
        #endregion
    }
}
