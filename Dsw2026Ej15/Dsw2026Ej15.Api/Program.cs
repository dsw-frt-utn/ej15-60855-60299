using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Api.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Dsw2026Ej15;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=true";

            // Add services to the container.
            builder.Services.AddDbContext<Dsw2026Ej15DbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks();

            // Aca cambia a Scoped y usa Entity Framework
            builder.Services.AddScoped<IPersistence, PersistenceEF>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHealthChecks("/health-check");

            app.Run();
        }
    }
}