using System;
using OrganizaTudo.Controllers;
using OrganizaTudo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizaTudo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarUsuario : ContentPage
    {
        public CadastrarUsuario()
        {
            InitializeComponent();
        }

        private async void btnCriarConta_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtApelido.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSenha.Text))
                {
                    lblErro.Text = $"Preencha todos os campos!";

                    // Destrava todas as opções
                    ControlarComponentes(true);
                }
                else
                {
                    lblErro.Text = "";
                    btnCriarConta.Text = "";

                    // Trava todas as opções
                    ControlarComponentes(false);

                    // Se não houver response.erro, significa que a conta foi criada corretamente
                    Response response = await UsuarioController.CriarConta(txtApelido.Text, txtEmail.Text, txtSenha.Text);

                    btnCriarConta.Text = "CRIAR CONTA";

                    // Destrava todas as opções
                    ControlarComponentes(true);

                    if (response.error == null)
                    {
                        // CrossToastPopUp.Current.ShowToastMessage(response.message);
                        await Navigation.PopAsync();
                        txtApelido.Text = "";
                        txtEmail.Text = "";
                        txtSenha.Text = "";
                    }
                    else
                    {
                        lblErro.Text = response.error;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErro.Text = $"{ex.Message}";
            }

        }

        private void ControlarComponentes(bool atividade)
        {
            actInd.IsRunning = !atividade;
            actInd.IsVisible = !atividade;
            txtApelido.IsEnabled = atividade;
            txtEmail.IsEnabled = atividade;
            txtSenha.IsEnabled = atividade;
            btnCriarConta.IsEnabled = atividade;
        }
    
    }
}