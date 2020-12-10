using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TextDecoder.ViewModels
{
    public class LoadingPageViewModel : BaseViewModel
    {
        private string message;
        
        public string Message
        {
            get => message;
            private set
            {
                if(message != value)
                {
                    message = value;
                    OnPropertyChanged();
                }
            }
        }

        public LoadingPageViewModel(string message)
        {
            Message = message;
        }

        public LoadingPageViewModel(string message, int sleep) : this(message)
        {
            SleepAsync(sleep);
        }
        
        public async void SleepAsync(int sleep)
        {
            await Task.Run(() => Thread.Sleep(sleep));
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
