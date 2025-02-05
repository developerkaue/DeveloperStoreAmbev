# Developer Evaluation API

## Sobre o Projeto

Este projeto é uma API para avaliação de desenvolvedores, seguindo as melhores práticas de desenvolvimento moderno, incluindo:

- Arquitetura em camadas (Clean Architecture)
- Domain-Driven Design (DDD)
- Princípios SOLID
- Testes Unitários e de Integração
- Banco de Dados Relacional (PostgreSQL) e NoSQL (MongoDB)
- Mensageria e eventos de domínio
- CI/CD com GitHub Actions e Docker

## Tecnologias Utilizadas

### Backend

- .NET 8.0
- Entity Framework Core + PostgreSQL
- MongoDB para armazenamento NoSQL
- Redis para cache

### DevOps & Deploy

- Docker & Docker Compose
- GitHub Actions para CI/CD

## Como Rodar o Projeto

### Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas:

- **Docker** ([Download](https://docs.docker.com/get-docker/))
- **.NET SDK 8.0+** ([Download](https://dotnet.microsoft.com/en-us/download))
- **Git** ([Download](https://git-scm.com/downloads))

### 1. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/DeveloperStoreAmbev.git
cd DeveloperStoreAmbev
```

### 2. Restaurar as dependências

```bash
dotnet restore
```

### 3. Configurar as variáveis de ambiente

O projeto utiliza um banco de dados PostgreSQL, cuja conexão pode ser configurada no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
    "Postgres": "Host=localhost;Port=5432;Database=developer_evaluation;Username=developer;Password=devpass;"
}
```

Caso precise alterar as credenciais, modifique o arquivo `docker-compose.yml`.

## Rodando o projeto com Docker

A maneira mais simples de rodar a API é utilizando Docker. Isso garantirá que todos os serviços necessários (PostgreSQL, MongoDB, Redis e a API) sejam iniciados corretamente.

### 4. Subir os containers

```bash
docker-compose up --build
```

Esse comando:

- Cria e configura os serviços de banco de dados e cache
- Inicia a API na porta **8081**

### 5. Acessar a API

Após a inicialização, a API estará disponível em:

```
http://localhost:8081/swagger
```

## Rodando o projeto manualmente (sem Docker)

Caso prefira rodar os serviços manualmente, siga os passos abaixo.

### 1. Iniciar o PostgreSQL

Se já tiver o PostgreSQL instalado, crie o banco de dados:

```sql
CREATE DATABASE developer_evaluation;
```

E configure o usuário e senha conforme o `appsettings.json`.

### 2. Aplicar as migrações do banco de dados

```bash
dotnet ef database update
```

### 3. Executar a API

```bash
dotnet run --project DeveloperEvaluation.API
```

Agora a API estará disponível em `http://localhost:8080`.

## Testando a API

### Testes Unitários

Para rodar os testes unitários:

```bash
dotnet test
```

### Testes de Integração

```bash
dotnet test DeveloperEvaluation.IntegrationTests
```

## Endpoints Disponíveis

A API possui um Swagger configurado para facilitar os testes:

```
http://localhost:8081/swagger
```

| Método  | Rota                 | Descrição                     |
|---------|----------------------|-------------------------------|
| POST    | `/api/sales`         | Criar uma nova venda         |
| GET     | `/api/sales/{id}`    | Buscar venda por ID          |
| POST    | `/api/customers`     | Criar um novo cliente        |
| GET     | `/api/customers/{id}`| Buscar cliente por ID        |

## Estrutura do Projeto

```
📛 DeveloperStoreAmbev
 ├ 📂 DeveloperEvaluation.API             # Camada de Apresentação (Controllers)
 ├ 📂 DeveloperEvaluation.Application     # Casos de Uso (Application Layer)
 ├ 📂 DeveloperEvaluation.Domain          # Entidades, Agregados e Regras de Negócio
 ├ 📂 DeveloperEvaluation.Infrastructure  # Banco de Dados e Repositórios
 ├ 📂 DeveloperEvaluation.CrossCutting    # Serviços Compartilhados (Ex: Log, Notificações)
 ├ 📂 DeveloperEvaluation.UnitTests       # Testes Unitários
 ├ 📂 DeveloperEvaluation.IntegrationTests# Testes de Integração
 ├ 📝 DeveloperStoreAmbev.sln             # Arquivo da Solution
 ├ 📝 docker-compose.yml                  # Configuração do Docker
 ├ 📝 Dockerfile                           # Configuração do Container da API
 └ 📝 README.md                            # Documentação do Projeto
```

## CI/CD e Deploy

Este projeto possui integração contínua (CI/CD) configurada com GitHub Actions.

- Cada push no repositório executa os testes automaticamente
- A pipeline constrói e roda a aplicação dentro de um container
- Facilidade para deploy em servidores ou Kubernetes

## Contribuindo

Caso queira contribuir com o projeto:

1. Faça um **Fork** do repositório
2. Crie uma **branch** (`git checkout -b minha-feature`)
3. Faça suas alterações e **commit** (`git commit -m 'Minha nova feature'`)
4. Envie para o repositório (`git push origin minha-feature`)
5. Abra um **Pull Request**

## Considerações Finais

- Arquitetura modular e escalável
- Código organizado seguindo SOLID e DDD
- Banco de dados relacional e NoSQL
- API testada e validada automaticamente
- Integração contínua com GitHub Actions

