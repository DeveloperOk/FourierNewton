using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourierNewton.Common.Cryptography
{
    public class CryptographyConstants
    {

        public static string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        public static string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Digits = "1234567890";
        public static string NonAlphaNumericCharacters = "*$-+?_!%{}/";


        public static int DefaultNumberOfLowerCaseLetters = 3;
        public static int DefaultNumberOfUpperCaseLetters = 3;
        public static int DefaultNumberOfDigits = 3;
        public static int DefaultNumberOfNonAlphaNumericCharacters = 3;

    }
}
