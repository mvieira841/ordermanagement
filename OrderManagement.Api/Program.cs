

using OrderManagement.API.Middlewares;
using OrderManagement.Infrastructure.Persistence.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Add Application
builder.Services.AddApplication(builder.Configuration);

// Add Api
builder.Services.AddApi(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var doDataSeed = builder.Configuration.GetValue<bool>("DoDataSeed");
    if (doDataSeed)
    {
        var services = scope.ServiceProvider;
        var testUserPw = builder.Configuration.GetValue<string>("SeedUserPW");
        await SeedData.Initialize(services, testUserPw);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionFluentValidationMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }