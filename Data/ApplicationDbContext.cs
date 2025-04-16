using Microsoft.EntityFrameworkCore;
using ReStyleUp.Models;

namespace ReStyleUp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Annonce> Annonces { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des relations plusieurs-à-plusieurs entre Commande et Article
            modelBuilder.Entity<Commande>()
                .HasMany(c => c.Articles)
                .WithMany(a => a.Commandes);
        }
    }
}
