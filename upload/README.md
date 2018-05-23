# CMMG UPLOAD

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
Criar variaveis de ambiente
```bash
setx PROJECT "Upload/Uploadcsproj" -m
setx SSO_HOST "https://cmmg-sso.azurewebsites.net" -m
setx UPLOAD_HOST "https://cmmg-upload.azurewebsites.net" -m
setx SECURITY_KEY "dd%88*377f6d&f£$$£$FdddFF33fssDG^!3" -m
setx BLOB_AZURE_ACCOUNT_NAME "cmmgdevelopment" -m
setx BLOB_AZURE_ACCESS_KEY "VhN/tJCNGWUqtmH+OBjs28qx3kWUu4ULERtSJduR7RzGpV9oyx5vnl3zWXje3eIoebfwRMvOYoHJ5w75ytw75A==" -m
setx BLOB_AZURE_CONTAINER "documents" -m
setx BLOB_AZURE_SHARED_ACCESS_EXPIRY_TIME "15" -m
setx SENTRY_API "https://d65854af58114f31ac105ac7a55ed60b:61ac0c663c9a4ad7940a11b83a3ebfde@sentry.io/303952" -m
```
Rodar o projeto
```bash
dotnet run
```
