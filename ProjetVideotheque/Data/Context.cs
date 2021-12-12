using Microsoft.EntityFrameworkCore;
using ProjetVideotheque.Models;

namespace ProjetVideotheque.Data
{
    public class Context : DbContext
    {

        public Context()
        {

        }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Film> Film { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<Location> Location { get; set; }
    }
}
