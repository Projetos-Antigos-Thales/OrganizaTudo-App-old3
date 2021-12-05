using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizaTudo.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizaTudo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarNota : ContentPage
    {
        public Nota nota;

        public EditarNota(Nota nota)
        {
            InitializeComponent();
            this.nota = nota;
            txtTitulo.Text = nota.titulo;
            txtNota.Text = nota.nota;
        }
        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                DisplayAlert("Ocorreu um erro:", ex.Message, "voltar");
            }
        }
        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                DisplayAlert("Ocorreu um erro:", ex.Message, "voltar");
            }
        }
    }
}