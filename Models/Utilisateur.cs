using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReStyleUp.Models
{
    public class Utilisateur
    {
        [Key]
        public Guid Id { get; set; }

        // Add the LegacyId property for the old integer ID system
        public int? LegacyId { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string MotDePasse { get; set; }

        public string Adresse { get; set; }

        public string Telephone { get; set; }

        // Navigation properties
        public ICollection<Commande> Commandes { get; set; }
        public ICollection<Annonce> Annonces { get; set; }
    }
}