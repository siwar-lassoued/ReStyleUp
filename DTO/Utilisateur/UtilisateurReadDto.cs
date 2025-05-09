using System;
using System.Collections.Generic;

namespace ReStyleUp.DTOs.Utilisateur
{
    public class UtilisateurReadDto
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }

        // Ajoute cette propriété si tu veux inclure les commandes
        public List<int> CommandeIds { get; set; } = new List<int>();
    }
}
