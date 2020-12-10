using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TextDecoder.Interfaces;
using TextDecoder.Models;
using TextDecoder.Utils;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileDialogWindows))]
class FileDialogWindows : IFileDialog
{
    public async Task<DataFileModel> OpenAsync(List<DataFileDialogModel> dataFilesDialog)
    {
        var openPicker = new FileOpenPicker();
        openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
        foreach (var item in dataFilesDialog)
        {
            openPicker.FileTypeFilter.Add(item.FileType);
        }
        StorageFile selectedFile = await openPicker.PickSingleFileAsync();
        if (selectedFile != null)
        {
            using (StreamReader reader = new StreamReader(await selectedFile.OpenStreamForReadAsync()))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    reader.BaseStream.CopyTo(ms);
                    return new DataFileModel(selectedFile.FileType, selectedFile.Name, ms.ToArray());
                }
            }
        }
        return new DataFileModel();
    }

    public async Task<string> SaveAsync(byte[] fileData, DataFileDialogModel dataFileDialog)
    {
        var savePicker = new Windows.Storage.Pickers.FileSavePicker();
        savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
        savePicker.FileTypeChoices.Add(dataFileDialog.TypeName, new List<string>() { dataFileDialog.FileType });
        savePicker.SuggestedFileName = dataFileDialog.DefaultFileName;
        Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
        if (file != null)
        {
            Windows.Storage.CachedFileManager.DeferUpdates(file);
            await FileIO.WriteBytesAsync(file, fileData);
            FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
            if (status == FileUpdateStatus.Complete)
            {
                return $"Файл {file.Name} сохранен";
            }
            else
            {
                return $"Ошибка сохранения Файла {file.Name}";
            }
        }
        else
        {
            return $"Сохранение файла отменено";
        }
    }
}