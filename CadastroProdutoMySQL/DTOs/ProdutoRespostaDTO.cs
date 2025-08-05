namespace CadastroProdutoMySQL.DTOs
{
    public class ProdutoRespostaDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }


        public ProdutoRespostaDTO()
        {
        }

        public ProdutoRespostaDTO(int id, string nome, decimal preco)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
        }
    }
}
