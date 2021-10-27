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
        /// <param name="Query">The SQL query.</param>
        /// <param name="ConnStringTag">Connection string tag of the connection to use, as defined on your config.</param>
        /// <param name="DAParameter">Optional. Parameter for this query.</param>
        public static void ExecuteNonQuery(string Query, string ConnStringTag, DataAccessParameter DAParameter = null) 
        {
            using (DataAccess da = new DataAccess(ConnStringTag))
            {
                if (DAParameter != null)
                {
                    if (DAParameter.ParameterName != null)
                    {
                        SqlParameter[] param = { new SqlParameter(DAParameter.ParameterName, DAParameter.Value) };
                        da.ExecuteNonQuery(Query, param);
                    }
                }
                else
                    da.ExecuteNonQuery(Query);
            }
        }

        /// <summary>
        /// This is to handle INSERT, UPDATE, and DELETE SQL statements
        /// </summary>
        /// <param name="Query">The SQL query.</param>
        /// <param name="ConnStringTag">Connection string tag of the connection to use, as defined on your config.</param>
        /// <param name="DAParameters">Parametera for this query.</param>
        public static void ExecuteNonQuery(string Query, string ConnStringTag, List<DataAccessParameter> DAParameters)
        {
            using (DataAccess da = new DataAccess(ConnStringTag))
            {
                if (DAParameters.Count > 0)
                    da.ExecuteNonQuery(Query, DAParameters.ToSqlParameterList());
            }
        }
    }

    
}
