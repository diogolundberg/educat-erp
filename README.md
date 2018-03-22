# CMMG

Este é o Monorepo do projeto CMMG.

## Deploy para o Azure

#### Configurando seu Deploy

Para deploy no Azure, basta adicionar os repositórios no git. Pela primeira vez:

```
git remote add azure-sso https://git-pub:Triangulo31@cmmg-sso.scm.azurewebsites.net:443/cmmg-sso.git
git remote add azure-upload https://git-pub:Triangulo31@cmmg-upload.scm.azurewebsites.net:443/cmmg-upload.git
git remote add azure-onboarding https://git-pub:Triangulo31@cmmg-onboarding.scm.azurewebsites.net:443/cmmg-onboarding.git
```

#### Para fazer deploy

```
git push azure-sso master
git push azure-upload master
git push azure-onboarding master
```

Pode dar push no repositório inteiro.

O Azure utilizará a variável de ambiente `PROJECT` para saber qual `csproj` ele deve carregar.

#### Exemplo de configuração da variável no Azure.

![captura de tela 2018-03-15 as 21 57 56](https://user-images.githubusercontent.com/25377830/37498490-75bad7c0-289d-11e8-839a-c661bf7c39a9.png)
