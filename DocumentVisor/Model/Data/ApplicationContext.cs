using System;
using System.Collections.Generic;
using System.Text;
using DocumentVisor.Model;
using Microsoft.EntityFrameworkCore;

namespace DocumentVisor.Data
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

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Supervisor.db");
        }
    }
}
