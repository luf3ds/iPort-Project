using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfHackthon.Classes;
using Container = WpfHackthon.Classes.Container;

namespace WpfHackthon
{
    public partial class MainWindow : Window
    {
        // A MainWindow agora gerencia os SERVIÇOS
        private readonly PatioService _patioService;
        private readonly JsonService _jsonService;
        private readonly LogService _logService;

        private string caminhoArquivoJson = "";
        private bool temAlteracoesNaoSalvas = false;

        public MainWindow()
        {
            InitializeComponent();

            // Inicializa os serviços
            _patioService = new PatioService();
            _jsonService = new JsonService();
            _logService = new LogService();

            dataGridContainers.LoadingRow += DataGridContainers_LoadingRow;
            this.Closing += MainWindow_Closing;
            AtualizarGridEStatus();
        }

        #region Métodos de Interface e Delegação

        private void BtnCarregarMapeamento_Click(object sender, RoutedEventArgs e)
        {
            if (temAlteracoesNaoSalvas)
            {
                var resultado = MessageBox.Show("Você tem alterações não salvas. Deseja descartá-las?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultado == MessageBoxResult.No) return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Arquivos JSON (*.json)|*.json", Title = "Selecione o arquivo JSON" };
            if (openFileDialog.ShowDialog() == true)
            {
                caminhoArquivoJson = openFileDialog.FileName;
                try
                {
                    var containersCarregados = _jsonService.CarregarDeArquivo(caminhoArquivoJson);
                    _patioService.CarregarContainers(containersCarregados);
                    MarcarComoModificado(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao ler o arquivo JSON: {ex.Message}", "Erro de Leitura", MessageBoxButton.OK, MessageBoxImage.Error);
                    _patioService.CarregarContainers(new List<Container>()); // Limpa o pátio
                }
                finally
                {
                    AtualizarGridEStatus();
                }
            }
        }

        private void BtnOrganizar_Click(object sender, RoutedEventArgs e)
        {
            if (!_patioService.Containers.Any()) { MessageBox.Show("Nenhum contêiner para organizar.", "Aviso"); return; }

            // Passamos o serviço de log para que o PatioService possa registrar os movimentos
            int movimentos = _patioService.OtimizarPatio(_logService);

            // Este log genérico pode ser mantido ou removido, pois agora temos logs individuais
            _logService.Registrar("OTIMIZADO", new Container { container = $"Pátio ({movimentos} mov)" });

            AtualizarGridEStatus();
            MarcarComoModificado();

            MessageBox.Show($"Organização concluída!\nPrioridade do Stacker aplicada.\nContêineres movimentados: {movimentos}", "Organização Finalizada");
        }


        private void BtnDesfazer_Click(object sender, RoutedEventArgs e)
        {
            if (!_patioService.ContainersOriginais.Any()) { MessageBox.Show("Nenhuma organização para desfazer.", "Aviso"); return; }

            _patioService.DesfazerOtimizacao();
            _logService.Registrar("DESFEITO", new Container { container = "Pátio Inteiro" });

            AtualizarGridEStatus();
            MarcarComoModificado();
        }

        private void BtnIncluir_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtContainerId.Text)) { MessageBox.Show("O campo 'Container ID' não pode ser vazio."); return; }
            if (_patioService.Containers.Count >= 48) { MessageBox.Show("Capacidade máxima atingida!"); return; }
            if (_patioService.Containers.Any(c => c.container == TxtContainerId.Text)) { MessageBox.Show("Já existe um contêiner com esse código."); return; }

            var pos = _patioService.SugerirPosicaoIdeal();
            if (pos == null) { MessageBox.Show("Pátio cheio!"); return; }

            var novo = new Container
            {
                bay = pos.Value.bay,
                row = pos.Value.row,
                tier = pos.Value.tier,
                container = TxtContainerId.Text,
                entrada = DpDataEntrada.SelectedDate ?? DateTime.Today,
                saida = DpDataSaida.SelectedDate ?? DateTime.Today
            };

            _patioService.AdicionarContainer(novo);
            _logService.Registrar("INSERIDO", novo);

            AtualizarGridEStatus();
            MarcarComoModificado();
        }

        private void BtnCarregar_Click(object sender, RoutedEventArgs e)
        {
            var c = _patioService.Containers.FirstOrDefault(x => x.container == TxtContainerId.Text);
            if (c == null) { MessageBox.Show("Contêiner não encontrado."); return; }
            DpDataEntrada.SelectedDate = c.entrada;
            DpDataSaida.SelectedDate = c.saida;
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var c = _patioService.Containers.FirstOrDefault(x => x.container == TxtContainerId.Text);
            if (c == null) { MessageBox.Show("Contêiner não encontrado."); return; }

            c.entrada = DpDataEntrada.SelectedDate ?? c.entrada;
            c.saida = DpDataSaida.SelectedDate ?? c.saida;

            dataGridContainers.Items.Refresh();
            _logService.Registrar("EDITADO", c);
            MarcarComoModificado();
        }

        private void BtnRetirar_Click(object sender, RoutedEventArgs e)
        {
            var c = _patioService.Containers.FirstOrDefault(x => x.container == TxtContainerId.Text);
            if (c == null) { MessageBox.Show("Nenhum contêiner selecionado ou encontrado.", "Aviso"); return; }

            var bloqueios = _patioService.VerificarBloqueios(c);
            if (bloqueios.Any())
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Não é possível retirar '{c.container}' pois está bloqueado por {bloqueios.Count} contêiner(es).");
                sb.AppendLine("É necessário mover primeiro:");
                foreach (var bloqueio in bloqueios)
                {
                    sb.AppendLine($"- {bloqueio.container} (Posição: B{bloqueio.bay}-R{bloqueio.row}-T{bloqueio.tier})");
                }
                MessageBox.Show(sb.ToString(), "Retirada Bloqueada", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var resultado = MessageBox.Show($"Deseja realmente retirar o contêiner '{c.container}'?", "Confirmar Retirada", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultado != MessageBoxResult.Yes) return;

            _patioService.RemoverContainer(c);
            _logService.Registrar("REMOVIDO", c);

            AtualizarGridEStatus();
            MarcarComoModificado();
        }

        private void BtnPlanos1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtContainerId.Text)) { MessageBox.Show("Informe um Container ID para ver o plano."); return; }
            var c = _patioService.Containers.FirstOrDefault(x => x.container == TxtContainerId.Text);
            if (c == null) { MessageBox.Show("Contêiner não encontrado."); return; }

            var bloqueios = _patioService.Containers.Where(x => x.bay == c.bay && x.row == c.row && x.tier > c.tier).OrderBy(x => x.tier).ToList();

            string msg = bloqueios.Any()
                ? $"Para retirar, remova antes {bloqueios.Count} contêiner(es) de cima:\n" + string.Join("\n", bloqueios.Select(b => $"- {b.container} (Tier {b.tier})"))
                : "Retirada direta! Nenhum contêiner bloqueando o topo.";
            MessageBox.Show(msg, "Plano de Retirada");
        }

        #endregion

        #region Eventos de Janela e UI Auxiliares

        private void dataGridContainers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (sender is DataGrid grid && grid.SelectedItem is Container selectedContainer)
                {
                    Clipboard.SetText(selectedContainer.container);
                    e.Handled = true;
                }
            }
        }

        private void DataGridContainers_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.DataContext is not Container container) return;
            double diasParaSair = (container.saida.Date - DateTime.Today).TotalDays;
            SolidColorBrush background = diasParaSair < 0 ? Brushes.DarkGray :
                                         diasParaSair <= 3 ? Brushes.LightCoral :
                                         diasParaSair <= 14 ? Brushes.LightGoldenrodYellow :
                                         Brushes.Transparent;
            e.Row.Background = background;
            e.Row.Foreground = diasParaSair < 0 ? Brushes.White : Brushes.Black;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!temAlteracoesNaoSalvas) return;
            var resultado = MessageBox.Show("Existem alterações não salvas. Deseja salvar antes de fechar?", "Confirmar Saída", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                if (!Salvar()) e.Cancel = true;
            }
            else if (resultado == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Métodos de Salvamento

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Salvar();
        }

        // O método SalvarComo não precisa de um botão, é chamado internamente pelo Salvar()
        private bool SalvarComo()
        {
            SaveFileDialog saveDialog = new SaveFileDialog { Filter = "Arquivos JSON (*.json)|*.json", Title = "Salvar Como" };
            if (saveDialog.ShowDialog() == true)
            {
                caminhoArquivoJson = saveDialog.FileName;
                return Salvar(); // Chama o método principal de salvamento agora com o caminho
            }
            return false;
        }

        private bool Salvar()
        {
            if (string.IsNullOrEmpty(caminhoArquivoJson))
            {
                return SalvarComo();
            }
            try
            {
                _jsonService.SalvarEmArquivo(caminhoArquivoJson, _patioService.Containers, 4, 4, 3);
                MarcarComoModificado(false);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar no JSON: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Métodos de Atualização de Status (UI)
        private void AtualizarGridEStatus()
        {
            dataGridContainers.ItemsSource = null; // Opcional, mas garante a atualização
            dataGridContainers.ItemsSource = _patioService.Containers;
            dataGridContainers.Items.Refresh();
            AtualizarEspaco();
        }

        private void AtualizarEspaco()
        {
            int capacidadeMaxima = 48;
            int ocupados = _patioService.Containers.Count;
            int disponiveis = capacidadeMaxima - ocupados;
            double percentual = (capacidadeMaxima > 0) ? (ocupados / (double)capacidadeMaxima) * 100 : 0;
            if (TxtStatusEspaco != null)
            {
                TxtStatusEspaco.Text = $"Ocupados: {ocupados} / {capacidadeMaxima} | Disponíveis: {disponiveis} | Utilização: {percentual:0.##}%";
            }
        }

        private void MarcarComoModificado(bool modificado = true)
        {
            if (temAlteracoesNaoSalvas == modificado) return;
            temAlteracoesNaoSalvas = modificado;
            string tituloBase = "iPort Dock Manager";
            this.Title = modificado ? tituloBase + "*" : tituloBase;
        }

        #endregion
    }
}