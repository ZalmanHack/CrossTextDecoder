using CrossTextDecoder.Utils;
using System.Text;
using TextDecoder.Interfaces;


namespace TextDecoder.Utils
{
    public class ReaderText : IReadable
    {
        Encoding ANSI;
        public ReaderText()
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //ANSI = ntics.Text.Encoding1251.GetEncoding(1251);
        }
        public string Read(DataFileModel dataFile)
        {
            Encoding encode = EncodingType.GetType(dataFile.Data);
            if(encode != null)
            {
                var d = dataFile.Data;
                return EncodingType.GetType(dataFile.Data).GetString(dataFile.Data);
            }
            else
            {
                return null;
            }

        }

        public byte[] Write(string text)
        {
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    ANSI = ntics.Text.Encoding1251.GetEncoding(1251);
                    return ANSI.GetBytes(text);
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception)
            {
                return Encoding.UTF8.GetBytes(text);
            }
        }
    }
}