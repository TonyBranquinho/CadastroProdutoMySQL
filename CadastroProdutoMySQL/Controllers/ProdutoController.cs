using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; // Importa atributos e tipos relacionados a API

namespace CadastroProdutoMySQL.Controllers
{
    [ApiController] // Diz que essa classe é um controlador de API
    [Route("[controller]")] // Define o nome da classe como rota

    public class ProdutoController : ControllerBase // Classe base para controlador de API
    {
        // GET: Produto
        [HttpGet]
        public ActionResult<List<Produto>> GetTodos()
        {
            // instancia OperacaoBancoDados
            OperacaoBancoDados operacao = new OperacaoBancoDados();

            // Chama o metodo que retorna os produtos
            List<Produto> lista = operacao.ListarProdutos();

            // Retorna a lista como resposta HTTP 200
            return Ok(lista);
        }
    }
}
