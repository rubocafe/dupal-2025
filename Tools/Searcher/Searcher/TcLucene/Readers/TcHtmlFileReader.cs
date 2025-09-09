using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

// Harshan Nishantha
// 2014-03-25

namespace Searcher.TcLucene.Readers
{
    public class TcHtmlFileReader
    {
        public static bool Type(string extension)
        {
            if (extension == ".HTM" || extension == ".HTML")
            {
                return true;
            }

            return false;
        }

        public List<TcLuceneDoc> GetDocs(string path)
        {
            List<TcLuceneDoc> list = new List<TcLuceneDoc>();

            string text = File.ReadAllText(path);
            text = ParseHtml(text);
            TcLuceneDoc doc = new TcLuceneDoc(TcLuceneDocType.Html.ToString(), path, text);
            list.Add(doc);

            return list;
        }

        private string ParseHtml(string html)
        {
            string temp = Regex.Replace(html, "<[^>]*>", "");
            temp = temp.Replace("&nbsp;", " ");

            return temp;
        }
    }
}
