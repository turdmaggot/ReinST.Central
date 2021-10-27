using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinST.Central.Objects
{
    /// <summary>
    /// Contains the details for an SQL Connection instance.
    /// </summary>
    public class DataAccessConnection
    {
        #region Properties

        /// <summary>
        /// The instance of the SQL database (e.g. localhost, localhols/sqlexpress etc.).
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// Default database to query on.
        /// </summary>
        public string InitialCatalog { get; set; }
        /// <summary>
        /// User Id for the database access.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// User Id for the database access.
        /// </summary>
        public string Password { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a DataAccessConnection object.
        /// </summary>
        /// <param name="dataSource">The instance of the SQL database (e.g. localhost, localhols/sqlexpress etc.).</param>
        /// <param name="initialCatalog">Default database to query on.</param>
        /// <param name="userId">User Id for the database access.</param>
        /// <param name="password">Password for the database access.</param>
        public DataAccessConnection(string dataSource, string initialCatalog, string userId, string password)
        {
            this.DataSource = dataSource;
            this.InitialCatalog = initialCatalog;
            this.UserId = userId;
            this.Password = password;
        }

        /// <summary>
        /// Default constructor for a DataAccessConnection instance. 
        /// </summary>
        public DataAccessConnection()
        {
        }

        #endregion
    }
}
