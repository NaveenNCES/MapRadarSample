using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace WeatherMapSample.Models
{
    public class TileAndTime
    {
        public DateTime TimeInterval { get; set; }
        public TileLayer TileLayer { get; set; }
    }
}
