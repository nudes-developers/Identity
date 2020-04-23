using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AppSample.Services
{
    public static class Settings
    {
        public static string AccessToken
        {
            get => Preferences.Get(nameof(AccessToken), null);
            set => Preferences.Set(nameof(AccessToken), value);
        }

        public static string RefreshToken
        {
            get => Preferences.Get(nameof(RefreshToken), null);
            set => Preferences.Set(nameof(RefreshToken), value);
        }

        public static DateTime AccessTokenExpiration
        {
            get => Preferences.Get(nameof(AccessTokenExpiration), DateTime.Now);
            set => Preferences.Set(nameof(AccessTokenExpiration), value);
        }
    }
}
