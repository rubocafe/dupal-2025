using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2014-03-26

namespace Searcher.TcLucene
{
    public class TcLuceneSearcher
    {
        private const int MAX_RESULTS_COUNT = 200;

        public string IndexDirectoryPath { get; set; }

        public string SearchText { get; set; }
        public TopDocs Hits { get; set; }
        public List<TcLuceneDoc> Results { get; set; }
        public TimeSpan Duration { get; set; }

        public TcLuceneSearcher(string indexDirectoryPath)
        {
            IndexDirectoryPath = indexDirectoryPath;

            SearchText = string.Empty;
            Results = new List<TcLuceneDoc>();
            Duration = TimeSpan.Zero;
        }

        public List<TcLuceneDoc> search(string searchText)
        {
            DateTime start = DateTime.Now;

            SearchText = searchText;
            Results.Clear();
            Duration = TimeSpan.Zero;

            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            IndexSearcher searcher = new IndexSearcher(FSDirectory.Open(IndexDirectoryPath));

            // parse the query, "text" is the default field to search
            var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "text", analyzer);
            Query query = parser.Parse(searchText);

            // search
            Hits = searcher.Search(query, MAX_RESULTS_COUNT);
            for (int i = 0; i < Hits.TotalHits; i++)
            {
                ScoreDoc scoreDoc = Hits.ScoreDocs[i];
                // get the document from index
                Document doc = searcher.Doc(scoreDoc.Doc);

                TcLuceneDoc luceneDoc = TcLuceneDoc.LoadFromDocument(doc);
                luceneDoc.Score = scoreDoc.Score;

                Results.Add(luceneDoc);
            }

            searcher.Dispose();

            Duration = DateTime.Now - start;

            return Results;
        }

        private string Highlight(Query query, string text)
        {
            // create highlighter
            IFormatter formatter = new SimpleHTMLFormatter("<span style=\"font-weight:bold;\">", "</span>");
            SimpleFragmenter fragmenter = new SimpleFragmenter(80);
            QueryScorer scorer = new QueryScorer(query);
            Highlighter highlighter = new Highlighter(formatter, scorer);
            highlighter.TextFragmenter = fragmenter;

            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            TokenStream stream = analyzer.TokenStream("", new StringReader(text));
            string sample = highlighter.GetBestFragments(stream, text, 2, "...");

            return sample;
        }
    }
}
