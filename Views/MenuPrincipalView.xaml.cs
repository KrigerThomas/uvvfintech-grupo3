using System.Windows;
using System.Windows.Controls;

namespace UVVFintech_Grupo3.Views
{
    public partial class MenuPrincipalView : Page
    {
        public MenuPrincipalView()
        {
            InitializeComponent();
        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientesView());
        }

        private void BtnContas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ContasView());
        }

        private void BtnTransacoes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TransacoesView());
        }
    }
}
