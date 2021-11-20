using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Realisateur> Realisateur { get; set; }

        public DbSet<MvcMovie.Models.Film> Film { get; set; }

        public DbSet<MvcMovie.Models.Client> Client { get; set; }

        public DbSet<MvcMovie.Models.Location> Location { get; set; }
    }
}
