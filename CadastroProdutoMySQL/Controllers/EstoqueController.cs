using CadastroProdutoMySQL.DTOs;
using CadastroProdutoMySQL.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutoMySQL.Controllers
{
    [ApiController] // Diz ao ASP.NET Core que essa classe é um controlador de API
    [Route("[controller]")] // Define o nome da classe como rota

    /* Declara que essa classe herda todos os metodos e propriedades da 
    ControllerBase que é a classe base da Microsoft para controller API */
    public class EstoqueController : ControllerBase
    {   
        // Campo privado/ atribuido somente uma vez/ tipo campo / nome do campo - Instancia a classe de operaçoes do banco
        private readonly EstoqueServico _estoqueServico;

        // Metodo para injeçao de dependencia
        public EstoqueController(EstoqueServico estoqueServico)
        {
            _estoqueServico = estoqueServico;
        }









    // Metodo que recebe um novo cadastro de estoque
    [HttpPost] // Diz aos ASP.NET Core que esse metodo responde a requisiçoes POST - CADASTRA
        public IActionResult Post([FromBody] EstoqueCriacaoDTO estoqueDTO)
        {
            // Verifica se os dados enviados passaram nas avaliaçoes da classe ProdutoCriacaoDTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna BadRequest 400 com detalhes dos erros
            }

            // Chama o metodo que faz o cadastro dos novos dados de estoque
            EstoqueRespostaDTO resp = _estoqueServico.CadastraEstoque(estoqueDTO);

            // Facilita a vida do FRONTEND,
            // retornar 201 Created com URL do nvo recurso.
            return StatusCode(201, resp);
        }
    }
}
