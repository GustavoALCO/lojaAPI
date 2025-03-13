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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
     

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Loja - API",
        Version = "v1"
    });

    // 🔹 Configuração do JWT no Swagger
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Jwt Authentication",
        Description = "Entre com o JWT Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securitySchema, new string[] {} }
    });
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        //para validar o Issuer
        ValidateAudience = true,
        //para valida a audience
        ValidateLifetime = true,
        //para verefica se esta no prazo de vida
        ValidateIssuerSigningKey = true,
        //informa que ira passar uma chave secreta
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

//Conexão para o banco de dados 
builder.Services.AddDbContext<ContextDB>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("BdConnection"))
);

builder.Services.AddLogging();

//Declarando Classe que gera um token Jwt para o Usuario
builder.Services.AddScoped<GenerateToken>();

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
builder.Services.AddScoped<EmployeeService>();

var app = builder.Build();

//Declarando Endpoints 
app.RegisterCupomEndPoint();
app.RegisterStorageEndPoints();
app.RegisterUserEndPoints();
app.RegisterEmployeeEndPoints();
app.RegisterLoginEndPoints();
app.RegisterMercadoPagoEndPoints();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();