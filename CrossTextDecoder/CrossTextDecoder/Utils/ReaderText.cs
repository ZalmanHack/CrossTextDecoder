using System;
using System.Collections.Generic;
using System.Text;
using TextDecoder.Interfaces;

namespace TextDecoder.Utils
{
    public class ReaderText : IReadable
    {
        public string Read(DataFileModel dataFile)
        {

                return Encoding.UTF8.GetString(dataFile.Data);

        }

        public byte[] Write(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return Encoding.UTF8.GetBytes(text);
            }
            else
            {
                return null;
            }
        }
    }
}
