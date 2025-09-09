using System.Data;

// Harshan Nishantha
// 2013-03-20

namespace LucidLibrary.Db
{
    public abstract class TcView
    {
        public abstract void LoadFromReader(IDataRecord record);
        public abstract object NewObject();
    }
}
