using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Views
{
    public partial class ContasView : Page
    {
        private readonly ContaController _controller = new();

        public ContasView()
        {
            InitializeComponent();
        }
        private void AtualizarListView()
        {
            lvContas.ItemsSource = _controller.ListarTodas();
        }

        private void BtnCriar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTipoConta.SelectedItem == null)
                    throw new Exception("Selecione o tipo da conta.");

                string tipo = ((ComboBoxItem)cbTipoConta.SelectedItem).Content.ToString()!;
                string cpf = txtCpfCliente.Text;

                _controller.CriarConta(cpf, tipo);

                MessageBox.Show($"Conta do tipo {tipo} criada com sucesso!");

                txtCpfCliente.Clear();
                cbTipoConta.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar conta: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cpf = txtCpfCliente.Text;

                if (string.IsNullOrWhiteSpace(cpf))
                {
                    AtualizarListView();
                    return;
                }

                lvContas.ItemsSource = _controller.BuscarPorCpf(cpf);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvContas.SelectedItem is not Conta conta)
                {
                    MessageBox.Show("Selecione uma conta para excluir.");
                    return;
                }

                _controller.ExcluirConta(conta.Id);

                MessageBox.Show("Conta excluída com sucesso!");
                AtualizarListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
