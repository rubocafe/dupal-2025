using LucidPayroll.General;
using System.Data;
using System.Data.SqlClient;

// Harshan Nishantha
// 2013-02-28

namespace LucidLibrary.Db
{
    public class TcBeansLoader<T> where T:TcBean
    {
        public TcBindingList<T> GetBeans(TcDatabase database, TcStoredProcedure procedure, TcBean emptyBean)
        {
            TcBindingList<T> list = new TcBindingList<T>();

            using (SqlConnection connection = database.GetConnection())
            {
                using (SqlDataReader reader = database.ExecuteReader(connection, procedure))
                {
                    while (reader.Read())
                    {
                        T newObject = (T)emptyBean.NewObject();
                        newObject.LoadFromReader((IDataRecord)reader);
                        list.Add(newObject);
                    }
                }
            }

            return list;
        }
    }
}
