using LucidLibrary.Db;

// Harshan Nishantha
// 2014-01-22

namespace LucidPayroll.Database
{
    public interface TiDatabaseConnectionParameters
    {
        bool Exists();
        TcDatabaseConnectionParameters Read();
        void Write(TcDatabaseConnectionParameters parameters);
        void Delete();
    }
}
