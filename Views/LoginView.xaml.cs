using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace UVVFintech_Grupo3.Views
{
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            // Futuramente você pode validar usuário/senha no BD
            NavigationService.Navigate(new MenuPrincipalView());
        }
    }
}
