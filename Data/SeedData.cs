using GameLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GameLibrary.Data
{
    public static class SeedData
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            using (var context = new GLContext(
                serviceProvider.GetRequiredService<DbContextOptions<GLContext>>()
                )
            )
            {
                context.Database.Migrate();

                if (!context.Games.Any())
                {
                    context.Games.Add(new Game() { Name = "Minesweeper", Description = "", Price = 10 });
                    context.Games.Add(new Game() { Name = "Shooter game", Description = "", Price = 20 });
                    context.Games.Add(new Game() { Name = "Ultimate Fight", Description = "", Price = 10 });
                }

                if (!context.Clients.Any())
                {
                    context.Clients.Add(new Client() { UserName = "Test1", Email = "test1@gmail.com", PasswordHash = "Test1!", Funds = 100 });
                    context.Clients.Add(new Client() { UserName = "Test2", Email = "test2@gmail.com", PasswordHash = "Test1!", Funds = 100 });
                    context.Clients.Add(new Client() { UserName = "Test3", Email = "test3@gmail.com", PasswordHash = "Test1!", Funds = 100 });
                }

                context.SaveChanges();
            }
        }
    }
}
