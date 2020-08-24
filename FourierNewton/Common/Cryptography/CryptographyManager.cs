using System;
using System.Collections.Generic;
using System.Linq;
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



    }
}
