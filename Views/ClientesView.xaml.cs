using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Views
{
    public partial class ClientesView : Page
    {
        private readonly ClienteController _controller = new();

        public ClientesView()
        {
            InitializeComponent();
        }

        private void AtualizarListView()
        {
            lvClientes.ItemsSource = _controller.ListarTodos();
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try { 
            _controller.Cadastrar(txtNome.Text, txtCpf.Text);
            MessageBox.Show("Cliente cadastrado com sucesso!");

            txtNome.Clear();
            txtCpf.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar cliente: {ex.Message}", "Erro" , MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCpf.Text))
                {
                    AtualizarListView();
                    return;
                }

                var cliente = _controller.BuscarPorCpf(txtCpf.Text);
                lvClientes.ItemsSource = new List<Cliente> { cliente };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvClientes.SelectedItem is not Cliente selecionado)
                {
                    MessageBox.Show("Selecione um cliente.", "Aviso");
                    return;
                }

                _controller.Excluir(selecionado.Id);
                MessageBox.Show("Cliente excluído com sucesso!");
                AtualizarListView();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir cliente: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
