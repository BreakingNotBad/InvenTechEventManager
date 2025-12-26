using Contract.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Infrastructure.Repository;
using Scalar.AspNetCore;
using Service;
using Service.Contract;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(connectionString));

// DI
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddControllers();

// Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "InventechEvent API", Version = "v1" });
});

// แก้บรรทัดนี้ครับ (เดิมอาจจะมีแค่ AddControllers()) มาลบด้วยถ้าทำ DTO แล้ว
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // สั่งให้ Ignore วงจรที่ซ้ำกัน
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // ส่วนของ Swagger UI เดิม (เก็บไว้หรือลบออกก็ได้)
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });

    // แก้ไขส่วน Scalar: ระบุ Path ของ Swagger JSON ให้ชัดเจน
    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
