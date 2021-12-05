using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OrganizaTudo.Controllers;
using OrganizaTudo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            foreach (Nota nota in notas)
            {
                Console.WriteLine(nota.titulo + "\n");
            }

            // Revertendo a ordem da lista, devido comportamento da API
            notas.Reverse();

            lv.ItemsSource = notas;
            lv.ItemTemplate = new DataTemplate(typeof(ListNotas));
            lv.ItemTemplate.SetBinding(ListNotas.TituloProperty, "titulo");
            lv.ItemTemplate.SetBinding(ListNotas.PublicaProperty, "publica");
            lv.HasUnevenRows = true;
            lv.IsPullToRefreshEnabled = true;
            lv.Refreshing += Lv_Refreshing;
            lv.RefreshControlColor = Color.FromHex("#35C0ED");


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
    }
}