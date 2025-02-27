using FluentValidation;
using loja_api.Context;
using loja_api.EndpointsHandlers;
using loja_api.Mapper.Cupom;
using loja_api.Mapper.Emploree;
using loja_api.Mapper.Storage;
using loja_api.Mapper.User;
using loja_api.Services;
using loja_api.Validators.Cupom;
using loja_api.Validators.Employee;
using loja_api.Validators.Storage;
using loja_api.Validators.User;
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
builder.Services.AddScoped<IValidator<CreateUserDTO>, CreateUserValidation>();
builder.Services.AddScoped<IValidator<UserUpdateDTO>, UpdateUserValidation>();
builder.Services.AddScoped<IValidator<EmployeeCreateDTO>, CreateEmployeeValidation>();
builder.Services.AddScoped<IValidator<EmployeeUpdateDTO>, UpdateEmployeeValidation>();

builder.Services.AddScoped<CupomService>();
builder.Services.AddScoped<HashService>();
builder.Services.AddScoped<StorageServices>(); 
builder.Services.AddScoped<UserServices>(); 

var app = builder.Build();

//Declarando Endpoints 
app.RegisterCupomEndPoint();
app.RegisterStorageEndPoints();
app.RegisterUserEndPoints();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

