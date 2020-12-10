﻿using System;
using TextDecoder.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossTextDecoder
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            MainPage = new NavigationPage(new DecoderView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
