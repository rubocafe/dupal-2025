using Payroll.Library.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-13

namespace Payroll.Library.MetaData
{
    public class TcMetaData
    {
        public string EmployerDataFilePath { get; set; }
        public string MetaDataFilePath { get; set; }

        public TcEmployerMetaData EmployerData { get; set; }
        public TcSettingsMetaData Settings { get; set; }
        public TcMasterMetaData MasterData { get; set; }
        public TcSalaryMetaData Salary { get; set; }

        public TcMetaData(string employerDataFilePath, string metaDataFilePath)
        {
            EmployerDataFilePath    = employerDataFilePath;
            MetaDataFilePath        = metaDataFilePath;

            EmployerData    = new TcEmployerMetaData();
            Settings        = new TcSettingsMetaData();
            MasterData      = new TcMasterMetaData();
            Salary          = new TcSalaryMetaData();
        }

        public void Load()
        {
            EmployerData.Load(EmployerDataFilePath);
            Settings.Load(MetaDataFilePath);
            MasterData.Load(MetaDataFilePath);
            Salary.Load(MetaDataFilePath);
        }
    }
}
