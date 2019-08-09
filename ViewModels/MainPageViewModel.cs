using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            NejakejText = "Už nechci umřít";
        }

        public string NejakejText { get; set; }
    }
}
