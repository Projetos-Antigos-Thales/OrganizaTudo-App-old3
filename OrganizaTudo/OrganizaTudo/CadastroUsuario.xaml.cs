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
    public partial class CadastroUsuario : ContentPage
    {
        public CadastroUsuario()
        {
            InitializeComponent();
        }

        private async void btnCriarConta_Clicked(object sender, EventArgs e)
        {
            txtApelido.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";
            await Navigation.PopAsync();
        }
    }
}