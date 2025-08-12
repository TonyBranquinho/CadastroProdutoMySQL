namespace CadastroProdutoMySQL.DTOs
{
    public class ProdutoDetalhadoDTO
    {

        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }


        public ProdutoDetalhadoDTO()
        {
        }

        public ProdutoDetalhadoDTO(long id, string categoria, string nome, decimal preco, int quantidade)
        {
            Id = id;
            Categoria = categoria;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }
    }
}
