namespace CadastroProdutoMySQL.Modelos
{
    public class Estoque
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }

        public Estoque()
        {
        }

        public Estoque(int id, int quantidade)
        {
            Id = id;
            Quantidade = quantidade;
        }
    }
}
