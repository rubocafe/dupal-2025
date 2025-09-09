using DUPALPayroll.General;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-11-20

namespace DUPALPayroll.UI.Common.AnalyzeBean
{
    public class TcAnalyzeFormHelper
    {
        public static void AddPayMasterInfoToStatus(ComboBox filterComboBox, Label statusLabel)
        {
            TeEmployeeAnalyzeFilter filter = TcEnum.GetEnumForText<TeEmployeeAnalyzeFilter>(TeEmployeeAnalyzeFilter.All, filterComboBox.Text);

            if (filter == TeEmployeeAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found ||
                filter == TeEmployeeAnalyzeFilter.Employee_Bank_Account_Number_Invalid ||
                filter == TeEmployeeAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster)
            {
                statusLabel.Text += ". These record(s) will be excluded from PayMaster";
                TcTheme.DisplayErrorLabel(statusLabel, statusLabel.Text);
            }
            else
            {
                TcTheme.DisplayInfoLabel(statusLabel, statusLabel.Text);
            }
        }
    }
}
