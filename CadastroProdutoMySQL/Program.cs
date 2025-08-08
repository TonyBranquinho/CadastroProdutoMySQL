using CadastroProdutoMySQL.Dados;
using CadastroProdutoMySQL.Servicos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ProdutoServico>();
builder.Services.AddScoped<RepositoryProduto>();
builder.Services.AddScoped<EstoqueServico>();
builder.Services.AddScoped<RepositoryEstoque>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var op = new CadastroProdutoMySQL.Dados.RepositoryProduto(builder.Configuration);
op.ListarProdutos();



app.Run();
