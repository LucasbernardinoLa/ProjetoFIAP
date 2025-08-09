# FIAP Games API

API REST desenvolvida como parte do Tech Challenge FIAP.

## Tecnologias

- .NET 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger/OpenAPI

## Funcionalidades

- Autenticação JWT
- Autorização baseada em roles (Admin/User)
- CRUD de Jogos
- Gerenciamento de Usuários
- Documentação Swagger
- Error Handling Global
- Database Seeding

## Como Executar

1. Configure a connection string no `appsettings.json`
2. Execute as migrations:
3. Execute o projeto:
4. Acesse: https://localhost:5046/swagger

## Credenciais de Teste

- Admin:
  - Email: admin@fiap.com
  - Senha: Admin@123456

- User:
  - Email: user@fiap.com
  - Senha: User@123456

## 🔒 Autenticação

A API utiliza JWT Bearer Authentication. Para acessar endpoints protegidos:

1. Faça login através do endpoint `/api/auth/login`
2. Copie o token retornado
3. Clique no botão "Authorize" no Swagger
4. Digite: `Bearer {seu-token}`

## Fluxograma do DDD

![Fluxo DDD Fiap](https://github.com/user-attachments/assets/0f482994-f613-4036-9a2a-3a2e47deea38)

