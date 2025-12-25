# 🚀 Guia de Deploy

Este documento descreve como fazer deploy da API em diferentes plataformas.

## 📋 Índice
1. [Docker](#docker)
2. [Azure App Service](#azure-app-service)
3. [Heroku](#heroku)
4. [GitHub Actions](#github-actions)

---

## 🐳 Docker

### Build local
```bash
docker build -t sistema-chamados-api .
```

### Rodar com Docker Compose
```bash
docker-compose up -d
```

A API estará disponível em: `http://localhost:5000`

### Push para Docker Hub
```bash
docker tag sistema-chamados-api seu-usuario/sistema-chamados-api:latest
docker push seu-usuario/sistema-chamados-api:latest
```

---

## ☁️ Azure App Service

### Pré-requisitos
- Conta no Azure
- Azure CLI instalado
- Publish Profile baixado

### Passos
1. Acesse [Portal Azure](https://portal.azure.com)
2. Crie um novo App Service (ASP.NET Core 10.0)
3. Baixe o Publish Profile
4. Adicione em Secrets do GitHub: `AZURE_PUBLISH_PROFILE`
5. O workflow `deploy-azure.yml` fará o deploy automaticamente

---

## 🎪 Heroku

### Pré-requisitos
- Conta no Heroku
- Heroku CLI instalado

### Passos
1. Crie um app no Heroku
```bash
heroku create sistema-chamados-api
```

2. Adicione os secrets no GitHub:
   - `HEROKU_API_KEY` - Token da API Heroku
   - `HEROKU_EMAIL` - Email da conta Heroku
   - `HEROKU_APP_NAME` - Nome do app

3. Configure a connection string:
```bash
heroku config:set ConnectionStrings__DefaultConnection="sua-string-conexao"
```

4. O workflow `deploy-heroku.yml` fará o deploy automaticamente

---

## 🤖 GitHub Actions

### Workflows disponíveis

#### 1. Build & Test (`dotnet.yml`)
- ✅ Executa em: push e PR na `main`
- ✅ Build em Release
- ✅ Testa a aplicação
- ✅ Publica artifacts

#### 2. Docker Push (`deploy-docker.yml`)
- ✅ Build e push da imagem Docker
- ✅ Push para Docker Hub e GitHub Container Registry
- ✅ Cache de layers para builds mais rápidos

#### 3. Azure Deploy (`deploy-azure.yml`)
- ✅ Deploy para Azure App Service
- ✅ Requer: `AZURE_PUBLISH_PROFILE`

#### 4. Heroku Deploy (`deploy-heroku.yml`)
- ✅ Deploy para Heroku
- ✅ Requer: `HEROKU_API_KEY`, `HEROKU_EMAIL`, `HEROKU_APP_NAME`

---

## 📊 Configuração de Secrets no GitHub

1. Vá para: **Settings → Secrets and variables → Actions**
2. Clique em **New repository secret**
3. Adicione os secrets conforme necessário:

| Secret | Descrição | Obrigatório para |
|--------|-----------|-----------------|
| `AZURE_PUBLISH_PROFILE` | Arquivo de publicação Azure | Azure Deploy |
| `DOCKER_USERNAME` | Usuário Docker Hub | Docker Push |
| `DOCKER_PASSWORD` | Token Docker Hub | Docker Push |
| `HEROKU_API_KEY` | Token API Heroku | Heroku Deploy |
| `HEROKU_EMAIL` | Email conta Heroku | Heroku Deploy |
| `HEROKU_APP_NAME` | Nome app Heroku | Heroku Deploy |

---

## ✅ Monitorar Deploys

1. Vá para a aba **Actions** no GitHub
2. Clique no workflow que deseja acompanhar
3. Veja os logs de cada etapa
4. Verifique se o deploy foi bem-sucedido

---

## 🆘 Troubleshooting

### Build falha com erro de pacotes
```bash
dotnet clean
dotnet restore
dotnet build
```

### Deploy falha no Azure
- Verifique se o Publish Profile está correto
- Confira a string de conexão do banco de dados

### Docker push falha
- Verifique credenciais do Docker Hub
- Confira se o secret `DOCKER_PASSWORD` é um token (não senha)

### Deploy Heroku falha
- Verifique se o app foi criado no Heroku
- Confira os secrets no GitHub

---

## 📞 Suporte

Para mais informações, consulte:
- [Microsoft Docs - Deploy ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/)
- [Azure Documentation](https://docs.microsoft.com/azure/)
- [Heroku Documentation](https://devcenter.heroku.com/)
- [Docker Documentation](https://docs.docker.com/)
