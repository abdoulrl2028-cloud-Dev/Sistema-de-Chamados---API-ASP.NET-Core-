# Sistema de Chamados - API (ASP.NET Core)

API REST em C# para gerenciamento de chamados com CRUD completo, SQL Server e controle de status.

![Build Status](https://github.com/abdoulrl2028-cloud-Dev/Sistema-de-Chamados---API-ASP.NET-Core-/actions/workflows/dotnet.yml/badge.svg)

## Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger

## Funcionalidades
- Criar chamado
- Listar chamados
- Buscar por ID
- Atualizar (inclui Status)
- Deletar

## Como rodar
1. Configure a connection string em `appsettings.json`
2. Rode migrations:
   - `dotnet ef migrations add InitialCreate`
   - `dotnet ef database update`
3. Execute:
   - `dotnet run`
4. Acesse o Swagger: `/swagger`

## Status do Chamado
- Aberto
- EmAndamento
- Resolvido
- Fechado
