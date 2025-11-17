using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UVVFintech_Grupo3.DB;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Views
{
    public partial class ExtratoView : Page
    {
        private readonly int contaId;
        private readonly SQLPuro _sql = new();

        public ExtratoView(int contaId)
        {
            InitializeComponent();
            this.contaId = contaId;
            dpInicio.SelectedDate = DateTime.Today.AddMonths(-1); // default: último mês
            dpFim.SelectedDate = DateTime.Today;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var inicio = dpInicio.SelectedDate ?? DateTime.Today.AddMonths(-1);
                var fim = dpFim.SelectedDate ?? DateTime.Today;

                if (inicio > fim)
                    throw new Exception("A data de início não pode ser posterior à data final.");

                var lista = _sql.BuscarExtratoPorPeriodo(inicio, fim.AddDays(1).AddSeconds(-1), contaId); // inclui o dia inteiro
                lvTransacoes.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar extrato: {ex.Message}");
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
