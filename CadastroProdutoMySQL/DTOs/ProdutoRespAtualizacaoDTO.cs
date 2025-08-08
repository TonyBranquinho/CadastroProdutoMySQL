namespace CadastroProdutoMySQL.DTOs
{
    public class ProdutoRespAtualizacaoDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int CategoriaId { get; set; }
        public int EstoqueId { get; set; }

        public ProdutoRespAtualizacaoDTO()
        {
        }

        public ProdutoRespAtualizacaoDTO(long id, string nome, decimal preco, int categoriaId, int estoqueId)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            CategoriaId = categoriaId;
            EstoqueId = estoqueId;
        }
    }
}
