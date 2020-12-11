using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextDecoder.Utils
{
    public class EncryptorText
    {
        private string _text;
        private string _key;
        private uint _keyIndex;

        public string Alphabet { get; private set; } = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        //                                              
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
            }
        }
        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                KeyIndex = 0;
            }
        }     

        private uint KeyIndex 
        {
            get => _keyIndex;
            set
            {
                if(value == 0)
                {
                    _keyIndex = 0;
                }
                else
                {
                    uint result = (uint)(value % (Key.Length));
                    _keyIndex = result;
                }
            }
        }

        public async Task<string> Encrypt (string text, string key)
        {
            string result = await Task.Run(() => Encryptor(text, key, true));
            return result;
        }

        public async Task<string> Decrypt(string text, string key)
        {
            string result = await Task.Run(() => Encryptor(text, key, false));
            return result;
        }

        private string Encryptor(string text, string key, bool encrypt)
        {
            if(!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(key))
            {
                Text = text;
                Key = key;
                string resultText = "";
                int q = Alphabet.Length;
                for (int i = 0; i < Text.Length; i++)
                {
                    if (Alphabet.IndexOf(char.ToLower(Text[i])) > -1)
                    {
                        int k = encrypt ? 1 : -1;
                        int codeIndex = Alphabet.IndexOf(Key[(int)KeyIndex]);
                        int letterIndex = Alphabet.IndexOf(char.ToLower(Text[i]));
                        int resultIndex = (q + letterIndex + (k * codeIndex)) % q;
                        resultText += char.IsLower(Text[i]) ? Alphabet[resultIndex] : char.ToUpper(Alphabet[resultIndex]);
                        KeyIndex++;
                    }
                    else
                    {
                        resultText += Text[i];
                    }
                }
                return resultText;
            }
            return string.Empty;
        }
    }
}





