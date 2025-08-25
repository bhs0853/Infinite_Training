using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Question2.Models
{
    public class MoviesContext : DbContext
    {
        public MoviesContext() : base("name = connectionstr") { }
        public DbSet<Movies> movies { get; set; }
    }
}