using ReStyleUp.Models;

public class Utilisateur
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MotDePasse { get; set; } = string.Empty;
    public string Adresse { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;

    // Rendez ces propriétés nullable
    public int? CommandeId { get; set; }
    public Commande? Commande { get; set; }
    public int? AnnonceId { get; set; }
    public Annonce? Annonce { get; set; }

    public ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    public ICollection<Annonce> Annonces { get; set; } = new List<Annonce>();
}