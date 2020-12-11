using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CrossTextDecoderTests.Helpers
{
    public static class FileReader
    {
        public static async Task<string> getDataStringAsync(string path)
        {
            string data = await Task.Run(() => {
                using (FileStream fs = File.OpenRead(path))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        return Encoding.GetEncoding(1251).GetString(br.ReadBytes((int)fs.Length));
                    }
                }
            });
            return data;
        }

        public static async Task<byte[]> getDataBytesAsync(string path)
        {
            byte[] data = await Task.Run(() => {
                using (FileStream fs = File.OpenRead(path))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        return br.ReadBytes((int)fs.Length);
                    }
                }
            });
            return data;
        }
    }
}
