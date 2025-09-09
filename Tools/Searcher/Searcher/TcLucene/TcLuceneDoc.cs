using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2014-03-25

namespace Searcher.TcLucene
{
    public class TcLuceneDoc
    {
        public string Type { get; set; }
        public string Path { get; set; }
        public string Text { get; set; }
        public float Score { get; set; }

        public TcLuceneDoc() 
            : this(string.Empty, string.Empty, string.Empty)
        {
        }

        public TcLuceneDoc(string type, string path, string text)
        {
            Type = type;
            Path = path;
            Text = text;
            Score = 0;
        }

        public Document GetDocument()
        {
            Document doc = new Document();

            doc.Add(new Field("type", Type, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("path", Path, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("text", Text, Field.Store.YES, Field.Index.ANALYZED));

            return doc;
        }

        public static TcLuceneDoc LoadFromDocument(Document document)
        {
            TcLuceneDoc luceneDoc = new TcLuceneDoc();

            luceneDoc.Type = document.Get("type");
            luceneDoc.Path = document.Get("path");
            luceneDoc.Text = document.Get("text");

            return luceneDoc;
        }
    }
}
