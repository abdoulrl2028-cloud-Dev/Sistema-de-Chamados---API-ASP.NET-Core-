<p align="center">
  <img src="https://raw.githubusercontent.com/abdoulrl2028-cloud-Dev/abdoulrl2028-cloud-Dev/main/assets/projects/chamados.jpg" alt="Ticket system API" width="100%">
</p>

# Ticket System API (ASP.NET Core)

C# REST API for ticket management, with full CRUD, SQL Server, and status tracking.

![Build status](https://github.com/abdoulrl2028-cloud-Dev/Sistema-de-Chamados---API-ASP.NET-Core-/actions/workflows/dotnet.yml/badge.svg)

## Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger

## Features

- Create a ticket
- List tickets
- Get a ticket by id
- Update a ticket, including status
- Delete a ticket

## Run

1. Set the connection string in `appsettings.json`.
2. Apply migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

3. Open Swagger at `/swagger`.

## Ticket status

- Open
- In progress
- Resolved
- Closed

## Docs

- [Deploy guide](DEPLOY.md) — Azure, Heroku, and Docker
- [CI/CD guide](CI-CD.md) — test and build pipeline
