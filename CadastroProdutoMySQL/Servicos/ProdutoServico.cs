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
        private readonly RepositoryCategoria _repositoryCategoria;


        // Construtor que recebe RepositoryProduto por injeçao e dependencia
        public ProdutoServico(RepositoryProduto repositoryProduto, RepositoryCategoria repositoryCategoria)
        {
            _repositoryProduto = repositoryProduto;
            _repositoryCategoria = repositoryCategoria;
        }




        // METODO GET
        public List<ProdutoResponseDTO> ListarTodos()
        {
            // Chama na Repository o metodo para listar o os produtos
            List<Produto> retorno = _repositoryProduto.ListarProdutos();

            if (retorno == null)
            {
                return null;
            }

            // Instancia lista para impressao
            List<ProdutoResponseDTO> listaProdutoDetalhadoDTO = new List<ProdutoResponseDTO>();


            // Mapeia o retorno PRODUTO da repository para a lista de impressao DTO
            foreach (Produto p in retorno)
            {
                ProdutoResponseDTO produtoDetalhadoDTO = new ProdutoResponseDTO();

                produtoDetalhadoDTO.Id = p.Id;
                produtoDetalhadoDTO.Nome = p.Nome;
                produtoDetalhadoDTO.Preco = p.Preco;
                produtoDetalhadoDTO.Categoria = p.Categoria.Nome;
                produtoDetalhadoDTO.Quantidade = p.Quantidade;

                listaProdutoDetalhadoDTO.Add(produtoDetalhadoDTO);
            }

            return listaProdutoDetalhadoDTO;
        }




        // METODO GET ID
        public ProdutoResponseDTO BuscaId(int id)
        {
            Produto buscaProduto = _repositoryProduto.BuscarProdutoId(id);

            if (buscaProduto == null)
            {
                return null;
            }

            return new ProdutoResponseDTO
            {
                Id = buscaProduto.Id,
                Nome = buscaProduto.Nome,
                Preco = buscaProduto.Preco,
                Categoria = buscaProduto.Categoria.Nome,
                Quantidade = buscaProduto.Quantidade,
            };
        }



















        // METODO - POST
        // Recebe um DTO request, e devolve um DTO response
        public ProdutoResponseDTO CadastroProduto(ProdutoRequestDTO dto)
        {
            // Regra de negocio: Verifica duplicidade
            if (_repositoryProduto.ExistsByName(dto.Nome))
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
                Quantidade = dto.Quantidade,
            };

            // Chama o metodo que adiciona o novo produto no Banco de Dados
            _repositoryProduto.InserirProduto(novoProduto);


            // Chama o listar categoria por id para trazer para o dto o nome da categoria
            Categoria categoria = _repositoryCategoria.ListarCategoriaId(dto.CategoriaId);


            // Cria um objeto ProdutoRespostaDTO com os dados que voltam para o cliente
            // isso evita expor propriedades desnecessarias e ou sensiveis do produto
            ProdutoResponseDTO respostaDTO = new ProdutoResponseDTO
            {   
                Id = novoProduto.Id,
                Nome = dto.Nome,
                Preco = dto.Preco,
                Categoria = categoria.Nome,
                CategoriaId = dto.CategoriaId,
                Quantidade = dto.Quantidade,
            };

            return respostaDTO;
        }
















        // METODO - PUT 
        public ProdutoRequesDTO Atualiza(ProdutoRequestDTO produtoAtualizadoDTO, long id)
        {
            Produto produtoAtualizado = new Produto
            {
                Id = id,
                Nome = produtoAtualizadoDTO.Nome,
                Preco = produtoAtualizadoDTO.Preco,
                CategoriaId = produtoAtualizadoDTO.CategoriaId,
                Quantidade = produtoAtualizadoDTO.Quantidade,
            };

            _repositoryProduto.AtualizarProduto(produtoAtualizado);

            ProdutoRequesDTO Resp = new ProdutoRequesDTO
            {
                Id = id,
                Nome = produtoAtualizado.Nome,
                Preco = produtoAtualizado.Preco,
                CategoriaId = produtoAtualizado.CategoriaId,
                Quantidade = produtoAtualizado.Quantidade,
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
