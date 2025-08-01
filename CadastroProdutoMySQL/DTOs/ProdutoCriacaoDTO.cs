using System.ComponentModel.DataAnnotations;

namespace CadastroProdutoMySQL.DTOs
{
    public class ProdutoCriacaoDTO
    {
        [Required] // Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public string Nome { get; set; }

        [Range(0.01, double.MaxValue)] // Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public decimal Preco { get; set; }

        [Required] // Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public int CategoriaId { get; set; }

        [Required] // Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public int EstoqueId { get; set; }

        public ProdutoCriacaoDTO()
        {
        }

        public ProdutoCriacaoDTO(string nome, decimal preco, int categoriaId, int estoqueId)
        {
            Nome = nome;
            Preco = preco;
            CategoriaId = categoriaId;
            EstoqueId = estoqueId;
        }
    }
}
