using System;
using System.Data;
using System.Data.SqlClient;

// Harshan Nishantha
// 2013-02-26

namespace LucidLibrary.Db
{
    public class TcDatabase
    {
        private static TcDatabase current;
        private string connectionString;

        public TcDatabase(string connectionString)
        {
            this.connectionString = connectionString;
            current = this;
        }

        public string GetDatabaseName()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

            return builder.InitialCatalog;
        }

        public static bool CheckConnectionString(string connectionString)
        {
            bool connected = false;

            string query = "SELECT @@VERSION";

            object result = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    result = command.ExecuteScalar();
                }
            }

            if (result == null)
            {
                connected = false;
            }
            else
            {
                connected = true;
            }

            return connected;
        }

        public bool Connected()
        {
            bool connected = false;

            string query = "SELECT @@VERSION";

            try
            {
                string result = (string)ExecuteScalar(query);
                if (result == null)
                {
                    connected = false;
                }
                else
                {
                    connected = true;
                }
            }
            catch (Exception)
            {
                connected = false;
            }
            
            return connected;
        }

        public static TcDatabase Current()
        {
            return current;
        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        public object ExecuteScalar(string query)
        {
            object result = null;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    result = command.ExecuteScalar();
                }
            }

            return result;
        }

        public int ExecuteNonQuery(TcStoredProcedure procedure)
        {
            int result = 0;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = procedure.GetCommand())
                {
                    command.Connection = connection;

                    SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    command.ExecuteNonQuery();

                    result = int.Parse(command.Parameters["@ReturnValue"].Value.ToString());
                }
            }

            return result;
        }

        public SqlDataReader ExecuteReader(SqlConnection connection, TcStoredProcedure procedure)
        {
            SqlDataReader reader = null;

            using (SqlCommand command = procedure.GetCommand())
            {
                command.Connection = connection;

                connection.Open();
                reader = command.ExecuteReader();
            }

            return reader;
        }
    }
}
