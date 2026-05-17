# 💰 FinanceApi

API REST para gerenciamento de finanças pessoais desenvolvida em C# com ASP.NET Core 8.

🔗 **[API em produção](https://finance-api-y04i.onrender.com)**

---

## ✨ Funcionalidades

- **Autenticação** — registro e login com JWT
- **Transações** — CRUD completo com filtros por tipo e categoria
- **Categorias** — gerenciamento de categorias personalizadas por usuário
- **Swagger** — documentação automática dos endpoints (ambiente de desenvolvimento)
- **Seed de dados** — dados iniciais para facilitar os testes

---

## 🛠️ Tecnologias

| Tecnologia | Uso |
|---|---|
| [C#](https://learn.microsoft.com/dotnet/csharp/) | Linguagem principal |
| [ASP.NET Core 8](https://learn.microsoft.com/aspnet/core/) | Framework web |
| [Entity Framework Core](https://learn.microsoft.com/ef/core/) | ORM para banco de dados |
| [SQLite](https://www.sqlite.org/) | Banco de dados |
| [JWT](https://jwt.io/) | Autenticação |
| [Swagger](https://swagger.io/) | Documentação da API |
| [BCrypt](https://github.com/BcryptNet/bcrypt.net) | Hash de senhas |
| [Docker](https://www.docker.com/) | Containerização |
| [Render](https://render.com/) | Deploy |

---

## 🚀 Como rodar localmente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Instalação

```bash
git clone https://github.com/cauamconceicao/finance-api.git
cd finance-api
dotnet restore
dotnet run
```

A API estará disponível em `http://localhost:5105`

O Swagger estará disponível em `http://localhost:5105/swagger`

---

## 📋 Endpoints

### Auth
| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/auth/register` | Registrar novo usuário |
| POST | `/api/auth/login` | Fazer login e obter token JWT |

### Transações
| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/transactions` | Listar todas as transações |
| GET | `/api/transactions/{id}` | Buscar transação por ID |
| POST | `/api/transactions` | Criar nova transação |
| PUT | `/api/transactions/{id}` | Atualizar transação |
| DELETE | `/api/transactions/{id}` | Remover transação |

### Categorias
| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/categories` | Listar categorias |
| POST | `/api/categories` | Criar categoria |
| PUT | `/api/categories/{id}` | Atualizar categoria |
| DELETE | `/api/categories/{id}` | Remover categoria |

---

## 🔐 Autenticação

Todos os endpoints (exceto registro e login) requerem autenticação via JWT.

Após o login, inclua o token no header:

```
Authorization: Bearer {seu_token}
```

---

## 📝 Exemplos de uso

### Registro

```json
POST https://finance-api-y04i.onrender.com/api/auth/register
{
  "name": "Cauã",
  "email": "seu@email.com",
  "password": "123456"
}
```

### Login

```json
POST https://finance-api-y04i.onrender.com/api/auth/login
{
  "email": "seu@email.com",
  "password": "123456"
}
```

### Criar transação

```json
POST https://finance-api-y04i.onrender.com/api/transactions
Authorization: Bearer {token}

{
  "title": "Salário",
  "amount": 5000,
  "type": 1,
  "description": "Salário do mês",
  "date": "2026-05-16",
  "categoryId": null
}
```

---

## 📁 Estrutura do projeto

```
FinanceApi/
├── Controllers/          # Endpoints da API
│   ├── AuthController.cs
│   ├── TransactionsController.cs
│   └── CategoriesController.cs
├── Models/               # Entidades do banco
│   ├── User.cs
│   ├── Transaction.cs
│   └── Category.cs
├── DTOs/                 # Objetos de transferência
├── Services/             # Lógica de negócio
├── Data/                 # Contexto do banco
│   └── AppDbContext.cs
├── Dockerfile            # Containerização
└── Program.cs            # Configuração da aplicação
```

---

## 📄 Licença

MIT © [Cauã Conceição](https://github.com/cauamconceicao)