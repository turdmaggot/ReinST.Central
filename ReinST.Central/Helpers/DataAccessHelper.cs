using System.Collections.Generic;
using System.Data.SqlClient;
using ReinST.Central.DataManagement;
using ReinST.Central.Objects;
using ReinST.Central.Extensions;

namespace ReinST.Central.Helpers
{
    /// <summary>
    /// Static helper for performing SQL functions.
    /// </summary>
    public static class DataAccessHelper
    {
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
    }

    
}
