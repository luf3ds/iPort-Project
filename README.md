# 🚢 Hackathon Unisanta - iPort Solutions

## Desafio: Sistema de Gestão de Pátio de Contêineres

### Contexto

Empresas portuárias enfrentam o desafio diário de organizar grandes pátios de contêineres. Uma organização inadequada pode levar ao desperdício de tempo, aumento dos custos operacionais e movimentações desnecessárias de contêineres durante as operações de manuseio de carga.

### Objetivo

Desenvolver um sistema que auxilie no gerenciamento de um pátio de contêineres, fornecendo:

* **Cadastro de contêineres** com datas previstas de chegada e saída.
* **Organização automática de contêineres** para otimizar o uso do espaço e reduzir movimentações.
* **Visualização do pátio atual** tanto por meio de uma tabela DataGrid quanto por representação 3D.
* **Planejamento de alocação** com sugestões da posição ideal para novos contêineres.
* **Planejamento de retirada**: o sistema sugere a estratégia mais eficiente para remover um contêiner, minimizando as movimentações.
* **Histórico de operações e relatórios**: todas as movimentações, adições, edições e remoções são registradas em log.

---

## 🚀 Funcionalidades

* **Entrada de Contêineres**: Adicione um ou múltiplos contêineres, especificando as datas de chegada e saída.
* **Organização Inteligente**: Um algoritmo de otimização organiza os contêineres de forma eficiente no pátio.
* **Visualização**:
* DataGrid detalhado exibindo o status dos contêineres e datas.
* Visualização 3D interativa do pátio com suporte do HelixToolkit.


* **Plano de Retirada**: Visualize as etapas necessárias para remover um contêiner específico.
* **Relatório de Histórico de Operações**: Logs detalhados de todas as operações e movimentações.
* **Confirmação de Ações Críticas**: Criação, edição e exclusão de contêineres exigem confirmação do usuário.

---

## ✅ Requisitos Mínimos Atendidos

* Cadastro de contêineres e acompanhamento de status.
* Interface simples e intuitiva para visualização do pátio.
* Algoritmo de posicionamento e otimização.
* Relatório de histórico de remoções e movimentações.

---

## 🛠️ Tecnologias Utilizadas

* **.NET 8 / C#**
* **WPF (Windows Presentation Foundation)**
* **HelixToolkit.Wpf** (Visualização 3D)
* **Newtonsoft.Json** (Persistência de Dados)
* **Arquitetura MVVM / Services**

---

## 💻 Como Executar

1. Clone este repositório.
2. Abra a solução no Visual Studio 2022 ou posterior.
3. Restaure todos os pacotes NuGet.
4. Compile e execute o projeto `WpfHackthon`.
5. Utilize a interface para cadastrar, organizar, visualizar e planejar operações de retirada de contêineres.

---

## 🏗️ Estrutura do Projeto

* `MainWindow.xaml` / `MainWindow.xaml.cs` – Interface principal do usuário e lógica de interação.
* `Classes/PatioService.cs` – Lógica de organização e gestão do pátio de contêineres.
* `Classes/LogService.cs` – Serviço de log para todas as operações.
* `Classes/JsonService.cs` – Persistência de dados baseada em JSON.
* `Patio3DViewWindow.xaml` – Janela de visualização 3D do pátio de contêineres.

---

## 📝 Observações

* Todas as operações críticas são registradas em log para fins de auditoria.
* O sistema foi projetado para ser facilmente expandido e adaptado a diferentes cenários de operações portuárias.
* O código segue boas práticas de organização, manutenibilidade e separação de responsabilidades.

---

## 🏆 Equipe

Este projeto foi desenvolvido para fins acadêmicos durante o **Hackathon Unisanta - iPort Solutions**.

### 🤝 Integrantes

| Nome | GitHub |
| --- | --- |
| Heitor Terrabuio | [@terrabuio-heitor](https://github.com/terrabuio-heitor) |
| Luis Felipe Dias de Souza | [@luf3ds](https://github.com/luf3ds) |
| Matheus Enrico Araujo Santos | [@V0rtexs](https://github.com/V0rtexs) |
| Scott Kayllou Vitorino Melo | [@scottmelo2005-ops](https://github.com/scottmelo2005-ops) |

---

## 📄 Licença

Este projeto foi desenvolvido para fins acadêmicos e educacionais como parte do **Hackathon Unisanta - iPort Solutions**.
