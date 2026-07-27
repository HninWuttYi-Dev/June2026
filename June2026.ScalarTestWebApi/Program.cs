using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// 1. Register Swashbuckle generator services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // 2. Serves the raw JSON file at /swagger/v1/swagger.json
    app.UseSwagger();

    // 3. Serves the Scalar UI at /scalar
    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/swagger/v1/swagger.json"); // Points to Swashbuckle JSON
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
