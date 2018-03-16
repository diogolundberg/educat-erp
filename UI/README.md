# CMMG-UI

Este é o frontend do projeto CMMG. Ele é desenvolvido e executado separadamente do [backend](https://github.com/diogolundberg/cmmg).

As duas partes se comunicam exclusivamente via APIs REST.

![screenshot](https://user-images.githubusercontent.com/25377830/37004837-96e372b2-20b0-11e8-87d4-a2df5a33b660.png)


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

## Arquitetura

#### Javascript

Estamos utilizando o Vue.js de forma tradicional, com a diferença de estar usando um autoloader baseado no plugin do Babel import-glob. Os componentes são carregados no escopo global do Vue.js, o que permite que possam ser usados em toda a aplicação. Isso garante mais agilidade

Para gerenciar os URLs, estamos utilizando o Vue-Router.

O Vuex é o storage global, que faz a comunicação com o backend utilizando a biblioteca Axios.

#### CSS

Estamos usando a metodologia Atomic CSS, que tem como objetivo a utilização de classes utilitárias mais simples e mais genéricas, evitando assim repetição e inchaço do CSS

O framework escolhido foi o BassCSS, pelo tamanho e pelas escolhas de nomeclatura das classes.

#### Componentes

Os componentes Vue.js ficam na pasta app/components, que é subdividida em três outras pastas:

 - `Elements` - Elementos de design que não são específicos da aplicação. São utilizados primariamente para garantir consistência no design e na usabilidade. Não contem regras de negócio ou acesso a dados (exceto se o mesmo for feito de maneira parametrizável).

 - `Partials` - Elementos reutilizáveis de página específicos da aplicação. Por exemplo, contem regras de negócio ou acesso a dados. Contem vários elements.

 - `Pages` - Páginas completas, que possuem um URL no router, e são compostas por Partials e por Elements.
