# CMMG-UI

Este é o frontend do projeto CMMG. Ele é desenvolvido e executado separadamente do [backend](https://github.com/diogolundberg/cmmg).

As duas partes se comunicam exclusivamente via APIs REST.

## Dependências

#### Dependências de Tempo de Execução

 - [Vue.js](http://vuejs.org) - Framework Web
 - [VueX](http://vuex.vuejs.org) - Gerenciamento de Estado
 - [Axios](https://github.com/axios/axios) - Cliente HTTP

#### Dependências de Desenvolvimento

 - [Parcel](http://github.com/parcel-bundler/parcel) - Bundler
 - [Babel](http://babeljs.io) - Compilador de ES6 + shims
 - [Stylus](http://stylus-lang.com) - Compilador de CSS
 - [Jest](https://facebook.github.io/jest/) - Framework de testes
 - [ESLint](https://eslint.org) - Linter

## Instruções de Uso

É necessário possuir a ferramenta `yarn` para executar as tarefas abaixo.

#### Instalação de Dependências

Basta baixar este repositório e rodar o comando abaixo:

```bash
yarn
```

#### Desenvolvimento

O desenvolvimento utiliza um servidor do próprio Parcel com a função HMR (Hot Module Reloading), que permite a edição de código sem recarregar a página ou perder o estado.

Para ativa-lo execute o comando:

```bash
yarn serve
```

O acesso é feito através de http://localhost:1234/

#### Compilação

A compilação é feita com o seguinte comando:

```bash
yarn build
```

O produto desta compilação será disponibilizada na pasta `dist`.
