using Payroll.Library.Csv;
using Payroll.Library.Date;
using Payroll.Library.Excel;
using Payroll.Library.General;
using Payroll.Library.MetaData;
using Payroll.UI.Controls;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2015-11-05

namespace Payroll.UI.Business.Salary
{
    public class TcBusinessSalaryTable
    {
        public TcSalaryMetaData MetaData { get; set; }
        public string FilePath { get; set; }
        public List<TcBusinessSalaryRow> Rows { get; set; }

        public TcBusinessSalaryTable(TcSalaryMetaData salaryMetaData, string filePath)
        {
            MetaData    = salaryMetaData;
            FilePath    = filePath;
            Rows        = new List<TcBusinessSalaryRow>();
        }

        public void Load()
        {
            var columns = MetaData.GetDataColumnNames();
            TcExcelReader reader = new TcExcelReader(FilePath, "Sheet1", 0, columns, true);

            if (!reader.State.Succeeded)
            {
                throw new Exception(reader.State.Message);
            }

            int index = 1;
            foreach (var row in reader.Table.Rows)
            {
                TcBusinessSalaryRow dataRow = new TcBusinessSalaryRow();
                dataRow.LoadFrom(index, row, MetaData);
                Rows.Add(dataRow);
                index++;
            }
        }

        public List<object[]> GetAsObjectArray(List<TcBusinessSalaryRow> rows)
        {
            List<object[]> all = new List<object[]>();

            var columns = MetaData.GetDataColumns();
            var properties = TcReflection.GetPublicReadWriteProperties(typeof(TcBusinessSalaryRow));

            foreach (var row in rows)
            {
                var data = new object[columns.Count + 1];

                data[0] = row.LineNumber;
                for (int i = 0; i < columns.Count; i++)
                {
                    var column = columns[i];
                    if (properties.ContainsKey(column.PropertyName))
                    {
                        var property = properties[column.PropertyName];
                        data[i + 1] = TcReflection.GetPropValue(row, property);
                    }
                    else
                    {
                        data[i + 1] = row.GetValue(column.UpperName, column.Type);
                    }
                }

                all.Add(data);
            }

            return all;
        }
    }
}
