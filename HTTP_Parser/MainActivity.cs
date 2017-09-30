using Android.App;
using Android.Widget;
using Android.OS;

namespace HTTP_Parser
{
    [Activity(Label = "HTTP_Parser", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button parser;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Main);

            var latitude = 46.7404283551898;
            var longitude = 28.8811472434254;

            string request = "https://api.vk.com/method/photos.search?lat=";
            string url = request + latitude + "&long=" + longitude + "&count=150&radius=500&v=5.67";

            parser = FindViewById<Button>(Resource.Id.button1);
            parser.Click += delegate
            {
                HttpParser http = new HttpParser();
                http.GetPhotos(url);
            };
        }
    }
}

