using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels.Models;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class MainPageViewModel : Abstract.Abstract
    {
        private string vstupniText;

        public Command Vyhledat { get; set; }
        public Command HledatGPS { get; set; }

        public MainPageViewModel()
        {
            Vyhledat = new Command(GetWeatherData);
            HledatGPS = new Command(NajitLokaci);
        }
        public string VstupniText { get; set; }

        private void NajitLokaci()
        {

        }

        private void GetWeatherData()
        {
            if (VstupniText == null)
            {
            }
            else
            {
                var json = new WebClient().DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + VstupniText + "&APPID=fedcf81469d34db86a7e24886bb9ae83");
                var data = JsonConvert.DeserializeObject<WeatherData>(json);
            }
        }
    }
}
