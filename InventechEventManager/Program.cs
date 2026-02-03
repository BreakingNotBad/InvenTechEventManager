using System.Text.Json.Serialization;
using Contracts.IRepository.BaseManager;
using FluentValidation;
using InventechEventManager.Exceptions;
using Microsoft.EntityFrameworkCore;
using Presentation.Validators;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Scalar.AspNetCore;
using Service.Contracts.IService;
using Service.Contracts.Manager;
using Service.Manager;
using Service.Service;
using Service.Validators.Company;
using Service.Validators.Event;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString));

// DI
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IFileService, FileService>();

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Service.Profiles.CompanyProfile));
    cfg.AddMaps(typeof(Presentation.Profiles.StaffProfile));
});

// Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateCompanyValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStaffRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEventValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEventValidator>();

// ช่วยจัดการ Format มาตรฐาน
builder.Services.AddProblemDetails();

// ลงทะเบียน Handler
builder.Services.AddExceptionHandler<ValidationExceptionHandler>(); // 1. เช็ค Validation (400)
builder.Services.AddExceptionHandler<DomainExceptionHandler>(); // 2. เช็ค Business/Not Found (404)
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // 3. ถ้าไม่เข้าพวกเลย ให้เป็น 500

builder.Services.AddControllers();

// Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "InventechEvent API", Version = "v1" });
});

// แก้บรรทัดนี้ครับ (เดิมอาจจะมีแค่ AddControllers()) มาลบด้วยถ้าทำ DTO แล้ว
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // สั่งให้ Ignore วงจรที่ซ้ำกัน
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// CORS Service
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

var app = builder.Build();

app.UseExceptionHandler(opt => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });

    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
    });
}

app.UseHttpsRedirection();

// เรียกใช้งาน CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
