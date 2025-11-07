using System.Windows;
using System.Windows.Controls;

namespace UVVFintech_Grupo3.Views
{
    public partial class TransacoesView : Page
    {
        public TransacoesView()
        {
            InitializeComponent();
        }

        private void BtnSacar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SaqueView());
        }

        private void BtnDepositar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DepositoView());
        }

        private void BtnTransferir_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TransferenciaView());
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
