using System;
using Android.App;
using Android.Runtime;

namespace WeatherMapSample.Droid
{
    [Application(
        Theme = "@style/MainTheme"
        )]
    [MetaData("com.google.android.maps.v2.API_KEY",
              Value = "AIzaSyCelNVPmZ6ia-9kKcVt3IAfD6UkdIy6AQE")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Xamarin.Essentials.Platform.Init(this);
        }
    }
}
