### Desafio Backend
-----

#### O desafio backend que consiste na obtenção das informações de geolocalização pelas APIs do Google.

Criar uma API Rest que:

1) A api deve receber 2 ou mais endereços como parâmetros de entrada;
2) Resolva a geolocalização entre os endereços utilizando a API do Google
3) Após isso, com a latitude e longitude em mãos dos endereços, implementar o algoritmo de cálculo de distância Euclidiana e aplicar em todas as combinações de endereços.
4) Retorne as distâncias calculadas entre os todos os endereços e indique os endereços
mais próximos e também os endereços mais distantes.

### Versões
-----

Não era o foco, mas resolvi fazer três versões do desfio backend, sendo:

- .NET/Calindra.Desafio (Asp.Net Core com .NET 5)
- Node.JS/calindra-desafio (com _**Nest.js**_ com _**Typescript**_)
- Node.JS/calindra-desafio-serverless (opção serverless p/ AWS por um template do framework _**serverless**_ também com _**Typescript**_)

### Ambiente
-----

Para todos os 3 projetos, a chave de acesso à API do Google foi deixada nas variáveis de ambiente do Sistema Operacional.
Portanto, é necessário registrar a variável de ambiente **`GOOGLE_GEOLOCATION_ACCESS_KEY`** com a chave de acesso à API.

- **O projeto em .NET**, a princípio, vai exigir apenas o download do pacotes via NuGet.  
- **Para o projeto em Node.JS** com _**Nest.js**_ será necessário garantir a instalação do CLI e instalar os pacotes.  
- **E para o projeto em Node.JS** _Serverless_ será necessário a instalação do Framework do **[serverless.com](https://www.serverless.com/)** e instalar os pacotes.  
  - Uma dica para rodar a opção em serverless localmente é usar o comando `sls offline`, sendo o pacote serverless-offline deverá estar instalado, uma vez que se encontra 
na sessão de dependências de desenvolvimento.

### Para testar
-----

O método de requisição é o _**POST**_.

Aa requisição deverá ser feita de acordo com o seguinte exemplo, com 2 ou mais endereços:
```JSON
{
  "addressList": [
     "Endereço 1 ...",
     "Endereço 2 ...",
  ]
}
```
