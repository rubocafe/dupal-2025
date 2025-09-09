using Payroll.Library.Excel;
using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-11-06

namespace Payroll.Library.MetaData
{
    public class TcSettingsMetaData : TaMetaDataBase<TcEmployerMetaDataRow>
    {
        public bool HasETF { get; set; }
        public bool HasEPF { get; set; }
        public decimal EPFDeductionPercentage { get; set; }
        public decimal EPFContributionPercentage { get; set; }
        public decimal ETFContributionPercentage { get; set; }

        public TcSettingsMetaData() : 
            base()
        {
        }

        public void Load(string path)
        {
            Load(path, "Settings");
        }

        public override void AddRequiredProperties()
        {
            RequiredProperties.Add(TcPropertyNames.HasETF);
            RequiredProperties.Add(TcPropertyNames.HasEPF);
            RequiredProperties.Add(TcPropertyNames.EPFDeductionPercentage);
            RequiredProperties.Add(TcPropertyNames.EPFContributionPercentage);
            RequiredProperties.Add(TcPropertyNames.ETFContributionPercentage);
        }

        public override void LoadToVariables()
        {
            object value = GetVariableValue(TcPropertyNames.HasETF);
            HasETF = TcExcelValueDecorder.GetBool(value);

            value = GetVariableValue(TcPropertyNames.HasEPF);
            HasEPF = TcExcelValueDecorder.GetBool(value);

            value = GetVariableValue(TcPropertyNames.EPFDeductionPercentage);
            EPFDeductionPercentage = TcExcelValueDecorder.GetDecimal(value);

            value = GetVariableValue(TcPropertyNames.EPFContributionPercentage);
            EPFContributionPercentage = TcExcelValueDecorder.GetDecimal(value);

            value = GetVariableValue(TcPropertyNames.ETFContributionPercentage);
            ETFContributionPercentage = TcExcelValueDecorder.GetDecimal(value);

            Clean();
        }

        private void Clean()
        {
        }

        public override List<string> GetColumnNames()
        {
            List<string> columns = new List<string>();

            columns.Add("KEY");
            columns.Add("VALUE");

            return columns;
        }

        public override string GetKey(TcExcelTableRow row)
        {
            var key = row.GetCell("KEY").StringValue();
            if (!string.IsNullOrEmpty(key))
            {
                key = key.ToUpper();
            }

            return key;     
        }

        public override TcEmployerMetaDataRow GetValue(int index, TcExcelTableRow row)
        {
            var key     = row.GetCell("KEY").StringValue();
            var value   = row.GetCell("VALUE").StringValue();

            var newRow = TcEmployerMetaDataRow.New(index, key, value);

            return newRow;
        }

        public override object GetVariableValue(string key)
        {
            object value = null;

            var x = GetData(key);
            if (x != null)
            {
                value = x.Value;
            }

            return value;
        }
    }
}
