using LabMvc.Data;
using LabMvc.Repository;
using LabMvc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com SQLite
builder.Services.AddDbContext<Context>(options =>
        options.UseSqlite("Data Source=database.db") // nosso querido banco local
);

// Registro dos repositórios
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<LoanRepository>();

// Registro dos serviços
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<LoanService>();
builder.Services.AddScoped<LibraryService>();

// Controllers API (Swagger) com JSON seguro
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// MVC com Views (Razor)
builder.Services.AddControllersWithViews();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Rota padrão para MVC Razor (Home Library/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Library}/{action=Index}/{id?}");

// Suporte a controllers API com rotas via atributos
app.MapControllers();

app.Run();