# Plataforma de Seguros — Microserviços (.NET 8)

Este repositório implementa uma **plataforma simples de seguros**, composta por dois microserviços independentes, desenvolvidos com **.NET 8**, **Clean Architecture**, **DDD** e comunicação assíncrona via **RabbitMQ**.

Projeto pensado para **avaliação técnica**, com foco em arquitetura, organização e boas práticas.

---

## 🧩 Visão Geral da Arquitetura

```
┌─────────────────────┐        Evento (RabbitMQ)        ┌────────────────────────┐
│   PropostaService   │ ─────────────────────────────▶ │  ContratacaoService    │
│                     │                                │                        │
│ - Criar proposta    │                                │ - Contratar proposta   │
│ - Listar propostas  │                                │ - Persistir contrato   │
│ - Alterar status    │                                │                        │
└─────────┬───────────┘                                └─────────┬──────────────┘
          │                                                        │
          ▼                                                        ▼
  PostgreSQL (proposta-db)                               PostgreSQL (contratacao-db)
```

---

## 🛠️ Stack Tecnológica

- .NET 8
- Docker + Docker Compose
- PostgreSQL 16
- RabbitMQ (Management)
- Entity Framework Core
- MediatR
- Clean Architecture + DDD

---

## 🚀 Como rodar o projeto

### Pré-requisitos
- Docker Desktop
- .NET SDK 8+
- EF Core Tools (`dotnet tool install --global dotnet-ef`)

---

### 1️⃣ Subir o ambiente

```bash
docker compose up -d --build
```

---

### 2️⃣ Executar as migrations (obrigatório)

As migrations **não rodam automaticamente no startup**.

#### PropostaService

```bash
dotnet ef database update   --project src/Proposta/PropostaService.Infrastructure   --startup-project src/Proposta/PropostaService.Api   --context PropostaDbContext   --connection "Host=localhost;Port=5433;Database=proposta_db;Username=proposta;Password=proposta"
```

#### ContratacaoService

```bash
dotnet ef database update   --project src/Contrato/ContratacaoService.Infrastructure   --startup-project src/Contrato/ContratacaoService.Api   --context ContratacaoDbContext   --connection "Host=localhost;Port=5434;Database=contratacao_db;Username=contratacao;Password=contratacao"
```

---

### 3️⃣ Acessar

- Proposta API → http://localhost:8081/swagger
- Contratação API → http://localhost:8082/swagger
- RabbitMQ → http://localhost:15672 (guest / guest)

---

## 🔌 Portas

| Serviço | Porta |
|------|------|
| Proposta API | 8081 |
| Contratação API | 8082 |
| RabbitMQ | 5672 |
| RabbitMQ Management | 15672 |
| Postgres Proposta | 5433 |
| Postgres Contratação | 5434 |

---

## 🧪 Fluxo funcional

1. Criar proposta
2. Alterar status para **Aprovada**
3. Evento publicado no RabbitMQ
4. Contratação consome evento
5. Contrato é persistido

---

## 🧹 Reset

```bash
docker compose down -v
```

---

## 📂 Estrutura

```
src/
 ├── Proposta/
 ├── Contrato/
 └── Shared/
```

---

## 👤 Observação

Projeto estruturado para avaliação técnica, priorizando arquitetura, clareza e boas práticas.
