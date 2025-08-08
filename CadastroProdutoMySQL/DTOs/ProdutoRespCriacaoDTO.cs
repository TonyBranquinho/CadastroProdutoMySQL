namespace CadastroProdutoMySQL.DTOs
{
    public class ProdutoRespCriacaoDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }


        public ProdutoRespCriacaoDTO()
        {
        }

        public ProdutoRespCriacaoDTO(int id, string nome, decimal preco)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
        }
    }
}
