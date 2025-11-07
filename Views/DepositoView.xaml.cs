using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;

namespace UVVFintech_Grupo3.Views
{
    public partial class DepositoView : Page
    {
        private readonly ContaController _controller = new();

        public DepositoView()
        {
            InitializeComponent();
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(txtValor.Text, out var valor))
            {
                _controller.Depositar(int.Parse(txtConta.Text), valor);
                MessageBox.Show("Depósito realizado!");
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
