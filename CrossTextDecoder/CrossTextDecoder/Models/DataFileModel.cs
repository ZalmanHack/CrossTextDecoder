namespace TextDecoder.Utils
{
    public class DataFileModel
    {
        public string TypeFile { get; private set; }
        public string NameFile { get; private set; }
        public byte[] Data { get; private set; }

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
