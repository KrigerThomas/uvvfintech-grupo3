using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Views
{
    public partial class SelecionarClienteView : Page
    {
        private readonly ClienteController _controller = new();
        public Cliente? ClienteSelecionado { get; private set; }

        public SelecionarClienteView()
        {
            InitializeComponent();
            lvClientes.ItemsSource = _controller.ListarTodos();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lista = _controller.BuscarPorCpf(txtCpfFiltro.Text);
                lvClientes.ItemsSource = new List<Cliente> { lista };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnSelecionar_Click(object sender, RoutedEventArgs e)
        {
            if (lvClientes.SelectedItem is Cliente cli)
            {
                ClienteSelecionado = cli;
                NavigationService.GoBack();
            }
            else
                MessageBox.Show("Selecione um cliente.");
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
