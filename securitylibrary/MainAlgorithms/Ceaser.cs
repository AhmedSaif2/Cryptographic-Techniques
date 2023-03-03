using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        
        public string Encrypt(string plainText, int key)
        {
            string cipherText = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                int charNumber = ((plainText[i] - 'a') + key) % 26;
                char x = (char)('A' + charNumber);
                cipherText += x;
            }
            return cipherText;
        }

        public string Decrypt(string cipherText, int key)
        {
            string plainText = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                int charNumber = ((cipherText[i] - 'A') - key + 26) % 26;
                char x = (char)('a' + charNumber);
                plainText += x;
            }
            return plainText;
        }

        public int Analyse(string plainText, string cipherText)
        {
            cipherText=cipherText.ToLower();
            int key = (cipherText[0] - plainText[0] + 26) % 26;
            return key;
        }
    }
}
