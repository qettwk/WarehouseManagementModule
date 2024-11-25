using Application;
using Application.MappingProfiles;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.OpenApi.Models;
using Serilog;


namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Serilog.Core.Logger serilogLogger = new LoggerConfiguration()
             .WriteTo.Console()
             .CreateLogger();

            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddSerilog(serilogLogger);

            Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger("StartupLogger");

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                serilogLogger.Information("Запуск приложения");

                builder.Services.AddInfrastucture(builder.Configuration);
                builder.Services.AddSerilog((services, logConf) =>
                {
                    logConf.ReadFrom.Configuration(builder.Configuration);
                    logConf.ReadFrom.Services(services);
                    logConf.Enrich.FromLogContext();
                });

                // Add some services to the container

                builder.Services.AddControllers();

                builder.Services.AddEndpointsApiExplorer();

                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Warehouse Managment", Version = "v1" });
                });

                builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
                builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

                builder.Services.AddApplication(builder.Configuration);

                builder.Services.AddAutoMapper(typeof(WarehouseMappingProfile));

                builder.Services.AddHttpClient();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse Managment");
                    });
                }
                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                serilogLogger.Information(ex, "Ошибка запуска приложения");
            }
        }
    }
}