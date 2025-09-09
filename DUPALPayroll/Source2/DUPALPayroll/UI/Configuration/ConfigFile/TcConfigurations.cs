using DUPALPayroll.General;
using DUPALPayroll.Library.Sys;
using System.Xml;

// Harshan Nishantha
// 2014-01-09

namespace DUPALPayroll.UI.Configuration.ConfigFile
{
    public class TcConfigurations
    {
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ZoneCode { get; set; }
        public string EmployeeNumber { get; set; }

        public TcConfigurations()
        {
            BranchCode      = "011";
            AccountNumber   = "001108629401";
            AccountName     = "DU PAL (PVT) LTD";
            ZoneCode        = "A";
            EmployeeNumber  = "046216";
        }

        public bool IsValid(bool showMessage)
        {
            if (!TcValidator.IsValidBranchCode(BranchCode))
            {
                if (showMessage)
                {
                    TcMessageBox.ShowWarning(string.Format("Branch Code [{0}] is invalid", BranchCode));
                }
                return false;
            }

            if (!TcValidator.IsValidBankAccountNumber(AccountNumber))
            {
                if (showMessage)
                {
                    TcMessageBox.ShowWarning(string.Format("Account Number [{0}] is invalid", AccountNumber));
                }
                return false;
            }

            if (string.IsNullOrEmpty(AccountName) ||
                (!string.IsNullOrEmpty(AccountName) && AccountName.Length > 20))
            {
                if (showMessage)
                {
                    TcMessageBox.ShowWarning(string.Format("Account Name [{0}] is invalid", AccountName));
                }
                return false;
            }

            if (!TcValidator.IsValidZoneCode(ZoneCode))
            {
                if (showMessage)
                {
                    TcMessageBox.ShowWarning(string.Format("Zone Code [{0}] is invalid", ZoneCode));
                }
                return false;
            }

            if (!TcValidator.IsValidEmployeeOrEmployerNumber(EmployeeNumber))
            {
                if (showMessage)
                {
                    TcMessageBox.ShowWarning(string.Format("Employee Number [{0}] is invalid", EmployeeNumber));
                }
                return false;
            }

            return true;
        }

        public void Clean()
        {
            BranchCode      = TcFormatter.GetFormattedBranchCode(BranchCode);
            AccountNumber   = TcFormatter.GetFormattedBankAccountNumber(AccountNumber);
            AccountName     = TcFormatter.TrimAndUpper(AccountName);
            ZoneCode        = TcFormatter.GetFormattedZoneCode(ZoneCode);
            EmployeeNumber  = TcFormatter.TrimAndUpper(EmployeeNumber);
        }

        public static TcConfigurations Read(string filePath)
        {
            TcConfigurations config = new TcConfigurations();

            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            config.BranchCode       = ReadValue(document, "BranchCode");
            config.AccountNumber    = ReadValue(document, "AccountNumber");
            config.AccountName      = ReadValue(document, "AccountName");
            config.ZoneCode         = ReadValue(document, "ZoneCode");
            config.EmployeeNumber   = ReadValue(document, "EmployeeNumber");

            return config;
        }

        private static string ReadValue(XmlDocument document, string xpathFromRoot)
        {
            string value = "";
            string xpath = string.Format("/Configuration/{0}", xpathFromRoot);
            XmlNode node = document.SelectSingleNode(xpath);
            if (node != null)
            {
                value = node.InnerText;
            }

            return value;
        }

        public void Write(string filePath)
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("Configuration");
            document.AppendChild(root);

            AddElement(document, root, "BranchCode", BranchCode);
            AddElement(document, root, "AccountNumber", AccountNumber);
            AddElement(document, root, "AccountName", AccountName);
            AddElement(document, root, "ZoneCode", ZoneCode);
            AddElement(document, root, "EmployeeNumber", EmployeeNumber);

            TcDirectory.CreateDirectoryOfFilePath(filePath);
            document.Save(filePath);
        }

        private void AddElement(XmlDocument document, XmlElement root, string elementName, string value)
        {
            XmlElement element = document.CreateElement(elementName);
            XmlText text = document.CreateTextNode(value);
            element.AppendChild(text);
            root.AppendChild(element);
        }
    }
}
