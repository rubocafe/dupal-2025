using System.Data;

// Harshan Nishantha
// 2013-02-28

namespace LucidLibrary.Db
{
    public abstract class TcBean
    {
        public bool ExistsInDatabase { get; set; }

        public TcBean()
        {
            ExistsInDatabase = false;
        }

        public abstract void LoadFromReader(IDataRecord record);
        public abstract object NewObject();
        protected abstract void Insert();
        protected abstract void Update();
        public abstract void Delete();
        public abstract void Reload();

        public virtual void Save()
        {
            if (ExistsInDatabase)
            {
                Update();
            }
            else
            {
                Insert();
            }
        }
    }
}
