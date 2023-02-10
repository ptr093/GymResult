

using GymResult;
using GymResult.DbContexts;
using GymResult.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = false;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Log.Logger = new LoggerConfiguration
           ().MinimumLevel.Debug().
           WriteTo.Console()
          .WriteTo.File("logs/GymResult.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Services.AddDbContext<TrainingInfoContext>(dbContextOptions =>
{
    dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:TraingInfoDb"


        ]);
});

builder.Services.AddScoped<ITrainingInfoRepository, TrainingInfoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
