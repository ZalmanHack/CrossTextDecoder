namespace TextDecoder.Utils
{
    public class DataFileModel
    {
        public string TypeFile { get;  set; }
        public string NameFile { get;  set; }
        public byte[] Data { get;  set; }

        public DataFileModel()
        {

        }

        public DataFileModel(string typeFile, string nameFile, byte[] data) : this()
        {
            TypeFile = typeFile;
            NameFile = nameFile;
            Data = data;
        }
    }
}
