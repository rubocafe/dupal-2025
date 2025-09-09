using Payroll.Library;
using Payroll.UI.Controls;

// Harshan Nishantha
// 2014-01-02

namespace Payroll.Library.Etf
{
    public class TcEtfFileValidator
    {
        public TcEtfFile File { get; set; }
        public bool Valid { get; set; }
        public TcBindingList<TcEtfDetailRow> ValidRows { get; set; }
        public TcBindingList<TcEtfDetailRow> InvalidRows { get; set; }

        public TcEtfFileValidator(TcEtfFile file)
        {
            File = file;

            ValidRows   = new TcBindingList<TcEtfDetailRow>();
            InvalidRows = new TcBindingList<TcEtfDetailRow>();
        }

        public bool Validate()
        {
            Valid = false;
            ValidRows.Clear();
            InvalidRows.Clear();

            foreach (TcEtfDetailRow row in File.Rows)
            {
                if (row.IsValid())
                {
                    ValidRows.Add(row);
                }
                else
                {
                    InvalidRows.Add(row);
                }
            }

            Valid = InvalidRows.Count > 0 ? false : true;

            return Valid;
        }

        public string GetHeaderText()
        {
            string text = string.Format("Total Rows: {0}, Valid: {1}, Invalid {2}", File.Rows.Count, ValidRows.Count, InvalidRows.Count);

            return text;
        }

        public TcBindingList<TeEtfError> GetAllErrors()
        {
            TcBindingList<TeEtfError> errors = new TcBindingList<TeEtfError>();

            foreach (TcEtfDetailRow row in InvalidRows)
            {
                foreach (TeEtfError error in row.Errors.Keys)
                {
                    if (!errors.Contains(error))
                    {
                        errors.Add(error);
                    }
                }
            }

            return errors;
        }

        public TcBindingList<TcEtfDetailRow> GetAllRows()
        {
            return File.Rows; 
        }

        public TcBindingList<TcEtfDetailRow> GetRowsWithError(TeEtfError error)
        {
            TcBindingList<TcEtfDetailRow> rows = new TcBindingList<TcEtfDetailRow>();

            if (error == TeEtfError.All)
            {
                rows = GetAllRows();
            }
            else if (error == TeEtfError.Valid)
            {
                rows = ValidRows;
            }
            else
            {
                foreach (TcEtfDetailRow row in InvalidRows)
                {
                    if (row.Errors.ContainsKey(error))
                    {
                        rows.Add(row);
                    }
                }
            }

            return rows;
        }

        public decimal GetAllRowsTotal()
        {
            return GetTotal(GetAllRows());
        }

        public decimal GetValidRowsTotal()
        {
            return GetTotal(ValidRows);
        }

        public decimal GetInvalidRowsTotal()
        {
            return GetTotal(InvalidRows);
        }

        private decimal GetTotal(TcBindingList<TcEtfDetailRow> rows)
        {
            decimal total = 0m;

            foreach (TcEtfDetailRow row in rows)
            {
                total += row.TotalContribution;
            }

            return total;
        }
    }
}
