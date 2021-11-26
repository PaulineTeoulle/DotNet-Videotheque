using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetVideotheque.Models;

namespace ProjetVideotheque.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<ProjetVideotheque.Models.Film> Film { get; set; }

        public DbSet<ProjetVideotheque.Models.Client> Client { get; set; }

        public DbSet<ProjetVideotheque.Models.Location> Location { get; set; }
    }
}
