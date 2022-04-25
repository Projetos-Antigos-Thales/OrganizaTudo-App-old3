using System;
using OrganizaTudo.Controllers;
using OrganizaTudo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizaTudo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarNota : ContentPage
    {
        private Sessao usuario;

        public CadastrarNota()
        {
            InitializeComponent();
            CarregarDadosSessao();
        }

        public async void CarregarDadosSessao()
        {
            usuario = await SessaoController.BuscarSessaoAsync();
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                NotasController notasController = new NotasController();
                Response response = await notasController.InserirNota(usuario.token, new Nota { titulo = txtTitulo.Text, nota = txtNota.Text });

                if (response.error == null)
                {
                    // CrossToastPopUp.Current.ShowToastMessage(response.message);
                    await Navigation.PushAsync(new Home());
                }
                else
                {
                    await DisplayAlert("Ocorreu um erro", response.error, "voltar");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ocorreu um erro:", ex.Message, "voltar");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}