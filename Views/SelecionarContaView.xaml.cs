using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Views
{
    public partial class SelecionarContaView : Page
    {
        private readonly ContaController _controller = new();

        public Conta? ContaSelecionada { get; private set; }

        public SelecionarContaView()
        {
            InitializeComponent();
            lvContas.ItemsSource = _controller.ListarTodas();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cpf = txtCpf.Text.Trim();

                if (string.IsNullOrWhiteSpace(cpf))
                {
                    MessageBox.Show("Digite um CPF para buscar.");
                    return;
                }

                var contas = _controller.BuscarPorCpf(cpf);

                if (contas.Count == 0)
                {
                    MessageBox.Show("Nenhuma conta encontrada para esse CPF.");
                }

                lvContas.ItemsSource = contas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar contas: {ex.Message}");
            }
        }

        private void BtnSelecionar_Click(object sender, RoutedEventArgs e)
        {
            if (lvContas.SelectedItem is not Conta conta)
            {
                MessageBox.Show("Selecione uma conta.");
                return;
            }

            ContaSelecionada = conta;
            NavigationService.GoBack();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
