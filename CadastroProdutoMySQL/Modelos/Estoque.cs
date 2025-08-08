using MySql.Data.MySqlClient;

namespace CadastroProdutoMySQL.Modelos
{
    public class Estoque
    {
        public long Id { get; set; }
        public int Quantidade { get; set; }

        public Estoque()
        {
        }

        public Estoque(long id, int quantidade)
        {
            Id = id;
            Quantidade = quantidade;
        }
    }
}
