using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.Modelos;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using MySqlX.XDevAPI;
using System.Runtime.Intrinsics.X86;

namespace CadastroProdutoMySQL.Controllers
{
    [ApiController] // Diz que essa classe é um controlador de API
    [Route("[controller]")] // Define o nome da classe como rota

    /* Declara que essa classe herda todos os metodos e propriedades da 
    ControllerBase que é a classe base da Microsoft para controller API */
    public class CategoriaController : ControllerBase
    {

        // Campo privado/ atribuido somente uma vez/ tipo campo / nome do campo - Instancia a classe de operaçoes do banco
        private readonly RepositoryCategoria _operacoes;


        // Construtor que inicializa a classe
        public CategoriaController()
        {
            _operacoes = new RepositoryCategoria();
        }



        // METODO PARA LER E LISTAR TODAS AS CATEGORIAS DE PRODUTOS
        [HttpGet]
        public ActionResult<List<Categoria>> GetTodasCategorias()
        {
            // Chama o metodo que faz a leitura das categorias
            List<Categoria> listarCategorias = _operacoes.ListarCategoria();

            // Retorna a lista como reposta HTTP 200 dizendo que a requisiçao foi bem sucedida
            return Ok(listarCategorias);
        }




        //METODO PARA LER E LISTAR UMA CATEGORIA ATRAVEZ DO ID
        [HttpGet("{Id}")]
        public ActionResult<Categoria> GetCategoriaId(int id)
        {
            // Chama o metodo que faz a leitura das categorias
            Categoria listarCategoriaId = _operacoes.ListarCategoriaId(id);

            // Retorna a lista como reposta HTTP 200 dizendo que a requisiçao foi bem sucedida
            return Ok(listarCategoriaId);

        }




        // METODO PARA CADASTRAR UMA NOVA CATEGORIA
        [HttpPost]
        public IActionResult InserirCategoria([FromBody] Categoria novaCategoria)
        {
            // Chama o metodo que adiciona a lista a nova categoria de produto
            _operacoes.InserirCategoria(novaCategoria);

            // Facilita a vida do FRONTEND
            return CreatedAtAction(nameof(GetTodasCategorias), new { id = novaCategoria.Id }, novaCategoria);
        }




        // METODO QUE ATUALIZA UMA CATEGORIA
        [HttpPut("{Id}")]
        public IActionResult AtualizaCategoria(int id, [FromBody] Categoria categoriaAtualizada)
        {
            // Testa se o categoria é valida
            if (categoriaAtualizada == null)
            {
                // Retorna erro 400 Bad Request se vier nulo
                return BadRequest();
            }

            // Testa se o Id da requisçao é o mesmo do 
            if (id !=  categoriaAtualizada.Id)
            {
                return BadRequest("Voce digitou um Id diferente");
            }

            // Chama o metodo que faz a atualizaçao da categoria do produto
            bool categoriaAualizada = _operacoes.AtualizaCategoriaId(categoriaAtualizada);
        
            // Verifica se a atualizaçao ocorreu ou o produto nao foi encontrado
            if (!categoriaAualizada)
            {
                return NotFound("Nao encontrei o produto");
            }
            return Ok(categoriaAualizada);
        }




        // METODO QUE DELETA UMA CATEGORIA DE PRODUTO
        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            //Verifica se o id recebido é valido
            if (id <= 0)
            {
                return BadRequest("Id invalido");
            }

            // Chama o metodo que deleta uma categoria
            bool categoriaDeletada = _operacoes.DeletarCategoriaId(id);

            // Testa se a categoria foi deletada
            if (!categoriaDeletada)
            {
                return NotFound("Nao encontrei essa categoria");
            }

            // Metodo do ASP.NET Core que retorna status HTTP 204 que indica o sucesso da requisicao
            return NoContent();
        }
    }
}