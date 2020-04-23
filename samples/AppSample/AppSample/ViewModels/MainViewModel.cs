using AppSample.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppSample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Api Api { get; set; }

        private string _text;

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }


        public ICommand AuthCommand { get; set; }

        public ICommand GetCommand { get; set; }


        public MainViewModel()
        {
            Api = new Api();
            AuthCommand = new Command(Auth, () => !IsBusy);
            GetCommand = new Command(Get, () => !IsBusy);
        }

        private async void Get()
        {
            IsBusy = true;
            try
            {
                var result = await Api.WeatherForecast();
                Text = result;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void Auth()
        {
            IsBusy = true;
            try
            {
                await Api.Auth();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
