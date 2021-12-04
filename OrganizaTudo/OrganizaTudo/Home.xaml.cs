using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OrganizaTudo.Controllers;
using OrganizaTudo.Models;
using System.Collections.Generic;

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

            lv.ItemsSource = notas;
            lv.ItemTemplate = new DataTemplate(typeof(ListNotas));
            lv.ItemTemplate.SetBinding(ListNotas.TituloProperty, "titulo");
            lv.ItemTemplate.SetBinding(ListNotas.PublicaProperty, "publica");
            lv.HasUnevenRows = true;

            lv.ItemTapped += (e, s) =>
            {
                DisplayAlert((s.Item as Nota).id.Oid, "", "OK");
                lv.SelectedItem = null;
            };
        }

        void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                Navigation.PushAsync(new CriarNota());
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