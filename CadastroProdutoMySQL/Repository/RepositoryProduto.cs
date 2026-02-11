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

        private readonly ILogger<RepositoryProduto> _logger;

        // Construtor que inicializa os campos e recebe Iconfiguration por injeçao de dependecia
        public RepositoryProduto(IConfiguration configuration, ILogger<RepositoryProduto> logger)
        {
            // Armazena o Iconfiguration recebido no campo privado para uso posterior
            _configuration = configuration;   // Inicializa .
            _logger = logger;
        }





        // METODO PRA LER OS DADOS NO BANCO DE DADOS E RETORNAR UMA LISTA
        public List<Produto> ListarProdutos()
        {
            try // Tenta rodar o codigo normalmente
            {

                // Cria uma lsita pra armazenas os produtos recuperados do banco
                List<Produto> listaP = new List<Produto>();

                Produto produto = null;

                // Obtem uma linha de conexao definida no appsetings.json
                string conexao = _configuration.GetConnectionString("ConexaoPadrao");


                // Cria um objeto de conexão com o banco usando a string acima
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    // Abre a conexão com o banco
                    conn.Open();
                    _logger.LogInformation("Conexão aberta com sucesso");////////////////////

                    // Comando SQL para buscar todos os produtos
                    string sql = @"
                        SELECT
                            p.Id,
                            p.Nome,
                            p.Preco,
                            p.CategoriaId,
                            p.Quantidade,
                            c.Nome AS NomeCategoria
                        FROM produto p
                        JOIN categoria c ON p.CategoriaId = c.Id ";


                    // Prepara o comando SQL para execuçao no banco (using garante limpeza automatica da memoria)
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {

                        // Executa a consulta e retornar um leitor que percorre os resultados
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            _logger.LogInformation("Query executada");///////////////////

                            // Enquanto houver linhas pra ler no resultado
                            int contador = 0;////////////////////////////
                            while (reader.Read())
                            {
                                contador++;
                                _logger.LogInformation($"Lendo linha {contador}");//////////////////////

                                // Cria um novo objeto Produto
                                produto = new Produto();

                                produto.Categoria = new Categoria();

                                produto.Id = reader.GetInt32("Id");
                                produto.Nome = reader.GetString("Nome");
                                produto.Preco = reader.GetDecimal("Preco");
                                produto.Quantidade = reader.GetInt32("Quantidade");
                                produto.Categoria.Nome = reader.GetString("NomeCategoria");
                                

                                listaP.Add(produto);
                            }
                            _logger.LogInformation($"Total de produtos lidos: {contador}");//////////////////
                            _logger.LogInformation($"Total na lista: {listaP.Count}");/////////////////////
                        }
                    }
                }
                _logger.LogInformation($"Retornando {listaP.Count} produtos");////////////////////////
                return listaP;
            }

            // Captura o erro veio do banco de dados
            catch (MySqlException ex) // ex é o objeto que contem os detalhes do erro como mensagem, codigo, e stack trace
            {
                // Registra no log que houve um erro ao buscar produtos
                _logger.LogError(ex, "Erro ao inserir produto no banco de dados");

                throw; // Joga o erro de novo pra cima, para que outra parte do sistema também possa tratar se precisar
            }

            catch (Exception ex) // Captura qualquer outro erro que nao seja do banco de dados
            {
                // Registra no log que houve um erro inesperado, com todos os detalhes
                _logger.LogError(ex, "Erro inesperado ao inserir produto.");
                _logger.LogError(ex, "ERRO COMPLETO");/////////////////////

                throw; // Joga o erro de novo para cima, igual antes, para nao "engolir" o erro
            }
        }



        // METODO PARA LER PELO ID UM PRODUTO NO BANCO DE DADOS E RETORNA-LO
        public Produto BuscarProdutoId(int id)
        {
            try
            {
                // Cria um novo objeto DTO para imprimir de maneira organizada
                Produto produtoEncontrado = null;


                // Obtem uma linha de conexao definida no appsetings.json
                string conexao = _configuration.GetConnectionString("ConexaoPadrao");

                // Cria um objeto de conexao com o banco usando a string acima
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    // Abre a conexao com banco
                    conn.Open();

                    // Comando SQL para buscar o produto usando JOIN
                    string sql = @"
                        SELECT
                            p.Id,
                            p.Nome,
                            p.Preco,
                            p.CategoriaId,
                            c.Nome AS CategoriaNome,
                            p.Quantidade
                        FROM produtos p
                        JOIN categoria c ON p.CategoriaId = c.Id
                        WHERE p.Id = @Id";

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
                                // Instancia um produto para a impressao organizada
                                produtoEncontrado = new Produto();

                                produtoEncontrado.Id = reader.GetInt32("Id");
                                produtoEncontrado.Nome = reader.GetString("Nome");
                                produtoEncontrado.Preco = reader.GetDecimal("Preco");
                                produtoEncontrado.Categoria.Nome = reader.GetString("CategoriaNome");
                                produtoEncontrado.Quantidade = reader.GetInt32("Quantidade");
                            }
                        }
                    }
                }
                // Retorna o produto encontrado (ou null se nao existir)
                return produtoEncontrado;
            }

            catch (MySqlException ex) // Se o erro veio do banco de dados
            {
                _logger.LogError(ex, "Erro ao listar o produto.");
                throw; // Joga o erro para cima
            }

            catch (Exception ex) // Se o erro veio de outro lugar
            {
                _logger.LogError(ex, "Erro inesperado ao listar o produto.");
                throw; // Joga o erro para cima
            }
        }



        // METODO PRA INSERIR UM PRODUTO NO BANCO DE DADOS
        public void InserirProduto(Produto novoProduto)
        {
            try // Tenta rodar o codigo normalmente
            {
                // Obtem uma linha de conexao definida no appsetings.json
                string conexao = _configuration.GetConnectionString("ConexaoPadrao");

                // Cria uma conexao MySQL usando a string acima
                // O 'using' garante que a conexao será encerrada e descartada mesmo se houver erro
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    // Abre a conexão com o banco
                    conn.Open();

                    // Comando SQL INSERT com parametros pra evitar SQL Injection
                    string sql = @"
                        INSERT INTO produtos (Nome, Preco, CategoriaId, Quantidade)
                        VALUES (@Nome, @Preco, @CategoriaId, @Quantidade)";


                    // Cria um comando SQL a partir da conexão aberta e do texto SQL
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {

                        // Adiciona o parametro Nome e define o valor vindo do objeto novoProduto
                        cmd.Parameters.AddWithValue("@Nome", novoProduto.Nome);
                        cmd.Parameters.AddWithValue("@Preco", novoProduto.Preco);
                        cmd.Parameters.AddWithValue("@CategoriaId", novoProduto.CategoriaId);
                        cmd.Parameters.AddWithValue("@Quantidade", novoProduto.Quantidade);

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
                }
            }

            catch (MySqlException ex) // Se o erro for do bando de dados
            {
                // Registra no log que houve um erro, guardando a mensagem de erro e os dados do produto
                // O "Produto" serve para mostrar o conteudo do objeto 'produto' no log, de forma detalhada
                _logger.LogError(ex, "Erro ao inserir produto no banco. Produto: {@Produto}", novoProduto);
                throw; // Joga o erro para cima
            }

            catch (Exception ex) // Se o erro foi de outro lugar
            {
                _logger.LogError(ex, "Erro inesperado ao inserir produto.");
                throw; // Joga o erro para cima
            }
        }



        // METODO PARA ATUALIZAR UM PRODUTO NO BANCO DE DADOS
        public bool AtualizarProduto(Produto produtoAtualizado)
        {
            try
            {
                // Obtem uma linha de conexao definida no appsetings.json
                string conexao = _configuration.GetConnectionString("ConexaoPadrao");

                // Variavel para armazenar o numero de linhas afetadas
                int linhasAfetadas = 0;

                // Cria uma conexao MySQL usando a string acima
                // O 'using' garante que a conexao será encerrada e descartada mesmo se houver erro
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    // Abre a conexão com o banco
                    conn.Open();

                    // Comando SQL UPDATE com parametros
                    string sql = @"
                        UPDATE
                            produtos SET
                            Nome = @Nome,
                            Preco = @Preco,
                            CategoriaId = @CategoriaId,
                            Quantidade = @Quantidade
                        WHERE Id = @Id";

                    // Cria um comando SQL vinculado a conexao aberta
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Adiciona os parametros com seus respectivos valores
                        cmd.Parameters.AddWithValue("@Id", produtoAtualizado.Id);
                        cmd.Parameters.AddWithValue("@Nome", produtoAtualizado.Nome);
                        cmd.Parameters.AddWithValue("@Preco", produtoAtualizado.Preco);
                        cmd.Parameters.AddWithValue("@CategoriaId", produtoAtualizado.CategoriaId);
                        cmd.Parameters.AddWithValue("@Quantidade", produtoAtualizado.Quantidade);

                        // Executa o comando no banco e armazena quantas linhas foram afetadas
                        linhasAfetadas = cmd.ExecuteNonQuery();
                    }
                }

                // Se ao menos uma linha foi afetada, significa que a atualizaçao ocorreu
                return linhasAfetadas > 0;
            }

            catch (MySqlException ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o produto.");
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, " Erro inesperado ao atualizar o produto.");
                throw;
            }
        }


        // METODO PARA DELETAR UM PRODUTO NO BANCO DE DADOS
        public void DeletarProduto(int id)
        {
            try
            {
                // Obtem uma linha de conexao definida no appsetings.json
                string conexao = _configuration.GetConnectionString("ConexaoPadrao");

                // Cria uma conexao MySQL usando a string acima
                // O 'using' garante que a conexao será encerrada e descartada mesmo se houver erro
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    // Abre a conexão com o banco
                    conn.Open();

                    // Comando SQL que exclui objeto se ele tiver o id igual ao id recebido
                    string sql = "DELETE FROM produtos WHERE Id = @Id";

                    // Cria um comando SQL vinculado a conexao aberta
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Adiciona o parametro @estoqueId e define seu valor
                        // Isso previne SQL Injection e garante tipagem correta
                        cmd.Parameters.AddWithValue("@id", id);

                        // Executa a instruçao DELETE e armazena quantas linhas foram afetadas
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception($"Produto com ID {id} nao encontrado");
                        }
                    }
                }
            }

            catch (MySqlException ex)
            {
                _logger.LogError(ex, "Erro ao deletar o produto.");
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao deletar o produto.");
                throw;
            }
        }





        // METODO PARA VERIFICAR DUPLICIDADE DE NOME DE PRODUTO
        public bool ExistsByName(string Nome)
        {
            try
            {
                // Obtem uma linha de conexao definida no appsetings.json
                string conexao = _configuration.GetConnectionString("ConexaoPadrao");

                // Cria uma conexao MySQL usando a string acima
                // O 'using' garante que a conexao será encerrada e descartada mesmo se houver erro
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    // Abre a conexão com o banco
                    conn.Open();

                    // Comando SQL para contar quantos registros tem o nome informado
                    string sql = "SELECT COUNT(*) FROM produto WHERE nome = @nome";

                    // Cria um comando SQL vinculado a conexao aberta
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Adiciona o parametro @nome e define seu valor
                        // Isso previne SQL Injection e garante tipagem correta
                        cmd.Parameters.AddWithValue("@Nome", Nome);

                        // Executa o comando e retorna o primeiro valor da lista
                        // (nesse caso, o COUNT(*)) retrona um objeto, e depois converte para int
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        // Retorna true se existir ao menos 1 registro com esse nome
                        return count > 0;
                    }
                }
            }

            catch (MySqlException ex)
            {
                _logger.LogError(ex, "Erro ao verificar o nome.");
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao verificar o nome.");
                throw;
            }
        }        
    }
}
