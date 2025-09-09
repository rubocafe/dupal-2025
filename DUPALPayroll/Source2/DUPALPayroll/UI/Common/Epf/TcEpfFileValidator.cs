using DUPALPayroll.Library;
using System.Collections;
using System.Collections.Generic;

// Harshan Nishantha
// 2014-01-01

namespace DUPALPayroll.UI.Common.Epf
{
    public class TcEpfFileValidator
    {
        public TcEpfFile File { get; set; }
        public bool Valid { get; set; }
        public TcBindingList<TcEpfRow> ValidRows { get; set; }
        public TcBindingList<TcEpfRow> InvalidRows { get; set; }

        public TcEpfFileValidator(TcEpfFile file)
        {
            File = file;

            ValidRows   = new TcBindingList<TcEpfRow>();
            InvalidRows = new TcBindingList<TcEpfRow>();
        }

        public bool Validate()
        {
            Valid = false;
            ValidRows.Clear();
            InvalidRows.Clear();

            foreach (TcEpfRow row in File.Rows)
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

        public TcBindingList<TeEpfError> GetAllErrors()
        {
            TcBindingList<TeEpfError> errors = new TcBindingList<TeEpfError>();

            foreach (TcEpfRow row in InvalidRows)
            {
                foreach (TeEpfError error in row.Errors.Keys)
                {
                    if (!errors.Contains(error))
                    {
                        errors.Add(error);
                    }
                }
            }

            return errors;
        }

        public TcBindingList<TcEpfRow> GetAllRows()
        {
            return File.Rows; 
        }

        public TcBindingList<TcEpfRow> GetRowsWithError(TeEpfError error)
        {
            TcBindingList<TcEpfRow> rows = new TcBindingList<TcEpfRow>();

            if (error == TeEpfError.All)
            {
                rows = GetAllRows();
            }
            else if (error == TeEpfError.Valid)
            {
                rows = ValidRows;
            }
            else
            {
                foreach (TcEpfRow row in InvalidRows)
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

        private decimal GetTotal(TcBindingList<TcEpfRow> rows)
        {
            decimal total = 0m;

            foreach (TcEpfRow row in rows)
            {
                total += row.TotalContribution;
            }

            return total;
        }
    }
}
