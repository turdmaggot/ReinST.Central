using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ReinST.Central.Objects;
using System.Data.SqlTypes;

namespace ReinST.Central.Extensions
{
    /// <summary>
    /// Extensions for the DataAccessParameter object.
    /// </summary>
    public static class DataAccessParameterExtensions
    {
        /// <summary>
        /// This is to convert a DataAccessParameter object into an SqlParameter object.
        /// </summary>
        /// <param name="value">The DataAccessParameter object to be converted.</param>
        public static SqlParameter ToSqlParameter(this DataAccessParameter value)
        {
            if (value.ParameterName != null)
            {
                if (value.Value is DateTime dateTime)
                    if (dateTime < SqlDateTime.MinValue.Value)
                        value.Value = SqlDateTime.Null;

                if (value.Value == null)
                    value.Value = DBNull.Value;

                return new SqlParameter(value.ParameterName, value.Value);
            } 
            else
                return null;
        }

        /// <summary>
        /// This is to convert a DataAccessParameter list into an SqlParameter list.
        /// </summary>
        /// <param name="value">The DataAccessParameter list to be converted.</param>
        public static List<SqlParameter> ToSqlParameterList(this List<DataAccessParameter> value)
        {
            if (value.Count > 0)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                foreach (DataAccessParameter dataParameter in value)
                {
                    parameters.Add(dataParameter.ToSqlParameter());
                }

                return parameters;
            }
            else
                return null;

        }
    }
}
