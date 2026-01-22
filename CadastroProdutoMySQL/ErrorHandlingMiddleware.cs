using Microsoft.AspNetCore.Http; // Fornece classes (HttpContext, RequestDelegate, StatusCodes) para lidar com requisiçoes e resposta http
using Microsoft.Extensions.Logging; // Fornece a interface de logging Ilogger para registrar mensagens de log 
using System; // Classes básicas do .NET (exception, etc.)
using System.Net; // Para usar códigos de status HTTP (ex.: 200, 404, 500)
using System.Text.Json; // Para transformar objetos em JSON (serializaçao)

namespace CadastroProdutoMySQL

{
    public class ErrorHandlingMiddleware
    {
        // CAMPOS PRIVADOS

        private readonly RequestDelegate _next; // Campo para guardar a referência do proximo componente da pipeline

        private readonly ILogger<ErrorHandlingMiddleware> _logger; // Variavel para fazer logging (registrar erros, avisos, informaçoes)


        // Constutor que recebe o proximo middleware e o logger por injeçao de dependência
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next; // Guarda a referencia do proximo middleware
            _logger = logger; // Guarda a ferramenta de logging
        }


        // Metodo principal que o ASP.NET Core executa para processar a requisiçao
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Continua para o proximo middleware na pipeline
            }

            catch (Exception ex)
            {

                context.Response.ContentType = "application/json"; // Define que o corpo da resposta será JSON (padrão esperado para APIs REST)

                context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Defie o código HTTP da resposta como 500 (Internal Server Error)

                _logger.LogError(ex, "Erro nao tratado."); // Registra o erro nos logs (stack trace fica guardado no servidor)


                var errorResponse = new // Cria um objeto com os dados que devem retornar ao cliente
                {
                    statusCode = context.Response.StatusCode, // reutiliza o código acima
                    message = "Ocorreu um erro inesperado."   // mensagem amigável para o cliente
                };


                string result = JsonSerializer.Serialize(errorResponse); // Serializa o objeto para uma string JSON

                await context.Response.WriteAsync(result); // Escreve a string JSON no corpo da resposta de forma assíncrona

            }
        }
    }
}
