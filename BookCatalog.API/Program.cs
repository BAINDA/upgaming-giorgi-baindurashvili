using BookCatalog.Application.Common;
using BookCatalog.Application.Repositories;
using BookCatalog.Application.Services.Implementations;
using BookCatalog.Application.Services.Interfaces;
using BookCatalog.Infrastructure.Data;
using BookCatalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add controllers with global model validation error handling and wrapping into ApiResponse for consistency

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage ?? e.Exception?.Message ?? string.Empty)
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();

            var apiResponse = ApiResponse<string>.Fail(errors, 400, "Validation failed.");
            return new BadRequestObjectResult(apiResponse);
        };
    });

// adding in memory database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("BookCatalogDb"));

// adding swagger for API documentation and testing endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection for repositories and services
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();


// initialize and seed the database

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
        SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();