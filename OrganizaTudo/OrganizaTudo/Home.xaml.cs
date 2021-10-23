using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizaTudo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        public Home()
        {
            InitializeComponent();
            // btnSair.Text = SessaoController.BuscarSessaoAsync().Result.apelido;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private async void btnSair_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}