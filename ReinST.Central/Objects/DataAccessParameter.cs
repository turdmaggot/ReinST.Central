using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinST.Central.Objects
{
    /// <summary>
    /// Holds parameters for your SQL query.
    /// </summary>
    public class DataAccessParameter
    {
        #region Properties

        /// <summary>
        /// Name of the paramter.
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Value of the paramter.
        /// </summary>
        public object Value { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a DataAccessParameter object.
        /// </summary>
        public DataAccessParameter()
        {
        }

        /// <summary>
        /// Constructs a DataAccessParameter object.
        /// </summary>
        /// <param name="ParameterName">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public DataAccessParameter(string ParameterName, object Value)
        {
            this.ParameterName = ParameterName;
            this.Value = Value;
        }

        #endregion
    }
}
