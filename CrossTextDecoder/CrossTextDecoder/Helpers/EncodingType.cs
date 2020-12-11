using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ntics;

namespace CrossTextDecoder.Utils
{
    public class EncodingType
    {

        private static Dictionary<byte, char> ANSI;
        public static System.Text.Encoding GetType(byte[] text)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (MemoryStream ms = new MemoryStream(text))
                {
                    return GetType(ms);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public static System.Text.Encoding GetType(MemoryStream fs)
        {
            
            using (BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default))
            {
                Encoding reVal = Encoding.Default;
                int.TryParse(fs.Length.ToString(), out int i);
                byte[] ss = r.ReadBytes(i);
                if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
                {
                    reVal = ANSIorUTF8(ss);
                }
                else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
                {
                    reVal = Encoding.BigEndianUnicode;
                }
                else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
                {
                    reVal = Encoding.Unicode;
                }
                else
                {
                    reVal = ANSIorUTF8(ss);
                }
                return reVal;
            }
        }

        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1;
            byte curByte;
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }

                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("Error byte format");
            }
            return true;
        }


        private static Encoding ANSIorUTF8(byte[] data)
        {
            // определяем BOM (EF BB BF)
            if (data.Length > 2 && data[0] == 0xef && data[1] == 0xbb && data[2] == 0xbf)
            {
                if (data.Length != 3)
                {
                    return Encoding.UTF8;
                }   
                else
                {
                    return null;
                }
            }
            int i = 0;
            while (i < data.Length - 1)
            {
                if (data[i] > 0x7f)
                { // не ANSI-символ
                    if ((data[i] >> 5) == 6)
                    {
                        if ((i > data.Length - 2) || ((data[i + 1] >> 6) != 2))
                        {
                            //return ntics.Text.Encoding1251.GetEncoding(1251);
                            return Encoding.GetEncoding(1251);
                        }
                        i++;
                    }
                    else if ((data[i] >> 4) == 14)
                    {
                        if ((i > data.Length - 3) || ((data[i + 1] >> 6) != 2) || ((data[i + 2] >> 6) != 2))
                        {
                            //return ntics.Text.Encoding1251.GetEncoding(1251);
                            return Encoding.GetEncoding(1251);
                        }
                        i += 2;
                    }
                    else if ((data[i] >> 3) == 30)
                    {
                        if ((i > data.Length - 4) || ((data[i + 1] >> 6) != 2) || ((data[i + 2] >> 6) != 2) || ((data[i + 3] >> 6) != 2))
                        {
                            //return ntics.Text.Encoding1251.GetEncoding(1251);
                            return Encoding.GetEncoding(1251);
                        }
                        i += 3;
                    }
                    else
                    {
                        //return ntics.Text.Encoding1251.GetEncoding(1251);
                        return Encoding.GetEncoding(1251);
                    }
                }
                i++;
            }
            return Encoding.UTF8;
        }
    }
}
