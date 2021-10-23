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
                if (String.IsNullOrEmpty(txtApelido.Text) || String.IsNullOrEmpty(txtSenha.Text))
                {
                    lblErro.Text = $"Preencha todos os campos!";
                }
                else
                {
                    if (UsuarioController.Login(txtApelido.Text, txtSenha.Text) == null)
                    {
                        lblErro.Text = $"Conta '{txtApelido.Text}' não encontrada!"; 
                    }
                    else
                    {
                        lblErro.Text = "";
                        txtApelido.Text = "";
                        txtSenha.Text = "";
                        // Salvar Sessão
                        await Navigation.PushAsync(new Home());
                    }
                }
            }
            catch (Exception ex)
            {
                lblErro.Text = $"Ocorreu um erro: {ex.Message}";
                // Console.WriteLine($"Erro {ex.Message}");
            }
        }

        private async void btnCriarConta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroUsuario());
        }
    }
}