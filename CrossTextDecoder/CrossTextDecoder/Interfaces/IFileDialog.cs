using System.Collections.Generic;
using System.Threading.Tasks;
using TextDecoder.Models;
using TextDecoder.Utils;

namespace TextDecoder.Interfaces
{
    public interface IFileDialog
    {
        Task<DataFileModel> OpenAsync(List<DataFileDialogModel> dataFilesDialog);
        Task<string> SaveAsync(byte[] fileData, DataFileDialogModel dataFileDialog);
    }
}
