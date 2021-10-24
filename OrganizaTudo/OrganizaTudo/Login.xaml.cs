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
            ValidarSessao();
            InitializeComponent();
        }

        public async void ValidarSessao()
        {
            try
            {
                Sessao usuario = await SessaoController.BuscarSessaoAsync();
                if (usuario != null){
                    EfetuarLogin(usuario.apelido, usuario.senha, usuario.manter);
                }
            }
            catch (Exception ex)
            {
                lblErro.Text = $"Ocorreu um erro ao efetuar o Login Automático: {ex.Message}";
            }
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                EfetuarLogin(txtApelido.Text, txtSenha.Text, cBoxManterConexao.IsChecked);
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

        private async void EfetuarLogin(string apelido, string senha, bool manter)
        {
            try
            {
                if (String.IsNullOrEmpty(apelido) || String.IsNullOrEmpty(senha))
                {
                    lblErro.Text = $"Preencha todos os campos!";
                }
                else
                {
                    Sessao login = UsuarioController.Login(apelido, senha);
                    if (login == null)
                    {
                        lblErro.Text = $"Conta \"{apelido}\" não encontrada!";
                    }
                    else
                    {
                        // Salvar Sessão
                        Sessao repository = (Sessao)BindingContext;
                        login.manter = manter;
                        await SessaoController.IniciarSessaoAsync(login);

                        await Navigation.PushAsync(new Home());
                        lblErro.Text = "";
                        txtApelido.Text = "";
                        txtSenha.Text = "";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}