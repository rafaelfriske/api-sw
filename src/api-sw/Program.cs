using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<MeuContexto>(options =>
    options.UseSqlite("Data Source=meubanco.db"));

// Configuração dos serviços
builder.Services.AddControllers();

// Configuração avançada do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minha API",
        Version = "v1",
        Description = "API para gerenciamento de status",
        Contact = new OpenApiContact
        {
            Name = "Seu Nome",
            Email = "seu@email.com"
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Configuração do CORS - versão mais segura para desenvolvimento
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();

// Pipeline de requisições HTTP
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
    c.RoutePrefix = string.Empty;
    c.DocumentTitle = "Documentação da API";
    c.DisplayRequestDuration();
});

app.UseHttpsRedirection();

// IMPORTANTE: UseCors deve vir antes de UseAuthorization e MapControllers
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
app.MapControllers();

app.Run();