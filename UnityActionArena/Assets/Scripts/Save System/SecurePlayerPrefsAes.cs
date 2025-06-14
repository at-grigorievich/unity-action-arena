using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

namespace ATG.Save
{
    public static class SecurePlayerPrefsAes
    {
        private static readonly string Key = "12345678901234567890123456789012";
        private static readonly string IV = "1234567890123456"; 
        
        public static void Save(string key, string value)
        {
            string encrypted = Encrypt(value);
            PlayerPrefs.SetString(key, encrypted);
            PlayerPrefs.Save();
        }
        
        public static bool TryRead(string key, out string result)
        {
            if (PlayerPrefs.HasKey(key) == false)
            {
                result = string.Empty;
                return false;
            }
            
            string encrypted = PlayerPrefs.GetString(key);
            
            result = Decrypt(encrypted);

            return true;
        }
        
        private static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }
        
        private static string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}