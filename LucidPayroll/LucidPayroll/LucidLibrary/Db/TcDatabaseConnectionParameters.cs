using System.Data.SqlClient;

// Harshan Nishantha
// 2013-04-01

namespace LucidLibrary.Db
{
    public class TcDatabaseConnectionParameters
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }

        public SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource      = DataSource;
            builder.InitialCatalog  = InitialCatalog;
            builder.UserID          = UserID;
            builder.Password        = Password;

            return builder;
        }

        public string GetDatabaseConnectionString()
        {
            SqlConnectionStringBuilder builder = GetConnectionStringBuilder();

            string connectionString = builder.ConnectionString;
            return connectionString;
        }

        public TcDatabaseConnectionParameters()
        {
        }
    }
}
