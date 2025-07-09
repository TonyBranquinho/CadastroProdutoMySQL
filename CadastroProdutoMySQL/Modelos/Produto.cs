namespace CadastroProdutoMySQL.Modelos
{
    public class Produto
    {
        // Atributos da classe
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }

        // Construtor vazio
        public Produto()
        {
        }

        // Construtor com parametros
        public Produto(int id, string nome, double preco)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
        }
    }
}
