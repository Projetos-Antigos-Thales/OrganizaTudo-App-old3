using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OrganizaTudo.Models
{
    public class ListNotas : ViewCell
    {
        Label lblTitulo, lblRotulo;
        CheckBox cboxPublica;

        public ListNotas()
        {
            // Instanciando os componentes + Definindo o Design
            StackLayout cellWrapper = new StackLayout();

            // Titulo da Nota
            StackLayout conteudo1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 15
            };

            lblTitulo = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                FontSize = 18,
            };

            // CheckBox de Publicidade da Nota
            StackLayout conteudo2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 15
            };

            lblRotulo = new Label
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14,
                Text = "Nota Publica",
            };

            cboxPublica = new CheckBox
            {
                VerticalOptions = LayoutOptions.Center,
            };

            // Definindo o valor dos atributo em cada componente
            lblTitulo.SetBinding(Label.TextProperty, "titulo");
            cboxPublica.SetBinding(CheckBox.IsCheckedProperty, "publica");

            // Adiciona os componentes nas View's
            conteudo1.Children.Add(lblTitulo);
            conteudo2.Children.Add(lblRotulo);
            conteudo2.Children.Add(cboxPublica);
            cellWrapper.Children.Add(conteudo1);
            cellWrapper.Children.Add(conteudo2);
            View = cellWrapper;
        }

        public static readonly BindableProperty TituloProperty =
            BindableProperty.Create("titulo", typeof(string), typeof(ListNotas), "titulo");
        public static readonly BindableProperty PublicaProperty =
            BindableProperty.Create("publica", typeof(bool), typeof(ListNotas), true);

        public string titulo
        {
            get { return (string)GetValue(TituloProperty); }
            set { SetValue(TituloProperty, value); }
        }

        public bool publica
        {
            get { return (bool)GetValue(PublicaProperty); }
            set { SetValue(PublicaProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                lblTitulo.Text = titulo;
                cboxPublica.IsChecked = publica;
            }
        }
    }
}
