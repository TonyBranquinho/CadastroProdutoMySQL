using System;
using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.DTOs;
using CadastroProdutoMySQL.Modelos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CadastroProdutoMySQL.Servicos
{
    public class ProdutoServico
    {

        private readonly RepositoryProduto _repositoryProduto;


        // Construtor que recebe RepositoryProduto por injeçao e dependencia
        public ProdutoServico(RepositoryProduto repositoryProduto)
        {
            _repositoryProduto = repositoryProduto;
        }




        // METODO GET
        public List<ProdutoDetalhadoDTO> ListarTodos()
        {
            // Chama na Repository o metodo para listar o os produtos
            List<Produto> retorno = _repositoryProduto.ListarProdutos();

            if (retorno == null)
            {
                return null;
            }

            // Instancia lista para impressao
            List<ProdutoDetalhadoDTO> listaProdutoDetalhadoDTO = new List<ProdutoDetalhadoDTO>();


            // Mapeia o retorno PRODUTO da repository para a lista de impressao DTO
            foreach (Produto p in retorno)
            {
                ProdutoDetalhadoDTO produtoDetalhadoDTO = new ProdutoDetalhadoDTO();

                produtoDetalhadoDTO.Id = p.Id;
                produtoDetalhadoDTO.Nome = p.Nome;
                produtoDetalhadoDTO.Preco = p.Preco;
                produtoDetalhadoDTO.Categoria = p.Categoria.Nome;
                produtoDetalhadoDTO.Quantidade = p.Estoque.Quantidade;

                listaProdutoDetalhadoDTO.Add(produtoDetalhadoDTO);
            }

            return listaProdutoDetalhadoDTO;
        }




        // METODO GET ID
        public ProdutoDetalhadoDTO BuscaId(int id)
        {
            Produto buscaProduto = _repositoryProduto.BuscarProdutoId(id);

            if (buscaProduto == null)
            {
                return null;
            }

            return new ProdutoDetalhadoDTO
            {
                Id = buscaProduto.Id,
                Nome = buscaProduto.Nome,
                Preco = buscaProduto.Preco,
                Categoria = buscaProduto.Categoria.Nome,
                Quantidade = buscaProduto.Estoque.Quantidade,
            };
        }



        // METODO - POST
        public ProdutoRespCriacaoDTO CadastroProduto(ProdutoCriacaoDTO dto)
        {
            // Regra de negocio: Verifica duplicidade
            if (_repositoryProduto.ExistsByName(dto.Nome))
            {
                return null; // Controller vai traduzir para HTTP 409
            }

            if (_repositoryProduto.ExistsByEstoqueId(dto.EstoqueId))
            {
                return null; // Controller vai traduzir para HTTP 409
            }


            // Mapeia o DTO para um Produto (modelo do dominio),
            // para evitar exposiçao de campos indesejados (Mais segurança)
            Produto novoProduto = new Produto
            {
                Nome = dto.Nome,
                Preco = dto.Preco,
                CategoriaId = dto.CategoriaId,
                EstoqueId = dto.EstoqueId,
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



        // METODO - PUT 
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



        // METODO - DELETE
        public bool ExcluirProduto(int id)
        {
            _repositoryProduto.DeletarProduto(id);

            return true;
        }
    }
}
