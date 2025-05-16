# Ambev Sales API

Sistema backend desenvolvido para o desafio técnico da Ambev, com foco na gestão de vendas utilizando arquitetura moderna baseada em DDD, testes automatizados, persistência com PostgreSQL e execução via Docker.

## 📚 Tecnologias Utilizadas

- ASP.NET Core 8.0
- PostgreSQL 13
- MongoDB 8.0
- Redis 7.4
- Entity Framework Core (com Migrations)
- MediatR
- FluentValidation
- Docker & Docker Compose
- xUnit + Bogus + NSubstitute

## ⚙️ Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)

### Passo a passo

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/ambev-sales-api.git
   cd ambev-sales-api
   ```

2. Suba os containers com Docker:
   ```bash
   docker-compose up --build
   ```

3. Acesse a API via Swagger:
   ```
   http://localhost:5119/swagger
   ```

## 🧪 Executando os Testes

Para rodar os testes unitários:

```bash
dotnet test
```

## 🗃️ Migrations (EF Core)

Para gerar novas migrations:

```bash
dotnet ef migrations add NomeDaMigration \
  -p src/Ambev.DeveloperEvaluation.ORM \
  -s src/Ambev.DeveloperEvaluation.WebApi
```

Para aplicar no banco:

```bash
dotnet ef database update \
  -p src/Ambev.DeveloperEvaluation.ORM \
  -s src/Ambev.DeveloperEvaluation.WebApi
```

## 🧱 Estrutura do Projeto

- `src/`
  - `Application`: lógica de negócio e validações
  - `Domain`: entidades e regras
  - `ORM`: contexto EF Core + migrations
  - `WebApi`: endpoints e Middlewares
- `tests/`: testes unitários, funcionais e de integração
- `docker-compose.yml`: orquestra banco de dados (PostgreSQL), Redis, MongoDB e API

## 📦 Funcionalidades

- Cadastro de vendas
- Cancelamento de venda
- Cancelamento de item de venda
- Validações com FluentValidation
- Totalizador de valor da venda
- Armazenamento persistente com PostgreSQL

## ✅ Checklist

- [x] Subida via Docker Compose
- [x] Testes automatizados com xUnit
- [x] Aplicação de DDD
- [x] CRUD funcional de vendas
- [x] Uso de MediatR + FluentValidation
- [x] API documentada com Swagger

## 👨‍💻 Autor

Juliano Lourencetti  
[LinkedIn](https://www.linkedin.com/in/julianolourencetti) | juliano_lourencetti@hotmail.com
