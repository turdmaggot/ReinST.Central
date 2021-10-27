using System.Collections.Generic;
using System.Data.SqlClient;
using ReinST.Central.DataManagement;
using ReinST.Central.Objects;
using ReinST.Central.Extensions;
using System.Data;

namespace ReinST.Central.Helpers
{
    /// <summary>
    /// Static helper for performing SQL functions.
    /// </summary>
    public static class DataAccessHelper
    {
        #region For INSERT, UPDATE, and DELETE

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="connStringTag">Connection string tag of the connection to use, as defined on your config.</param>
        /// <param name="daParameter">Optional. Parameter for this query.</param>
        public static void ExecuteNonQuery(string query, string connStringTag, DataAccessParameter daParameter = null) 
        {
            using (DataAccess da = new DataAccess(connStringTag))
            {
                if (daParameter != null)
                {
                    if (daParameter.ParameterName != null)
                    {
                        SqlParameter[] param = { new SqlParameter(daParameter.ParameterName, daParameter.Value) };
                        da.ExecuteNonQuery(query, param);
                    }
                }
                else
                    da.ExecuteNonQuery(query);
            }
        }

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="connStringTag">Connection string tag of the connection to use, as defined on your config.</param>
        /// <param name="daParameters">Parametera for this query.</param>
        public static void ExecuteNonQuery(string query, string connStringTag, List<DataAccessParameter> daParameters)
        {
            using (DataAccess da = new DataAccess(connStringTag))
            {
                if (daParameters.Count > 0)
                    da.ExecuteNonQuery(query, daParameters.ToSqlParameterList());
            }
        }

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="daConn">DataAccessConnection to use.</param>
        /// <param name="daParameter">Optional. Parameter for this query.</param>
        public static void ExecuteNonQuery(string query, DataAccessConnection daConn, DataAccessParameter daParameter = null)
        {
            using (DataAccess da = new DataAccess(daConn.DataSource, daConn.InitialCatalog, daConn.UserId, daConn.Password))
            {
                if (daParameter != null)
                {
                    if (daParameter.ParameterName != null)
                    {
                        SqlParameter[] param = { new SqlParameter(daParameter.ParameterName, daParameter.Value) };
                        da.ExecuteNonQuery(query, param);
                    }
                }
                else
                    da.ExecuteNonQuery(query);
            }
        }

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="daConn">DataAccessConnection to use.</param>
        /// <param name="daParameters">Parametera for this query.</param>
        public static void ExecuteNonQuery(string query, DataAccessConnection daConn, List<DataAccessParameter> daParameters)
        {
            using (DataAccess da = new DataAccess(daConn.DataSource, daConn.InitialCatalog, daConn.UserId, daConn.Password))
            {
                if (daParameters.Count > 0)
                    da.ExecuteNonQuery(query, daParameters.ToSqlParameterList());
            }
        }

        #endregion

        #region SELECT

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="connStringTag">Connection string tag of the connection to use, as defined on your config.</param>
        /// <param name="daParameter">Optional. Parameter for this query.</param>
        public static DataTable GetTable(string query, string connStringTag, DataAccessParameter daParameter = null)
        {
            using (DataAccess da = new DataAccess(connStringTag))
            {
                DataSet ds;

                if (daParameter != null)
                {
                    if (daParameter.ParameterName != null)
                    {
                        SqlParameter[] param = { new SqlParameter(daParameter.ParameterName, daParameter.Value) };
                        ds = da.ReturnDataSet(query, param);
                    }
                    else
                        return null;
                }
                else
                    ds = da.ReturnDataSet(query);

                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="daConn">DataAccessConnection to use.</param>
        /// <param name="daParameter">Optional. Parameter for this query.</param>
        public static DataTable GetTable(string query, DataAccessConnection daConn, DataAccessParameter daParameter = null)
        {
            using (DataAccess da = new DataAccess(daConn.DataSource, daConn.InitialCatalog, daConn.UserId, daConn.Password))
            {
                DataSet ds;

                if (daParameter != null)
                {
                    if (daParameter.ParameterName != null)
                    {
                        SqlParameter[] param = { new SqlParameter(daParameter.ParameterName, daParameter.Value) };
                        ds = da.ReturnDataSet(query, param);
                    }
                    else
                        return null;
                }
                else
                    ds = da.ReturnDataSet(query);

                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="connStringTag">Connection string tag of the connection to use, as defined on your config.</param>
        /// <param name="daParameters">Parameter for this query.</param>
        public static DataTable GetTable(string query, string connStringTag, List<DataAccessParameter> daParameters)
        {
            using (DataAccess da = new DataAccess(connStringTag))
            {
                DataSet ds;
                ds = da.ReturnDataSet(query, daParameters.ToSqlParameterList());
   
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="query">The SQL query.</param>
        /// <param name="daConn">DataAccessConnection to use.</param>
        /// <param name="daParameters">Parameters for this query.</param>
        public static DataTable GetTable(string query, DataAccessConnection daConn, List<DataAccessParameter> daParameters)
        {
            using (DataAccess da = new DataAccess(daConn.DataSource, daConn.InitialCatalog, daConn.UserId, daConn.Password))
            {
                DataSet ds;
                ds = da.ReturnDataSet(query, daParameters.ToSqlParameterList());

                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }

        #endregion
    }


}
