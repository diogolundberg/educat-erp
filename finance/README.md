# FINANCE

VERSÃO .NET CORE 2.1.4
[https://docs.microsoft.com/en-us/dotnet/core/get-started](https://docs.microsoft.com/en-us/dotnet/core/get-started)

### Windows

[https://docs.microsoft.com/en-us/dotnet/core/windows-prerequisites?tabs=netcore2x](https://docs.microsoft.com/en-us/dotnet/core/windows-prerequisites?tabs=netcore2x)

### MacOS

[https://docs.microsoft.com/en-us/dotnet/core/macos-prerequisites?tabs=netcore2x](https://docs.microsoft.com/en-us/dotnet/core/macos-prerequisites?tabs=netcore2x)

[https://www.microsoft.com/net/download/macos](https://www.microsoft.com/net/download/macos)

### Linux

[https://docs.microsoft.com/en-us/dotnet/core/linux-prerequisites?tabs=netcore2x](https://docs.microsoft.com/en-us/dotnet/core/linux-prerequisites?tabs=netcore2x)


### Versão dotnet
```bash
dotnet --version
```

Compilar o projeto

```bash
cd invoice
```

Criar o banco de dados ambiente de Desenvolvimento
```bash
SQLLocalDB create MSSQLLocalDB 
SQLLocalDB start MSSQLLocalDB 
dotnet ef database update
```

OBS: Na versão SQLLocalDB 14, para criar o banco deve rodar o seguinte comando
```bash
CREATE DATABASE finance ON (NAME='finance',  FILENAME='c:\databases\finance.mdf')
```

Criar variaveis de ambiente
```bash
setx PROJECT "finance/finance.csproj" -m
setx SSO_HOST "https://cmmg-sso.azurewebsites.net" -m
setx INVOICE_HOST "https://cmmg-finance.azurewebsites.net" -m
setx SECURITY_KEY "dd%88*377f6d&f£$$£$FdddFF33fssDG^!3" -m
setx FINANCE_DATABASE_CONNECTION "Server=(localdb)\mssqllocaldb;Database=finance;" -m
setx SMTP_USERNAME "azure_cb670a222faa4e88621781f17888b964@azure.com" -m
setx SMTP_PASSWORD "241213AAl" -m
setx SENTRY_API "https://edc6b53b54bf4d709bd9f47c24f37588:74b2bba0aa7a458faadd5441286db8de@sentry.io/814482" -m
setx WEBSITE_TIME_ZONE "E. South America Standard Time" -m
```

Criar um novo migration
```bash
dotnet ef migrations add {nome-do-migration}
```

Drop database
```bash
dotnet ef database drop
```

Como obter script sql da migração
```bash
dotnet ef migrations script
```

Rodar o projeto
```bash
dotnet run
```
