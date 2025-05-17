using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Adicione este using

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MeuContexto>(options =>
    options.UseSqlite("Data Source=meubanco.db"));

// Adiciona serviços ao contêiner
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

    // Adiciona comentários XML (opcional)
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MinhaPoliticaDeCors", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configura o pipeline de requisições HTTP
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");

    // Para acessar na raiz (opcional)
    c.RoutePrefix = string.Empty;

    // Configurações opcionais de UI
    c.DocumentTitle = "Documentação da API";
    c.DisplayRequestDuration();
});

// Habilite o Swagger também em produção se desejar
// if (app.Environment.IsDevelopment())
// {
// }

app.UseHttpsRedirection();
app.UseCors("MinhaPoliticaDeCors");
app.UseAuthorization();
app.MapControllers();
app.Run();