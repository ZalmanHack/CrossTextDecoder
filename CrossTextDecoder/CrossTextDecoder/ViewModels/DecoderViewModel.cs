using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TextDecoder.Interfaces;
using TextDecoder.Models;
using TextDecoder.Utils;
using TextDecoder.Views;
using Xamarin.Forms;

namespace TextDecoder.ViewModels
{
    public class DecoderViewModel : BaseViewModel
    {
        private static int ShowTime = 1500; 
        private DataModel Data;
        public DecoderViewModel()
        {
            Data = new DataModel();
            // форматы возможных варианов сохранения файлов
            DataFilesDialog = new List<DataFileDialogModel>();
            DataFilesDialog.Add(new DataFileDialogModel("Документ Word", ".docx", "Сгенерированный текст"));
            DataFilesDialog.Add(new DataFileDialogModel("Текстовый документ", ".txt", "Сгенерированный текст"));
            SelectedDataFileDialog = DataFilesDialog[0];
            InitializationCommands();
        }

        #region Properties [MODEL]
        public string SourseText 
        { 
            get => Data.SourseText; 
            set
            {
                if(Data.SourseText != value)
                {
                    Data.SourseText = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ResultText
        {
            get => Data.ResultText;
            set
            {
                if (Data.ResultText != value)
                {
                    Data.ResultText = value;
                    OnPropertyChanged();

                    if(!string.IsNullOrEmpty(value))
                    {
                        CanBeSave = true;
                    }
                    else
                    {
                        CanBeSave = false;
                    }
                }
            }
        }
        public string EncryptKey
        {
            get => Data.EncryptKey;
            set
            {
                if (Data.EncryptKey != value)
                {
                    Data.EncryptKey = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(value))
                    {
                        CanBeCrypt = true;
                    }
                    else
                    {
                        CanBeCrypt = false;
                    }
                }
            }
        }
        #endregion

        #region Properties [MODEL VIEW] 
        private bool _canBeSave;
        private bool _canBeCrypt;
        private bool _isAwait;
        private List<DataFileDialogModel> _dataFilesDialog;
        private DataFileDialogModel _selectedDataFileDialog;

        public bool CanBeSave
        {
            get => _canBeSave;
            private set
            {
                if(_canBeSave != value)
                {
                    _canBeSave = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool CanBeCrypt
        {
            get => _canBeCrypt;
            private set
            {
                if (_canBeCrypt != value)
                {
                    _canBeCrypt = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool IsAwait
        {
            get => _isAwait;
            set
            {
                if(_isAwait != value)
                {
                    _isAwait = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<DataFileDialogModel> DataFilesDialog
        {
            get => _dataFilesDialog;
            private set
            {
                if (_dataFilesDialog != value)
                {
                    _dataFilesDialog = value;
                    OnPropertyChanged();
                }
            }
        }
        public DataFileDialogModel SelectedDataFileDialog
        {
            get => _selectedDataFileDialog;
            set
            {
                if (_selectedDataFileDialog != value)
                {
                    _selectedDataFileDialog = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands
        public ICommand OpenCommand { protected set; get; }
        public ICommand SaveCommand { protected set; get; }
        public ICommand EncrCommand { protected set; get; }
        public ICommand DecrCommand { protected set; get; }
        private void InitializationCommands()
        {
            OpenCommand = new Command(OpenFile);
            SaveCommand = new Command(SaveFile);
            EncrCommand = new Command(EncryptText);
            DecrCommand = new Command(DecryptText);
        }
        #endregion

        #region Comands methods
        private async void OpenFile()
        {
            DataFileModel dataFile = await Xamarin.Forms.DependencyService.Get<IFileDialog>().OpenAsync(DataFilesDialog);
            if (dataFile.Data != null && dataFile.Data.Length != 0)
            {
                await ShowLoadingPageAsync($"Чтение файла\n{dataFile.NameFile}");
                string result = null;
                await Task.Run(() =>
                {
                    result = new ReaderWord().Read(dataFile);
                    if (result == null)
                    {
                        result = new ReaderText().Read(dataFile);
                    }
                });
                await CloseLoadingPageAsync();
                if (result == null)
                {
                    await ShowLoadingPageAsync($"Ошибка\n\nФайл {dataFile.NameFile} не поодерживается", ShowTime);
                }
                else
                {
                    SourseText = result;
                }
            }
        }

        private async void SaveFile()
        {
            byte[] data = null;
            if(SelectedDataFileDialog.FileType == ".docx")
            {
                data = new ReaderWord().Write(ResultText);
            } else if(SelectedDataFileDialog.FileType == ".txt")
            {
                data = new ReaderText().Write(ResultText);
            }
            if (data != null)
            {
                var filePath = await Xamarin.Forms.DependencyService.Get<IFileDialog>().SaveAsync(data, SelectedDataFileDialog);
                await ShowLoadingPageAsync($"{filePath}", ShowTime);
            }
        }
        
        private async void EncryptText()
        {
            await ShowLoadingPageAsync("Подождите, идет шифрование текста");
            ResultText = await new TextEncryptor().Encrypt(SourseText, EncryptKey);
            await CloseLoadingPageAsync();
        }

        private async void DecryptText()
        {
            await ShowLoadingPageAsync("Подождите, идет расшифровка текста");
            ResultText = await new TextEncryptor().Decrypt(SourseText, EncryptKey);
            await CloseLoadingPageAsync();
        }
        #endregion

        #region LoadingPage methods
        private async Task ShowLoadingPageAsync(string message)
        {
            if(!IsAwait)
            {
                IsAwait = true;
                await Application.Current.MainPage.Navigation.PushAsync(new LoadingPage(message));
            }
        }

        private async Task ShowLoadingPageAsync(string message, int showTime)
        {
            if (!IsAwait)
            {
                IsAwait = true;
                await Application.Current.MainPage.Navigation.PushAsync(new LoadingPage(message, showTime));
            }
        }

        private async Task CloseLoadingPageAsync()
        {
            if (IsAwait)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
                IsAwait = false;
            }
        }
        #endregion
    }
}
