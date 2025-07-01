namespace CadastroProdutoMySQL.Modelos
{
    public class Estoque
    {
        public int Codigo { get; set; }
        public int Quantidade { get; set; }

        public Estoque()
        {
        }

        public Estoque(int codigo, int quantidade)
        {
            Codigo = codigo;
            Quantidade = quantidade;
        }
    }
}
