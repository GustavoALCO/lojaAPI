using FluentValidation;
using loja_api.Context;
using loja_api.EndpointsHandlers;
using loja_api.Mapper.Cupom;
using loja_api.Validators.Cupom;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Conexão para o banco de dados 
builder.Services.AddDbContext<ContextDB>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("BdConnection"))
);

builder.Services.AddLogging();

//necessario para o AutoMapper Funcionar
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Declarando as Validações no código principal 
builder.Services.AddScoped<IValidator<CupomCreateDTO>, CreateCupomValidation>();

var app = builder.Build();

//Declarando Endpoints 
app.RegisterCupomEndPoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

