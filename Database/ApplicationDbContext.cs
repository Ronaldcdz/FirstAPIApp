using Database.Common;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added: 
                        entry.Entity.CreatedAt= DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                        
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API

            #region tables

            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Author>().ToTable("Authors");
            modelBuilder.Entity<Category>().ToTable("Categories");

            #endregion


            #region "primary keys"

            modelBuilder.Entity<Book>()
                .HasKey(book => book.Id);

            modelBuilder.Entity<Author>()
               .HasKey(author => author.Id);

            modelBuilder.Entity<Category>()
               .HasKey(category=> category.Id);


            #endregion


            #region relationships

            modelBuilder.Entity<Category>()
                .HasMany(category => category.Books)
                .WithOne(book => book.Category)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>()
                .HasMany(author => author.Books)
                .WithOne(book =>book.Author)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }

    }
}
