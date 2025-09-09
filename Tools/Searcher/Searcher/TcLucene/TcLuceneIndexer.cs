using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Searcher.TcLucene.Readers;
using System;
using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2014-03-25

namespace Searcher.TcLucene
{
    public class TcLuceneIndexer
    {
        private IndexWriter writer;

        public string IndexPath { get; set; }

		/// <summary>
		/// Creates a new index in <c>directory</c>. Overwrites the existing index in that directory.
		/// </summary>
		/// <param name="indexPath">Path to index (will be created if not existing).</param>
        public TcLuceneIndexer(string indexPath)
		{
            IndexPath = indexPath;

            writer = new IndexWriter
                (FSDirectory.Open(indexPath), 
                new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), 
                true, 
                IndexWriter.MaxFieldLength.LIMITED);

			writer.UseCompoundFile = true;
		}

		/// <summary>
		/// Add files from <c>directory</c> and its subdirectories that match <c>pattern</c>.
		/// </summary>
		/// <param name="directory">Directory with the files.</param>
		/// <param name="pattern">Search pattern, e.g. <c>"*.html"</c></param>
		public void AddDirectory(DirectoryInfo directory, string pattern)
		{
			AddSubDirectory(directory, pattern);
		}

        private void AddSubDirectory(DirectoryInfo directory, string pattern)
		{
			foreach (FileInfo file in directory.GetFiles(pattern))
			{
				AddDocument(file.FullName);
			}

			foreach (DirectoryInfo subDirectory in directory.GetDirectories())
			{
				AddSubDirectory(subDirectory, pattern);
			}
		}

		/// <summary>
		/// Loads, parses and indexes an file.
		/// </summary>
		/// <param name="path"></param>
		public void AddDocument(string path)
		{
            List<TcLuceneDoc> list = TcLuceneDocReader.GetDocs(path);
            foreach (TcLuceneDoc doc in list)
            {
                writer.AddDocument(doc.GetDocument());
            }
		}

		/// <summary>
		/// Optimizes and save the index.
		/// </summary>
		public void Close()
		{
			writer.Optimize();
			writer.Dispose();
		}

        public List<TcLuceneDoc> Read()
        {
            List<TcLuceneDoc> list = new List<TcLuceneDoc>();

            IndexReader reader = IndexReader.Open(FSDirectory.Open(IndexPath), true);
            int num = reader.NumDocs();
            for ( int i = 0; i < num; i++)
            {
                if (!reader.IsDeleted(i))
                {
                    Document document = reader.Document(i);
                    list.Add(TcLuceneDoc.LoadFromDocument(document));
                }
            }

            reader.Dispose();

            return list;
        }
    }
}
