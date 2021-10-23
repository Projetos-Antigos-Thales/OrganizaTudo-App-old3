using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Threading.Tasks;
using Android.Util;
using Android.Content;

namespace OrganizaTudo.Droid
{
    [Activity(Label = "Organiza Tudo", Icon = "@mipmap/icon", Theme = "@style/SplashScreenTheme", MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            await Task.Delay(1000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}