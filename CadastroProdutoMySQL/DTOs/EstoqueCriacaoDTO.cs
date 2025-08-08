using System.ComponentModel.DataAnnotations;

namespace CadastroProdutoMySQL.DTOs
{
    public class EstoqueCriacaoDTO
    {
        [Required]// Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public long Id { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")] // Data Annotations / Validaçao automatica dizendo que esse atributo é obrigatorio
        public int Quantidade { get; set; }


        public EstoqueCriacaoDTO()
        {
        }

        public EstoqueCriacaoDTO(long id, int quantidade)
        {
            Id = id;
            Quantidade = quantidade;
        }
    }
}
