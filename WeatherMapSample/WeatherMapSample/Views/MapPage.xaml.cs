using BruTile;
using BruTile.Predefined;
using BruTile.Web;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.UI.Forms;
using Mapsui.Utilities;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
using System;
using System.IO;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherMapSample.Views
{
    public partial class MapPage : ContentPage
    {
        private DateTime CurrentDateTime;
        private static System.Timers.Timer aTimer;
        private Mapsui.Map map;

        public MapPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var map = new Mapsui.Map();

            CurrentDateTime = Roundoff();

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromDays(10));
            var location = await Geolocation.GetLocationAsync(request);

            mapView.MyLocationLayer.UpdateMyLocation(new Position(location.Latitude, location.Longitude), true);
            GetMap();
        }

        void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            aTimer = new System.Timers.Timer(3000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        async void Button_Clicked_3(System.Object sender, System.EventArgs e)
        {
            aTimer.Stop();
            aTimer.Dispose();
           // GetMap();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                CurrentDateTime = CurrentDateTime.AddMinutes(10);
                GetRepeatedMap();
            });
        }

        private void GetRepeatedMap()
        {
            var formatedTime = CurrentDateTime.ToString("yyyy'-'MM'-'dd'T'hh:mm");
            label.Text = formatedTime;

            var a = map.Layers;            
            map.Layers.Add(CreateTileLayer(CurrentDateTime));            
            mapView.Map = map;
        }

        private async void GetMap()
        {
            map = new Mapsui.Map
            {
                CRS = "EPSG:3857",
                Transformation = new MinimalTransformation()
            };

            var tileLayer = OpenStreetMap.CreateTileLayer();
            TileLayer objTile = null;

            map.Layers.Add(tileLayer);
            map.Layers.Add(CreateTileLayer(Roundoff()));
            map.Widgets.Add(new ScaleBarWidget(map) { TextAlignment = Alignment.Center, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Bottom });

            mapView.Map = map;
        }

        private static readonly BruTile.Attribution OpenStreetMapAttribution = new BruTile.Attribution(
            "© OpenStreetMap contributors", "https://www.openstreetmap.org/copyright");

        private static HttpTileSource CreateTileSource(DateTime date, string userAgent)
        {
            var formatedTime = date.ToString("yyyy'-'MM'-'dd'T'hh:mm");
            string url = "https://maps.openweathermap.org/maps/2.0/radar/{z}/{x}/{y}?appid=9de243494c0b295cca9337e1e96b00e2&day=";
            string a = $"{url}{formatedTime}";
            return new HttpTileSource(new GlobalSphericalMercator(),
                a,
                new[] { "a", "b", "c" }, name: "OpenRailwayMap",
                attribution: OpenStreetMapAttribution, userAgent: userAgent);
        }

        public static TileLayer CreateTileLayer(DateTime date, string userAgent = null)
        {
            if (String.IsNullOrEmpty(userAgent))
                userAgent = $"user-agent-of-{Path.GetFileNameWithoutExtension(System.AppDomain.CurrentDomain.FriendlyName)}";

            return new TileLayer(CreateTileSource(date, userAgent)) { Name = "OpenRailwayMap" };
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

            return new DateTime(
                                dateTime.Year,
                                dateTime.Month,
                                dateTime.Day,
                                dateTime.Hour + 1,
                                0,
                                dateTime.Second,
                                dateTime.Millisecond,
                                dateTime.Kind); ;
        }
    }
}
