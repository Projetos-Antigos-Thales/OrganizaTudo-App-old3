﻿using System;
using OrganizaTudo.Controllers;
using OrganizaTudo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizaTudo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarUsuario : ContentPage
    {
        public CadastrarUsuario()
        {
            InitializeComponent();
        }

        private async void btnCriarConta_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtApelido.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSenha.Text))
                {
                    lblErro.Text = $"Preencha todos os campos!";
                }
                else
                {
                    lblErro.Text = "";
                    btnCriarConta.Text = "";

                    // Trava todas as opções
                    bool controles = false;
                    actInd.IsRunning = !controles;
                    actInd.IsVisible = !controles;
                    txtApelido.IsEnabled = controles;
                    txtEmail.IsEnabled = controles;
                    txtSenha.IsEnabled = controles;
                    btnCriarConta.IsEnabled = controles;

                    // Se não houver response.erro, significa que a conta foi criada corretamente
                    Response response = await UsuarioController.CriarConta(txtApelido.Text, txtEmail.Text, txtSenha.Text);

                    btnCriarConta.Text = "CRIAR CONTA";

                    controles = true;
                    actInd.IsRunning = !controles;
                    actInd.IsVisible = !controles;
                    txtApelido.IsEnabled = controles;
                    txtEmail.IsEnabled = controles;
                    txtSenha.IsEnabled = controles;
                    btnCriarConta.IsEnabled = controles;

                    if (response.error == null)
                    {
                        // CrossToastPopUp.Current.ShowToastMessage(response.message);
                        await Navigation.PopAsync();
                        txtApelido.Text = "";
                        txtEmail.Text = "";
                        txtSenha.Text = "";
                    }
                    else
                    {
                        lblErro.Text = response.error;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErro.Text = $"{ex.Message}";
            }

        }
    }
}