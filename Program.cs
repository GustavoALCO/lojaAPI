using loja_api.Context;
using Microsoft.EntityFrameworkCore;
using SQLitePCL; // Adicione isso


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Conexão para o banco de dados 
builder.Services.AddDbContext<ContextDB>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("BdConnection"))
);


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

