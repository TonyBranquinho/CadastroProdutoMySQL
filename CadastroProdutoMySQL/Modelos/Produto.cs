using MySql.Data.MySqlClient;

namespace CadastroProdutoMySQL.Modelos
{
    public class Produto
    {
        // Atributos da classe
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        // Construtor vazio
        public Produto()
        {
        }

        // Construtor com parametros
        public Produto(int id, string nome, decimal preco)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
        }
    }
}
