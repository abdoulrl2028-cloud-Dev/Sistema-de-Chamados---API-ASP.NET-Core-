# 🚀 CI/CD - Como Funciona

Este documento explica como o pipeline de CI/CD está configurado.

## 📊 O que é CI/CD?

- **CI (Continuous Integration)**: Automatiza build, testes e análise
- **CD (Continuous Deployment)**: Automatiza deploy em produção

---

## 🔄 Como o Workflow Funciona

### 1️⃣ **Trigger (Gatilho)**
O workflow `dotnet.yml` é disparado quando:
- ✅ Você faz `git push` na branch `main`
- ✅ Você abre/atualiza um Pull Request para `main`

### 2️⃣ **Etapas do Pipeline**

```
┌─────────────────────────────────────┐
│ 1. Checkout do código               │
└─────────────────────────────────────┘
                ↓
┌─────────────────────────────────────┐
│ 2. Setup do .NET 10.0               │
└─────────────────────────────────────┘
                ↓
┌─────────────────────────────────────┐
│ 3. Restore de dependências          │
└─────────────────────────────────────┘
                ↓
┌─────────────────────────────────────┐
│ 4. Build em Release                 │
└─────────────────────────────────────┘
                ↓
┌─────────────────────────────────────┐
│ 5. Rodar Testes (continue on error) │
└─────────────────────────────────────┘
                ↓
┌─────────────────────────────────────┐
│ 6. Publish da aplicação             │
└─────────────────────────────────────┘
                ↓
┌─────────────────────────────────────┐
│ 7. Upload dos Artifacts             │
└─────────────────────────────────────┘
```

### 3️⃣ **O que cada etapa faz:**

| Etapa | O que faz | Tempo |
|-------|-----------|-------|
| Checkout | Copia o código do GitHub | 2-3s |
| Setup .NET | Instala .NET 10.0 | 30-40s |
| Restore | Baixa NuGet packages | 20-30s |
| Build | Compila Release | 15-20s |
| **Testes** | **Roda 13 testes** | **5-10s** |
| Publish | Gera artifact | 10-15s |
| Upload | Salva artifacts | 5-10s |
| **TOTAL** | | **~90-130s** |

---

## 📈 Monitorar o Workflow

### No GitHub
1. Acesse: https://github.com/abdoulrl2028-cloud-Dev/Sistema-de-Chamados---API-ASP.NET-Core-
2. Clique na aba **"Actions"**
3. Veja a execução em tempo real

### Status Possíveis:
- 🟢 **Sucesso** - Todos os testes passaram
- 🟡 **Parcial** - Testes falharam mas workflow continua
- 🔴 **Falha** - Build ou dependência falhou

---

## 📥 Downloads dos Artifacts

Após cada workflow executar, você pode baixar:

### 1. **Build Artifacts** (`build-artifacts`)
- Arquivo publicado pronto para deploy
- Contém a DLL e todas as dependências
- Tamanho: ~50-100MB

### 2. **Test Results** (`test-results`)
- Arquivo TRX com resultados dos testes
- Pode ser aberto no Visual Studio
- Mostra quais testes passaram/falharam

**Como baixar:**
1. Abra o workflow que executou
2. Desça até "Artifacts"
3. Clique em "build-artifacts" ou "test-results"
4. Aguarde o download

---

## 🧪 Entender os Testes

### Testes Implementados:
- **7 testes** de Service (ChamadoService)
- **6 testes** de Repository (ChamadoRepository)
- **Total: 13 testes** ✅

### Se um teste falhar:
1. Vá para "Actions" → selecione o workflow
2. Clique em "Run tests"
3. Veja qual teste falhou
4. Corrija o código local
5. Faça commit e push novamente

---

## 🚀 Próximos Passos

### Deploy Automático
Para ativar deploy automático, configure:
- `deploy-azure.yml` - Para Azure App Service
- `deploy-docker.yml` - Para Docker Registry
- `deploy-heroku.yml` - Para Heroku

### Adição de novos testes
Qualquer novo teste adicionado em `SistemaChamados.Api.Tests` será automaticamente executado!

**Exemplo:**
```bash
# Cria novo teste
echo "seu código de teste" > SistemaChamados.Api.Tests/MyNewTest.cs

# Commit e push
git add .
git commit -m "feat: adicionar novo teste"
git push
```

O workflow executará automaticamente! 🎯

---

## 📊 Status Badge

Adicione no README para mostrar status:
```markdown
[![Build Status](https://github.com/abdoulrl2028-cloud-Dev/Sistema-de-Chamados---API-ASP.NET-Core-/actions/workflows/dotnet.yml/badge.svg)](https://github.com/abdoulrl2028-cloud-Dev/Sistema-de-Chamados---API-ASP.NET-Core-/actions/workflows/dotnet.yml)
```

Isso mostra um badge 🟢 ou 🔴 indicando status da build!

---

## ⚡ Dicas

### Forçar nova execução do workflow
```bash
# Commit vazio para disparar workflow
git commit --allow-empty -m "chore: trigger workflow"
git push
```

### Ver logs detalhados
GitHub Actions > Selecione o workflow > Clique em cada etapa

### Otimizar tempo de build
- Use cache de packages
- Paralelize testes
- Minimize dependencies

---

## 📞 Troubleshooting

### Workflow não executa
- ✅ Verifique se está na branch `main`
- ✅ Verifique arquivo `.github/workflows/dotnet.yml`
- ✅ Faça push (não apenas commit local)

### Testes falham no CI mas passam localmente
- ✅ Diferença de ambiente (.NET version)
- ✅ Problemas de timezone
- ✅ Ordem de execução dos testes

### Upload de artifacts falha
- ✅ Verifique permissões do repositório
- ✅ Verifique se os arquivos existem
- ✅ Veja o log de erro na aba "Actions"

---

**Sucesso! Seu CI/CD está rodando! 🎉**
