namespace CadastroProdutoMySQL.DTOs
{
    public class ProdutoResponseDTO
    {

        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Categoria { get; set; }
        public int CategoriaId { get; set; }
        public int Quantidade { get; set; }


        public ProdutoResponseDTO()
        {
        }

        public ProdutoResponseDTO(long id, string categoria, string nome, decimal preco, int quantidade, int categoriaId)
        {
            Id = id;
            Categoria = categoria;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
            CategoriaId = categoriaId;
        }
    }
}
