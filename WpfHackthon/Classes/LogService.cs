using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfHackthon.Classes;

namespace WpfHackthon.Classes
{
    public class LogService
    {
        private readonly string _logPath;
        private readonly string _countsPath;
        private Dictionary<string, int> _movementCounts;

        public LogService()
        {
            string logDirectory = "Logs";
            Directory.CreateDirectory(logDirectory);
            _logPath = Path.Combine(logDirectory, "log_containers.txt");
            _countsPath = Path.Combine(logDirectory, "movement_counts.json");

            CarregarContagens();
        }
        private void CarregarContagens()
        {
            if (File.Exists(_countsPath))
            {
                var json = File.ReadAllText(_countsPath);
                _movementCounts = JsonConvert.DeserializeObject<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
            }
            else
            {
                _movementCounts = new Dictionary<string, int>();
            }
        }
        private void SalvarContagens()
        {
            var json = JsonConvert.SerializeObject(_movementCounts, Formatting.Indented);
            File.WriteAllText(_countsPath, json);
        }

        public void Registrar(string acao, Container c)
        {
            string saidaStr = (c.saida == DateTime.MinValue) ? "" : $" | Saída: {c.saida:dd/MM/yyyy}";
            string posStr = (c.bay == 0) ? "" : $" | Posição: B{c.bay}-R{c.row}-T{c.tier}";
            string log = $"{DateTime.Now:G} | {acao,-10} | ID: {c.container,-12}{saidaStr}{posStr}";
            File.AppendAllText(_logPath, log + Environment.NewLine);
        }
        public void RegistrarMovimento(Container cNovo, Container cAntigo)
        {
            if (_movementCounts.ContainsKey(cNovo.container))
            {
                _movementCounts[cNovo.container]++;
            }
            else
            {
                _movementCounts[cNovo.container] = 1;
            }
            SalvarContagens();
            string posAntiga = $"B{cAntigo.bay}-R{cAntigo.row}-T{cAntigo.tier}";
            string posNova = $"B{cNovo.bay}-R{cNovo.row}-T{cNovo.tier}";
            int totalMovimentos = _movementCounts[cNovo.container];
            string log = $"{DateTime.Now:G} | {"MOVIDO",-10} | ID: {cNovo.container,-12} | De: {posAntiga,-10} -> Para: {posNova,-10} | Total de Movimentos: {totalMovimentos}";
            File.AppendAllText(_logPath, log + Environment.NewLine);
        }
    }
}