using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Prism;
using Prism.Ioc;
using System;
using Xamarin.Forms.GoogleMaps.Android;

namespace WeatherMapSample.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Override default BitmapDescriptorFactory by your implementation. 
            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };


            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));

            //Native.Initialize();

            //_mapView = new MapView(this, new MapViewSurface(this));
            //_mapView.MapTilt = 0; // must be set otherwise NullReferenceException
            //_mapView.MapCenter = new GeoCoordinate(52.207767, 8.803513);
            //_mapView.MapZoom = 12;
            //_mapView.Map = new Map();
            //_mapView.MapAllowZoom = true;
            //_mapLayer = _mapView.Map.AddLayerTile("https://tile.openweathermap.org/map/precipitation_new/2/1/2.png?appid=f7008b24e47d455fadccbe35175fbe33.png");

            //using (var bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.pin))
            //{
            //    var marker = new MapMarker(this, new GeoCoordinate(52.207767, 8.803513), MapMarkerAlignmentType.CenterBottom, bitmap);
            //    _mapView.AddMarker(marker);
            //}

            //_switchOfflineToMbTiles = new Button(this);
            //_switchOfflineToMbTiles.Text = "Switch to MBTiles";
            //_switchOfflineToMbTiles.Click += SwitchOfflineToMbTiles_Click;

            //var layout = new RelativeLayout(this);
            //layout.AddView(_mapView);
            //layout.AddView(_switchOfflineToMbTiles);

            //SetContentView(layout);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //void SwitchOfflineToMbTiles_Click(object sender, EventArgs e)
        //{
        //    Console.WriteLine("Offline init started");

        //    try
        //    {
        //        using (var mapStream = Resources.Assets.Open("demo_layers.mbtiles"))
        //        {
        //            if (_mapLayer != null)
        //                _mapView.Map.RemoveLayer(_mapLayer);

        //            _mapView.Map.AddLayer(new LayerMBTile(SQLiteConnection.CreateFrom(mapStream, "map")));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //    Console.WriteLine("Offline init finished");
        //}
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

