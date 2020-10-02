
# Para executar migrations com dotnet cli:

Os comandos devem ser executados com o terminal aberto na pasta do csproj Infra.Data.Persistent.csproj

# Adicionando Migrations:
```sh
dotnet ef migrations add <NOME_MIGRATIONS>> --context Infra.Data.Persistent.Contexts.EasyDbMigrationsContext
```

# Atualizando Database
```sh
dotnet ef  database update --context Infra.Data.Persistent.Contexts.EasyDbMigrationsContext
```
