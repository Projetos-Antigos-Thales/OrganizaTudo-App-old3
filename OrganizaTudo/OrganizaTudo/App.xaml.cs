using Xamarin.Forms;

namespace OrganizaTudo
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
