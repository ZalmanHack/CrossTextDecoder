using Android;
using Android.Support.V4.App;
using Java.IO;
using PCLStorage;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TextDecoder.Interfaces;
using TextDecoder.Models;
using TextDecoder.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;
using FileSystem = PCLStorage.FileSystem;

[assembly: Dependency(typeof(FileDialogAndroid))]
class FileDialogAndroid : IFileDialog
{
    private static string DefaultDir = "Text Decoder";
    public async Task<DataFileModel> OpenAsync(List<DataFileDialogModel> dataFilesDialog)
    {
        try
        {
            FileData fileData = await CrossFilePicker.Current.PickFile();
            if (fileData == null)
            {
                return new DataFileModel();
            }

            var arrayName = fileData.FileName.Split(".");
            string typeFile = arrayName[arrayName.Length - 1];
            return new DataFileModel(typeFile, fileData.FileName, fileData.DataArray);
        }
        catch (Exception )
        {
            return new DataFileModel();
        }

    }

    public async Task<string> SaveAsync(byte[] fileData, DataFileDialogModel dataFileDialog)
    {
        try
        {
            await Task.Run(() =>
            {
                string filePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, DefaultDir);
                Java.IO.File myDir = new Java.IO.File(filePath);
                if (!myDir.Exists())
                {
                    myDir.Mkdir();
                }
                string fullName = Path.Combine(filePath, dataFileDialog.DefaultFileName + dataFileDialog.FileType);
                if (System.IO.File.Exists(fullName))
                {
                    System.IO.File.Delete(fullName);
                }
                System.IO.File.WriteAllBytes(fullName, fileData);
            });
            return $"Файл сохранен в папку {DefaultDir}";
        }
        catch (Exception)
        {
            return "Ошибка сохранения файла";
        }
    }
}