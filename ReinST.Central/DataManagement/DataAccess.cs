using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ReinST.Central.DataManagement
{
    public class DataAccess : IDisposable
    {
        #region Properties

        private SqlConnection con;

        #endregion

        #region Constructor

        /// <summary>
        /// This is to make connecting to SQL databases a lot easier.
        /// </summary>
        /// <param name="conStringConfigTag">
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

        #endregion

        #region Public Methods

        #region Read

        /// <summary>
        /// This is to return a set of values from a given SQL statement
        /// </summary>
        /// <param name="strSQLQuery">The SQL query for the given method</param>
        /// <returns>A DataSet object</returns>
        public DataSet ReturnDataSet(string strSQLQuery, SqlParameter[] Parameters = null)
        {
            DataSet dataSet = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand(strSQLQuery, con))
                {
                    if (Parameters != null)
                        cmd.Parameters.AddRange(Parameters);

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
        /// <param name="strSQLQuery">The SQL query for the given method</param>
        public bool IsExisting(string strSQLQuery, SqlParameter[] Parameters = null)
        {
            bool isExisting = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand(strSQLQuery, con))
                {
                    con.Open();

                    if (Parameters != null)
                        cmd.Parameters.AddRange(Parameters);

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
        /// <param name="strSQLQuery">The SQL query for the given method</param>
        /// <param name="Parameter">The parameter(s) for the SQL query</param>
        public int ReturnEntryCount(string strSQLQuery, SqlParameter[] Parameters = null)
        {
            int entrycount = 0;

            try
            {
                using (SqlCommand cmd = new SqlCommand(strSQLQuery, con))
                {
                    if (Parameters != null)
                        cmd.Parameters.AddRange(Parameters);

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
        /// <param name="strSQLQuery">The SQL query for the given method</param>
        /// <param name="Parameter">The parameter(s) for the SQL query</param>
        public int ReturnIndex(string strSQLQuery, SqlParameter[] Parameters = null)
        {
            int newId = 0;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = strSQLQuery.Trim() + "; SELECT SCOPE_IDENTITY()";

                    if (Parameters != null)
                        cmd.Parameters.AddRange(Parameters);

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
        /// <param name="strSQLQuery">The SQL query for the given method</param>
        /// <param name="strConnectionString">The connection string to the relevant database</param>
        public void ExecuteNonQuery(string strSQLQuery, SqlParameter[] Parameters = null)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSQLQuery, con))
                {
                    if (Parameters != null)
                        cmd.Parameters.AddRange(Parameters);

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
