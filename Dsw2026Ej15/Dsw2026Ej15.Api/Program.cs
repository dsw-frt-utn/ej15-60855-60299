using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // registro de singelton 
            //var services = new ServiceCollection() ;
            builder.Services.AddSingleton <IPersistence, PersistenceInMemory> ();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //builder.Services.AddHealthChecks();
            //app.MapHealthChecks("/health-check");

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
