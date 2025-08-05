using CadastroProdutoMySQL.Modelos;
using MySql.Data.MySqlClient;

namespace CadastroProdutoMySQL.Dados
{
    public class RepositoryEstoque
    {
        // METODO PARA LISTA O ESTOQUE
        public List<Estoque> Estoque()
        {
            List<Estoque> listaEstoque = new List<Estoque>();

            // Define uma linha de conexão com o banco de dados
            string conexao = "server=localhost;database=cadastroprodutosdb;uid=root;pwd=Sarcofilos666$Mundica;";

            // Cria um objeto de conexao com banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexao com o banco
                conn.Open();

                // Define o comando SQL para buscar todos os valores de estoque
                string sql = "SELECT * FROM estoque";

                // Prepara o comando SQL para execuçao no banco
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Executa a consutla e retorna um leitor de linhas
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Equanto houver linhas para ler no resultado
                        while (reader.Read())
                        {
                            // Cria um novo objeto Estoque
                            Estoque e = new Estoque();

                            e.Id = reader.GetInt32("Id");

                            e.Quantidade = reader.GetInt32("Quantidade");

                            listaEstoque.Add(e);
                        }
                    }
                }

                return listaEstoque;
            }
        }




        // METODO QUE BUSCA O INDICE DE ESTOQUE PELO ID
        public Estoque BuscaEstoqueId(int id)
        {
            Estoque estoqueEncontrado = null;

            // Define uma linha de conexão com o banco de dados
            string conexao = "server=localhost;database=cadastroprodutosdb;uid=root;pwd=Sarcofilos666$Mundica;";

            // Cria um objeto de conexao com banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexao com o banco
                conn.Open();

                // Define o comando SQL para buscaro indice de estoque pelo ID
                string sql = "SELECT * FROM estoque WHERE Id = @id";

                // Prepara o comando SQL para execuçao no banco (using garante limpeza automatica da memoria)
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Substitui o @Id do comando ali em cima, pelo id recebido no metodo
                    cmd.Parameters.AddWithValue("@Id", id);

                    // Executa a consulta e retornar um leitor que percorre os resultados
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Se houver um resultado
                        if (reader.Read())
                        {
                            estoqueEncontrado = new Estoque();

                            estoqueEncontrado.Id = reader.GetInt32("Id");
                            estoqueEncontrado.Quantidade = reader.GetInt32("Quantidade");
                        }
                    }
                }
                return estoqueEncontrado;
            }
        }
    }
}
