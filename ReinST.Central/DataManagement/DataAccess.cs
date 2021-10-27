using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ReinST.Central.DataManagement
{
    /// <summary>
    /// Main class for performing SQL functions.
    /// </summary>
    public class DataAccess : IDisposable
    {
        #region Properties

        private SqlConnection con;

        #endregion

        #region Constructor

        /// <summary>
        /// This is to make connecting to SQL databases a lot easier.
        /// </summary>
        /// <param name="connStringConfigTag">
        /// The connection string defined inside the web.config file.
        /// </param>
        public DataAccess(string connStringConfigTag)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings[connStringConfigTag].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This is to make connecting to SQL databases a lot easier.
        /// </summary>
        /// <param name="dataSource">
        /// Database Instance/URL on you which you want to connect to.
        /// </param>
        /// <param name="initialCatalog">
        /// The database you want to query on.
        /// </param>
        /// <param name="userId">
        /// Username to access the database.
        /// </param>
        /// <param name="password">
        /// Password to access the database
        /// </param>
        public DataAccess(string dataSource, string initialCatalog, string userId, string password)
        {
            try
            {
                string strCon = "Data Source=" + dataSource +
                    ";Initial Catalog="+ initialCatalog + 
                    ";User Id=" + userId + 
                    ";Password=" + password 
                    + ";";

                con = new SqlConnection(strCon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Public Methods

        #region Read

        /// <summary>
        /// This is to return a set of values from a given SQL statement
        /// </summary>
        /// <param name="sqlQuery">The SQL query for the given method</param>
        /// <param name="parameters">Optional parameters, if your query has parameters.</param>
        /// <returns>A DataSet object</returns>
        public DataSet ReturnDataSet(string sqlQuery, SqlParameter[] parameters = null)
        {
            DataSet dataSet = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    con.Open();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return dataSet;
        }

        /// <summary>
        /// This is to return a set of values from a given SQL statement
        /// </summary>
        /// <param name="sqlQuery">The SQL query for the given method</param>
        /// <param name="parameters">Optional parameters, if your query has parameters.</param>
        /// <returns>A DataSet object</returns>
        public DataSet ReturnDataSet(string sqlQuery, List<SqlParameter> parameters)
        {
            DataSet dataSet = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    foreach (SqlParameter sqlParameter in parameters)
                    {
                        cmd.Parameters.Add(sqlParameter);
                    }

                    con.Open();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return dataSet;
        }

        /// <summary>
        /// This is to determine if value(s) returned by a given SQL statement exists
        /// </summary>
        /// <param name="sqlQuery">The SQL query for the given method</param>
        /// <param name="parameters">Optional parameters, if your query has parameters.</param>
        public bool IsExisting(string sqlQuery, SqlParameter[] parameters = null)
        {
            bool isExisting = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        isExisting = dr.HasRows;
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return isExisting;
        }

        /// <summary>
        /// This is to determine the number of entries returned by an SQL statement
        /// </summary>
        /// <param name="sqlQuery">The SQL query for the given method</param>
        /// <param name="parameters">The parameter(s) for the SQL query</param>
        public int ReturnEntryCount(string sqlQuery, SqlParameter[] parameters = null)
        {
            int entrycount = 0;

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        entrycount = Convert.ToInt32(dr[0]);
                        dr.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }

            return entrycount;
        }

        #endregion

        #region Create/Update/Delete

        /// <summary>
        /// This is to return the primary key of an SQL INSERT statement
        /// </summary>
        /// <param name="sqlInsertQuery">The SQL query for the given method</param>
        /// <param name="parameters">The parameter(s) for the SQL query</param>
        public int ReturnIndex(string sqlInsertQuery, SqlParameter[] parameters = null)
        {
            int newId = 0;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = sqlInsertQuery.Trim() + "; SELECT SCOPE_IDENTITY()";

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    con.Open();
                    newId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return newId;
        }

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="sqlQuery">The SQL query for the given method</param>
        /// <param name="parameters">Optional parameters, if your query has parameters.</param>
        public void ExecuteNonQuery(string sqlQuery, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="sqlQuery">The SQL query for the given method</param>
        /// <param name="parameters">List of SqlParameter objects.</param>
        public void ExecuteNonQuery(string sqlQuery, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Executes SQL scripts from an SQL file.
        /// </summary>
        /// <param name="scriptContent">Content extracted from the SQL script file.</param>
        public void ExecuteScript(string scriptContent)
        {
            try
            {
                IEnumerable<string> cmdStrings = Regex.Split(scriptContent, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                con.Open();

                foreach (string strCommand in cmdStrings)
                {
                    if (!string.IsNullOrWhiteSpace(strCommand.Trim()))
                    {
                        using (SqlCommand cmd = new SqlCommand(strCommand, con))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion

        #region Closing Connection Manually

        /// <summary>
        /// This is to close the connection of the instance manually, if it is still open.
        /// </summary>
        public void CloseConnection()
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }

        #endregion

        #endregion

        #region Dispose

        public void Dispose()
        {
            CloseConnection();
            ((IDisposable)con).Dispose();
        }

        #endregion
    }
}
