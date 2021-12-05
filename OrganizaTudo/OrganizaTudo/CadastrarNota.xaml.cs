using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizaTudo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarNota : ContentPage
    {
        public CadastrarNota()
        {
            InitializeComponent();
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
    }
}