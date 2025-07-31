using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.DTOs;
using CadastroProdutoMySQL.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; // Importa atributos e tipos relacionados a API

namespace CadastroProdutoMySQL.Controllers
{
    [ApiController] // Diz que essa classe é um controlador de API
    [Route("[controller]")] // Define o nome da classe como rota

    public class ProdutoController : ControllerBase // Classe base para controlador de API
    {

        // Campo privado/ atribuido somente uma vez/ tipo campo / nome do campo - Instancia a classe de operaçoes do banco
        private readonly RepositoryProduto _operacoes;

        


        // Construtor que inicializa a classe
        public ProdutoController()
        {
            _operacoes = new RepositoryProduto();
            
        }




        // METODO PARA LER E LISTAR TODOS OS PRODUTOS DO BANCO DE DADOS
        [HttpGet]
        public ActionResult<List<Produto>> GetTodos()
        {
            // Chama o metodo que retorna os produtos
            List<Produto> listaProdutos = _operacoes.ListarProdutos();

            // Retorna a lista como resposta HTTP 200
            return Ok(listaProdutos);
        }





        // METODO PARA LER E LISTAR UM PRODUTO SELECIONADO PELO ID
        [HttpGet("{id}")] // Diz aos ASP.NET Core que esse metodo responde a requisiçoes GET ID - BUSCA
        public ActionResult<Produto> GetPorId(int id)
        {
            // Busca o produto pelo ID no banco de dados
            ProdutoDetalhadoDTO produtoId = _operacoes.BuscarProdutoId(id);

            // Se nao encontrar o produto, retorna 404 Not Found
            if (produtoId == null)
            {
                return NotFound("Produto nao encontrado.");
            }

            // Retorna o produto encontrado com resposta HTTP 200
            return Ok(produtoId);
        }





        // METODO PRA RECEBER UM NOVO PRODUTO E INSERIR NO BANCO DE DADOS
        [HttpPost] // Diz aos ASP.NET Core que esse metodo responde a requisiçoes POST - CADASTRA
        public IActionResult Cadastrar([FromBody] Produto novoProduto) // Metodo que retorna o novo objeto

        {
            // Adiciona o novo produto na lista
            _operacoes.InserirProduto(novoProduto);

            // Facilita a vida do FRONTEND
            return CreatedAtAction(nameof(GetTodos), new { id = novoProduto.Id }, novoProduto);
        }





        // METODO PRA RECEBER UM PRODUTO DO BANCO E ATUALIZA-LO
        [HttpPut("{id}")] // Diz ao ASP.NET Core que esse metodo responde a requisçoes PUT com id na rota
        public IActionResult Atualizar(int id, [FromBody] Produto produtoAtualizado)
        {
            // Verifica se o objeto recebido é nulo
            if (produtoAtualizado == null)
            {
                // Retorna erro 400 Bad Request se vier nulo
                return BadRequest("Dados invalidos. ");
            }

            // Verifica se o ID passado na URL é igual ao ID do objeto recebido
            if (id != produtoAtualizado.Id)
            {
                // Retorna erro 400 Bad Request se IDs nao baterem
                return BadRequest("ID da URL diferente do ID do produto enviado.");
            }

            // Chama metodo para atualizar o produto no banco de dados
            bool atualizadoComSucesso = _operacoes.AtualizarProduto(produtoAtualizado);

            // Verifica se a atualizaçao ocorreu (ou seja, o produto existia)
            if (!atualizadoComSucesso)
            {
                // Retorna erro 404 Not Found se o produto nao existir
                return NotFound("Produt nao encontrado.");
            }

            return NoContent(); // Retorna 204 - indica atualizaçao feita com sucesso
        }






        // METODO QUE DELETA UM PRODUTO DO BANCO DE DADOS
        [HttpDelete("{id}")] // Diz ao ASP.NET Core que esse metodo responde a requisiçoes do tipo DELETE com id na roda
        public IActionResult DeletarPorId(int id)
        {
            // Verifica se o objeto recebido é nulo
            if (id == null)
            {
                // Retorna erro 400 Bad Request se vier nulo
                return BadRequest("Dados invalidos. ");
            }

            // Chama o metodo DeletarProduto
            bool deletadoComSucesso = _operacoes.DeletarProduto(id);

            // Testa se o produto foi deletado
            if (!deletadoComSucesso)
            {
                return NotFound("Nao encontrei esse produto");
            }

            // Metodo do ASP.NET Core que retorna status HTTP 204 que indica o sucesso da requisicao
            return NoContent();
        }
    }
}

