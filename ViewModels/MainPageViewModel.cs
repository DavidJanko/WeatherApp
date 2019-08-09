using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels.Models;
using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class MainPageViewModel : Abstract.Abstract
    {
        private readonly Database database;

        public Command Vyhledat { get; set; } //Commandy(buttony)
        public Command HledatGPS { get; set; }

        public MainPageViewModel()
        {
            Vyhledat = new Command(GetWeatherData); //přiřazení akce k commandu
            HledatGPS = new Command(NajitLokaci);
            database = new Database(); //načtení databáze
        }
        public string VstupniText { get; set; } //Název města

        public string Teplota { get; set; }
        public string DruhPocasi { get; set; }
        public string Oblacnost { get; set; }
        public string Vlhkost { get; set; }
        public string ZapadSlunce { get; set; }

        private void NajitLokaci()
        {
            
        }

        private void GetWeatherData()
        {
            if (VstupniText == null || VstupniText == "")
            {
                App.Current.MainPage.DisplayAlert("Chyba!", "Není zadané město!", "Ok"); 
            }
            else
            {
                try
                {
                    var json = new WebClient().DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + VstupniText + "&APPID=fedcf81469d34db86a7e24886bb9ae83");
                    var data = JsonConvert.DeserializeObject<WeatherData>(json);

                    Teplota = (data.Main.Temperature - 272.15).ToString(); //převod na celsius
                    DruhPocasi = data.Weather[0].Visibility.ToString();
                    Oblacnost = data.Clouds.All.ToString() + "%"; //Oblačnost v procentech
                    Vlhkost = data.Main.Humidity.ToString();
                    ZapadSlunce = GetDateTime(data.Sys.Sunset);


                    database.PridatDoHistorie(new HistoryData { Title = data.Title, Temperature = data.Main.Temperature });
                }
                catch
                {
                    App.Current.MainPage.DisplayAlert("Chyba!", "Město neexistuje!", "Ok");
                }
            }

            OnPropertyChanged("Teplota"); //aktualizování property
            OnPropertyChanged("DruhPocasi");
            OnPropertyChanged("Oblacnost");
            OnPropertyChanged("Vlhkost");
            OnPropertyChanged("ZapadSlunce");
        }

        private string GetDateTime(long time) //převod z unix času + časové pásmo pro ČR
        {
            int TZ = 2;
            DateTime date = new System.DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(time);

            TimeSpan timee = new TimeSpan(0, TZ, 0, 0);

            DateTime final = date.Add(timee);

            return final.ToString();
        }
    }
}
