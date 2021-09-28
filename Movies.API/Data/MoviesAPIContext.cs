﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.API.Models;


namespace Movies.API.Data
{
    public class MoviesApiContext : DbContext
    {
        public MoviesApiContext (DbContextOptions<MoviesApiContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
