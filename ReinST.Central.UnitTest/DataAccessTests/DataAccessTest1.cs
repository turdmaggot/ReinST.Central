using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ReinST.Central.DataManagement;
using System.IO;

namespace ReinST.Central.UnitTest.DataAccessTests
{
    [TestClass]
    public class DataAccessTest1
    {
        // Set DB here:
        private const string DatabaseInstance = "localhost";
        private const string InitialCatalog = "ReinSTCentral";
        private const string Username = "sa";
        private const string Password = "iamreiner";

        [TestMethod]
        public void CreateTestTableIfNotExists()
        {
            using (DataAccess da = new DataAccess(DatabaseInstance, InitialCatalog, Username, Password))
            {
                string script = File.ReadAllText(@"SQLScripts\SampleTable1.sql");
                //da.ExecuteScript(script);
            }
        }
    }
}
