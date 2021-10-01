using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DocumentVisor.Model.Data
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Query> Queries { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Privacy> Privacies { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Article> Articles { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Supervisor.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
