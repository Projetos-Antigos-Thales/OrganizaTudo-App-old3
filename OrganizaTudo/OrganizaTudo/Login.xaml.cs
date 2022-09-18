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
                if (usuario != null)
                {
                    if (usuario.manter) EfetuarLogin(usuario.apelido, usuario.senha, usuario.manter);
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
                lblErro.Text = "";
                EfetuarLogin(txtApelido.Text, txtSenha.Text, cBoxManterConexao.IsChecked);
            }
            catch (Exception ex)
            {
                lblErro.Text = $"Ocorreu um erro: {ex.Message}";
            }
        }

        private async void btnCriarConta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastrarUsuario());
        }

        private async void EfetuarLogin(string apelido, string senha, bool manter)
        {
            try
            {
                IniciarLoad();

                // Trava todas as opções
                ControlarComponentes(false);

                if (String.IsNullOrEmpty(apelido) || String.IsNullOrEmpty(senha))
                {
                    lblErro.Text = $"Preencha todos os campos!";

                    // Destrava todas as opções
                    ControlarComponentes(true);

                    FinalizarLoad();
                }
                else
                {
                    Sessao login = await UsuarioController.Login(apelido, senha);
                    if (login.error == null)
                    {
                        // Salvar Sessão
                        Sessao repository = (Sessao)BindingContext;
                        login.manter = manter;
                        await SessaoController.IniciarSessaoAsync(login);

                        await Navigation.PushAsync(new Home());
                        lblErro.Text = "";
                        txtApelido.Text = "";
                        txtSenha.Text = "";

                        // Destrava todas as opções
                        ControlarComponentes(true);

                        FinalizarLoad();
                    }
                    else
                    {
                        lblErro.Text = login.error;
                        SessaoController.FinalizarSessaoAsync();
                        FinalizarLoad();
                    }
                }
            }
            catch (Exception)
            {
                FinalizarLoad();
                throw;
            }
        }

        public void IniciarLoad()
        {
            btnLogin.Text = "";
            btnLogin.IsEnabled = false;
            actInd.IsRunning = true;
        }

        public void FinalizarLoad()
        {
            btnLogin.Text = "ACESSAR";
            btnLogin.IsEnabled = true;
            actInd.IsRunning = false;

        }

        public void Credenciais_Inseridas(object sender, EventArgs e)
        {
            lblErro.Text = "";
            EfetuarLogin(txtApelido.Text, txtSenha.Text, cBoxManterConexao.IsChecked);
        }

        private void ControlarComponentes(bool atividade)
        {
            txtApelido.IsEnabled = atividade;
            txtSenha.IsEnabled = atividade;
            btnLogin.IsEnabled = atividade;
            btnCriarConta.IsEnabled = atividade;
            // btnOrganizacaoOffline.IsEnabled = atividade;
            cBoxManterConexao.IsEnabled = atividade;
        }

    }
}