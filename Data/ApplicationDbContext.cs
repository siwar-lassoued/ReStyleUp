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

            // Configure Annonce to Utilisateur relationship (one-to-many)
            modelBuilder.Entity<Annonce>()
                .HasOne(a => a.Utilisateur)
                .WithMany(u => u.Annonces)  // This assumes Utilisateur has an Annonces collection property
                .HasForeignKey(a => a.UtilisateurId);

            // Configure Commande to Utilisateur relationship (one-to-many)
            modelBuilder.Entity<Commande>()
                .HasOne(c => c.Utilisateur)
                .WithMany(u => u.Commandes)  // This assumes Utilisateur has a Commandes collection property
                .HasForeignKey(c => c.UtilisateurId);
        }
    }
}