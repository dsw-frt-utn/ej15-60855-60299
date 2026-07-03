using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //var services = new ServiceCollection() ;
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Dsw2026Ej15DbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IPersistence, PersistenceEf>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks();
            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

           
            app.MapHealthChecks("/health-check");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
              //  app.MapOpenApi();
              app.UseSwagger();
              app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
