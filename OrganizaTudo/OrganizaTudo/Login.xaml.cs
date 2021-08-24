using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OrganizaTudo.Controllers;


namespace OrganizaTudo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (UsuarioController.Login(txtApelido.Text, txtSenha.Text).Any())
                {
                    txtApelido.Text = "";
                    txtSenha.Text = "";

                    await Navigation.PushAsync(new Home());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro {ex.Message}");
            }
        }

        private async void btnCriarConta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroUsuario());
        }
    }
}