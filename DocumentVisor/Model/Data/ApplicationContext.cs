using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DocumentVisor.Model.Data
{
    public sealed class ApplicationContext : DbContext
    {
        #region DBSets
        public DbSet<Query> Queries { get; set; }
        public DbSet<QueryType> QueryTypes { get; set; }

        public DbSet<Theme> Themes { get; set; }
        public DbSet<QueryTheme> QueryThemes { get; set; }
        
        public DbSet<Action> Actions { get; set; }
        public DbSet<QueryAction> QueryActions { get; set; }
        
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<QueryAction> QueryPersons { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<QueryArticle> QueryArticles { get; set; }

        public DbSet<Privacy> Privacies { get; set; }
        public DbSet<Division> Divisions { get; set; }

        public DbSet<Identifier> Identifiers { get; set; }
        public DbSet<IdentifierType> IdentifierTypes { get; set; }
        public DbSet<QueryIdentifier> QueryIdentifiers { get; set; }

        #endregion

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
            modelBuilder.Entity<QueryTheme>()
                .HasKey(bc => new { bc.QueryId, bc.ThemeId });
            modelBuilder.Entity<QueryTheme>()
                .HasOne(bc => bc.Query)
                .WithMany(b => b.QueryThemes)
                .HasForeignKey(bc => bc.QueryId);
            modelBuilder.Entity<QueryTheme>()
                .HasOne(bc => bc.Theme)
                .WithMany(c => c.QueryThemes)
                .HasForeignKey(bc => bc.ThemeId);
            modelBuilder.Entity<QueryAction>()
                .HasKey(bc => new { bc.QueryId, bc.ActionId });
            modelBuilder.Entity<QueryAction>()
                .HasOne(bc => bc.Query)
                .WithMany(b => b.QueryActions)
                .HasForeignKey(bc => bc.QueryId);
            modelBuilder.Entity<QueryAction>()
                .HasOne(bc => bc.Action)
                .WithMany(c => c.QueryActions)
                .HasForeignKey(bc => bc.ActionId);
            modelBuilder.Entity<QueryPerson>()
                .HasKey(bc => new { bc.QueryId, bc.PersonId });
            modelBuilder.Entity<QueryPerson>()
                .HasOne(bc => bc.Query)
                .WithMany(b => b.QueryPersons)
                .HasForeignKey(bc => bc.QueryId);
            modelBuilder.Entity<QueryPerson>()
                .HasOne(bc => bc.Person)
                .WithMany(c => c.QueryPersons)
                .HasForeignKey(bc => bc.PersonId);
            modelBuilder.Entity<QueryArticle>()
                .HasKey(bc => new { bc.QueryId, bc.ArticleId });
            modelBuilder.Entity<QueryArticle>()
                .HasOne(bc => bc.Query)
                .WithMany(b => b.QueryArticles)
                .HasForeignKey(bc => bc.QueryId);
            modelBuilder.Entity<QueryArticle>()
                .HasOne(bc => bc.Article)
                .WithMany(c => c.QueryArticles)
                .HasForeignKey(bc => bc.ArticleId);
            modelBuilder.Entity<QueryIdentifier>()
                .HasKey(bc => new { bc.QueryId, bc.IdentifierId });
            modelBuilder.Entity<QueryIdentifier>()
                .HasOne(bc => bc.Query)
                .WithMany(b => b.QueryIdentifiers)
                .HasForeignKey(bc => bc.QueryId);
            modelBuilder.Entity<QueryIdentifier>()
                .HasOne(bc => bc.Identifier)
                .WithMany(c => c.QueryIdentifiers)
                .HasForeignKey(bc => bc.IdentifierId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned())
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}