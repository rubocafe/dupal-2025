using Payroll.Library.Date;
using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.Model
{
    public class TcPayrollFolderStructure
    {
        public string RootFolder { get; set; }
        public bool Exists { get; set; }

        public TcPayrollFolderStructure(string rootFolder)
        {
            RootFolder    = rootFolder;
        }

        public List<string> GetCompanies()
        {
            List<string> list = new List<string>();
            if (Directory.Exists(RootFolder))
            {
                var companies = Directory.GetDirectories(RootFolder, "*", SearchOption.TopDirectoryOnly);
                foreach (var company in companies)
                {
                    var companyName = TcDirectory.GetName(company);
                    list.Add(companyName);
                }
            }
            list = list.OrderBy(c => c).ToList();
            return list;
        }

        public List<string> GetCustomers(string company, string yearMonth)
        {
            List<string> list = new List<string>();
            var folderPath = Path.Combine(RootFolder, company, yearMonth);
            if (Directory.Exists(folderPath))
            {
                var customers = Directory.GetDirectories(folderPath, "*", SearchOption.TopDirectoryOnly);
                foreach (var customer in customers)
                {
                    var customerName = TcDirectory.GetName(customer);
                    if (customerName != "Shared")
                    {
                        list.Add(customerName);
                    }
                }
            }

            list = list.OrderBy(c => c).ToList();
            return list;
        }

        public List<string> GetBusinesses(string company, string yearMonth, string customer)
        {
            List<string> list = new List<string>();
            var folderPath = Path.Combine(RootFolder, company, yearMonth, customer);
            if (Directory.Exists(folderPath))
            {
                var businesses = Directory.GetDirectories(folderPath, "*", SearchOption.TopDirectoryOnly);
                var settingsFolderName = TcPaths.GetCutomerSettingsFolderName();
                var outputFolderName = TcPaths.GetCustomerOutputFolderName();
                foreach (var business in businesses)
                {
                    var businessName = TcDirectory.GetName(business);
                    if (business.EndsWith(settingsFolderName) || business.EndsWith(outputFolderName))
                    {
                        continue;
                    }
                    list.Add(businessName);
                }
            }

            list = list.OrderBy(c => c).ToList();
            return list;
        }
    }
}
