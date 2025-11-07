using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;

namespace UVVFintech_Grupo3.Views
{
    public partial class TransferenciaView : Page
    {
        private readonly ContaController _controller = new();

        public TransferenciaView()
        {
            InitializeComponent();
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(txtValor.Text, out var valor))
            {
                _controller.Transferir(
                    int.Parse(txtContaOrigem.Text),
                    int.Parse(txtContaDestino.Text),
                    valor);
                MessageBox.Show("Transferência realizada!");
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
