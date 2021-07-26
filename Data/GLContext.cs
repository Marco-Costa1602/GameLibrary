using GameLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Data
{
    public class GLContext : IdentityDbContext<Client>
    {
        public GLContext(DbContextOptions<GLContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
