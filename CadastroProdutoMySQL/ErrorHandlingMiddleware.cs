using Microsoft.AspNetCore.Http; // Fornece classes para lidar com requisiçoes e resposta http
using Microsoft.Extensions.Logging; // Fornece a interface de logging (para registrar mensagens de log 
using System; // Classes básicas do .NET (exception, etc.)
using System.Net; // Para usar códigos de status HTTP (ex.: 200, 404, 500)
using System.Text.Json; // Para transformar objetos em JSON

namespace CadastroProdutoMySQL
    
{
    public class ErrorHandlingMiddleware
    {
        // Variável para guardar a referência do proximo middleware da pipeline
        private readonly RequestDelegate _next;

        // Variavel para fazer logging (registrar erros, avisos, informaçoes)
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        // Constutor que recebe o proximo middleware e o logger por injeçao de dependência
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next; // Guarda a referencia do proximo middleware
            _logger = logger; // Guarda a ferramenta de logging
        }

        // Metodo principal que o ASP.NET Core executa para processar a requisiçao
        public void Invoke(HttpContext context)
        {
            try
            {
                // Chama o próximo middleware na pipeline (síncrono)
                _next(context).Wait();
            }

            catch (AggregateException aggEx) // Quando usa .Wait(), erros vem agrupados
            {
                // Pega o erro real dentro do AggregateException
                var ex = aggEx.InnerException ?? aggEx;
                _logger.LogError(ex, "Erro nao tratado.");
                HandleException(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro nao tratado.");
                HandleException(context, ex);
            }

            PrivateKeyFactory void 
            }
        }
}
// Importa as bibliotecas necessárias
using Microsoft.AspNetCore.Http; // Fornece classes para lidar com requisições e respostas HTTP
using Microsoft.Extensions.Logging; // Fornece a interface de logging (para registrar mensagens de log)
using System; // Classes básicas do .NET (Exception, etc.)
using System.Net; // Para usar códigos de status HTTP (ex.: 200, 404, 500)
using System.Text.Json; // Para transformar objetos em JSO

public class ErrorHandlingMiddleware
{
    // Variável para guardar a referência do próximo middleware da pipeline
    private readonly RequestDelegate _next; // Delegate

    // Variável para fazer logging (registrar erros, avisos, informações)
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    // Construtor: recebe o próximo middleware e o logger por injeção de dependência
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next; // Guarda a referência do próximo middleware
        _logger = logger; // Guarda a ferramenta de logging
    }

    // Método principal que o ASP.NET Core executa para processar a requisição
    public void Invoke(HttpContext context)
    {
        try
        {
            // Continua para o próximo middleware na pipeline
            // Usamos .Wait() pois o código do projeto é síncrono
            _next(context).Wait();
        }
        catch (AggregateException aggEx) // Erro que acontece quando usamos .Wait() e a Task dá falha
        {
            // Pega o erro "de verdade" que está dentro do AggregateException
            var ex = aggEx.InnerException ?? aggEx;

            // Registra no log que houve um erro não tratado
            // "ex" guarda todos os detalhes (mensagem, pilha de chamadas, etc.)
            _logger.LogError(ex, "Erro não tratado.");

            // Chama o método para criar a resposta de erro para o cliente
            HandleException(context, ex);
        }
        catch (Exception ex) // Captura qualquer outro tipo de erro
        {
            // Também registra o erro no log
            _logger.LogError(ex, "Erro não tratado.");

            // Também monta a resposta de erro para o cliente
            HandleException(context, ex);
        }
    }

    // Método que prepara e envia a resposta de erro para o cliente
    private void HandleException(HttpContext context, Exception ex)
    {
        // Define um status HTTP padrão de 500 (Erro Interno do Servidor)
        var statusCode = HttpStatusCode.InternalServerError;

        // Define uma mensagem genérica para não expor informações sensíveis
        var message = "Ocorreu um erro inesperado no servidor.";

        // Se o erro for do banco de dados (SqlException), mudamos a mensagem e o código HTTP
        if (ex is SqlException)
        {
            statusCode = HttpStatusCode.BadRequest; // 400 - Requisição inválida
            message = "Erro ao acessar o banco de dados.";
        }

        // Cria um objeto anônimo com as informações que vamos devolver em JSON
        var result = JsonSerializer.Serialize(new
        {
            statusCode = (int)statusCode, // Converte o código HTTP para número (ex.: 500)
            message, // Mensagem amigável
            details = ex.Message // Detalhes técnicos do erro (opcional em produção)
        });

        // Define que o tipo da resposta será JSON
        context.Response.ContentType = "application/json";

        // Define o código HTTP da resposta
        context.Response.StatusCode = (int)statusCode;

        // Escreve o JSON no corpo da resposta (também síncrono usando .Wait())
        context.Response.WriteAsync(result).Wait();
    }
}
}
