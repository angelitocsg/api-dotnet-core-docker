using System.Linq;
using DotNetCoreDocker.Context;
using DotNetCoreDocker.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreDocker.Seeders
{
    public class TodoSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Seed(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        public static void Seed(AppDbContext context)
        {
            System.Console.WriteLine("Applying migrations...");

            context.Database.Migrate();

            if (!context.Todos.Any())
            {
                System.Console.WriteLine("Creating initial data...");

                context.Todos.AddRange(
                    new Todo("Preparar transmissão", true),
                    new Todo("Criar conteúdo para live", true),
                    new Todo("Publicar código da apresentação no GitHub", false)
                );

                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Initial data already exists...");
            }
        }
    }
}