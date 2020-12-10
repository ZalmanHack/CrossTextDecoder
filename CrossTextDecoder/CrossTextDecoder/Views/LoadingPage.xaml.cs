using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextDecoder.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextDecoder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage(string message)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new LoadingPageViewModel(message);
        }

        public LoadingPage(string message, int sleep)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new LoadingPageViewModel(message, sleep);
        }
    }
}