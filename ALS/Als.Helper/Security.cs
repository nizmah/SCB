using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Als
{
    public class Security
    {
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        public static bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("There is nothing to encrypt");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("There is no encryption key provided");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV encryptor not provided");


            MemoryStream msEncrypt = null;
            byte[] encBuff = null;
            RijndaelManaged aesAlg = null;

            try
            {
                aesAlg = new RijndaelManaged();
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                msEncrypt = new MemoryStream();
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encBuff = msEncrypt.ToArray();
                }

            }
            catch
            {
                return null;
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return encBuff;
        }

        public static string DecryptBytesToString(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("There is nothing to decrypt.");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("There is no decryption key provided");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV decryptor not provided");

            RijndaelManaged aesAlg = null;
            string plaintext = null;

            try
            {
                aesAlg = new RijndaelManaged();
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            plaintext = srDecrypt.ReadToEnd();
                    }
                }

            }
            catch
            {
                return null;
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;

        }

    }
}
