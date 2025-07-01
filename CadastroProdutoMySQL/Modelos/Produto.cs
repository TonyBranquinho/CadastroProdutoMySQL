namespace CadastroProdutoMySQL.Modelos
{
    public class Produto
    {
        // Atributos da classe
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }

        // Construtor vazio
        public Produto()
        {
        }

        // Construtor com parametros
        public Produto(int codigo, string nome, double preco)
        {
            Codigo = codigo;
            Nome = nome;
            Preco = preco;
        }
    }
}
