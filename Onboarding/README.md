# OnBoarding

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

Clonar o projeto

```bash
git clone https://github.com/sandbox-eti/cmmg-onboarding.git
```

Compilar o projeto

```bash
cd SSO
```

Criar o banco de dados ambiente de Desenvolvimento
```bash
SQLLocalDB create MSSQLLocalDB 
SQLLocalDB start MSSQLLocalDB 
dotnet ef database update
```

OBS: Na versão SQLLocalDB 14, para criar o banco deve rodar o seguinte comando
```bash
CREATE DATABASE onboarding ON (NAME='onboarding',  FILENAME='c:\databases\onboarding.mdf')
```

Criar variaveis de ambiente
```bash
setx PROJECT "Onboarding/Onboarding.csproj" -m
setx SSO_HOST "https://cmmg-sso.azurewebsites.net" -m
setx ONBOARDING_HOST "https://cmmg-onboarding.azurewebsites.net" -m
setx SECURITY_KEY "dd%88*377f6d&f£$$£$FdddFF33fssDG^!3" -m
setx ONBOARDING_DATABASE_CONNECTION "Server=(localdb)\mssqllocaldb;Database=onboarding;" -m
setx SMTP_USERNAME "azure_cb670a222faa4e88621781f17888b964@azure.com" -m
setx SMTP_PASSWORD "241213AAl" -m
setx EMAIL_SENDER "matricula@cmmg.com.br" -m
setx SENTRY_API "https://edc6b53b54bf4d709bd9f47c24f37588:74b2bba0aa7a458faadd5441286db8de@sentry.io/814482" -m
setx WEBSITE_TIME_ZONE "E. South America Standard Time" -m
setx BILLET_HOST "http://localhost:51762/" -m
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
