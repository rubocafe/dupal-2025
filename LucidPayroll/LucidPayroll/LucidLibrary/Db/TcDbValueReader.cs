using System;
using System.Data;

// Harshan Nishantha
// 2014-01-22

namespace LucidLibrary.Db
{
    public class TcDbValueReader
    {
        private static object ReadObject(IDataRecord record, string key)
        {
            object value = record[key];

            if (value == DBNull.Value)
            {
                value = null;
            }

            return value;
        }

        public static T Read<T>(IDataRecord record, string key)
        {
            object obj = ReadObject(record, key);

            T value = default(T);
            if (obj != null)
            {
                value = (T)obj;
            }

            return value;
        }
    }
}
