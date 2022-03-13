using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OrganizaTudo.Controllers;
using OrganizaTudo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Toast;
using Xamarin.Essentials;

namespace OrganizaTudo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        private Sessao usuario;

        public Home()
        {
            InitializeComponent();
            CarregarDadosSessao();
        }

        protected virtual void OnResume()
        {
            CarregarNotas();
        }

        public async void CarregarDadosSessao()
        {
            usuario = await SessaoController.BuscarSessaoAsync();
            btnSair.Text = $"Sair - {usuario.apelido}";
            CarregarNotas();
        }

        public async void CarregarNotas()
        {
            NotasController notasController = new NotasController();
            List<Nota> notas = await notasController.BuscarNotas(usuario.token);

            // Revertendo a ordem da lista, devido comportamento da API
            notas.Reverse();

            lv.ItemsSource = notas;
            lv.Refreshing += Lv_Refreshing;

            lv.ItemTapped += async (e, s) =>
            {
                lv.SelectedItem = null;
                await Navigation.PushAsync(new EditarNota(s.Item as Nota));
            };

        }

        private async void Lv_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(500);
            CarregarNotas();
            lv.IsRefreshing = false;
        }

        void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                Navigation.PushAsync(new CadastrarNota());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void btnSair_Clicked(object sender, EventArgs e)
        {
            SessaoController.FinalizarSessaoAsync();
            await Navigation.PopAsync();
        }

        public async void LinkNota(object sender, EventArgs e)
        {
            Nota nota = (Nota)((MenuItem)sender).CommandParameter;
            string URL = $"https://organizatudo.netlify.app/nota/{nota.id.Oid}";

            try
            {
                await Clipboard.SetTextAsync(URL);
                CrossToastPopUp.Current.ShowToastMessage("Link Copiado!");
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastError("Ocorreu um erro durante esse processo!");
            }

        }

        public async void VisualizarNota(object sender, EventArgs e)
        {
            Nota nota = (Nota)((MenuItem)sender).CommandParameter;
            string URL = $"https://organizatudo.netlify.app/nota/{nota.id.Oid}";

            try
            {
                await Browser.OpenAsync(URL, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastError("Ocorreu um erro durante esse processo!");
            }

        }

        public async void DeletarNota(object sender, EventArgs e)
        {
            Nota nota = (Nota)((MenuItem)sender).CommandParameter;

            try
            {
                NotasController notasController = new NotasController();


                bool confirm = await DisplayAlert("Excluir Nota", "Tem certeza que deseja excluir essa nota?", "Sim", "Cancelar");

                if (confirm)
                {
                    if (notasController.DeletarNota(usuario.token, nota.id.Oid))
                    {
                        CarregarNotas();
                        CrossToastPopUp.Current.ShowToastMessage("Nota Excluida!");
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastError("Oorreu um erro, por favor tente novamente");
                    }
                }
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastError($"Oorreu um erro: {ex.Message}");
            }

        }

    }
}