using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.Excel
{
    public class TcExcel
    {
        //private static string xlsConnectionString =
        //    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;\";{1}";
        //private static string xlsxConnectionString =
        //    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;\";{1}";

        //private static string xlsConnectionString =
        //    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;ReadOnly=true\";";
        //private static string xlsxConnectionString =
        //    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;ReadOnly=true\";";
        private string connectionString = string.Empty;

        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public string Mode { get; set; }

        public DataSet Data { get; set; }

        public TcExcel(string filePath, bool readOnly)
        {
            FilePath = filePath;

            if (readOnly)
            {
                Mode = "Mode=Read;";
            }
            else
            {
                Mode = "";
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                FileExtension = Path.GetExtension(filePath);
            }

            Data = new DataSet();
        }

        public DataSet ReadSheet(string sheetName, bool readFirstSheet = false)
        {
            if (TcFile.IsFileLocked(FilePath))
            {
                throw new Exception(String.Format("File [{0}] is not readable, it may already be open in another program", FilePath));
            }
            Data = new DataSet();

            SetConnectionString();

            //using (OleDbConnection connection1 = new OleDbConnection(connectionString))
            //{
            //    connection1.Open();
            //    DataTable dt = new DataTable();

            //    dt = connection1.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //    if (dt == null)
            //    {
            //        return null;
            //    }

            //    String[] excelSheets = new String[dt.Rows.Count];
            //    int t = 0;
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        excelSheets[t] = row["TABLE_NAME"].ToString();
            //        t++;
            //    }

            //    using (OleDbConnection connection2 = new OleDbConnection(connectionString))
            //    {
            //        string query = string.Format("Select * from [{0}$]", sheetName);
            //        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection2))
            //        {
            //            dataAdapter.Fill(Data);
            //        }
            //    }
            //}

            var tableName = string.Format("{0}$", sheetName);
            if (readFirstSheet)
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    var dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            tableName = row["TABLE_NAME"].ToString(); ;
                            break;
                        }
                    }
                }
            }

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = string.Format("Select * from [{0}]", tableName);
                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection))
                {
                    dataAdapter.Fill(Data);
                }
            }

            return Data;
        }

        private void SetConnectionString()
        {
            //if (FileExtension == ".xls")
            //{
            //    connectionString = string.Format(xlsConnectionString, FilePath, Mode);
            //}
            //else if (FileExtension == ".xlsx")
            //{
            //    connectionString = string.Format(xlsxConnectionString, FilePath, Mode);
            //}

            // connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;\";{1}", FilePath, Mode);
            connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;ImportMixedTypes=Text\";{1}", FilePath, Mode);
        }

        public DataTable GetDataTable(int index)
        {
            DataTable table = null;

            if (Data != null && Data.Tables.Count > index)
            {
                table = Data.Tables[index];
            }

            return table;
        }

        //public void Insert(TcWorkType wt, int sheet)
        //{
        //    SetConnectionString();
        //    using (OleDbConnection connection = new OleDbConnection(connectionString))
        //    {
        //        var command = wt.UpdateCommand(sheet);
        //        connection.Open();
        //        command.Connection = connection;
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //}
    }
}
