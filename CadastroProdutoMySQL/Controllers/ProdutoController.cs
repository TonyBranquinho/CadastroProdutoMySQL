using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.DTOs;
using CadastroProdutoMySQL.Modelos;
using CadastroProdutoMySQL.Servicos;
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

        private readonly ProdutoServico _produtoServico;

        public ProdutoController(ProdutoServico produtoServico, RepositoryProduto operacoes)
        {
            _produtoServico = produtoServico;
            _operacoes = operacoes;
        }

        



        // METODO PARA LER E LISTAR TODOS OS PRODUTOS DO BANCO DE DADOS
        [HttpGet]
        public ActionResult<List<ProdutoDetalhadoDTO>> GetTodos()
        {
            
            // Chama o metodo que retorna os produtos
            List<ProdutoDetalhadoDTO> listaProdutos = _produtoServico.ListarTodos();

            if (listaProdutos == null )
            {
                return NotFound();
            }

            // Retorna a lista como resposta HTTP 200
            return Ok(listaProdutos);
        }



        // METODO PARA LER E LISTAR UM PRODUTO SELECIONADO PELO ID
        [HttpGet("{id}")] // Diz aos ASP.NET Core que esse metodo responde a requisiçoes GET ID - BUSCA
        public ActionResult<ProdutoDetalhadoDTO> GetPorId(int id)
        {
            // Testa validade do ID
            if (id <= 0)
            {
                return BadRequest("ID deve ser maior que 0"); // Http 400
            }

            // Busca o produto pelo ID no banco de dados
            ProdutoDetalhadoDTO produtoId = _produtoServico.BuscaId(id);

            if (produtoId == null)
            {
                return NotFound("Produto nao encontrado."); // http 404 Not Found
            }

            // Retorna o produto encontrado com resposta HTTP 200
            return Ok(produtoId);
        }



        // METODO PRA RECEBER UM NOVO PRODUTO E INSERIR NO BANCO DE DADOS
        [HttpPost] // Diz aos ASP.NET Core que esse metodo responde a requisiçoes POST - CADASTRA
        public IActionResult Cadastrar([FromBody] ProdutoCriacaoDTO dto)
        {
            // o atributo [ApiController] ja faz a validaçao dos parametros do DTO ([Required]),

            // Chama o metodo CadastroProduto e manda os dados que foram descritos na ediçao
            ProdutoRespCriacaoDTO respostaDTO = _produtoServico.CadastroProduto(dto);
            

            // Facilita a vida do FRONTEND,
            // retornar 201 Created com URL do nvo recurso.
            return CreatedAtAction(nameof(GetTodos), new { id = respostaDTO.Id }, respostaDTO);
        }
        



        // METODO PRA RECEBER UM PRODUTO DO BANCO E ATUALIZA-LO
        [HttpPut("{id}")] // Diz ao ASP.NET Core que esse metodo responde a requisçoes PUT com id na rota
        public IActionResult Atualizar(int id, [FromBody] ProdutoCriacaoDTO produtoAtualizadoDTO)
        {
            // Valida o ID da entrada
            if (id <= 0)
            {
                return BadRequest("ID deve ser maior que zero");
            }
                        
            // o atributo [ApiController] ja faz a validaçao dos parametros do DTO ([Required]),
            

            // Chama serviço para tentar atualizar o produto no banco de dados
            ProdutoRespAtualizacaoDTO atualizado = _produtoServico.Atualiza(produtoAtualizadoDTO, id);

            if (atualizado == null)
            {
                return NotFound($"Produto com ID {id} nao encontrado"); // Retorna 404
            }

            return Ok(atualizado); // Retorna 200
        }



        // METODO QUE DELETA UM PRODUTO DO BANCO DE DADOS
        [HttpDelete("{id}")] // Diz ao ASP.NET Core que esse metodo responde a requisiçoes do tipo DELETE com id na roda
        public IActionResult DeletarPorId(int id)
        {
            // Verifica se o objeto recebido é nulo
            if (id <= 0)
            {
                // Retorna erro 400 Bad Request se vier nulo
                return BadRequest("O ID deve ser maior que 0");
            }

            // Chama o metodo DeletarProduto
            bool deletadoComSucesso = _produtoServico.ExcluirProduto(id);

            // Tipo de validaçao indicada par aum retorno booleano
            if (!deletadoComSucesso)
            {
                return NotFound("Nao encontrei esse produto"); // Http 404
            }

            // Metodo do ASP.NET Core que retorna status HTTP 204 que indica o sucesso da requisicao
            return NoContent();
        }
    }
}

