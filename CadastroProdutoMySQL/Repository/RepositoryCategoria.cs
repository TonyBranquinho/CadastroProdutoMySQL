using CadastroProdutoMySQL.Modelos;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace CadastroProdutoMySQL.Dados
{
    public class RepositoryCategoria
    {
        // Declara um campo privado para armazenar a configuraçao recebida,
        // privado/somente leitura/interface/campo
        private readonly IConfiguration _configuration;


        // Construtor que inicializa os campos e recebe Iconfiguration por injeçao de dependecia
        public RepositoryCategoria(IConfiguration configuration)
        {
            // Armazena o Iconfiguration recebido no campo privado para uso posterior
            _configuration = configuration;   // Inicializa

        }


        // METODO PARA LISTAR TODAS AS CATEGORIAS DE PRODUTOS
        public List<Categoria> ListarCategoria()
        {
            // Cria uma lista para armazenar as categorias de produtos recuperadas do banco de dados
            List<Categoria> listaCategoria = new List<Categoria>();

            // Define uma linha de conexão com o banco de dados
            string conexao = _configuration.GetConnectionString("ConexaoPadrao");

            // Cria um objeto de conexao com banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexao com o banco
                conn.Open();

                // Define o comando SQL para buscar todos os produtos
                string sql = "SELECT * FROM categoria";

                // Prepara o comando SQL para execuçao no banco
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Executa a consutla e retorna um leitor de linhas
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Equanto houver linhas para ler no resultado
                        while (reader.Read())
                        {
                            // Cria um novo objeto Categoria
                            Categoria c = new Categoria();

                            // Le o campo Id
                            c.Id = reader.GetInt32("Id");
                            // Le o campo Nome
                            c.Nome = reader.GetString("Nome");

                            listaCategoria.Add(c);
                        }
                    }
                }
                // Retorna a lista de Categoria preenchida
                return listaCategoria;
            }
        }



        // METODO PARA LISTAR A CATEGORIA POR ID
        public Categoria ListarCategoriaId(int id)
        {
            // Cria a variavel para retornar o objeto categoria
            Categoria categoriaEncontrada = null;

            // Define uma linha de conexao com o banco de dados
            string conexao = _configuration.GetConnectionString("ConexaoPadrao");

            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Define o comando SQL para buscar a categoria por ID
                string sql = "SELECT * FROM categoria WHERE Id = @Id";

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
                            categoriaEncontrada = new Categoria();

                            categoriaEncontrada.Id = reader.GetInt32("Id");
                            categoriaEncontrada.Nome = reader.GetString("Nome");
                        }
                    }
                }
                // Retorna o produto encontrado ou null se nao existir
                return categoriaEncontrada;
            }
        }



        // METODO PARA CADASTRAR UMA CATEGORIA
        public void InserirCategoria(Categoria novaCategoria)
        {
            // Define uma linha de conexao com o banco de dados
            string conexao = _configuration.GetConnectionString("ConexaoPadrao");

            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Define o comando SQL para cadastrar uma categoria
                string sql = "INSERT INTO categoria (Id, Nome) VALUES (@Id, @Nome)";

                // Prepara o comando SQL para execuçao no banco (using garante limpeza automatica da memoria)
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Define o paramentro @Id com o valor 
                    cmd.Parameters.AddWithValue("@Id", novaCategoria.Id);

                    // Define o paramentro @Nome com o valor 
                    cmd.Parameters.AddWithValue("@Nome", novaCategoria.Nome);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        // METODO PARA ATUALIZAR UMA CATEGORIA POR ID
        public bool AtualizaCategoriaId(Categoria categoriaAtualizada)
        {
            // Define uma linha de conexao com o banco de dados
            string conexao = _configuration.GetConnectionString("ConexaoPadrao");

            int linhasAfetadas = 0;

            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Define o comando SQL para atualizar uma categoria
                string sql = "UPDATE categoria SET Nome = @Nome WHERE Id = @Id";

                // Prepara o comando SQL para execuçao no banco (using garante limpeza automatica da memoria)
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Define o parametro @Nome com o valor
                    cmd.Parameters.AddWithValue("@Nome", categoriaAtualizada.Nome);
                    
                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }
            return linhasAfetadas > 0;
        }


        
        // METODO PARA REMOVER UMA CATEGORIA POR ID
        public bool DeletarCategoriaId(int Id)
        {
            // Define uma linha de conexao com o banco de dados
            string conexao = _configuration.GetConnectionString("ConexaoPadrao");

            int linhasAfetadas = 0;

            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Define o comando SQL para deletar uma categoria
                string sql = "DELETE FROM categoria WHERE Id = @Id";

                // Prepara o comando SQL para execuçao no banco (using garante limpeza automatica da memoria)
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Adiciona o parametro @Id e define seu valor como o id recebido no metodo
                    cmd.Parameters.AddWithValue("@id", Id);

                    // Executa a instruçao DELETE e armazena quantas linhas foram afetadas
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Retorna true se deletou algo, false se não encontrou nada
                    return rowsAffected > 0;
                }
            }
        }
    }
}
