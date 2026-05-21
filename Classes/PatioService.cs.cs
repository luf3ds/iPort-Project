using System;
using System.Collections.Generic;
using System.Linq;
using WpfHackthon.Classes;

namespace WpfHackthon.Classes
{
    public class PatioService
    {
        public List<Container> Containers { get; private set; } = new List<Container>();
        public List<Container> ContainersOriginais { get; private set; } = new List<Container>();

        public void CarregarContainers(List<Container> containersCarregados)
        {
            Containers = containersCarregados ?? new List<Container>();
            ContainersOriginais.Clear();
        }

        public List<Container> VerificarBloqueios(Container containerAlvo)
        {
            if (containerAlvo == null) return new List<Container>();

            return Containers
                .Where(c => c.bay == containerAlvo.bay && c.row == containerAlvo.row && c.tier > containerAlvo.tier)
                .OrderBy(c => c.tier)
                .ToList();
        }

        public int OtimizarPatio(LogService logService, int totalBays = 4, int totalRows = 4, int totalTiers = 3)
        {
            if (!Containers.Any()) return 0;

            ContainersOriginais = Containers.Select(c => c.Clone()).ToList();

            var containersOrdenados = Containers.OrderBy(c => c.saida).ToList();
            var containersOrganizados = new List<Container>();

            int tamanhoDoGrupo = (int)Math.Ceiling((double)containersOrdenados.Count / totalBays);
            if (tamanhoDoGrupo == 0) return 0;
            var gruposPorBaia = containersOrdenados
                .Select((container, index) => new { container, index })
                .GroupBy(x => x.index / tamanhoDoGrupo)
                .Select(g => g.Select(x => x.container).ToList())
                .ToList();

            for (int i = 0; i < gruposPorBaia.Count; i++)
            {
                int baiaAtual = i + 1;
                var grupoDaBaia = gruposPorBaia[i];
                var pilhasDeContainers = grupoDaBaia
                    .Select((container, index) => new { container, index })
                    .GroupBy(x => x.index / totalTiers)
                    .Select(g => g.Select(x => x.container).ToList())
                    .ToList();

                for (int j = 0; j < pilhasDeContainers.Count; j++)
                {
                    int fileiraAtual = j + 1;
                    var pilhaAtual = pilhasDeContainers[j];
                    int tierInicial = (pilhaAtual.Count < totalTiers) ? pilhaAtual.Count : totalTiers;
                    for (int k = 0; k < pilhaAtual.Count; k++)
                    {
                        var container = pilhaAtual[k];
                        container.bay = baiaAtual;
                        container.row = fileiraAtual;
                        container.tier = tierInicial - k;
                        containersOrganizados.Add(container);
                    }
                }
            }

            Containers = containersOrganizados;
            int movimentos = 0;
            foreach (var cNovo in Containers)
            {
                var cAntigo = ContainersOriginais.FirstOrDefault(c => c.container == cNovo.container);
                if (cAntigo != null && (cAntigo.bay != cNovo.bay || cAntigo.row != cNovo.row || cAntigo.tier != cNovo.tier))
                {
                    movimentos++;
                    logService.RegistrarMovimento(cNovo, cAntigo);
                }
            }
            return movimentos;
        }

        public void DesfazerOtimizacao()
        {
            if (ContainersOriginais.Any())
            {
                Containers = new List<Container>(ContainersOriginais);
            }
        }

        public (int bay, int row, int tier)? SugerirPosicaoIdeal(int totalBays = 4, int totalRows = 4, int totalTiers = 3)
        {
            var ocupadas = new HashSet<string>(Containers.Select(c => $"{c.bay}-{c.row}-{c.tier}"));
            for (int b = 1; b <= totalBays; b++)
                for (int r = 1; r <= totalRows; r++)
                    for (int t = totalTiers; t >= 1; t--)
                        if (!ocupadas.Contains($"{b}-{r}-{t}"))
                            return (b, r, t);
            return null;
        }

        public void AdicionarContainer(Container novo)
        {
            Containers.Add(novo);
        }

        public void RemoverContainer(Container paraRemover)
        {
            Containers.Remove(paraRemover);
        }
    }
}