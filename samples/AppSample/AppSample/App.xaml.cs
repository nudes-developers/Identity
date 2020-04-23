using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppSample.Services;
using AppSample.Views;

namespace AppSample
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
