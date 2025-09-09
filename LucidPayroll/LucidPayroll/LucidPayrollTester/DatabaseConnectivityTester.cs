using LucidLibrary.Db;
using LucidPayroll.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// Harshan Nishatha
// 2014-01-22

namespace LucidPayrollTester
{
    [TestClass]
    public class DatabaseConnectivityTester
    {
        private static TcDatabaseConnectionParameters GetConnectionParametersOnUser()
        {
            TcDatabaseConnectionParameters connectionParameters;
            string user = Environment.UserName;

            if (user == "harshan")
            {
                connectionParameters = ConnectionStringParameters(
                    "SHARK\\SQL2008R2",
                    "PAYROLL",
                    "harshan",
                    "harshan");
            }
            else if (user == "ruchira")
            {
                connectionParameters = ConnectionStringParameters(
                    "SERVER",
                    "PAYROLL",
                    "user",
                    "password");
            }
            else
            {
                connectionParameters = ConnectionStringParameters(
                    "SERVER",
                    "PAYROLL",
                    "user",
                    "password");
            }

            return connectionParameters;
        }

        public static TcDatabase GetDatabase()
        {
            string connectionString = GetConnectionString();
            TcDatabase database = new TcDatabase(connectionString);

            return database;
        }

        public static string GetConnectionString()
        {
            string newConnectionString = "";

            TiDatabaseConnectionParameters parameters = new TcDatabaseConnectionParametersFromRegistry();
            TcDatabaseConnectionParameters connectionParameters = parameters.Read();
            newConnectionString = connectionParameters.GetDatabaseConnectionString();

            return newConnectionString;
        }

        private static TcDatabaseConnectionParameters ConnectionStringParameters(string server, string dbName, string user, string password)
        {
            TcDatabaseConnectionParameters connectionParameters = new TcDatabaseConnectionParameters();

            connectionParameters.DataSource     = server;
            connectionParameters.InitialCatalog = dbName;
            connectionParameters.UserID         = user;
            connectionParameters.Password       = password;

            return connectionParameters;
        }

        public DatabaseConnectivityTester()
        {
        }

        [TestMethod]
        public void SetDatabaseRegistryParameters()
        {
            TiDatabaseConnectionParameters parameters = new TcDatabaseConnectionParametersFromRegistry();
            TcDatabaseConnectionParameters connectionParameters = GetConnectionParametersOnUser();
            parameters.Write(connectionParameters);

            TcDatabaseConnectionParameters readParameters = parameters.Read();
            Assert.AreEqual(true, parameters.Exists());

            Assert.AreEqual(readParameters.DataSource, connectionParameters.DataSource);
            Assert.AreEqual(readParameters.InitialCatalog, connectionParameters.InitialCatalog);
            Assert.AreEqual(readParameters.UserID, connectionParameters.UserID);
            Assert.AreEqual(readParameters.Password, connectionParameters.Password);
        }

        [TestMethod]
        public void DeleteDatabaseRegistryParameters()
        {
            TiDatabaseConnectionParameters parameters = new TcDatabaseConnectionParametersFromRegistry();
            parameters.Delete();

            TcDatabaseConnectionParameters readParameters = parameters.Read();
            Assert.AreEqual("", readParameters.DataSource);
            Assert.AreEqual("", readParameters.InitialCatalog);
            Assert.AreEqual("", readParameters.UserID);
            Assert.AreEqual("", readParameters.Password);

            Assert.AreEqual(false, parameters.Exists());
        }

        [TestMethod]
        public void TestDatabaseConnectionOnRegistryParameters()
        {
            TiDatabaseConnectionParameters parameters = new TcDatabaseConnectionParametersFromRegistry();
            TcDatabaseConnectionParameters  connectionParameters = parameters.Read();
            TcDatabase database = new TcDatabase(connectionParameters.GetDatabaseConnectionString());
            bool connected = database.Connected();
            Assert.AreEqual(true, connected);
        }
    }
}
