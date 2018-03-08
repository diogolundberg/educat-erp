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
setx SSO_HOST "http://localhost:5000/" -m
setx ONBOARDING_HOST "http://localhost:5001/" -m
setx SECURITY_KEY "dd%88*377f6d&f£$$£$FdddFF33fssDG^!3" -m
setx ONBOARDING_DATABASE_CONNECTION "Server=(localdb)\mssqllocaldb;Database=onboarding;" -m
setx SENDGRID_APIKEY "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" -m
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
