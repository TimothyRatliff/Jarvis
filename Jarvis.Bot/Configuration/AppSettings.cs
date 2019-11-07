using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Jarvis.Bot.Configuration
{
    public class AppSettings
    {
        public static AppSettings _settings;
        public string token { get; set; }

        public static AppSettings Initialize()
        {
            AppSettings settings = new AppSettings();

            if(_settings != null)
            {
                return _settings;
            }

            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

            settings.token = _configuration["AppSettings:token"];

            _settings = settings;

            return _settings;
        }
    }
}
