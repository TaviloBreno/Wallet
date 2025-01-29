# 💰 Wallet API - Digital Wallet Management

![API Status](https://img.shields.io/badge/status-active-brightgreen) ![License](https://img.shields.io/badge/license-MIT-blue)

## 🚀 Sobre o Projeto
A **Wallet API** é uma aplicação **RESTful** desenvolvida com **.NET 8** e **PostgreSQL** para gerenciar carteiras digitais e transações financeiras. A API oferece funcionalidades como:

- ✅ Autenticação JWT
- ✅ Gerenciamento de saldo da carteira
- ✅ Transferência de saldo entre usuários
- ✅ Listagem de transferências com filtros opcionais

## 🛠 Tecnologias Utilizadas

- **C# / .NET 8** - Backend
- **Entity Framework Core** - ORM
- **PostgreSQL** - Banco de Dados
- **JWT (JSON Web Token)** - Autenticação
- **Swagger** - Documentação da API
- **Docker** (Opcional) - Para deploy

## ⚡ Estrutura do Projeto
```plaintext
wallet-api/
├── src/
│   ├── Controllers/        # Lida com requisições HTTP
│   ├── DTOs/               # Transferência de dados
│   ├── Models/             # Representação das entidades do banco
│   ├── Data/               # Configuração do banco e Migrations
│   ├── Services/           # Regras de negócio
│   ├── Program.cs          # Configuração principal da API
├── README.md               # Documentação do projeto
├── .gitignore              # Arquivos ignorados no Git
└── docker-compose.yml      # Configuração opcional do Docker
````
## 🎯 Pré-requisitos
Antes de iniciar, você precisará ter instalado:

- **.NET 8 SDK**
- **PostgreSQL**
- **Visual Studio 2022**
- **(Opcional) Docker**

# 🚀 Como Configurar o Projeto

## 🔧 1. Clone o repositório:
```plaintext
git clone https://github.com/seu-usuario/wallet-api.git
cd wallet-api
````
## 🔧 2. Configure o PostgreSQL
Crie o banco de dados `wallet_db` e atualize a string de conexão em `appsettings.json`:

```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=wallet_db;Username=postgres;Password=yourpassword"
}
```

## 🔧 3. Instale as dependências e aplique as migrations:
```bash
dotnet restore
dotnet ef database update
```

## 🔧 4. Execute a aplicação:
```bash
dotnet run
```
A API estará disponível em `http://localhost:5000`.

## 📌 Rotas da API

### 🔑 Autenticação

#### 📌 Criar Usuário
```http
POST /api/Auth/register
```

#### 📌 Exemplo de Payload
```json
{
    "name": "John Doe",
    "email": "john@example.com",
    "password": "password123"
}
```

#### 📌 Login e Geração de Token JWT
```http
POST /api/Auth/login
```

#### 📌 Exemplo de Payload
```json
{
    "email": "john@example.com",
    "password": "password123"
}
```

#### 📌 Resposta
```json
{
    "token": "eyJhbGciOiJIUzI1..."
}
```
## 💰 Carteira Digital

### 📌 Consultar Saldo
```http
GET /api/Wallet/balance
Authorization: Bearer {token}
```

#### 📌 Resposta
```json
{
    "balance": 1000.00
}
```

### 📌 Adicionar Saldo
```http
POST /api/Wallet/add-balance
Authorization: Bearer {token}
```

#### 📌 Payload
```json
{
    "amount": 200.00
}
```

#### 📌 Resposta
```json
{
    "message": "Balance added successfully.",
    "newBalance": 1200.00
}
```

## 🔄 Transferências

### 📌 Criar Transferência
```http
POST /api/Transfer/create
Authorization: Bearer {token}
```

#### 📌 Payload
```json
{
    "receiverId": 2,
    "amount": 100.00
}
```

#### 📌 Resposta
```json
{
    "message": "Transfer completed successfully.",
    "newBalance": 900.00
}
```

### 📌 Listar Transferências (Opcionalmente filtradas por data)
```http
GET /api/Transfer/history?startDate=2025-01-01&endDate=2025-01-30
Authorization: Bearer {token}
```

#### 📌 Resposta
```json
[
    {
        "amount": 100.00,
        "transferDate": "2025-01-29T12:00:00Z",
        "receiverWallet": "User B"
    }
]
```

## 📘 Testando no Swagger
Após iniciar a API, acesse o Swagger para testar as requisições: [http://localhost:5000/swagger](http://localhost:5000/swagger)

## 🏗 Futuras Melhorias
🔹 Implementar sistema de notificações via WebSockets  
🔹 Criar relatórios financeiros personalizados  
🔹 Melhorar logs e monitoramento  

## 🤝 Contribuindo
Contribuições são bem-vindas! Para contribuir:

1. Fork o projeto 🍴
2. Crie uma branch: `git checkout -b feature-minha-feature`
3. Commit suas alterações: `git commit -m "feat: minha nova feature"`
4. Envie para o repositório: `git push origin feature-minha-feature`
5. Abra um Pull Request 🎉

## 📜 Licença
Este projeto está licenciado sob a MIT License.

## 📌 Feito com ❤️ por Seu Nome 🚀