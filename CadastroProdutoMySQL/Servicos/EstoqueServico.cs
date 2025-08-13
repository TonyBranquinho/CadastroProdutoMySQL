using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.DTOs;
using CadastroProdutoMySQL.Modelos;

namespace CadastroProdutoMySQL.Servicos
{
    public class EstoqueServico
    {

        private readonly RepositoryEstoque _repositoryEstoque;

        // Construtor que recebe RepositoryEstoque por injeçao de dependencia
        public EstoqueServico(RepositoryEstoque repositoryEstoque)
        {
            _repositoryEstoque = repositoryEstoque;
        }





        // METODO CADASTRO ESTOQUE - POST
        public EstoqueRespostaDTO CadastraEstoque(EstoqueCriacaoDTO estoqueDTO)
        {

            // Mapeia o DTO para um objeto estoque (modelo do dominio),
            // para evitar exposiçao de campos indesejados (Mais segurança)       
            Estoque novoEstoque = new Estoque
            {
                Id = estoqueDTO.Id,
                Quantidade = estoqueDTO.Quantidade,
            };

            
            _repositoryEstoque.CadastraEstoqueRepository(novoEstoque);


            EstoqueRespostaDTO respostaDTO = new EstoqueRespostaDTO
            {
                Id = estoqueDTO.Id,
                Quantidade = estoqueDTO.Quantidade,
            };

            return respostaDTO;
        }
    }
}
