using LucidPayroll.General;
using System.Data;
using System.Data.SqlClient;

// Harshan Nishantha
// 2013-02-28

namespace LucidLibrary.Db
{
    public class TcViewsLoader<T> where T:TcView
    {
        public TcBindingList<T> GetViews(TcDatabase database, TcStoredProcedure procedure, TcView emptyView)
        {
            TcBindingList<T> list = new TcBindingList<T>();

            using (SqlConnection connection = database.GetConnection())
            {
                using (SqlDataReader reader = database.ExecuteReader(connection, procedure))
                {
                    while (reader.Read())
                    {
                        T newObject = (T)emptyView.NewObject();
                        newObject.LoadFromReader((IDataRecord)reader);
                        list.Add(newObject);
                    }
                }
            }

            return list;
        }
    }
}
