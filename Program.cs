using Alpha.Service.Interfaces;
using Alpha.Service.Services;

using Alpha.Store.Abstraction;
using Alpha.Store.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger services
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IBookStore, BookStore>();

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

app.Run();