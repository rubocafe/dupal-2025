using Payroll.Library.Excel;
using Payroll.Library.General;
using Payroll.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-11-02

namespace Payroll.Library.MetaData
{
    public abstract class TaMetaDataBase<T> where T : TcPropertyNameRow
    {
        public List<string> RequiredProperties = new List<string>();

        public Dictionary<string, T> Data { get; set; }
        public HashSet<string> ExistingProperties { get; set; }

        public TaMetaDataBase()
        {
            if (RequiredProperties.Count == 0)
            {
                AddRequiredProperties();
            }

            Data = new Dictionary<string, T>();
            ExistingProperties = new HashSet<string>();
        }

        public abstract void AddRequiredProperties();
        public abstract List<string> GetColumnNames();
        public abstract string GetKey(TcExcelTableRow row);
        public abstract T GetValue(int index, TcExcelTableRow row);
        public abstract object GetVariableValue(string key);
        public abstract void LoadToVariables();

        public List<string> GetDataColumnNames()
        {
            var list = Data.Values
                .OrderBy(r => r.Index)
                .Select(r => r.Name)
                .ToList();

            return list;
        }

        public List<T> GetDataColumns()
        {
            var list = Data.Values
                .OrderBy(v => v.Index)
                .ToList();

            return list;
        }

        public void Load(string path, string sheet)
        {
            var columns = GetColumnNames();
            TcExcelReader reader = new TcExcelReader(path, sheet, 0, columns);

            if (!reader.FileExists)
            {
                throw new Exception(string.Format("Meta Data file [{0}] does not exist to load {1}", path, sheet));
            }

            if (!reader.TableExists)
            {
                throw new Exception(string.Format("There are no any data in the sheet \"{0}\" to read", sheet));
            }

            if (!reader.ColumnsState.Succeeded)
            {
                throw new Exception(string.Format("{0}{1} sheet of file [{2}]{3}{4}",
                    "Some required columns not found in the ", sheet, path, Environment.NewLine, reader.ColumnsState.Message));
            }

            var table = reader.Table;
            int index = 1;
            foreach (var row in table.Rows)
            {
                var key = GetKey(row);
                var value = GetValue(index, row);
                AddData(key, value);
                if (!ExistingProperties.Contains(key))
                {
                    ExistingProperties.Add(key);
                }

                index++;
            }

            LoadToVariables();

            TcOperationState state = Validate(sheet, path);
            if (!state.Succeeded)
            {
                throw new Exception(state.Message);
            }
        }

        public virtual TcOperationState Validate(string sheet, string path)
        {
            TcOperationState state = new TcOperationState();

            var requiredPropertiesNotFound = "";
            foreach (var property in RequiredProperties)
            {
                if (!ExistingProperties.Contains(property))
                {
                    requiredPropertiesNotFound += string.Format("{0}, ", property);
                }
            }

            if (!string.IsNullOrEmpty(requiredPropertiesNotFound))
            {
                requiredPropertiesNotFound.TrimEnd(new char[] { ' ', ',' });
                state.Succeeded = false;
                state.Message = string.Format("{0}{1} sheet of file {2}{3}{4}",
                    "Some required Properties not found in the ", sheet, path, Environment.NewLine, requiredPropertiesNotFound);
                return state;
            }

            state.Succeeded = true;
            return state;
        }

        protected void AddData(string key, T value)
        {
            if (Data.ContainsKey(key))
            {
                Data[key] = value;
            }
            else
            {
                Data.Add(key, value);
            }
        }

        protected T GetData(string key)
        {
            T value = default(T);

            if (Data.ContainsKey(key))
            {
                value = Data[key];
            }

            return value;
        }

        protected object Get(string key)
        {
            object value = null;

            if (Data.ContainsKey(key))
            {
                value = GetVariableValue(key);
            }

            return value;
        }
    }
}
