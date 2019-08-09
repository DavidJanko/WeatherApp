using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels.Models;

namespace WeatherApp.ViewModels
{
    public class MainPageViewModel
    {
        private string _nejakejText;

        public MainPageViewModel()
        {
            NejakejText = "Už nechci umřít";
        }

        public string NejakejText { get { return _nejakejText; } set { _nejakejText = value; GetWeatherData("Liberec"); } }

        private WeatherData GetWeatherData(string mesto)
        {
            if (mesto == null)
            {
                return null;
            }
            else
            {
                var json = new WebClient().DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + mesto + "&APPID=fedcf81469d34db86a7e24886bb9ae83");
                var data = JsonConvert.DeserializeObject<WeatherData>(json);

                return data;
            }
        }
    }
}
