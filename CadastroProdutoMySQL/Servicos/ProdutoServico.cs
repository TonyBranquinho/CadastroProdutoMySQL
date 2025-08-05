using System;
using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.DTOs;
using CadastroProdutoMySQL.Modelos;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CadastroProdutoMySQL.Servicos
{
    public class ProdutoServico
    {
        // Atributos da classe
        public List<Produto> produtos { get; private set; }
        public List<Estoque> estoque { get; private set; }


        private readonly RepositoryProduto _repositoryProduto;

        
        

        
        // Construtor com parametros
        public ProdutoServico(RepositoryProduto repositoryProduto)
        {
            _repositoryProduto = repositoryProduto;
        }


        // Metodo Cadastro - POST
        public ProdutoRespostaDTO CadastroProduto(ProdutoCriacaoDTO DTO)
        {
            // Mapeia o DTO para um Produto (modelo do dominio),
            // recebe os dados tipo ProdutoCriacaoDTO, passa eles para o tipo Produto,
            // para evitar exposiçao de campos indesejados (Mais segurança)
            Produto novoProduto = new Produto
            {
                Nome = DTO.Nome,
                Preco = DTO.Preco,
                CategoriaId = DTO.CategoriaId,
                EstoqueId = DTO.EstoqueId,
            };

            // Chama o metodo que adiciona o novo produto no Banco de Dados
            _repositoryProduto.InserirProduto(novoProduto);


            // Cria um objeto ProdutoRespostaDTO com os dados que voltam para o cliente
            // isso evita expor propriedades desnecessarias e ou sensiveis do produto
            ProdutoRespostaDTO respostaDTO = new ProdutoRespostaDTO
            {
                Id = novoProduto.Id,
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco,
            };

            return respostaDTO;
        }





























        // Metodo Busca - READ
        public Produto BuscaProduto(int Codigo)
        {
            Produto produtoEncontrado = produtos.FirstOrDefault(p => p.Id == Codigo);
            return produtoEncontrado;
        }


        // Metodo Atualiza - UPDATE
        public void AtualizaProduto(Produto produtoEncontrado, string novoNome, decimal novoPreco)
        {
            produtoEncontrado.Nome = novoNome;
            produtoEncontrado.Preco = novoPreco;
        }


        // Metodo Excluir - DELETE
        public void ExcluirProduto(Produto excluiProduto)
        {
            produtos.Remove(excluiProduto);
        }
    }
}
