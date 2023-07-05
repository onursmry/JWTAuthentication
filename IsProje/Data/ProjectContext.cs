using IsProje.Data.Mappings;
using IsProje.Models;
using IsProje.Repo;
using Microsoft.EntityFrameworkCore;

namespace IsProje.Data
{
    public class ProjectContext:DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options):base(options) 
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "Admin",
                    UserPassword = Method.Sha256_hash("admin"),
                    Name= "Name",
                    SurName="SurName"                    
                });
           
        }
    }
}
