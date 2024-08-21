using LabCourse.DbContexts;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using LabCourse.Validations;
using Serilog;
using LabCourse.Mapping;
using LabCourse.Interfaces;
using LabCourse.Repositories;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/StocksLogger.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();


// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StokuValidator>());

//Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


//DbContext
builder.Services.AddDbContext<StokuDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LabCourseDbString"))
);

//Repositories
builder.Services.AddScoped<IStokuRepository, StokuRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

