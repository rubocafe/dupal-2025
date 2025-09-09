using Searcher.TcLucene;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-03-26

namespace Searcher
{
    public partial class TcLuceneSearcherForm : Form
    {
        private TcLuceneIndexer indexer;
        private TcLuceneSearcher searcher;
        private BindingSource source = new BindingSource(); 

        public TcLuceneSearcherForm()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            source.DataSource = new List<TcLuceneDoc>();
            dataGridView.DataSource = source;
        }

        private void IndexData()
        {
            indexer = new TcLuceneIndexer("Lucene\\Index");
            indexer.AddDirectory(new System.IO.DirectoryInfo(@"C:\Users\harshan\Desktop\Searcher\Docs"), "*");
            indexer.AddDirectory(new System.IO.DirectoryInfo(@"E:\Shared\DMS\LuceneNetTutorial-1.1\LuceneNetTutorial\Documentation"), "*");
            indexer.Close();
        }

        private List<TcLuceneDoc> Search(string text)
        {
            searcher = new TcLuceneSearcher(indexer.IndexPath);
            List<TcLuceneDoc> results = searcher.search(text);

            return results;
        }

        private void UpdateStatus()
        {
            statusLabel.Text = string.Format("{0} result(s) found for word \"{1}\" in {2}", 
                searcher.Hits.TotalHits, searcher.SearchText, searcher.Duration);
        }

        private void TcLuceneSearcherForm_Load(object sender, EventArgs e)
        {
            try
            {
                IndexData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    List<TcLuceneDoc> results = new List<TcLuceneDoc>();
                    string text = searchTextBox.Text;

                    if (!string.IsNullOrEmpty(text))
                    {
                        results = Search(text);
                        UpdateStatus();
                    }
                    else
                    {
                        statusLabel.Text = string.Empty;
                    }

                    source.DataSource = results;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
