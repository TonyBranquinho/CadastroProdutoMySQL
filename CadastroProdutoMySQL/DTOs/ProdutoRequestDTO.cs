using System.ComponentModel.DataAnnotations;

namespace CadastroProdutoMySQL.DTOs
{
    public class ProdutoRequestDTO
    {
        [Required] // Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public string Nome { get; set; }

        [Range(0.01, double.MaxValue)] // Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public decimal Preco { get; set; }

        [Required] // Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public int CategoriaId { get; set; }

        [Required] // Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public int Quantidade { get; set; }

        public ProdutoRequestDTO()
        {
        }

        public ProdutoRequestDTO(string nome, decimal preco, int categoriaId, int quantidade)
        {
            Nome = nome;
            Preco = preco;
            CategoriaId = categoriaId;
            Quantidade = quantidade;
        }
    }
}
