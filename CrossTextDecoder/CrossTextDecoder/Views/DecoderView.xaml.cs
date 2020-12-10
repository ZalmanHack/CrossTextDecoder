using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextDecoder.ViewModels;
using Xamarin.Forms;

namespace TextDecoder.Views
{
    public partial class DecoderView : ContentPage
    {
        public DecoderView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new DecoderViewModel();
        }
    }
}
