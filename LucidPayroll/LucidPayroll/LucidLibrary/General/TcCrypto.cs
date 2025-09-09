using System;
using System.Security.Cryptography;
using System.Text;

// Harshan Nishantha
// 2013-02-27

namespace LucidPayroll.General
{
    public class TcCrypto
    {
        public static bool useHashing = false;

        public static string Encrypt(string text, string key)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] keyArray;
            byte[] textArray = UTF8Encoding.UTF8.GetBytes(text);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
            tripleDes.Key = keyArray;
            tripleDes.Mode = CipherMode.ECB;
            tripleDes.Padding = PaddingMode.PKCS7;

            ICryptoTransform transformer = tripleDes.CreateEncryptor();
            byte[] resultArray = transformer.TransformFinalBlock(textArray, 0, textArray.Length);

            tripleDes.Clear();

            string encryptedText = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return encryptedText;
        }

        public static string Decrypt(string encryptedText, string key)
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                return encryptedText;
            }

            byte[] keyArray;

            byte[] encryptedTextArray = Convert.FromBase64String(encryptedText);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd = new MD5CryptoServiceProvider();
                keyArray = hashmd.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
            tripleDes.Key = keyArray;
            tripleDes.Mode = CipherMode.ECB;
            tripleDes.Padding = PaddingMode.PKCS7;

            ICryptoTransform transformer = tripleDes.CreateDecryptor();
            byte[] resultArray = transformer.TransformFinalBlock(encryptedTextArray, 0, encryptedTextArray.Length);

            tripleDes.Clear();

            string decryptedText = UTF8Encoding.UTF8.GetString(resultArray, 0, resultArray.Length);
            return decryptedText;
        }
    }
}
