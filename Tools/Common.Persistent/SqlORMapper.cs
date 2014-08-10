using Common.Persistent.ORMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Persistent
{
    public class SqlServerORMapper : ORMapper
    {
        #region Sql statments

        public override string ConditionSql
        {
            get { return "{0} = {1}"; }
        }
        public override string CountSql
        {
            get { return "SELECT COUNT_BIG(*) FROM {0}"; }
        }
        public override string DeleteSql
        {
            get { return "DELETE FROM {0}"; }
        }
        public override string FindAttributeSql
        {
            get { return "SELECT DISTINCT {0} FROM {1}"; }
        }
        public override string FindSql
        {
            get { return "SELECT {0} FROM {1}"; }
        }
        public override string InsertSql
        {
            get { return "INSERT INTO {0}({1}) VALUES( {2} )"; }
        }
        public override string OrderSql
        {
            get { return "{0} ORDER BY {1}"; }
        }
        public override string SetSql
        {
            get { return "[{0}] = {1}"; }
        }
        public override string UpdateSql
        {
            get { return "UPDATE {0} SET {1} "; }
        }
        public override string UpdateSqlValueParameterName
        {
            get { return "Value_{0}"; }
        }
        public override string UpdateSqlValueParameter
        {
            get { return "@" + UpdateSqlValueParameterName; }
        }
        public override string KeySqlValueParameterName
        {
            get { return "KeyValue_{0}"; }
        }
        public override string KeySqlValueParameter
        {
            get { return "@" + KeySqlValueParameterName; }
        }
        public override string WhereSql
        {
            get { return "{0} WHERE ( {1} )"; }
        }
        public override string And
        {
            get { return " AND "; }
        }
        public override string Comma
        {
            get { return " , "; }
        }
        public override string Join
        {
            get { return " JOIN "; }
        }

        public override string On
        {
            get { return " ON "; }
        }

        public override string Equal
        {
            get { return "="; }
        }

        public override string Dot
        {
            get { return "."; }
        }

        public override string SemiColon
        {
            get { return ";"; }
        }
        public override string LeftSquare
        {
            get { return "["; }
        }

        public override string RightSquare
        {
            get { return "]"; }
        }

        public override string StoreProcedureExistSql
        {
            get { return "SELECT NAME FROM [{0}].sys.procedures WHERE NAME='{1}'"; }
        }


        public override string CreateDatabaseSql
        {
            get
            {
                return @"CREATE DATABASE [{0}] ALTER DATABASE [{0}] SET READ_WRITE";
            }
        }

        public override string DropDatabaseSql
        {
            get { return "DROP DATABASE [{0}]"; }
        }

        public override string CommandSpitterRegexString
        {
            get { return @"\r{0,1}\nGO\r{0,1}\n"; }
        }

        public override string DatabaseExistSql
        {
            get { return @"SELECT NAME FROM SYSDATABASES WHERE NAME = '{0}'"; }

        }

        public override string MasterDatabaseName
        {
            get { return "MASTER"; }
        }

        private const string UseString = @" use [{0}]";


        #endregion

        private string connectionString;

        public SqlServerORMapper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override string GetExistSelect()
        {
            return "1";
        }

        public override IDbConnection NewConnection()
        {
            return new SqlConnection(connectionString);
        }

        public override IDbConnection GetDatabaseConnection(string databaseName)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = databaseName;
            return new SqlConnection(builder.ToString());
        }

        public override void CreateDatabase(string databaseName)
        {
            base.CreateDatabase(databaseName);
            WaitUntilConnectionBeUsed(NewConnection());
        }


        public override string GetDatabaseName()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }

        public override string GetDatabaseHost()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            return builder.DataSource;
        }

        private static readonly List<string> systemDatabases = new List<string> { "master", "tempdb", "model", "msdb" };
        public override bool IsSystemDatabase(string databaseName)
        {
            return systemDatabases.Contains(databaseName.ToLower());
        }

        public override bool StoreProcedureExist(string databaseName, string storeProceudreName)
        {
            try
            {
                using (var connection = GetDatabaseConnection(databaseName))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = string.Format(StoreProcedureExistSql, databaseName, storeProceudreName);
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception)
            {
            }

            // The database cannot be opened, just return false.
            return false;
        }


        /// <summary>
        /// Retry to open the connection.
        /// </summary>
        /// <param name="con">The connection to open.</param>
        /// <param name="times">How many times to retry if open failed.</param>
        /// <param name="retryTimeInterval">The time interval between two retries. It is in miliseconds.</param>
        public override void WaitUntilConnectionBeUsed(IDbConnection connection, int retryTimes = 20, int retryInterval = 500)
        {
            if (connection != null)
            {
                for (int i = 0; i < retryTimes; ++i)
                {
                    System.Threading.Thread.Sleep(retryInterval);
                    try
                    {
                        connection.Open();
                        connection.Close();
                        break;
                    }
                    catch (SqlException)
                    {
                        System.Threading.Thread.Sleep(retryInterval);
                    }
                }
            }
        }
    }
}
