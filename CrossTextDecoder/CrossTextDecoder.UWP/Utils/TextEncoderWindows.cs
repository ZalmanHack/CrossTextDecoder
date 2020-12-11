using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileDialogWindows))]
public class TextEncoderWindows
{
    string GetAnsiString(byte[] data)
    {
        Encoding ANSI = ntics.Text.Encoding1251.GetEncoding(1251);
        return ANSI.GetString(data);
    }
}

