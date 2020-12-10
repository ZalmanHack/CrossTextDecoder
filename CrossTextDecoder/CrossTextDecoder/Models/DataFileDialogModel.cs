namespace TextDecoder.Models
{
    public class DataFileDialogModel
    {
        public string TypeName { get; set; }
        public string FileType { get; set; }
        public string DefaultFileName { get; set; }

        public DataFileDialogModel()
        {

        }

        public DataFileDialogModel(string typeName, string fileType, string defaultFileName) : this()
        {
            TypeName = typeName;
            FileType = fileType;
            DefaultFileName = defaultFileName;
        }
    }
}
