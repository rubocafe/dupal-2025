using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Harshan Nishantha
// 2014-03-25

namespace Searcher.TcLucene.Readers
{
    public class TcPdfFileReader
    {
        public static bool Type(string extension)
        {
            if (extension == ".PDF")
            {
                return true;
            }

            return false;
        }

        public List<TcLuceneDoc> GetDocs(string path)
        {
            List<TcLuceneDoc> list = new List<TcLuceneDoc>();

            string text = ExtractTextFromPdf(path);
            TcLuceneDoc doc = new TcLuceneDoc(TcLuceneDocType.Pdf.ToString(), path, text);
            list.Add(doc);

            return list;
        }

        private string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.AppendLine(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }
    }
}
