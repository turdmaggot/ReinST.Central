using ReinST.Central.Objects;
using System.Data.SqlClient;

namespace ReinST.Central.Extensions
{
    /// <summary>
    /// Extensions for the DataAccessConnection class.
    /// </summary>
    public static class DataAccessConnectionExtensions
    {
        /// <summary>
        /// Gets the connection string of a DataAccessConnection object.
        /// </summary>
        /// <param name="value">The DataAccessConnection object.</param>
        public static string GetConnectionString(this DataAccessConnection value)
        {
            return "Data Source=" + value.DataSource +
            ";Initial Catalog=" + value.InitialCatalog +
            ";User Id=" + value.UserId +
            ";Password=" + value.Password
            + ";";
        }

        /// <summary>
        /// Gets an SQLConnection from the DataAccessConnection object.
        /// </summary>
        /// <param name="value">The DataAccessConnection object.</param>
        public static SqlConnection GetSqlConnection(this DataAccessConnection value)
        {
            return new SqlConnection(value.GetConnectionString());
        }


    }
}
