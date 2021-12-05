using System;
using OrganizaTudo.Controllers;
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
                }
                else
                {
                    lblErro.Text = UsuarioController.CriarConta(txtApelido.Text, txtEmail.Text, txtSenha.Text);

                    if (lblErro.Text == null)
                    {
                        await Navigation.PopAsync();
                        txtApelido.Text = "";
                        txtEmail.Text = "";
                        txtSenha.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErro.Text = $"{ex.Message}";
            }

        }
    }
}