using LucidPayroll.General;
using Microsoft.Win32;
using System;

// Harshan Nishantha
// 2010/07/09

namespace LucidLibrary.Entries
{
    public class TcRegistryEntry
    {
        const string Root = "SOFTWARE";

        private RegistryHive hive;
        private string subKey;
        private string name;
        private RegistryValueKind   kind;

        public bool Exists { get; set; }
        public object Value { get; set; }


        public TcRegistryEntry(RegistryHive hive, string subKey, string name, RegistryValueKind kind)
        {
            this.hive = hive;

            if (subKey == string.Empty)
            {
                this.subKey = Root;
            }
            else
            {
                this.subKey = String.Format("{0}\\", subKey);
            }

            this.name = name;
            this.kind = kind;

            Read();
        }

        public void Read()
        {
            RegistryKey registryKey = GetRootKey().OpenSubKey(subKey, false);

            if (registryKey != null)
            {
                this.Value = registryKey.GetValue(name);

                if (this.Value != null)
                {
                    Exists = true;
                }
                else
                {
                    Exists = false;
                }
            }
        }

        public void Write()
        {
            RegistryKey registryKey = GetRootKey().CreateSubKey(subKey);

            if (registryKey != null && this.Value != null)
            {
                registryKey.SetValue(name, this.Value, kind);
                Exists = true;
            }
        }

        public void Delete()
        {
            RegistryKey registryKey = GetRootKey().OpenSubKey(subKey, true);

            if (registryKey != null)
            {
                registryKey.DeleteValue(name);
                Exists = false;
            }
        }

        private RegistryKey GetRootKey()
        {
            switch (hive)
            {
                case RegistryHive.ClassesRoot:
                    return Registry.ClassesRoot;

                case RegistryHive.CurrentConfig:
                    return Registry.CurrentConfig;

                case RegistryHive.CurrentUser:
                    return Registry.CurrentUser;

                case RegistryHive.LocalMachine:
                    return Registry.LocalMachine;

                case RegistryHive.DynData:
                case RegistryHive.PerformanceData:
                    return Registry.PerformanceData;

                case RegistryHive.Users:
                    return Registry.Users;

                default:
                    return Registry.CurrentUser;
            }
        }

        public string StringValue()
        {
            return (string)Value;
        }

        public int IntValue()
        {
            return (int)Value;
        }

        public static string ReadAndDecrypt(TcRegistryEntry entry, string key)
        {
            string result = "";

            entry.Read();
            if (entry.Exists)
            {
                result = entry.StringValue();
            }

            if (!string.IsNullOrEmpty(result))
            {
                result = TcCrypto.Decrypt(result, key);
            }

            return result;
        }

        public static void WriteAndEncrypt(TcRegistryEntry entry, string value, string key)
        {
            string result = "";

            if (!string.IsNullOrEmpty(value))
            {
                result = TcCrypto.Encrypt(value, key);
            }

            entry.Value = result;
            entry.Write();
        }
    }
}
