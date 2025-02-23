using FluentValidation;
using loja_api.Context;
using loja_api.EndpointsHandlers;
using loja_api.Mapper.Cupom;
using loja_api.Mapper.Storage;
using loja_api.Validators.Cupom;
using loja_api.Validators.Storage;
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
builder.Services.AddScoped<IValidator<CupomUpdateDTO>, UpdateCupomValidation>();
builder.Services.AddScoped<IValidator<StorageCreateDTO>, CreateStorageValidation>();
builder.Services.AddScoped<IValidator<StorageUpdateDTO>, UpdateStorageValition>();

var app = builder.Build();

//Declarando Endpoints 
app.RegisterCupomEndPoint();
app.RegisterStorageEndPoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

