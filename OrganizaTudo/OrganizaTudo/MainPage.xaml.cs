using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OrganizaTudo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }

        private async void btnCriarConta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cadastro_Usuario());
        }

    }
}
