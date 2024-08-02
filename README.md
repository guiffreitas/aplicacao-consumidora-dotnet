# Aplicação Consumidora .NET

Esta é uma API construída em .NET 8 com fins didáticos.
Ela implementa a obtenção de um token de acesso para realizar chamadas à [API Servidora](https://github.com/guiffreitas/aplicacao-servidora-dotnet), que obriga autenticação e autorização à seus consumidores.

A API consumidora possui apenas um endpoint `/ConsumerWeatherForecast`. Ao chamar essa rota, a API busca um token de acesso na plataforma do Entra ID, usando uma secret para se autenticar. 
Após a obtenção do token, ele é adicionado ao Header da chamada que é então enviada à rota `/WeatherForecast` da API servidora.

O passo a passo de cadastros e configurações na plataforma Entra ID e na API servidora, estão descritos no artigo abaixo.

[Autenticação e Autorização entre APIs .NET usando Microsoft Entra ID](https://medium.com/@ffaria.gui/autoriza%C3%A7%C3%A3o-e-autentica%C3%A7%C3%A3o-entre-apis-net-usando-microsoft-entra-id-9e6e1d113ef0)

## Tecnologias Utilizadas

- .NET 8
- Microsoft.Identity.Client
- Microsoft Entra ID

## Pré-requisitos

- .NET 8 SDK
- IDE (Visual Studio, Visual Studio Code, etc.)
- Conta na Microsoft Entra ID

## Configuração
Será necessário adicionar ao repositório um arquivo appsettings.json com os IDs referentes ao diretório da sua organização no Entra ID.
```
{
  "AllowedHosts": "*",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "EntraID": {
    "TenantId": "<tenant-id>",
    "Servidor": {
      "Id": "<application-id-servidor>"
    },
    "Consumidor": {
      "Id": "<application-id-consumidor>"
    },
    "Swagger": {
      "Id": "<application-id-swagger>",
      "Escopo": "api://<application-id-servidor>/swagger_access"
    }
  }
}
```
Para testar o endpoint da aplicação será necessário rodar o projeto da API servidora em paralelo, pois uma chamada é realizada nessa API. A URL da API servidora está configurada na classe `Program.cs`, na linha 10 conforme a seguir
```
builder.Services.AddHttpClient("ApiServidora", c => c.BaseAddress = new System.Uri("https://localhost:44300/"));
```
## Rodando o projeto
### Clone o repositório
```bash
git clone https://github.com/guiffreitas/aplicacao-consumidora-dotnet.git
```
### Rode o comando no terminal
```
dotnet run --lauch-profile "aplicacao-consumidora-dotnet"
```
### Acesse a URL
https://localhost:44317/swagger/index.html
