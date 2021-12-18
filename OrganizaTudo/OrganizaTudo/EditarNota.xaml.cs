using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizaTudo.Models;
using OrganizaTudo.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

                if (notasController.EditarNota(usuario.token, new Nota { titulo = txtTitulo.Text , nota = txtNota.Text }, nota.id.Oid))
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Ocorreu um erro", "Tente novamente", "voltar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ocorreu um erro:", ex.Message, "voltar");
            }
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            try
            {
                NotasController notasController = new NotasController();

                if (notasController.DeletarNota(usuario.token, nota.id.Oid))
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Ocorreu um erro", "Tente novamente", "voltar");
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

    }
}