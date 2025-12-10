# 💰 ControleGastos API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat&logo=microsoft-sql-server)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=flat)

## 📖 Sobre o Projeto

O **ControleGastosApi** é uma API RESTful desenvolvida em **.NET 8**, projetada para o gerenciamento eficiente de finanças pessoais. O sistema permite o registro detalhado de receitas e despesas, categorização inteligente e oferece endpoints para análise de fluxo de caixa (Dashboard).

O foco principal deste projeto foi a implementação de uma **Arquitetura em Camadas** robusta, visando escalabilidade, testabilidade e desacoplamento de responsabilidades.

---

## 🚀 Funcionalidades

- **Gerenciamento de Transações:** CRUD completo (Criar, Ler, Atualizar, Deletar) de receitas e despesas.
- **Categorização Relacional:** Sistema de categorias com ícones, vinculado às transações via banco de dados relacional.
- **Dashboard Financeiro:** Endpoint exclusivo que calcula o saldo total, total de receitas, despesas e gastos agrupados por categoria.
- **Validações de Negócio:** Regras centralizadas na camada de Serviço (ex: bloqueio de transações com data futura).

---

## 🏗️ Arquitetura e Padrões de Projeto

O projeto foi refatorado para seguir boas práticas de engenharia de software, saindo de uma estrutura monolítica para camadas bem definidas:

1.  **Repository Pattern:**
    - Isolamento total do acesso a dados (Entity Framework Core).
    - O Controller desconhece a implementação do banco de dados, dependendo apenas de interfaces (`ITransacaoRepository`).
    - Uso de *Eager Loading* (`.Include`) para consultas performáticas com JOINs.

2.  **Service Layer (Business Logic):**
    - Centralização das regras de negócio.
    - O Controller atua apenas como "proxy", repassando requisições para o Service.
    - Cálculos do Dashboard movidos para esta camada, garantindo o Princípio da Responsabilidade Única (SRP).

3.  **DTOs (Data Transfer Objects):**
    - Utilização de objetos específicos para entrada e saída de dados.
    - Previne *Over-posting* e *Mass Assignment*, protegendo a integridade das entidades do banco.

---

## 🛠️ Tecnologias Utilizadas

- **.NET 8 (C#)**
- **Entity Framework Core** (ORM)
- **SQL Server** (Banco de Dados)
- **Swagger / OpenAPI** (Documentação)
- **Dependency Injection** (Nativa do .NET)

---

## 🔧 Como Rodar o Projeto

### Pré-requisitos
- .NET SDK 8.0 instalado.
- SQL Server (LocalDB ou Express).

### Passo a Passo

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/devpedrosales/ControleGastosApi.git
   cd ControleGastosApi
