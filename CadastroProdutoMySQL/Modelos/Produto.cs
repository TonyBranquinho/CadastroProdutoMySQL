using MySql.Data.MySqlClient;

namespace CadastroProdutoMySQL.Modelos
{
    public class Produto
    {
        // Atributos da classe
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public int CategoriaId {  get; set; }
        public Categoria Categoria { get; set; }


        public int EstoqueId {  get; set; }
        public Estoque Estoque { get; set; }

        // Construtor vazio
        public Produto()
        {
        }

        // Construtor com parametros
        public Produto(long id, string nome, decimal preco, int categoriaId, int estoqueId)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            CategoriaId = categoriaId;
            EstoqueId = estoqueId;
        }
    }
}
