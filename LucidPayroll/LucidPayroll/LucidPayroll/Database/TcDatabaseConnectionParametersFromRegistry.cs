using LucidLibrary.Db;
using LucidLibrary.Entries;
using LucidPayroll.General;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2014-01-22

namespace LucidPayroll.Database
{
    public class TcDatabaseConnectionParametersFromRegistry : TiDatabaseConnectionParameters
    {
        private RegistryHive hive = RegistryHive.CurrentUser;
        private string subKey = "Software\\Lucid Consulting\\PAYROLL\\Database";
        private TcDatabaseConnectionParameters parameters;

        private TcRegistryEntry dataSourceEntry;
        private TcRegistryEntry initialCatalogEntry;
        private TcRegistryEntry userIDEntry;
        private TcRegistryEntry passwordEntry;

        public TcDatabaseConnectionParametersFromRegistry()
        {
        }

        private void Init()
        {
            dataSourceEntry     = GetEntry("DataSource");
            initialCatalogEntry = GetEntry("InitialCatalog");
            userIDEntry         = GetEntry("UserID");
            passwordEntry       = GetEntry("Password");

            parameters = new TcDatabaseConnectionParameters();
        }

        private TcRegistryEntry GetEntry(string name)
        {
            TcRegistryEntry entry = new TcRegistryEntry(hive, subKey, name, RegistryValueKind.String);

            return entry;
        }

        private void ReadFromRegistry()
        {
            parameters.DataSource       = TcRegistryEntry.ReadAndDecrypt(dataSourceEntry, TcKeys.DATABASE_REGISTRY_SETTINGS_USER_KEY);
            parameters.InitialCatalog   = TcRegistryEntry.ReadAndDecrypt(initialCatalogEntry, TcKeys.DATABASE_REGISTRY_SETTINGS_USER_KEY);
            parameters.UserID           = TcRegistryEntry.ReadAndDecrypt(userIDEntry, TcKeys.DATABASE_REGISTRY_SETTINGS_USER_KEY);
            parameters.Password         = TcRegistryEntry.ReadAndDecrypt(passwordEntry, TcKeys.DATABASE_REGISTRY_SETTINGS_PASSWORD_KEY);
        }

        public void Write()
        {
            TcRegistryEntry.WriteAndEncrypt(dataSourceEntry, parameters.DataSource, TcKeys.DATABASE_REGISTRY_SETTINGS_USER_KEY);
            TcRegistryEntry.WriteAndEncrypt(initialCatalogEntry, parameters.InitialCatalog, TcKeys.DATABASE_REGISTRY_SETTINGS_USER_KEY);
            TcRegistryEntry.WriteAndEncrypt(userIDEntry, parameters.UserID, TcKeys.DATABASE_REGISTRY_SETTINGS_USER_KEY);
            TcRegistryEntry.WriteAndEncrypt(passwordEntry, parameters.Password, TcKeys.DATABASE_REGISTRY_SETTINGS_PASSWORD_KEY);
        }

        #region TiDatabaseConnectionParameters

        public bool Exists()
        {
            Read();

            bool exists = dataSourceEntry.Exists &&
                initialCatalogEntry.Exists &&
                userIDEntry.Exists &&
                passwordEntry.Exists;

            return exists;
        }

        public TcDatabaseConnectionParameters Read()
        {
            Init();
            ReadFromRegistry();

            return parameters;
        }

        public void Write(TcDatabaseConnectionParameters parameters)
        {
            Init();
            this.parameters = parameters;
            Write();
        }

        public void Delete()
        {
            Init();
            ReadFromRegistry();

            DeleteEntry(dataSourceEntry);
            DeleteEntry(initialCatalogEntry);
            DeleteEntry(userIDEntry);
            DeleteEntry(passwordEntry);
        }

        private void DeleteEntry(TcRegistryEntry entry)
        {
            if (entry.Exists)
            {
                entry.Delete();
            }
        }

        #endregion
    }
}
