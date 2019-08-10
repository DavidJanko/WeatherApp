using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using WeatherApp.ViewModels.Models;

namespace WeatherApp.ViewModels.Models
{

    public class CityItem
    {
        [JsonProperty("components")]
        public Components components { get; set; }
    }

    public class Components
    {
        public string _type { get; set; }

        public string city { get; set; }

        public string commercial { get; set; }

        public string continent { get; set; }

        public string country { get; set; }

        public string Country_code { get; set; }

        public string county { get; set; }

        public string state { get; set; }
    }

}