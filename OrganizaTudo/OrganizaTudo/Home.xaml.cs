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
        // Obj que salva os dados de sessão do usuário para manuseio
        private Sessao usuario;

        // Objs que armazenam as notas do usuário para manuseio
        private List<Nota> notasOnline;
        private List<Nota> notasOffline;

        // true = a lista está sendo atualizada
        private bool isRefresing = false;

        public Home()
        {
            InitializeComponent();
            CarregarDadosSessao();
        }

        // Obtem os dados locais do usuário logado
        public async void CarregarDadosSessao()
        {
            usuario = await SessaoController.BuscarSessaoAsync();
            btnSair.Text = $"Sair - {usuario.apelido}";
            CarregarNotas();
        }

        // Faz o logoff do usuário
        private async void btnSair_Clicked(object sender, EventArgs e)
        {
            SessaoController.FinalizarSessaoAsync();
            await Navigation.PopAsync();
        }

        // Obtem as notas online
        public async void CarregarNotas()
        {
            NotasController notasController = new NotasController();
            List<Nota> notas = await notasController.BuscarNotas(usuario.token);

            // Revertendo a ordem da lista, devido comportamento da API
            notas.Reverse();

            notasOnline = notas;
            notasOffline = notas;
            lv.ItemsSource = notas;
            lv.Refreshing += Lv_Refreshing;

            lv.ItemTapped += async (e, s) =>
            {
                lv.SelectedItem = null;
                await Navigation.PushAsync(new EditarNota(s.Item as Nota));
            };

        }

        // Evento que roda quando a ListView está sendo atualizada
        private async void Lv_Refreshing(object sender, EventArgs e)
        {
            isRefresing = true;
            txtSearch.Text = "";
            await Task.Delay(500);
            CarregarNotas();
            lv.IsRefreshing = false;
            isRefresing = false;
        }

        // Copia a URL da nota para o dispositivo do usuário
        public async void LinkNota(object sender, EventArgs e)
        {
            Nota nota = (Nota)((MenuItem)sender).CommandParameter;
            string URL = $"https://organizatudo.netlify.app/nota/{nota._id}";

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

        // Redireciona o usuário para o navegador (abrindo a nota)
        public async void VisualizarNota(object sender, EventArgs e)
        {
            Nota nota = (Nota)((MenuItem)sender).CommandParameter;
            string URL = $"https://organizatudo.netlify.app/nota/{nota._id}";

            try
            {
                await Browser.OpenAsync(URL, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastError("Ocorreu um erro durante esse processo!");
            }

        }

        // Excluir a nota do usuario (possui uma mensagem de confirmação)
        public async void DeletarNota(object sender, EventArgs e)
        {
            Nota nota = (Nota)((MenuItem)sender).CommandParameter;

            try
            {
                NotasController notasController = new NotasController();


                bool confirm = await DisplayAlert("Excluir Nota", "Tem certeza que deseja excluir essa nota?", "Sim", "Cancelar");

                if (confirm)
                {
                    Response response = await notasController.DeletarNota(usuario.token, nota._id);

                    if (response.error == null)
                    {
                        CarregarNotas();
                        CrossToastPopUp.Current.ShowToastMessage(response.message);
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastError(response.error);
                    }
                }
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastError($"Oorreu um erro: {ex.Message}");
            }

        }

        // Altera a publicidade de uma nota
        public async void PrivacidadeNota(object sender, EventArgs e)
        {
            Nota nota = (Nota)((MenuItem)sender).CommandParameter;

            try
            {
                NotasController notasController = new NotasController();
                Response response = await notasController.AtualizarPrivacidadeNota(usuario.token, nota._id);

                if (response.error == null)
                {
                    CarregarNotas();
                    CrossToastPopUp.Current.ShowToastMessage(response.message);
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError(response.error);
                }
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastError($"Oorreu um erro");
            }

        }

        // Busca notas pelo título
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string regex = e.NewTextValue;
            if (!string.IsNullOrEmpty(regex))
            {

                NotasController notasController = new NotasController();
                List<Nota> notas = notasController.PesquisarNotas(notasOffline, regex);
                lv.ItemsSource = notas;
            }
            else if (!isRefresing)
            {
                lv.ItemsSource = notasOnline;
            }
        }


        // Botão para criar nova nota (redireciona para outra página)
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

        // Previne o usuário voltar para a tela de login
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void Dados_Inseridos(object sender, EventArgs e)
        {

        }

    }
}