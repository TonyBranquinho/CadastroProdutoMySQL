[![Build and deploy ASP.Net Core app to Azure Web App - CadastroProduto](https://github.com/TonyBranquinho/CadastroProdutoMySQL/actions/workflows/main_cadastroproduto.yml/badge.svg)](https://github.com/TonyBranquinho/CadastroProdutoMySQL/actions/workflows/main_cadastroproduto.yml)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?style=flat&logo=mysql&logoColor=white)](https://www.mysql.com)
[![Azure](https://img.shields.io/badge/Azure-Deployed-0078D4?style=flat&logo=microsoftazure&logoColor=white)](https://cadastroproduto-edg8gfhmatayhefb.brazilsouth-01.azurewebsites.net)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

 # CadastroProdutoMySQL

Esse é um sistema de cadastro e gerenciamento do produtos, categorias e estoque desenvolvido em **C#**
com **ASP.NET Core e persistencia em **MySQL**. Segue a arquitetura em camadas e boas praticas de 
desenvolvimento, incluindo DTOs, serviços, repositorios e controllers.

---

## Funcionalidades:

- **Cadastro de Produtos** com informações detalhadas.
- **Gerenciamento de Categorias** para organização de produtos.
- **Controle de Estoque** com movimentaçoes e consultas.
- **API RESTful** com métodos CRUD completos.
- **DTOs** para separar as entidades de cominio da comunicaçao com o cliente.
- **Injeçao de Dependência** para repositórios e serviços.
- **Configuração de Banco de Dados MySQL** via `appsettings.json`.

---

## Estrutura do projeto:
CadastroProdutoMySQL
- CONTROLLERS/ # Controladores da API
- DTOS/ # Objetos de Transferencia de Dados
- MODELOS/ # Modelos de domínio
- REPOSITORY/ # Repositório para acesso ao banco de dados
- SERVICOS/ # Regras de negócios e lógica de aplicaçâo
- appsettings.json # Configuraçoes de ambiente e banco
- Program.cs # Ponto de entrada da aplicaçao

---

## Tecnologias Utilizadas:
- **C# / .NET Core**
- **ASP.NET Core Web API**
- **MySQL**
- **DTOs (Data Transfer Objects)**
- **Swagger** para execuçao.

---

## Endpoints

### Produtos
- `Get /api/produto` - Lista todos os produtos.
- `Get /api/produto/{id}` - Busca produto por ID. 
- `POST /api/produto` - Cadastra novo produto. 
- `PUT /api/produto/{id}` - Atualiza produto. 
- `DELETE /api/produto{id}` - Remove produto. 

### Categorias
- `GET /api/categoria` - Lista todas as categorias.
- `POST /api/caetgoria` - Cadastra nova categoria.

### Estoque
- `GET /api/estoque` - Lista registros de estoque.
- `POST /api/produto` - Adiciona entrada/saída de estoque. 

---

## Como executar:
1. Clone o repositorio: 
[GitHub](https://github.com/TonyBranquinho/CadastroProdutoMySQL)
2. Configurar conexão MySQL no appsettings.json
"ConnectionStrings": {"DefaultConnection": "server=localhost;database=cadastro_produto;user=root;password=suasenha"}
3. Execute o projeto
4. Acesse o Swagger para testar os endpoints:
[https://localhost:7128/swagger/index.html](https://localhost:7128/swagger/index.html)


## Melhorias futuras, e que estao sendo implementadas:
- Implementar Entity Framework Core (Code First + Migrations).
- Implementar validaçoes de entrada mais robustas. 
- Criar testes automatizados (xUnit).
- Implementar autenticaçao e autorizaçao.

**Autor:** Tony Branquinho
[LinkedIn](https://www.linkedin.com/in/jeferson-branquinho/)
[GitHub](https://github.com/TonyBranquinho)
