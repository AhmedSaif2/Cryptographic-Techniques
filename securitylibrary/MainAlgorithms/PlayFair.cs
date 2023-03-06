using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public const int ROWS = 5, COLUMNS = 5, LETTERS = 26;
        char[,] charMatrix = new char[5, 5];
        public string Decrypt(string cipherText, string key)
        {
            constructCharMatrix(key);
            string plainText = "";
            cipherText = cipherText.ToLower();
            int cipherLength = cipherText.Length;
            for (int i = 0; i < cipherLength; i += 2)
            {
                Tuple<int, int> A = findCharInMatrix(cipherText[i]);
                Tuple<int, int> B = findCharInMatrix(cipherText[i + 1]);
                char firstChar, secondChar;
                if (A.Item1 == B.Item1)
                {
                    firstChar = charMatrix[A.Item1, (A.Item2 - 1 + COLUMNS) % COLUMNS];
                    secondChar = charMatrix[B.Item1, (B.Item2 - 1 + COLUMNS) % COLUMNS];
                }
                else if (A.Item2 == B.Item2)
                {
                    firstChar = charMatrix[(A.Item1 - 1 + ROWS) % ROWS, A.Item2];
                    secondChar = charMatrix[(B.Item1 - 1 + ROWS) % ROWS, B.Item2];
                }
                else
                {
                    firstChar = charMatrix[A.Item1, B.Item2];
                    secondChar = charMatrix[B.Item1, A.Item2];
                }
                plainText += firstChar;
                plainText += secondChar;
            }
            plainText=removeXFromPlain(plainText);
            return plainText;
        }

        public string Encrypt(string plainText, string key)
        {
            constructCharMatrix(key);
            string cipherText = "";
            for (int i = 0; i < plainText.Length; i+=2)
            {
                Tuple<int,int> A = findCharInMatrix(plainText[i]);
                Tuple<int, int> B;
                if (i + 1 >= plainText.Length || plainText[i] == plainText[i + 1])
                {
                    B = findCharInMatrix('x');
                    i--;
                }
                else
                {
                    B = findCharInMatrix(plainText[i + 1]);
                }
                if (A.Item1 == B.Item1)
                {
                    cipherText += charMatrix[A.Item1, (A.Item2 + 1) % COLUMNS];
                    cipherText += charMatrix[B.Item1, (B.Item2 + 1) % COLUMNS];
                }
                else if (A.Item2 == B.Item2)
                {
                    cipherText += charMatrix[(A.Item1 + 1) % ROWS, A.Item2 ];
                    cipherText += charMatrix[(B.Item1 + 1) % ROWS, B.Item2 ];
                }
                else
                {
                    cipherText += charMatrix[A.Item1 , B.Item2];
                    cipherText += charMatrix[B.Item1, A.Item2];
                }
            }
            return cipherText;
        }
        public HashSet<char> prepareCharacterSet()
        {
            HashSet<char> letters = new HashSet<char>();
            for (char i = 'a'; i <= 'z'; i++)
            {
                if (i == 'j') continue;
                letters.Add(i);
            }
            return letters;
        }
        public void constructCharMatrix(string key)
        {
            HashSet<char>letters = prepareCharacterSet();
            int keyIterator = 0;
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    if (keyIterator < key.Length)
                    {
                        char nextChar = key[keyIterator++];
                        if (nextChar == 'j') nextChar = 'i';
                        if (!letters.Contains(nextChar)) { j--; continue; }
                        charMatrix[i, j] = nextChar;
                        letters.Remove(charMatrix[i, j]);
                    }
                    else
                    {
                        charMatrix[i, j] = letters.First();
                        letters.Remove(charMatrix[i, j]);
                    }
                }
            }
        }
        public Tuple<int, int> findCharInMatrix(char x)
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    if (charMatrix[i, j]==x)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }
            return null;
        }
        public string removeXFromPlain(string plainText)
        {
            int plainLength = plainText.Length;
            string text = "";
            text += plainText[0];
            for (int i = 1; i < plainLength-1; i++)
            {
                if (i % 2 == 1 && plainText[i] == 'x' && plainText[i - 1] == plainText[i + 1])
                    continue;
                text += plainText[i];
            }
            if (plainText.Last()!='x')text += plainText.Last();
            return text;
        }

        public string Analyse(string largeCipher)
        {
            throw new NotImplementedException();
        }
    }
}
