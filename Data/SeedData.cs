using GameLibrary.API;
using GameLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GameLibrary.Data
{
    /// <summary>
    /// Used for initializing the context for when the application is in production
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Initializes the Seed
        /// </summary>
        /// <param name="serviceProvider">Service provider of the current context</param>
        public static void Init(IServiceProvider serviceProvider)
        {
            using (var context = new GLContext(
                serviceProvider.GetRequiredService<DbContextOptions<GLContext>>()
                )
            )
            {
                context.Database.Migrate();
                var userManager = serviceProvider.GetRequiredService<UserManager<Client>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // If a role is added to RoleTypes, but not in the context, this function adds it when the app initializes
                foreach (var role in Enum.GetNames(typeof(RoleTypes)))
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole { Name = role }).Wait();
                    }
                }

                if (!context.Games.Any())
                {
                    context.Games.Add(new Game() { Name = "Minesweeper", Description = "Classic Game", Price = 10 });
                    context.Games.Add(new Game() { Name = "Shooter game", Description = "War Experience", Price = 20 });
                    context.Games.Add(new Game() { Name = "Ultimate Fight", Description = "Brutal Fights", Price = 10 });
                    context.Games.Add(new Game() { Name = "Test Game", Description = "Testing", Price = 10 });
                    context.Games.Add(new Game() { Name = "Hide and Seek", Description = "Hunt other players, or hide!", Price = 10 });
                    context.Games.Add(new Game() { Name = "Car Soccer", Description = "Isn't that a crazy game idea?!", Price = 10 });
                }

                #region// COMMON SEED
                if (!context.Clients.Any())
                {
                    context.Clients.Add(new Client() { UserName = "Test1", Email = "test1@gmail.com", PasswordHash = "Test1!", Funds = 100 });
                    context.Clients.Add(new Client() { UserName = "Test2", Email = "test2@gmail.com", PasswordHash = "Test1!", Funds = 100 });
                    context.Clients.Add(new Client() { UserName = "Test3", Email = "test3@gmail.com", PasswordHash = "Test1!", Funds = 100 });
                }
                #endregion

                #region// ADMIN SEED
                if (!context.Clients.Any(u => u.UserName == "Admin"))
                {
                    var user = new Client() { UserName = "Admin", Email = "admin@gmail.com", PasswordHash = "Test1!", Funds = 100 };
                    userManager.CreateAsync(user, user.PasswordHash);
                    userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleTypes), 1)).Wait();
                };
                #endregion


                context.SaveChanges();
            }
        }
    }
}