using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MiniJIRAWebApplication
{
    public class DbConnection
    {
        private static SqlConnection connection = new SqlConnection();
        private static SqlCommand command = new SqlCommand();
        private static SqlDataReader DbReader;
        private static SqlDataAdapter adapter = new SqlDataAdapter();
        public SqlTransaction DbTran;

        //private static string StrConnString = "Data Source=(local);Initial Catalog=Users;Integrated Security=True";
        private static string StrConnString = "Data Source=ADMIN-KOMPUTER\\LOKSRSQL;Initial Catalog=SECURITY;Integrated Security=True";
        // można uzyskac informacje jako Tools -> Connect To DataBase... -> Microsoft SQL Server -> ServerName {local} -> DataBase name {moja}
        // Wymaga wstępnie instalacji tego SQL Servera


        public int executeDataAdapter(DataTable tblName, string strSelectSql)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }
                adapter.SelectCommand.CommandText = strSelectSql;
                adapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBulider = new SqlCommandBuilder(adapter);
                DbCommandBulider.ConflictOption = ConflictOption.OverwriteChanges;

                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AliExecuteDataAdapter(DataTable tblName, string strSelectSql)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }
                adapter.SelectCommand.CommandText = strSelectSql;
                adapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBulider = new SqlCommandBuilder(adapter);
                DbCommandBulider.ConflictOption = ConflictOption.OverwriteChanges;

                string inser = DbCommandBulider.GetInsertCommand().ToString();
                string update = DbCommandBulider.GetUpdateCommand().CommandText.ToString();
                string delete = DbCommandBulider.GetDeleteCommand().CommandText.ToString();

                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void beginTrans()
        {
            try
            {
                if (DbTran == null)
                {
                    if (connection.State == 0)
                    {
                        createConn();
                        DbTran = connection.BeginTransaction();
                        adapter.SelectCommand.Transaction = DbTran;
                    }
                    else
                    {
                        DbTran = connection.BeginTransaction();
                        adapter.SelectCommand.Transaction = DbTran;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void commitTrans()
        {
            try
            {
                if (DbTran != null)
                {
                    DbTran.Commit();
                    DbTran = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void rollbackTrans()
        {
            try
            {
                if (DbTran != null)
                {
                    DbTran.Rollback();
                    DbTran = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void createConn()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = StrConnString;
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void closeConn()
        {
            connection.Close();
        }

        public void readDataThroughAdapter(string query, DataTable tblName)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int executeQuery(SqlCommand dbCommand)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }

                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;

                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}