using DUPALPayroll.General;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common;
using System.IO;

// Harshan Nishantha
// 2014-01-09

namespace DUPALPayroll.UI.Configuration.ConfigFile
{
    public class TcConfigurationFile
    {
        public string               FilePath { get; private set; }
        public bool                 Exists { get; private set; }
        public TcYearMonth          WorkingYearMonth { get; private set; }
        public bool                 IsSupported { get; private set; }
        public TcConfigurations     Configurations { get; set; }

        public TcConfigurationFile()
        {
            Reload();
        }

        public void Reload()
        {
            FilePath = TcPaths.GetConfigFilePath();
            if (File.Exists(FilePath))
            {
                Exists = true;
            }
            else
            {
                Exists = false;
            }

            WorkingYearMonth = TcSettings.WorkingYearMonth;
            if (TcVersions.IsConfigFileSupported(WorkingYearMonth))
            {
                IsSupported = true;
            }
            else
            {
                IsSupported = false;
            }

            if (Exists && IsSupported)
            {
                Configurations = TcConfigurations.Read(FilePath);
            }
            else
            {
                Configurations = new TcConfigurations(); // Defaults
            }

            Configurations.Clean();
        }

        public void Write()
        {
            if (Configurations != null)
            {
                Configurations.Write(FilePath);
            }
        }

        public static bool ValidForWorkingYearMonth()
        {
            TcConfigurationFile file = new TcConfigurationFile();

            if (TcVersions.IsConfigFileSupported(file.WorkingYearMonth))
            {
                if (!file.Exists)
                {
                    TcMessageBox.ShowWarning(
                        string.Format("Configuration file does not exist for [{0}]", 
                        file.WorkingYearMonth.ToString()));
                }
                else
                {
                    if (!file.Configurations.IsValid(false))
                    {
                        TcMessageBox.ShowWarning(
                            string.Format("Some configurations are invalid in configuration file of [{0}]", 
                            file.WorkingYearMonth.ToString()));
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }

            return false;
        }
    }
}
