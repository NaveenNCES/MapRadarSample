using System;
using WeatherMapSample.Droid.CustomRenderer;
using WeatherMapSample.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(WeatherMapRenderer), typeof(WeatherMapRendererAndroid))]
namespace WeatherMapSample.Droid.CustomRenderer
{
    [Obsolete]
    public class WeatherMapRendererAndroid : MapRenderer
    {
        public WeatherMapRendererAndroid()
        {
            //var _mapView = new MapView(Context, new MapViewSurface(Context));
            //_mapView.MapCenter = new GeoCoordinate(52.207767, 8.803513);
            //_mapView.MapZoom = 12;
            //_mapView.Map.AddLayerTile("http://a.tile.openstreetmap.de/tiles/osmde/{0}/{1}/{2}.png");
        }
    }
}