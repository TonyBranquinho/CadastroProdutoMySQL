using CadastroProdutoMySQL.Modelos;
using MySql.Data.MySqlClient; // Importa o namespace do driver MySql.Data

namespace CadastroProdutoMySQL.Dados
{
    public class OperacaoBancoDados
    {




        // METODO PRA LER OS DADOS NO BANCO DE DADOS E RETORNAR UMA LISTA
        public List<Produto> ListarProdutos()
        {
            // Cria uma lsita pra armazenas os produtos recuperados do banco
            List<Produto> listaProdutos = new List<Produto>();

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
                            // Cria um novo objeto Produto
                            Produto p = new Produto();

                            // Lê o campo Id (tipo INT) e atribui ao produto
                            p.Id = reader.GetInt32("Id");

                            // Lê o campo Nome (tipo VARCHAR) e atribui ao produto
                            p.Nome = reader.GetString("Nome");

                            // Lê o campo Preco (tipo Decimal) e atribui ao produto
                            p.Preco = reader.GetDecimal("Preco");

                            listaProdutos.Add(p);
                        }
                    }
                }

                // Retorna a lista de produtos preenchida
                return listaProdutos;
            }
        }







        // METODO - POST - PRA INSERIR UM PRODUTO NO BANCO DE DADOS
        public void InserirProduto(Produto novoProduto)
        {
            // Define uma linha de conexao com o banco de dados
            string conexao = "server=localhost;database=cadastroprodutosdb;uid=root;pwd=Sarcofilos666$Mundica;";

            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Cria o comando SQL INSERT com parametros pra evitar SQL Injection
                string sql = "INSERT INTO produtos (Nome, Preco) VALUES (@Nome, @Preco)";

                // Cria um comando SQL a partir da conexão aberta e do texto SQL
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Adiciona o parametro @Nome e define o valor cindo do objeto novoProduto
                    cmd.Parameters.AddWithValue("@Nome", novoProduto.Nome);

                    // Adiciona o parametro @Preco e define seu valor vindo do objeto novoProduto
                    cmd.Parameters.AddWithValue("@Preco", novoProduto.Preco);

                    // Executa o comando no banco (nao retorna resultados, apenas executa)
                    cmd.ExecuteNonQuery();
                }

                // Fecha atomaticamente a conexao ao sair do bloco using
            }
        }
    }
}
