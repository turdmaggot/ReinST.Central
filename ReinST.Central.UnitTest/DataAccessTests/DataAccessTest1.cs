using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReinST.Central.DataManagement;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using ReinST.Central.Extensions;
using ReinST.Central.UnitTest.Objects;
using System.Collections.Generic;
using System;
using ReinST.Central.Helpers;
using ReinST.Central.Objects;

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

        // For DataAccessConnection
        DataAccessConnection DACon = new DataAccessConnection(DatabaseInstance, InitialCatalog, Username, Password);

        [TestMethod]
        public void TestCreate()
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

        [TestMethod]
        public void TestRead()
        {
            using (DataAccess da = new DataAccess(DatabaseInstance, InitialCatalog, Username, Password))
            {
                DataSet ds = da.ReturnDataSet("SELECT * FROM SampleTable1");
                List<SampleObject1> objects = new List<SampleObject1>();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SampleObject1 newObj = new SampleObject1()
                        {
                            ID = row.FieldOrDefault<int>("ID"),
                            DateAdded = row.FieldOrDefault<DateTime>("DateAdded"),
                            FirstName = row.FieldOrDefault<string>("FirstName"),
                            LastName = row.FieldOrDefault<string>("LastName")
                        };

                        objects.Add(newObj);
                    }

                    foreach (SampleObject1 obj in objects)
                    {
                        Console.WriteLine("ID: " + obj.ID + " Date Added: " + obj.DateAdded.ToShortDateString() + " Name: " + obj.FirstName + " " + obj.LastName);
                    }

                    Assert.AreEqual(ds.Tables[0].Rows.Count, objects.Count);
                }
                else
                {
                    Assert.Inconclusive();
                }
                
                
                //Assert.IsNotNull(newId);
            }
        }

        [TestMethod]
        public void TestHelperExecuteNonQueryWithoutParameters()
        {
            CreateTestTableIfNotExists();

            string firstName = StringHelper.GenerateRandomText(10);
            string lastName = StringHelper.GenerateRandomText(10);

            string query = "INSERT INTO SampleTable1 ([FirstName], [LastName]) VALUES ('" + firstName + "', '" + lastName + "')";

            DataAccessHelper.ExecuteNonQuery(query, DACon);
            List<SampleObject1> samples;
            int itemCount = ReadAndCountFromTestTable(out samples);

            bool pass = false;

            if ((firstName.Equals(samples[itemCount - 1].FirstName, StringComparison.OrdinalIgnoreCase)) &&
               (lastName.Equals(samples[itemCount - 1].LastName, StringComparison.OrdinalIgnoreCase)))
            {
                pass = true;
            }

            Assert.IsTrue(pass);
        }

        public void TestHelperExecuteNonQueryWithParameters()
        {
            CreateTestTableIfNotExists();

            string firstName = StringHelper.GenerateRandomText(10);
            string lastName = StringHelper.GenerateRandomText(10);

            List<DataAccessParameter> dataAccessParameters = new List<DataAccessParameter>();
            dataAccessParameters.Add(new DataAccessParameter("@fn", firstName));
            dataAccessParameters.Add(new DataAccessParameter("@ln", firstName));

            string query = "INSERT INTO SampleTable1 ([FirstName], [LastName]) VALUES (@fn, @ln)";

            DataAccessHelper.ExecuteNonQuery(query, DACon, dataAccessParameters);
            List<SampleObject1> samples;
            int itemCount = ReadAndCountFromTestTable(out samples);

            bool pass = false;

            if ((firstName.Equals(samples[itemCount - 1].FirstName, StringComparison.OrdinalIgnoreCase)) &&
               (lastName.Equals(samples[itemCount - 1].LastName, StringComparison.OrdinalIgnoreCase)))
            {
                pass = true;
            }

            Assert.IsTrue(pass);
        }

        private int ReadAndCountFromTestTable(out List<SampleObject1> objects)
        {
            using (DataAccess da = new DataAccess(DatabaseInstance, InitialCatalog, Username, Password))
            {
                DataSet ds = da.ReturnDataSet("SELECT * FROM SampleTable1");
                objects = new List<SampleObject1>();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SampleObject1 newObj = new SampleObject1()
                        {
                            ID = row.FieldOrDefault<int>("ID"),
                            DateAdded = row.FieldOrDefault<DateTime>("DateAdded"),
                            FirstName = row.FieldOrDefault<string>("FirstName"),
                            LastName = row.FieldOrDefault<string>("LastName")
                        };

                        objects.Add(newObj);
                    }

                    foreach (SampleObject1 obj in objects)
                    {
                        Console.WriteLine("ID: " + obj.ID + " Date Added: " + obj.DateAdded.ToShortDateString() + " Name: " + obj.FirstName + " " + obj.LastName);
                    }

                    if (objects.Count == ds.Tables[0].Rows.Count)
                        return objects.Count;
                    else
                        return 0;
                }
                else
                {
                    return 0;
                }
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
