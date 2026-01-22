using CadastroProdutoMySQL;
using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.Servicos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
builder.Services.AddScoped<EstoqueServico>();
builder.Services.AddScoped<RepositoryEstoque>();
//builder.Services.AddScoped<CategoriaServico>();
builder.Services.AddScoped<RepositoryCategoria>();

builder.Services.AddSingleton(builder.Configuration.GetConnectionString("ConexaoPadrao"));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("PermitirFrontEnd");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



//op.ListarProdutos();



app.Run();
