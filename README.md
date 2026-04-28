# 📦 Sistema de Organização de Containers (WPF)

Projeto desenvolvido durante um **hackathon**, com o objetivo de simular e otimizar a organização de containers em um pátio logístico utilizando **WPF (Windows Presentation Foundation)**.

---

## 🚀 Funcionalidades

- 📥 Importação de dados via JSON  
- 💾 Persistência de containers em arquivo  
- 📊 Organização automática do pátio  
- 🔄 Otimização baseada na data de saída dos containers  
- 📍 Controle de posição (Bay, Row, Tier)  
- ⚠️ Detecção de bloqueios (containers sobrepostos)  
- 📝 Sistema de logs de movimentação  
- 📈 Contagem de movimentações por container  
- ↩️ Desfazer otimização  

---

## 🧠 Lógica do Sistema

O sistema organiza os containers com base na **data de saída**, priorizando aqueles que sairão primeiro.

### 📌 Exemplo da lógica

- Containers são ordenados por data de saída  
- Distribuídos entre as baias (bays)  
- Empilhados respeitando o limite de altura (tiers)  
- Evita bloqueios sempre que possível  

---

## 🏗️ Estrutura do Projeto

### 📁 Classes principais

#### 📦 Container
- Representa um container no sistema  
- Possui posição e datas  

---

#### 🧠 PatioService
- Responsável pela lógica principal do pátio  
- Otimização, movimentação e organização  

---

#### 💾 JsonService
- Leitura e escrita de arquivos JSON  

---

#### 📝 LogService
- Registro de ações e movimentações  
- Contagem de operações por container  

---

## ⚙️ Tecnologias Utilizadas

- C#  
- WPF  
- Newtonsoft.Json  
- .NET  

---

## 🏁 Objetivo do Projeto

Demonstrar habilidades em:

- Estruturação de sistemas em C#  
- Manipulação de dados (JSON)  
- Lógica de otimização  
- Organização de código (services)  
- Desenvolvimento desktop com WPF  
