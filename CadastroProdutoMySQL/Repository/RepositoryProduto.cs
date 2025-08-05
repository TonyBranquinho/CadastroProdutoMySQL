using CadastroProdutoMySQL.DTOs;
using CadastroProdutoMySQL.Modelos;
using MySql.Data.MySqlClient;
using System.Data; // Importa o namespace do driver MySql.Data
using Microsoft.Extensions.Configuration; // Importa o namespace necessario para Iconfiguration funcionar

namespace CadastroProdutoMySQL.Dados
{
    public class RepositoryProduto
    {
        // Declara um campo privado para armazenar a configuraçao recebida,
        // privado/somente leitura/interface/campo
        private readonly IConfiguration _configuration;


        // Construtor que inicializa os campos e recebe Iconfiguration por injeçao de dependecia
        public RepositoryProduto(IConfiguration configuration)
        {
            // Armazena o Iconfiguration recebido no campo privado para uso posterior
            _configuration = configuration;   // Inicializa

        }


        // METODO PRA LER OS DADOS NO BANCO DE DADOS E RETORNAR UMA LISTA
        public List<ProdutoDetalhadoDTO> ListarProdutos()
        {
            // Cria uma lsita pra armazenas os produtos recuperados do banco
            List<ProdutoDetalhadoDTO> listaProdutosDTO = new List<ProdutoDetalhadoDTO>();

            // Define uma linha de conexao com o banco de dados
            string conexao = _configuration.GetConnectionString("ConexaoPadrao");


            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Define o comando SQL para buscar todos os produtos
                string sql = "SELECT " +
                                "p.Id, p.Nome, p.Preco, " +
                                "p.CategoriaId, p.EstoqueId, " +
                                "c.Nome AS NomeCategoria, e.Quantidade " +
                                "FROM produtos p " +
                                "JOIN Categoria c ON p.CategoriaId = c.Id " +
                                "JOIN Estoque e ON p.EstoqueId = e.Id";


                // Prepara o comando SQL para execuçao no banco (using garante limpeza automatica da memoria)
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {

                    // Executa a consulta e retornar um leitor que percorre os resultados
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        // Enquanto houver linhas pra ler no resultado
                        while (reader.Read())
                        {
                            // Cria um novo objeto Produto
                            ProdutoDetalhadoDTO dto = new ProdutoDetalhadoDTO();

                            dto.Id = reader.GetInt32("Id");
                            dto.Nome = reader.GetString("Nome");
                            dto.Preco = reader.GetDecimal("Preco");
                            dto.Categoria = reader.GetString("NomeCategoria");
                            dto.Quantidade = reader.GetInt32("Quantidade");

                            listaProdutosDTO.Add(dto);

                        }
                    }
                }

                // Retorna a lista de produtos preenchida
                return listaProdutosDTO;
            }
        }





        // METODO PARA LER PELO ID UM PRODUTO NO BANCO DE DADOS E RETORNA-LO EM UMA LISTA
        public ProdutoDetalhadoDTO BuscarProdutoId(int id)
        {
            // Cria um novo objeto DTO para imprimir de maneira organizada
            ProdutoDetalhadoDTO dto = null;


            // Define uma linha de conexao com o banco de dados
            string conexao = _configuration.GetConnectionString("ConexaoPadrao");

            // Cria um objeto de conexao com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexao com banco
                conn.Open();

                // Define o comando SQL para buscar o produto usando JOIN
                string sql = "SELECT " +
                                "p.Id, p.Nome, p.Preco, " +
                                "p.CategoriaId, c.Nome AS CategoriaNome, " +
                                "p.EstoqueId, e.Quantidade " +
                             "FROM produtos p " +
                             "JOIN categoria c ON p.CategoriaId = c.Id " +
                             "JOIN estoque e ON p.EstoqueId = e.Id " +
                             "WHERE p.Id = @Id";

                // Cria um comando SQL a partir da conexão aberta e do texto SQL
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Adiciona o parametro @Id
                    cmd.Parameters.AddWithValue("@Id", id);

                    // Executa o comando SQL e cria um leitor de dados (DataReader)
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Se houver um resultado
                        if (reader.Read())
                        {
                            // Instancia DTO para a impressao organizada
                            dto = new ProdutoDetalhadoDTO();

                            dto.Id = reader.GetInt32("Id");
                            dto.Nome = reader.GetString("Nome");
                            dto.Preco = reader.GetDecimal("Preco");
                            dto.Categoria = reader.GetString("CategoriaNome");
                            dto.Quantidade = reader.GetInt32("Quantidade");
                        }
                    }

                }

                // Retorna o produto encontrado (ou null se nao existir)
                return dto;
            }
        }





        // METODO - POST - PRA INSERIR UM PRODUTO NO BANCO DE DADOS
        public void InserirProduto(Produto novoProduto)
        {
            // Define uma linha de conexao com o banco de dados
            // Mantive propositalmente essa linha, e nao a substitui pela maneira feita nos metodos acima,
            // com _configuration.GetConnectionString("ConexaoPadrao") para tambem ter um registro,
            // de uma maneira alternativa, mesmo q nao recomendada em praticas no mercado.
            string conexao = "server=localhost;database=cadastroprodutosdb;uid=root;pwd=Sarcofilos666$Mundica;";


            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Cria o comando SQL INSERT com parametros pra evitar SQL Injection
                string sql = "INSERT INTO produtos (Nome, Preco, CategoriaId, EstoqueId) " +
                             "VALUES (@Nome, @Preco, @CategoriaId, @EstoqueId)";


                // Cria um comando SQL a partir da conexão aberta e do texto SQL
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {

                    // Adiciona o parametro Nome e define o valor vindo do objeto novoProduto
                    cmd.Parameters.AddWithValue("@Nome", novoProduto.Nome);

                    // Adiciona o parametro Preco e define seu valor vindo do objeto novoProduto
                    cmd.Parameters.AddWithValue("@Preco", novoProduto.Preco);

                    // Adiciona o parametro CategoriaId e define seu valor vindo do objeto novoProduto
                    cmd.Parameters.AddWithValue("@CategoriaId", novoProduto.CategoriaId);

                    // Adiciona o parametro EstoqueId e define seu valor vindo do objeto novoProduto
                    cmd.Parameters.AddWithValue("@EstoqueId", novoProduto.EstoqueId);

                    // Executa o comando no banco (nao retorna resultados, apenas executa)
                    cmd.ExecuteNonQuery();

                }

                // Cria um segundo comando sql para recuperar o ultimo id disponivel na lista
                string sqlGetId = "SELECT LAST_INSERT_ID();";

                using (var cmd = new MySqlCommand(sqlGetId, conn))
                {

                    // tipo / variavel / recupera o id gerado automaticamente polo banco 
                    long novoId = Convert.ToInt64(cmd.ExecuteScalar());

                    novoProduto.Id = novoId;

                }

                // Fecha atomaticamente a conexao ao sair do bloco using
            }
        }





        // METODO - UPDATE - PARA ATUALIZAR UM PRODUTO NO BANCO DE DADOS
        public bool AtualizarProduto(Produto produtoAtualizado)
        {
            // Define uma linha de conexao com o banco de dados
            // Mantive propositalmente essa linha, e nao a substitui pelo maneira feita nos metodos acima,
            // com _configuration.GetConnectionString("ConexaoPadrao") para tambem ter um registro,
            // de uma maneira alternativa, mesmo q nao recomendada em praticas no mercado.
            string conexao = "server=localhost;database=cadastroprodutosdb;uid=root;pwd=Sarcofilos666$Mundica;";

            // Variavel para armazenar o numero de linhas afetadas
            int linhasAfetadas = 0;

            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Cria o comando SQL UPDATE com parametros
                string sql = "UPDATE produtos SET Nome = @Nome, Preco = @Preco WHERE Id = @Id";

                // Cria um comando SQL a partir da conexao aberta e do texto SQL
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Adiciona os parametros com seus respectivos valores
                    cmd.Parameters.AddWithValue("@Nome", produtoAtualizado.Nome);
                    cmd.Parameters.AddWithValue("@Preco", produtoAtualizado.Preco);
                    cmd.Parameters.AddWithValue("@Id", produtoAtualizado.Id);

                    // Executa o comando no banco e armazena quantas linhas foram afetadas
                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }

            // Se ao menos uma linha foi afetada, significa que a atualizaçao ocorreu
            return linhasAfetadas > 0;
        }




        // METODO - DELETE -  PARA DELETAR UM PRODUTO NO BANCO DE DADOS
        public bool DeletarProduto(int id)
        {
            // Define uma linha de conexao com o banco de dados
            string conexao = _configuration.GetConnectionString("ConexaoPadrao");

            // Cria um objeto de conexão com o banco usando a string acima
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                // Abre a conexão com o banco
                conn.Open();

                // Cria o comando SQL DELETE
                string sql = "DELETE FROM produtos WHERE Id = @Id";

                // Cria um comando SQL a partir da conexao aberta e do texto SQL
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    // Adiciona o parametro @Id e define seu valor como o id recebido no metodo
                    cmd.Parameters.AddWithValue("@id", id);

                    // Executa a instruçao DELETE e armazena quantas linhas foram afetadas
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Retorna true se deletou algo, false se não encontrou nada
                    return rowsAffected > 0;
                }
            }
        }
    }
}
