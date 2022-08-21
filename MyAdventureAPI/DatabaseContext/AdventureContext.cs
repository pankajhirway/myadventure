using System;
using Microsoft.EntityFrameworkCore;
using MyAdventureAPI.models;

namespace MyAdventureAPI.DatabaseContext
{
        public class AdventureContext : DbContext
        {
            public AdventureContext(DbContextOptions<AdventureContext> options) : base(options)
            { }

            public DbSet<Adventure> AdventureItems { get; set; }
            
        }
}

