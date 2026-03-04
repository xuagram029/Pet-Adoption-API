using Microsoft.EntityFrameworkCore;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Shelter)
                .WithMany(s => s.Pets)
                .HasForeignKey(p => p.ShelterId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(a => a.Owner)
                .WithMany(o => o.Applications)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(a => a.Pet)
                .WithMany() 
                .HasForeignKey(a => a.PetId);
        }
    }
}
