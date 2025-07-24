#CadastroProdutoMySQL

Esse é um projeto **Back-End** desenvolvido em **C# com ASP.NET Core**,
chamdo **CadastroProdutoMySQL**, que implementa uma API REST
para operaçoes CRUD com persistencia em MySQL.

## Tecnologias Utilizadas:
- C#
- ASP.NET Core
- Controllers
- Swagger
- MySQL

## Funcionalidades:
- **GET /produto** - Retorna todos os produtos cadastrados.
- **GET /produto/{id}** - Retorna o produto pelo ID.
- **POST /produto** - Cadastra um novo produto.
- **PUT /produto/{id}** - Atualiza um produto existente.
- **DELETE /produto/{id}** - Deleta um produto.

## Estrutura do projeto:
CadastroProdutoMySQL
- Controllers
--- ProdutoController.cs
- Modelos
--- Produto.cs
- Dados
--- RepositoryProduto.cs
- Program.cs

## Como executar:
1. Clone o repositorio:
[GitHub](https://github.com/TonyBranquinho/CadastroProdutoMySQL/tree/main/CadastroProdutoMySQL)
2. Execute o projeto:
3. Acesse o Swagger para testar os endpoints:
[https://localhost:7128/swagger/index.html](https://localhost:7128/swagger/index.html)

## Conceitos Utilizados:
- **API REST:** Interface que permite comunicaçao entre aplicaçoes via 
requisiçao HTTP padronizadas.
- **Controllers:** Classes responsaveis por gerenciar rotas (endpoints)
e receber requisições.
- **Endpoints:** Cada rota criafa que corresponde a um verbo HTTP (GET,
POST, PUT, DELETE).
- **Swagger:** Ferramenta de documentaçao e testes de APIs.
- **Persistencia em MySQL:** Os dados sao armazenados em banco relacional,
garantindo integridade e segurança.

## Melhorias futuras, e que estao sendo implementadas:
- Criar relacionamento PRODUTO x ESTOQUE x CATEGORIA.
- Implementar Entity Framework Core (Code First + Migrations).
- Implementar validaçoes de entrada mais robustas. 
- Criar testes automatizados (xUnit).
- Implementar autenticaçao e autorizaçao.

**Autor:** Tony Branquinho
[Linkedin](https://www.linkedin.com/in/jeferson-branquinho/)
[GitHub](https://github.com/TonyBranquinho)