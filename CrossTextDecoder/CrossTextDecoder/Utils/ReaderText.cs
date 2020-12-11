using CrossTextDecoder.Utils;
using System.Text;
using TextDecoder.Interfaces;


namespace TextDecoder.Utils
{
    public class ReaderText : IReadable
    {
        static Encoding encode = null;

        public string Read(DataFileModel dataFile)
        {
            encode = EncodingType.GetType(dataFile.Data);
            if(encode != null)
            {
                return encode.GetString(dataFile.Data);
            }
            else
            {
                return null;
            }
        }

        public byte[] Write(string text)
        {

            if (!string.IsNullOrEmpty(text))
            {
                if(encode != null)
                {
                    return encode.GetBytes(text);
                }
                else
                {
                    return Encoding.UTF8.GetBytes(text);
                }
            }
            else
            {
                return null;
            }
        }
    }
}