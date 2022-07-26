using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace WeatherMapSample.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly IPermissions _permissions;
        private readonly IGeolocation _geoLocation;
        private static System.Timers.Timer aTimer;
        private DateTime CurrentDateTime;
        private List<DateTime> IntervalList = new List<DateTime>();
        private List<TileLayer> TileLayersList = new List<TileLayer>();
        private int minCount = 10;
        private int timeCount = 0;

        public MainPage(IPermissions permissions, IGeolocation geolocation)
        {
            InitializeComponent();
            _geoLocation = geolocation;
            _permissions = permissions;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AddTimeIntervals();
            //CurrentDateTime = Roundoff();

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromDays(10));
            var location = await Geolocation.GetLocationAsync(request);
            var lat = location.Latitude;
            var lng = location.Longitude;

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lng), Distance.FromMiles(500)));
        }


        void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            //aTimer = new System.Timers.Timer(1000);
            //aTimer.Elapsed += AddTimeIntervals;
            //aTimer.AutoReset = true;
            //aTimer.Enabled = true;
            SetTileLayer();
        }

        private void AddTimeIntervals()
        {
            for (int i = 0; i < 60; i++)
            {
                IntervalList.Add(Roundoff().AddMinutes(minCount));
                minCount += 10;
            }

            foreach (DateTime date in IntervalList)
            {
                var formatedTime = date.ToString("yyyy'-'MM'-'dd'T'hh:mm");

                TileLayersList.Add(TileLayer.FromTileUri((int x, int y, int zoom) =>
                {
                    Uri uri = new Uri($"https://maps.openweathermap.org/maps/2.0/radar/{zoom}/{x}/{y}?appid=9de243494c0b295cca9337e1e96b00e2&day=" + formatedTime);
                    return uri;
                }));
            }
        }

        private async void SetTileLayer()
        {
            map.MapType = MapType.Street;
            foreach (TileLayer objTile in TileLayersList)
            {
                label.Text = IntervalList[timeCount].ToString("MM/dd/yyyy HH:mm");

                TileLayer t = null;
                if (map.TileLayers.Count > 0)
                {
                    t = map.TileLayers[0];
                }

                map.TileLayers.Add(objTile);
                await Task.Delay(1000);

                if (t != null)
                {
                    map.TileLayers.Remove(t);
                }

                timeCount++;
            }
            timeCount = 0;
        }

        public static DateTime Roundoff()
        {
            var dateTime = DateTime.Now.AddDays(-1).AddMinutes(15);
            int minutes = dateTime.Minute;
            for (int i = minutes; i < 60; i++)
            {
                if (minutes % 10 == 0)
                {
                    return new DateTime(
                                 dateTime.Year,
                                 dateTime.Month,
                                 dateTime.Day,
                                 dateTime.Hour,
                                 minutes,
                                 dateTime.Second,
                                 dateTime.Millisecond,
                                 dateTime.Kind);
                }

                minutes++;
            }
            var a = new DateTime(
                                 dateTime.Year,
                                 dateTime.Month,
                                 dateTime.Day,
                                 dateTime.Hour,
                                 10,
                                 dateTime.Second,
                                 dateTime.Millisecond,
                                 dateTime.Kind);
            a = a.AddHours(1);
            return a;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                CurrentDateTime = CurrentDateTime.AddMinutes(10);
                var formatedTime = CurrentDateTime.ToString("yyyy'-'MM'-'dd'T'hh:mm");
                label.Text = formatedTime;

                TileLayer objTile = null;

                objTile = TileLayer.FromTileUri((int x, int y, int zoom) =>
                {
                    Uri uri = new Uri($"https://maps.openweathermap.org/maps/2.0/radar/{zoom}/{x}/{y}?appid=9de243494c0b295cca9337e1e96b00e2&day=" + formatedTime);
                    return uri;
                });
                objTile.Tag = "OSMTILE"; // Can set any object
                TileLayer t = null;
                if (map.TileLayers.Count > 0)
                {
                    t = map.TileLayers[0];
                }

                map.TileLayers.Add(objTile);

                if (t != null)
                {
                    map.TileLayers.Remove(t);
                }

                map.MapType = MapType.Street;

            });
        }

        async void Button_Clicked_3(System.Object sender, System.EventArgs e)
        {
            return;
            CurrentDateTime = CurrentDateTime.AddMinutes(10);
            var formatedTime = CurrentDateTime.ToString("yyyy'-'MM'-'dd'T'hh:mm");
            label.Text = formatedTime;

            TileLayer objTile = null;
            map.TileLayers.Clear();

            objTile = TileLayer.FromTileUri((int x, int y, int zoom) =>
            {
                string uriString = $"https://maps.openweathermap.org/maps/2.0/radar/{zoom}/{x}/{y}?appid=9de243494c0b295cca9337e1e96b00e2&day={formatedTime}";
                return new Uri(uriString);
            });

            objTile.Tag = "OSMTILE"; // Can set any object
            map.TileLayers.Add(objTile);
            map.MapType = MapType.Street;

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromDays(10));
            var location = await Geolocation.GetLocationAsync(request);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMiles(500)));
            return;
        }
    }
}
