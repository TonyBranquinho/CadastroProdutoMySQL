using MySql.Data.MySqlClient; // Importa o namespace do driver MySql.Data

namespace CadastroProdutoMySQL.Dados
{
    public class OperacaoBancoDados
    {

        public void ListarProdutos() // Metodo que vai ler os dados do banco de dados
        {
            // Define uma linha de conexao com o banco de dados
            string conexao = "server=localhost;database=cadastroprodutosdb;uid=root;pwd=Sarcofilos666$Mundica;";


            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Define o comando SQL para buscar todos os produtos
                string sql = "SELECT * FROM produtos";


                // Cria um comando SQL a partir da conexão aberta e do texto SQL
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {

                    // Executa o comando SQL e cria um leitor de dados (DataReader)
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        // Enquanto houver linhas pra ler no resultado
                        while (reader.Read())
                        {
                            

                            // Lê o campo Id (tipo INT) da tabela e armazena em uma variavel loca
                            int id = reader.GetInt32("Id");

                            // Lê o campo Nome (tipo VARCHAR) da tabela e armazena em uma variavel loca
                            string nome = reader.GetString("Nome");

                            // Lê o campo Preco (tipo Decimal) da tabela e armazena em uma variavel loca
                            decimal preco = reader.GetDecimal("Preco");

                            // Imprime os dados no console para teste
                            Console.WriteLine($"Id: {id}, Nome: {nome}, Preco: {preco}");
                        }
                    }
                }
            }
        }
    }
}
