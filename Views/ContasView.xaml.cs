using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;

namespace UVVFintech_Grupo3.Views
{
    public partial class ContasView : Page
    {
        private readonly ContaController _controller = new();

        public ContasView()
        {
            InitializeComponent();
        }

        private void BtnCriar_Click(object sender, RoutedEventArgs e)
        {
            string tipo = ((ComboBoxItem)cbTipoConta.SelectedItem).Content.ToString()!;
            //_controller.CriarConta(txtCpfCliente.Text, tipo);
            MessageBox.Show("Conta criada com sucesso!");
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //lvContas.ItemsSource = _controller.ListarContas();
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (lvContas.SelectedItem is not null)
            {
                dynamic conta = lvContas.SelectedItem;
                //_controller.ExcluirConta(conta.Id);
                MessageBox.Show("Conta excluída com sucesso!");
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
