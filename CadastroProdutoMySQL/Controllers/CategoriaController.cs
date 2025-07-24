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





    }
}