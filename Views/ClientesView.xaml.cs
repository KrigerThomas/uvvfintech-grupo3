using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;

namespace UVVFintech_Grupo3.Views
{
    public partial class ClientesView : Page
    {
        private readonly ClienteController _controller = new();

        public ClientesView()
        {
            InitializeComponent();
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            _controller.Cadastrar(txtNome.Text, txtCpf.Text);
            MessageBox.Show("Cliente cadastrado com sucesso!");
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            lvClientes.ItemsSource = _controller.ListarTodos();
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (lvClientes.SelectedItem is not null)
            {
                dynamic cliente = lvClientes.SelectedItem;
                _controller.Excluir(cliente.Id);
                MessageBox.Show("Cliente excluído.");
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
