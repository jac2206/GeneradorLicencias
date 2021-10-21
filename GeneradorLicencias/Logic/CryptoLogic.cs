using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorLicencias.Logic
{
    public static class CryptoLogic
    {

        //public static string Encriptar(string textoEncriptado)
        //{

        //    try
        //    {
        //        string result = string.Empty;
        //        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(textoEncriptado);
        //        result = Convert.ToBase64String(encryted);

        //        return result;

        //    }

        //    catch (Exception ex)
        //    {
        //        return "";
        //    }

        //}

        ///// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        //public static string DesEncriptar(string textoDesencriptado)
        //{
        //    try
        //    {

        //        string result = string.Empty;
        //        byte[] decryted = Convert.FromBase64String(textoDesencriptado);
        //        //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
        //        result = System.Text.Encoding.Unicode.GetString(decryted);
        //        return result;

        //    }

        //    catch (Exception ex)
        //    {
        //        return "";
        //    }

        //}


        private const string initVector = "pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        //Encrypt
        public static string EncryptString(string plainText, string passPhrase)
        {

            try
            {

                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();

                return Convert.ToBase64String(cipherTextBytes);
            }

            catch(Exception ex)
            {
                return "";
            }

        }
        //Decrypt
        public static string DecryptString(string cipherText, string passPhrase)
        {

            try
            {

                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }

            catch(Exception ex)
            {
                return "";
            }

        }


    }
}
