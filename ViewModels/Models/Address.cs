using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WeatherApp.ViewModels.Models
{
    public class Address
    {

        [JsonProperty("country")]
        public string[] Results { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        
    }
}
