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
CREATE DATABASE sso ON (NAME='sso',  FILENAME='c:\databases\sso.mdf')
```

Criar variaveis de ambiente
```bash
setx SSO_HOST "http://localhost:5000/" -m
setx HOST "http://localhost:5000/" -m
setx SECURITY_KEY "dd%88*377f6d&f£$$£$FdddFF33fssDG^!3" -m
setx SSO_DATABASE_CONNECTION "Server=(localdb)\mssqllocaldb;Database=sso;" -m
setx SMTP_HOST "smtp.sendgrid.net" -m
setx SMTP_PORT "587" -m
setx SMTP_USERNAME "azure_cb670a222faa4e88621781f17888b964@azure.com" -m
setx SMTP_PASSWORD "241213AAl" -m
setx EMAIL_SENDER_RESET_PASSWORD "no-reply@cmmg.com.br" -m
setx EMAIL_RESET_PASSWORD_SUBJECT "RESETAR SENHA - CMMG" -m
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
