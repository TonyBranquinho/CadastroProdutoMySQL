using CadastroProdutoMySQL.DTOs;
using CadastroProdutoMySQL.Modelos;
using MySql.Data.MySqlClient;
using System.Data; // Importa o namespace do driver MySql.Data

namespace CadastroProdutoMySQL.Dados
{
    public class RepositoryProduto
    {

        private readonly RepositoryCategoria _repositoryCategoria;
        private readonly RepositoryEstoque _repositoryEstoque;

        // Construtor que inicializa a classe
        public RepositoryProduto()
        {
            _repositoryCategoria = new RepositoryCategoria();
            _repositoryEstoque = new RepositoryEstoque();
        }


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
                            Produto p = new Produto();

                            // Lê o campo Id (tipo INT) e atribui ao produto
                            p.Id = reader.GetInt32("Id");

                            // Lê o campo Nome (tipo VARCHAR) e atribui ao produto
                            p.Nome = reader.GetString("Nome");

                            // Lê o campo Preco (tipo Decimal) e atribui ao produto
                            p.Preco = reader.GetDecimal("Preco");

                            // Lê o campo CategoriaId e atribui ao produto mas tambem atribui 0 se o valor for nulo
                            p.CategoriaId = reader.IsDBNull("CategoriaId") ? 0 : reader.GetInt32("CategoriaId");
                            p.EstoqueId = reader.IsDBNull("EstoqueId") ? 0 : reader.GetInt32("EstoqueId");


                            // Busca dados complementares do produto nas respectivas tabelas
                            p.Categoria = _repositoryCategoria.ListarCategoriaId(p.CategoriaId);
                            p.Estoque = _repositoryEstoque.BuscaEstoqueId(p.EstoqueId);

                            listaProdutos.Add(p);

                        }
                    }
                }

                // Retorna a lista de produtos preenchida
                return listaProdutos;
            }
        }





        // METODO PARA LER PELO ID UM PRODUTO NO BANCO DE DADOS E RETORNA-LO EM UMA LISTA
        public ProdutoDetalhadoDTO BuscarProdutoId(int id)
        {
            // Cria um novo objeto DTO para imprimir de maneira organizada
            ProdutoDetalhadoDTO dto = null;

            // Cria o objeto os objetos para atribuir os dados de outras tabelas
            Categoria categoria = null;
            Estoque estoque = null;


            // Define uma linha de conexao com o banco de dados
            string conexao = "server=localhost;database=cadastroprodutosdb;uid=root;pwd=Sarcofilos666$Mundica;";

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





        // METODO - UPDATE - PARA ATUALIZAR UM PRODUTO NO BANCO DE DADOS
        public bool AtualizarProduto(Produto produtoAtualizado)
        {
            // Define uma linha de conexao com o banco de dados
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
            string conexao = "server=localhost;database=cadastroprodutosdb;uid=root;pwd=Sarcofilos666$Mundica;";

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
