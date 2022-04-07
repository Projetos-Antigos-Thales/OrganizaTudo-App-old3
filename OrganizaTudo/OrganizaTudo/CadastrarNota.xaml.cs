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
                await notasController.InserirNota(usuario.token, new Nota { titulo = txtTitulo.Text, nota = txtNota.Text });
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ocorreu um erro:", ex.Message, "voltar");
            }
        }
    }
}