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
        
        
        // Construtor que recebe RepositoryProduto por injeçao e dependencia
        public ProdutoServico(RepositoryProduto repositoryProduto)
        {
            _repositoryProduto = repositoryProduto;
        }




        // Metodo Cadastro - POST
        public ProdutoRespCriacaoDTO CadastroProduto(ProdutoCriacaoDTO DTO)
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
            ProdutoRespCriacaoDTO respostaDTO = new ProdutoRespCriacaoDTO
            {
                Id = novoProduto.Id,
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco,
            };

            return respostaDTO;
        }


























        // METODO ATUALIZAR - PUT 
        public ProdutoRespAtualizacaoDTO Atualiza(ProdutoCriacaoDTO produtoAtualizadoDTO, long id)
        {
            Produto produtoAtualizado = new Produto
            {   
                Id = id,
                Nome = produtoAtualizadoDTO.Nome,
                Preco = produtoAtualizadoDTO.Preco,
                CategoriaId = produtoAtualizadoDTO.CategoriaId,
                EstoqueId = produtoAtualizadoDTO.EstoqueId,
            };

            _repositoryProduto.AtualizarProduto(produtoAtualizado);

            ProdutoRespAtualizacaoDTO Resp = new ProdutoRespAtualizacaoDTO
            {
                Id = id,
                Nome = produtoAtualizado.Nome,
                Preco = produtoAtualizado.Preco,
                CategoriaId = produtoAtualizado.CategoriaId,
                EstoqueId = produtoAtualizado.EstoqueId,
            };

            return Resp;
        }




































        // Metodo Busca - READ
        public Produto BuscaProduto(int Codigo)
        {
            Produto produtoEncontrado = produtos.FirstOrDefault(p => p.Id == Codigo);
            return produtoEncontrado;
        }


       // Metodo Excluir - DELETE
        public void ExcluirProduto(Produto excluiProduto)
        {
            produtos.Remove(excluiProduto);
        }
    }
}
