using System;
using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.Controllers;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Views
{
    public partial class TransacoesView : Page
    {
        private readonly ContaController _contaController = new();

        private Conta? contaSelecionada;

        public TransacoesView()
        {
            InitializeComponent();
        }

        private void AtualizarExibicaoConta()
        {
            if (contaSelecionada == null)
            {
                txtContaSelecionada.Text = "Nenhuma conta selecionada.";
                return;
            }

            
            txtContaSelecionada.Text =
                $"{contaSelecionada.Cliente?.Nome} | {contaSelecionada.TipoConta} | Nº {contaSelecionada.Numero} | Saldo R$: {contaSelecionada.Saldo}";
        }

        private void BtnSelecionarConta_Click(object sender, RoutedEventArgs e)
        {
            var seletor = new SelecionarContaView();

            NavigationService.Navigate(seletor);

            NavigationService.Navigated += (s, args) =>
            {
                if (seletor.ContaSelecionada != null)
                {
                    contaSelecionada = seletor.ContaSelecionada;
                    AtualizarExibicaoConta();
                }
            };
        }

        private void BtnDepositar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contaSelecionada == null)
                    throw new Exception("Selecione uma conta antes.");

                if (!decimal.TryParse(txtValor.Text, out decimal valor))
                    throw new Exception("Valor inválido.");

                _contaController.Depositar(contaSelecionada.Id, valor);

                contaSelecionada = _contaController.GetById(contaSelecionada.Id);

                MessageBox.Show("Depósito realizado!");
                AtualizarExibicaoConta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void BtnSacar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contaSelecionada == null)
                    throw new Exception("Selecione uma conta antes.");

                if (!decimal.TryParse(txtValor.Text, out decimal valor))
                    throw new Exception("Valor inválido.");

                _contaController.Sacar(contaSelecionada.Id, valor);

                contaSelecionada = _contaController.GetById(contaSelecionada.Id);

                MessageBox.Show("Saque realizado!");
                AtualizarExibicaoConta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void BtnTransferir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contaSelecionada == null)
                {
                    MessageBox.Show("Selecione a conta de origem primeiro.");
                    return;
                }

                var transferencia = new TransferenciaView(contaSelecionada);
                NavigationService.Navigate(transferencia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void BtnExtrato_Click(object sender, RoutedEventArgs e)
        {
            if (contaSelecionada == null)
            {
                MessageBox.Show("Selecione uma conta antes.");
                return;
            }

            var extrato = new ExtratoView(contaSelecionada.Id);
            NavigationService.Navigate(extrato);
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
