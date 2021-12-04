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
            lv.ItemTemplate = new DataTemplate(typeof(TextCell));
            lv.ItemTemplate.SetBinding(TextCell.TextProperty, "titulo");
            //lv.ItemTemplate.SetBinding(TextCell.DetailProperty, "nota");

            lv.ItemTapped += (e, s) =>
            {
                DisplayAlert((s.Item as Nota).titulo, "Deseja excluir essa nota?", "Sim");
            };

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