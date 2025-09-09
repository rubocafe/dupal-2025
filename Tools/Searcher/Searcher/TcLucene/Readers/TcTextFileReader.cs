using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2014-03-25

namespace Searcher.TcLucene.Readers
{
    public class TcTextFileReader
    {
        public static bool Type(string extension)
        {
            if (extension == ".TXT")
            {
                return true;
            }

            return false;
        }

        public List<TcLuceneDoc> GetDocs(string path)
        {
            List<TcLuceneDoc> list = new List<TcLuceneDoc>();

            string text = File.ReadAllText(path);
            TcLuceneDoc doc = new TcLuceneDoc(TcLuceneDocType.Text.ToString(), path, text);
            list.Add(doc);

            return list;
        }
    }
}
