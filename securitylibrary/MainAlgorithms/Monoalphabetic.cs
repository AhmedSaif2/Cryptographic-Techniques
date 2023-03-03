using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            HashSet<char> letters = prepareCharacterSet();
            string key = "";
            char[] arr = new char[26];
            for (int i = 0; i < plainText.Length; i++)
            {
                char curCipher = (char)((cipherText[i] - 'A') + 'a');
                arr[plainText[i] - 'a'] = curCipher;
                letters.Remove(curCipher);
            }
            for (int i = 0; i < 26; i++)
            {
                if (arr[i] >= 'a' && arr[i] <= 'z') key += arr[i];
                else
                {
                    key += letters.First();
                    letters.Remove(letters.First());
                }
            }
            return key;
        }
        public string Decrypt(string cipherText, string key)
        {
            string plainText = "";
            char[] arr = new char[26];
            for (int i = 0; i < key.Length; i++)
            {
                arr[key[i] - 'a'] = (char)('a' + i);
            }
            for (int i = 0; i < cipherText.Length; i++)
            {
                int charNumber = (cipherText[i] - 'A');
                plainText += arr[charNumber];
            }
            return plainText;
        }
        public string Encrypt(string plainText, string key)
        {
            string cipherText = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                int charNumber = (plainText[i] - 'a');
                cipherText += key[charNumber];
            }
            return cipherText;
        }         
        /// <summary>
                            /// Frequency Information:
                            /// E   12.51%
                            /// T    9.25
                            /// A    8.04
                            /// O    7.60
                            /// I    7.26
                            /// N    7.09
                            /// S    6.54
                            /// R    6.12
                            /// H    5.49
                            /// L    4.14
                            /// D    3.99
                            /// C    3.06
                            /// U    2.71
                            /// M    2.53
                            /// F    2.30
                            /// P    2.00
                            /// G    1.96
                            /// W    1.92
                            /// Y    1.73
                            /// B    1.54
                            /// V    0.99
                            /// K    0.67
                            /// X    0.19
                            /// J    0.16
                            /// Q    0.11
                            /// Z    0.09
                            /// </summary>
                            /// <param name="cipher"></param>
                            /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            //Tuple<char, int>[] cipher_freq = new Tuple<char, int>[26];

            //string mostfreq = "etaoinsrhldcumfpgwybvkxjqz";
            //for (int i = 0; i < cipher.Length; i++)
            //    cipher_freq[cipher[i] - 'a'].Item1 = cipher[i];
            return "";

        }
        public HashSet<char> prepareCharacterSet()
        {
            HashSet<char> letters = new HashSet<char>();
            for (char i = 'a'; i <= 'z'; i++)
            {
                letters.Add(i);
            }
            return letters;
        }
    }
}