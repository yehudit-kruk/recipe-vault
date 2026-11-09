var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // OpenAPI document at /openapi/v1.json, Swagger UI at /swagger
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "RecipeVault API v1");
    });
}

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
