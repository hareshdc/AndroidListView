using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Plugin.Settings.Abstractions;
using Plugin.Settings;

namespace ListViewExample.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        #region Setting Constants
        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;
        #endregion
        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        private const string UsersKey = "users_key";

        public static List<User> Users
        {
            get
            {
                string value = AppSettings.GetValueOrDefault(UsersKey, string.Empty);
                List<User> users;

                if (string.IsNullOrEmpty(value))
                    users = new List<User>();
                else
                    users = JsonConvert.DeserializeObject<List<User>>(value);
                return users;
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsersKey, JsonConvert.SerializeObject(value));
            }
        }
    }
}