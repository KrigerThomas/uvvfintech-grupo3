using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;

namespace UVVFintech_Grupo3.Views
{
    public partial class SaqueView : Page
    {
        private readonly ContaController _controller = new();

        public SaqueView()
        {
            InitializeComponent();
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(txtValor.Text, out var valor))
            {
                _controller.Sacar(int.Parse(txtConta.Text), valor);
                MessageBox.Show("Saque realizado!");
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
