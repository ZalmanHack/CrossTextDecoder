
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Collections.Generic;
using System.IO;
using TextDecoder.Interfaces;

namespace TextDecoder.Utils
{
    public class ReaderWord : IReadable
    {
        public string Read(DataFileModel dataFile)
        {
            try
            {
                using (WordDocument document = new WordDocument(new MemoryStream(dataFile.Data), FormatType.Automatic))
                {
                    return document.GetText().Substring(61); // удаляем первые 62 символа, так как там записано - Created with a trial version of Syncfusion Essential DocIO.\r\n;
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public byte[] Write(string text)
        {
            if(!string.IsNullOrEmpty(text))
            {
                MemoryStream stream = new MemoryStream();
                using (WordDocument document = new WordDocument())
                {
                    document.EnsureMinimal();
                    document.LastParagraph.AppendText(text);
                    document.Save(stream, FormatType.Docx);
                    return stream.ToArray();
                }
            }
            return null;
        }
    }
}
