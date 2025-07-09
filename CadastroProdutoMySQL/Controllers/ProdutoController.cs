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

        // Campo privado/ atribuido somente uma vez/ tipo campo / nome do campo - Instancia a classe de operaçoes do banco
        private readonly OperacaoBancoDados _operacoes;


        // Construtor que inicializa a OperacaoBancoDados
        public ProdutoController()
        {
            _operacoes = new OperacaoBancoDados();
        }



        // GET: Produto
        [HttpGet]
        public ActionResult<List<Produto>> GetTodos()
        {            
            // Chama o metodo que retorna os produtos
            List<Produto> listaProdutos = _operacoes.ListarProdutos();

            // Retorna a lista como resposta HTTP 200
            return Ok(listaProdutos);
        }
    }
}
