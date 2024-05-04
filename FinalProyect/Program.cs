using Microsoft.EntityFrameworkCore;
using FinalProyect.Infrastructure;
using FinalProyect.Domain.Interface;
using FinalProyect.Infrastructure.Repositories;
using FinalProyect.Application.Services;
using FinalProyect.Middleware;
using Microsoft.Extensions.Caching.Distributed;
using FinalProyect.Infrastructure.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<AdvWorksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        opt => opt.MigrationsAssembly(typeof(AdvWorksDbContext).Assembly.FullName));
});

builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

builder.Services.AddTransient<ISalesReportRepository, SalesReportRepository>();
builder.Services.AddTransient<ISalesReportService, SalesReportService>();
builder.Services.AddTransient<IRedisCacheService, RedisCacheService>();
builder.Services.AddTransient<IGeneratePdfService, GeneratePdfService>();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "FinalProyect_";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CORSPolicy");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
