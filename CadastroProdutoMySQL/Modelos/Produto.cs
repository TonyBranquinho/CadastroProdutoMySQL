using MySql.Data.MySqlClient;

namespace CadastroProdutoMySQL.Modelos
{
    public class Produto
    {
        // Atributos da classe
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria {  get; set; }

      
        // Construtor vazio
        public Produto()
        {
            Categoria = new Categoria();
        }


        // Construtor com parametros
        public Produto(long id, string nome, decimal preco, int quantidade, int categoriaId)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
            CategoriaId = categoriaId;            
        }
    }
}
