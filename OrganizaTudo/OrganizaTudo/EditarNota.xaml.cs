using System;
using OrganizaTudo.Models;
using OrganizaTudo.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Toast;

namespace OrganizaTudo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarNota : ContentPage
    {
        private Sessao usuario;
        public Nota nota;

        public EditarNota(Nota nota)
        {
            InitializeComponent();
            CarregarDadosSessao();
            this.nota = nota;
            txtTitulo.Text = nota.titulo;
            txtNota.Text = nota.nota;
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
                Response response = await notasController.EditarNota(usuario.token, new Nota { titulo = txtTitulo.Text, nota = txtNota.Text }, nota._id);

                if (response.error == null)
                {
                    CrossToastPopUp.Current.ShowToastMessage(response.message);
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

        public void Titulo_Changed(object sender, EventArgs e)
        {
            CompararNota();
        }

        public void Nota_Changed(object sender, EventArgs e)
        {
            CompararNota();
        }

        public void CompararNota()
        {
            // Verifica se os componentes foram carregados
            if (txtTitulo.Text == null || txtNota.Text == null)
            {
                btnSalvar.BackgroundColor = Color.Gray;
                btnSalvar.IsEnabled = false;
            }
            // Verifica se os componentes estão vazios
            else if (txtTitulo.Text.Equals("") || txtNota.Text.Equals(""))
            {
                btnSalvar.BackgroundColor = Color.Gray;
                btnSalvar.IsEnabled = false;
            }
            else
            {
                // Verifica se os componentes possuem seu conteúdo original
                if (txtTitulo.Text.Equals(nota.titulo) && txtNota.Text.Equals(nota.nota))
                {
                    btnSalvar.BackgroundColor = Color.Gray;
                    btnSalvar.IsEnabled = false;
                }
                // Verifica se os componentes foram modificados
                else
                {
                    btnSalvar.BackgroundColor = Color.FromHex("#35C0ED");
                    btnSalvar.IsEnabled = true;
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}