# Hackathon Unisanta - iPort Solutions

## Desafio: Sistema de Gestão de Pátio de Contêineres

### Contexto

Empresas portuárias enfrentam diariamente o desafio de organizar grandes pátios de contêineres. Uma má organização pode gerar desperdício de tempo, maior custo operacional e excesso de movimentos desnecessários na movimentação das cargas.

### Objetivo

Criar um sistema que auxilie a gestão de um pátio de contêineres, permitindo:

- **Cadastro de contêineres** com datas de entrada e saída previstas.
- **Organização automática dos contêineres** para otimizar espaço e reduzir movimentos.
- **Visualização do estado atual do pátio** em tabela (DataGrid) e visualização 3D.
- **Planejamento de alocação** e sugestão de posição ideal para novos contêineres.
- **Plano de retirada**: o sistema sugere a melhor estratégia para retirar um contêiner, minimizando movimentos.
- **Histórico e relatório de operações**: todas as movimentações, inclusões, edições e remoções são registradas em log.

---

## Funcionalidades

- **Input de contêineres**: Adicione um ou mais contêineres, informando datas de entrada e saída.
- **Organização inteligente**: Algoritmo otimiza a disposição dos contêineres no pátio.
- **Visualização**: 
  - Tabela detalhada (DataGrid) com status e datas.
  - Visualização 3D interativa do pátio (HelixToolkit).
- **Plano de retirada**: Veja os passos necessários para retirar um contêiner específico.
- **Relatório de histórico**: Log detalhado de todas as operações e movimentações.
- **Confirmação de ações críticas**: Edição, inclusão e remoção de contêineres exigem confirmação do usuário.

---

## Requisitos Mínimos Atendidos

- Cadastro e status dos contêineres.
- Interface simples e intuitiva para visualização do pátio.
- Algoritmo de posicionamento e otimização.
- Relatório/histórico de remoções e movimentações.

---

## Tecnologias Utilizadas

- **.NET 8 / C#**
- **WPF (Windows Presentation Foundation)**
- **HelixToolkit.Wpf** (visualização 3D)
- **Newtonsoft.Json** (persistência dos dados)
- **MVVM/Services** (organização do código)

---

## Como Executar

1. Clone este repositório.
2. Abra a solução no Visual Studio 2022 ou superior.
3. Restaure os pacotes NuGet.
4. Compile e execute o projeto `WpfHackthon`.
5. Utilize a interface para cadastrar, organizar, visualizar e planejar a retirada dos contêineres.

---

## Estrutura do Projeto

- `MainWindow.xaml` / `MainWindow.xaml.cs`: Interface principal e lógica de interação.
- `Classes/PatioService.cs`: Lógica de organização e manipulação dos contêineres.
- `Classes/LogService.cs`: Registro de todas as operações.
- `Classes/JsonService.cs`: Persistência dos dados em JSON.
- `Patio3DViewWindow.xaml`: Visualização 3D do pátio.

---

## Observações

- Todas as operações críticas são registradas em log para auditoria.
- O sistema foi desenvolvido para ser facilmente expandido e adaptado a diferentes cenários portuários.
- O código segue boas práticas de organização e separação de responsabilidades.

---

## Licença

Este projeto foi desenvolvido para fins acadêmicos no Hackathon Unisanta - iPort Solutions.
Pelo grupo Teck Minds participante: [Heitor Terrabuio, Luis Felipe Dias de Souza, Matheus Enrico Araujo Santos, Scott Kayllou Vitorino Melo].