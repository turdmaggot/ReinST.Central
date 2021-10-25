using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ReinST.Central.DataManagement;
using System.IO;
using System.Data.SqlClient;

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
        public void TestInsert()
        {
            CreateTestTableIfNotExists();

            using (DataAccess da = new DataAccess(DatabaseInstance, InitialCatalog, Username, Password))
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@fn", "Reiner"),
                    new SqlParameter("@ln", "Tupaz")
                };
                
                int newId = da.ReturnIndex("INSERT INTO SampleTable1 ([FirstName], [LastName]) VALUES (@fn, @ln)", param);

                Assert.IsNotNull(newId);
            }
        }


        private void CreateTestTableIfNotExists()
        {
            try
            {
                using (DataAccess da = new DataAccess(DatabaseInstance, InitialCatalog, Username, Password))
                {
                    string script = File.ReadAllText(@"SQLScripts\SampleTable1.sql");
                    da.ExecuteScript(script);
                }
            }
            catch (SqlException ex)
            {
                // Do nothing as table already exists!
            }  
        }

    }
}
