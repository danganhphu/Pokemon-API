using Microsoft.EntityFrameworkCore;
using PokemonApp.Models;

namespace PokemonApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //flutent api cho PokemonCategory
            modelBuilder.Entity<PokemonCategory>(pCategory =>
            {
                pCategory.HasKey(pc => new { pc.PokemonId, pc.CategoryId });

                pCategory.HasOne(p => p.Pokemon)
                    .WithMany(pc => pc.PokemonCategories)
                    .HasForeignKey(p => p.PokemonId);

                pCategory.HasOne(p => p.Category)
                    .WithMany(pc => pc.PokemonCategories)
                    .HasForeignKey(c => c.CategoryId);
            });

            //flutent api cho PokemonOwner
            modelBuilder.Entity<PokemonOwner>(pOwner =>
            {
                pOwner.HasKey(po => new { po.PokemonId, po.OwnerId });

                pOwner.HasOne(p => p.Pokemon)
                    .WithMany(po => po.PokemonOwners)
                    .HasForeignKey(p => p.PokemonId);

                pOwner.HasOne(o => o.Owner)
                    .WithMany(po => po.PokemonOwners)
                    .HasForeignKey(c => c.OwnerId);
            });
        }
    }
}