<h1 align="center">FIAP - PosTech - Arquitetura Azure .Net - 2024 - Grupo 72</h1> 

<p align="center">
  <img loading="lazy" src="http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge"/>
</p>

# Índice 
* [Descrição do Projeto](#descrição-do-projeto)
* [Funcionalidades e Demonstração da Aplicação](#funcionalidades-e-demonstração-da-aplicação)
* [Tecnologias utilizadas](#tecnologias-utilizadas)
* [Link do Projeto no YouTube](#link-do-projeto-no-youtube)

# Descrição do Projeto

Desenvolver um aplicativo utilizando a plataforma .NET 8 para cadastro de contatos regionais, considerando a persistência de dados e a qualidade do software.

* Requisitos Funcionais
  - Cadastro de contatos: permitir o cadastro de novos contatos, incluindo nome, telefone e e-mail. Associe cada contato a um DDD correspondente à região.
  - Consulta de contatos: implementar uma funcionalidade para consultar e visualizar os contatos ca‐ dastrados, os quais podem ser filtrados pelo DDD da região.
  - Atualização e exclusão: possibilitar a atualização e exclusão de contatos previamente cadastrados.

* Requisitos Técnicos
  - Persistência de Dados: utilizar um banco de dados para armazenar as informações dos contatos. Escolha entre Entity Framework Core ou Dapper para a camada de acesso a dados.
  - Validações: implementar validações para garantir dados consistentes (por exemplo: validação de
formato de e-mail, telefone, campos obrigatórios).
  - Testes Unitários: desenvolver testes unitários utilizando xUnit ou NUnit.
  - Testes de integração.
  - CI/CD.
  - Observabilidade/Monitoramento: Prometheu e Grafana on Docker.


# Funcionalidades e Demonstração da Aplicação
- `Domain Story Telling`: 
<img loading="lazy" width="50%" height="50%" src="Docs/Domain Storytelling/Domain Story Telling.jpg"/>

- `Schemas`: 
<img loading="lazy" width="40%" height="40%" src="Docs/Domain Storytelling/Schemas.PNG"/>

- `Endpoints`: 
<img loading="lazy" width="100%" height="100%" src="Docs/Domain Storytelling/Endpoints.PNG"/>

# Tecnologias utilizadas
- C#, .Net 8, Minimal API, InMemory Database, EF Core 8, OpenAPI

# Link do Projeto no YouTube
- https://www.youtube.com/watch?v=gmRAX21eDeo

