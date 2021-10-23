using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OrganizaTudo.Controllers;
using OrganizaTudo.Models;

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
                    Sessao login = UsuarioController.Login(txtApelido.Text, txtSenha.Text);
                    if (login == null)
                    {
                        lblErro.Text = $"Conta \"{txtApelido.Text}\" não encontrada!";
                    }
                    else
                    {
                        // Salvar Sessão
                        Sessao repository = (Sessao)BindingContext;
                        await SessaoController.IniciarSessaoAsync(login);

                        await Navigation.PushAsync(new Home());
                        lblErro.Text = "";
                        txtApelido.Text = "";
                        txtSenha.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErro.Text = $"Ocorreu um erro: {ex.Message}";
            }
        }

        private async void btnCriarConta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroUsuario());
        }
    }
}