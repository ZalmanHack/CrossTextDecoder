using System;
using System.Collections.Generic;
using System.Text;
using TextDecoder.Utils;

namespace TextDecoder.Interfaces
{
    public interface IReadable
    {
        string Read(DataFileModel dataFile);
        byte[] Write(string text);
    }
}
