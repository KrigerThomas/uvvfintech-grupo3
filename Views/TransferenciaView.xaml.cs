using System;
using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Views
{
    public partial class TransferenciaView : Page
    {
        private readonly ContaController _controller = new();
        private readonly Conta contaOrigem;
        private Conta? contaDestino;

        // Recebe a conta de origem já selecionada (isso segue o fluxo que implementamos)
        public TransferenciaView(Conta origem)
        {
            InitializeComponent();
            contaOrigem = origem ?? throw new ArgumentNullException(nameof(origem));
            txtOrigem.Text = $"{contaOrigem.Cliente?.Nome} | {contaOrigem.TipoConta} | Nº {contaOrigem.Numero} | Saldo R$ {contaOrigem.Saldo}";
        }

        private void BtnSelecionarDestino_Click(object sender, RoutedEventArgs e)
        {
            var seletor = new SelecionarContaView();
            NavigationService.Navigate(seletor);

            // Ao voltar, pega a conta selecionada
            NavigationService.Navigated += (s, args) =>
            {
                if (seletor.ContaSelecionada != null)
                {
                    contaDestino = seletor.ContaSelecionada;
                    txtDestino.Text = $"{contaDestino.Cliente?.Nome} | {contaDestino.TipoConta} | Nº {contaDestino.Numero}";
                }
            };
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contaDestino == null)
                    throw new Exception("Selecione a conta de destino.");

                if (!decimal.TryParse(txtValor.Text, out decimal valor))
                    throw new Exception("Valor inválido.");

                // Reaproveita lógica do controller (SRP: controller orquestra)
                _controller.Transferir(contaOrigem.Id, contaDestino.Id, valor);

                MessageBox.Show("Transferência realizada com sucesso!");
                // Atualiza exibição do saldo na origem (pega novamente da DB para garantir)
                var atualizada = _controller.GetById(contaOrigem.Id);
                txtOrigem.Text = $"{atualizada.Cliente?.Nome} | {atualizada.TipoConta} | Nº {atualizada.Numero} | Saldo R$ {atualizada.Saldo}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
