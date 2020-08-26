using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FourierNewton.Common.Cryptography
{
    public class CryptographyManager
    {

        public static string GeneratePassword()
        {

            var password = GeneratePassword(CryptographyConstants.DefaultNumberOfLowerCaseLetters,
                                            CryptographyConstants.DefaultNumberOfUpperCaseLetters, 
                                            CryptographyConstants.DefaultNumberOfDigits, 
                                            CryptographyConstants.DefaultNumberOfNonAlphaNumericCharacters);

            return password;

        }


        public static string GeneratePassword(int numberOfLowerCaseLetters, int numberOfUpperCaseLetters, 
                                                int numberOfDigits, int numberOfNonAlphaNumericCharacters)
        {
            string password = "";

            password += GeneratePartOfPassword(numberOfLowerCaseLetters, CryptographyConstants.LowerCaseLetters);
            password += GeneratePartOfPassword(numberOfUpperCaseLetters, CryptographyConstants.UpperCaseLetters);
            password += GeneratePartOfPassword(numberOfDigits, CryptographyConstants.Digits);
            password += GeneratePartOfPassword(numberOfNonAlphaNumericCharacters, CryptographyConstants.NonAlphaNumericCharacters);

            password = ShufflePassword(password);

            return password;
        }

        private static string GeneratePartOfPassword(int length, string valid)
        {
            
            StringBuilder res = new StringBuilder();
            Random random = new Random();
            while (0 < length--)
            {
                res.Append(valid[random.Next(valid.Length)]);
            }
            return res.ToString();

        }

        private static string ShufflePassword(string str)
        {

            char[] array = str.ToCharArray();
            Random random = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);

        }


        public static void GenerateAesKeyAndInitializationVector() {

            // Create a new instance of the Aes
            // class.  This generates a new key and initialization
            // vector (IV).
            using (Aes aes = Aes.Create())
            {

                string aesKey = Convert.ToBase64String(aes.Key);
                string aesInitializationVector = Convert.ToBase64String(aes.IV);

            }

        }

        public static string EncryptStringWithAes(string plainText)
        {

            byte[] Key = Convert.FromBase64String(CryptographyConstants.AesKey);
            byte[] IV = Convert.FromBase64String(CryptographyConstants.InitializationVector);

            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            string encryptedText = Convert.ToBase64String(encrypted);
            return encryptedText;
        }

        public static string DecryptStringWithAes(string encryptedText)
        {

            byte[] cipherText = Convert.FromBase64String(encryptedText);
            byte[] Key = Convert.FromBase64String(CryptographyConstants.AesKey);
            byte[] IV = Convert.FromBase64String(CryptographyConstants.InitializationVector);

            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }



    }
}
