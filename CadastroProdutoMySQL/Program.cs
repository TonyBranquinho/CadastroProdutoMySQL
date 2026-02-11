using CadastroProdutoMySQL;
using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.Servicos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();






builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontEnd",
        policy => policy
            .AllowAnyOrigin()      // Permite qualquer origem
            .AllowAnyMethod()      // Permite GET, POST, PUT, DELETE
            .AllowAnyHeader());    // Permite qualquer cabeçalho
});





builder.Services.AddScoped<ProdutoServico>();
builder.Services.AddScoped<RepositoryProduto>();
builder.Services.AddScoped<RepositoryCategoria>();

builder.Services.AddSingleton(builder.Configuration.GetConnectionString("ConexaoPadrao"));



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// para rodar quando o banco estiver no meu pc
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("PermitirFrontEnd");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



//op.ListarProdutos();



app.Run();
