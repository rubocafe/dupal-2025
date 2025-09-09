using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2014-03-25

namespace Searcher.TcLucene.Readers
{
    public enum TcLuceneDocType
    {
        Text,
        Html,
        Pdf,
        MS_Word,
        MS_Excel,
        Unknown
    }

    public class TcLuceneDocReader
    {
        public static List<TcLuceneDoc> GetDocs(string path)
        {
            List<TcLuceneDoc> list = new List<TcLuceneDoc>();

            string extension = GetExtension(path);
            if (!string.IsNullOrEmpty(extension))
            {
                if (TcTextFileReader.Type(extension))
                {
                    TcTextFileReader reader = new TcTextFileReader();
                    list = reader.GetDocs(path);
                }
                else if (TcHtmlFileReader.Type(extension))
                {
                    TcHtmlFileReader reader = new TcHtmlFileReader();
                    list = reader.GetDocs(path);
                }
                else if (TcPdfFileReader.Type(extension))
                {
                    TcPdfFileReader reader = new TcPdfFileReader();
                    list = reader.GetDocs(path);
                }
            }

            return list;
        }

        private static string GetExtension(string path)
        {
            string extension = "";
            FileInfo info = new FileInfo(path);
            if (!string.IsNullOrEmpty(info.Extension))
            {
                extension = info.Extension.ToUpper();
            }

            return extension;
        }
    }
}
