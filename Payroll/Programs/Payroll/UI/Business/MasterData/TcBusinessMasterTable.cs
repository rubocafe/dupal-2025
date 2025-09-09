using Payroll.Library.Excel;
using Payroll.Library.General;
using Payroll.Library.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-11-03

namespace Payroll.UI.Business.MasterData
{
    public class TcBusinessMasterTable
    {
        public TcMasterMetaData MetaData { get; set; }
        public string FilePath { get; set; }
        public List<TcBusinessMasterRow> Rows { get; set; }

        public TcBusinessMasterTable(TcMasterMetaData masterMetaData, string filePath)
        {
            MetaData    = masterMetaData;
            FilePath    = filePath;
            Rows        = new List<TcBusinessMasterRow>();
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
                TcBusinessMasterRow dataRow = new TcBusinessMasterRow();
                dataRow.LoadFrom(index, row);
                Rows.Add(dataRow);
                index++;
            }
        }

        public List<object[]> GetAsObjectArray(List<TcBusinessMasterRow> rows)
        {
            List<object[]> all = new List<object[]>();

            var columns = MetaData.GetDataColumns();
            var properties = TcReflection.GetPublicReadWriteProperties(typeof(TcBusinessMasterRow));

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
