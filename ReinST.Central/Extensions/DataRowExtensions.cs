using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinST.Central.Extensions
{
    public static class DataRowExtensions
    {
        /// <summary>
        /// Checks if the data row is not null before getting its value.
        /// </summary>
        /// <param name="row">
        /// Data row of which you are trying to retrieve value from.
        /// </param>
        /// <param name="columnName">
        /// DB Column of the value being retrieved.
        /// </param>
        /// <returns>
        /// Value if the object isn't null inside the db. If it is null there, it will be null here too.
        /// </returns>
        public static T FieldOrDefault<T>(this DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? default(T) : row.Field<T>(columnName);
        }
    }
}
