using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Models;
namespace ReStyleUp.Data
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
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
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(a => a.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Commande to Utilisateur relationship (one-to-many)
            modelBuilder.Entity<Commande>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(c => c.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}