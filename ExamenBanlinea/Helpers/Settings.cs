using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExamenBanlinea.Helpers
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

        const string LstContactsKey = "LstContacts";
        static readonly string LstContactsDefault = null;

        public static List<Models.DTOs.Contact> LstContacts
        {
            get
            {
                List<Models.DTOs.Contact> obj = new List<Models.DTOs.Contact>();
                string json = Settings.AppSettings.GetValueOrDefault(LstContactsKey, LstContactsDefault);
                return String.IsNullOrEmpty(json) ? obj : JsonConvert.DeserializeObject<List<Models.DTOs.Contact>>(json);
            }
            set
            {
                Settings.AppSettings.AddOrUpdateValue(LstContactsKey, JsonConvert.SerializeObject(value));
            }
        }

    }
}
