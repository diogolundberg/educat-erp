# CMMG

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
git clone git@github.com:diogolundberg/cmmg.git
```

Atualizar packages
```bash
dotnet restore
```

Criar o banco de dados ambiente de Desenvolvimento

- Windows
```powershell
SQLLocalDB create MSSQLLocalDB
SQLLocalDB start MSSQLLocalDB
```

OBS: Na versão SQLLocalDB 14, para criar o banco deve rodar o seguinte comando
```bash
CREATE DATABASE sso ON (NAME='sso',  FILENAME='c:\databases\sso.mdf')
```

- Linux
```bash
docker run -d --name mssql-server-linux -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=curls288&poet' -p 1433:1433 microsoft/mssql-server-linux:2017-latest
docker start mssql-server-linux
```

Migrate
```bash
dotnet ef database update
```

Compilar o projeto
```bash
dotnet build
```

Rodar o projeto
```bash
dotnet run
```

Vari'aveis de ambiente
```bash
setx PROJECT "SSO/SSO.csproj" -m
setx SSO_HOST "https://cmmg-sso.azurewebsites.net" -m
setx SECURITY_KEY "dd%88*377f6d&f£$$£$FdddFF33fssDG^!3" -m
setx SSO_DATABASE_CONNECTION "Server=(localdb)\mssqllocaldb;Database=sso;" -m
setx SMTP_USERNAME "azure_cb670a222faa4e88621781f17888b964@azure.com -m
setx SMTP_PASSWORD "241213AAl" -m
setx EMAIL_SENDER_RESET_PASSWORD "no-reply@cmmg.com.br" -m
setx EMAIL_RESET_PASSWORD_SUBJECT "RESETAR SENHA - CMMG" -m
setx SENTRY_API "https://d65854af58114f31ac105ac7a55ed60b:61ac0c663c9a4ad7940a11b83a3ebfde@sentry.io/303952" -m
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
