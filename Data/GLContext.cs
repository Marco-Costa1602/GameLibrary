using GameLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Data
{
    /// <summary>
    /// Context
    /// </summary>
    public class GLContext : IdentityDbContext<Client>
    {
        /// <summary>
        /// Context constructor
        /// </summary>
        /// <param name="options"></param>
        public GLContext(DbContextOptions<GLContext> options) : base(options) { }

        /// <summary>
        /// Set of Clients Objects
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Set of Games Objects
        /// </summary>
        public DbSet<Game> Games { get; set; }

        /// <summary>
        /// Set of Sales Objects
        /// </summary>
        public DbSet<Sale> Sales { get; set; }
    }
}
