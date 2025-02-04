# Developer Evaluation API (AMBEV)
- Uma API completa com arquitetura SOLID, DDD e integração contínua via Docker e GitHub Actions.
- Rodando em .NET 8 e utilizando PostgreSQL, MongoDB e Redis!

# Sobre o Projeto
- Este projeto é uma API para avaliação de desenvolvedores, seguindo as melhores práticas de desenvolvimento moderno, incluindo:

* Arquitetura em camadas (Clean Architecture)
* Domain-Driven Design (DDD)
* Princípios SOLID
* Testes Unitários e de Integração
* Banco de Dados Relacional (PostgreSQL) e NoSQL (MongoDB)
* Mensageria e eventos de domínio
* CI/CD com GitHub Actions e Docker


# Tecnologias Utilizadas
- Back-End:
* NET 8.0
* Entity Framework Core + PostgreSQL
* MongoDB para armazenamento NoSQL
* Redis para cache

- DevOps & Deploy:
* Docker & Docker Compose
* GitHub Actions para CI/CD

# Como Rodar o Projeto (Passo a Passo)

- Pré-requisitos:

* Docker instalado ([Baixe aqui](https://www.docker.com))
* .NET SDK 8.0+ ([Baixe aqui](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0))
* Git ([Baixe aqui](https://git-scm.com/downloads))

- Clone o repositório
* Abra o terminal e execute:

    git clone https://github.com/seu-usuario/DeveloperStoreAmbev.git
    cd DeveloperStoreAmbev

- Configure as dependências
* No terminal, execute:

    dotnet restore

-  Configure as variáveis de ambiente:
* Antes de rodar, certifique-se de que as configurações do banco de dados estão corretas.
As conexões com PostgreSQL e MongoDB estão no appsettings.json:

    "ConnectionStrings": {
        "Postgres": "Host=localhost;Port=5432;Database=developer_evaluation;Username=developer;Password=devpass;"
    }
Se precisar alterar as credenciais, edite o arquivo docker-compose.yml.

