﻿using Microsoft.EntityFrameworkCore;
using RecipeBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data.Context
{
    public class Ctx : DbContext, ICtx
    {
        public Ctx(DbContextOptions<Ctx> options) :
            base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Recipe> Recipes { get; set;}
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Quantity> Quantity { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellation);
        }

    }
}
