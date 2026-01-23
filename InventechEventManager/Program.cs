using Contracts.IRepository.BaseManager;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Scalar.AspNetCore;
using Service.Contracts.IService;
using Service.Contracts.Manager;
using Service.Manager;
using Service.Service;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Service.Validators.Company;
using Presentation.Validators.staff;
using Service.Validators.Outsource;
using Service.Validators.Equipment;
using Service.Validators.Package;

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
    cfg.AddMaps(typeof(Program));
    cfg.AddMaps(typeof(Service.Profiles.CompanyProfile));
    cfg.AddMaps(typeof(Presentation.Mapping.StaffProfile));
});

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
            policy
                .WithOrigins("http://localhost:5173") // 👈 ใส่ URL ของ Frontend (ห้ามมี / ปิดท้าย)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

// Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCompanyValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCompanyValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStaffValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateStaffValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOutsourceValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOutsourceValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEquipmentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEquipmentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePackageValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdatePackegeValidator>();

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

// เรียกใช้งาน CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
