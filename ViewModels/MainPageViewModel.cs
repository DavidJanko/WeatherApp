﻿using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Net;
using WeatherApp.ViewModels.Models;
using Xamarin.Essentials;
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

        private async void NajitLokaci()
        {
            try
            {
                Permission[] _requiredPermissions = { Permission.Location }; //Povolení lokace
                var results = await CrossPermissions.Current.RequestPermissionsAsync(_requiredPermissions); //Čekání na povolení lokace
                var location = await Geolocation.GetLocationAsync(); //Získání lokace
                var json = new WebClient().DownloadString("https://api.opencagedata.com/geocode/v1/json?q=" + location.Latitude.ToString() + "+" + location.Longitude.ToString() + "&key=40bea2833c1f4a3aabd05266cc01b682"); //API call
                var data = JsonConvert.DeserializeObject<GeocoderResponse>(json);
                foreach (var component in data.Results[0].ComponentsDictionary)
                {
                    if (component.Key == "city")
                    {
                        VstupniText = component.Value;
                    }
                }
                OnPropertyChanged("VstupniText");
            }
            catch
            {
                Permission[] _requiredPermissions = { Permission.Location }; 
                var results = await CrossPermissions.Current.RequestPermissionsAsync(_requiredPermissions); 
            }
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
