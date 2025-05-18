using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.Services.Interfaces;
using ReStyleUp.DTOs.Commande;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReStyleUp.DTOs.Annonce;

namespace ReStyleUp.Services
{
    public class CommandeService : ICommandeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<CommandeService> _logger;



        public CommandeService(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager,
            ILogger<CommandeService> logger)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;

        }

        public IEnumerable<CommandeReadDto> GetAllCommandes()
        {
            var commandes = _context.Commandes.Include(c => c.Utilisateur).ToList();
            return _mapper.Map<IEnumerable<CommandeReadDto>>(commandes);
        }

        public CommandeReadDto GetCommandeById(int id)
        {
            var commande = _context.Commandes.Include(c => c.Utilisateur).FirstOrDefault(c => c.Id == id);
            return _mapper.Map<CommandeReadDto>(commande);
        }

        public IEnumerable<CommandeReadDto> GetCommandesByUtilisateurId(string utilisateurId)
        {
            var commandes = _context.Commandes
                                    .Where(c => c.UtilisateurId == utilisateurId) // Filtrer par UtilisateurId
                                    .Include(c => c.Utilisateur)  // Inclure les informations de l'utilisateur
                                    .ToList();

            return _mapper.Map<IEnumerable<CommandeReadDto>>(commandes);
        }

        public async Task<int> AddCommande(CommandeCreateDto commandeDto)
        {
            var utilisateurId = commandeDto.UtilisateurId;
            _logger.LogInformation($"Recherche de l'utilisateur avec l'ID {utilisateurId}");

            var identityUser = await _userManager.FindByIdAsync(utilisateurId);
            if (identityUser == null)
            {
                _logger.LogWarning($"L'utilisateur avec l'ID {utilisateurId} n'existe pas dans IdentityUsers");
                throw new ArgumentException($"L'utilisateur avec l'ID {utilisateurId} n'existe pas");
            }

            _logger.LogInformation($"Utilisateur Identity trouvé: {identityUser.UserName}");

            // Récupérer les articles associés
            var articles = _context.Articles
                                   .Where(a => commandeDto.ArticlesIds.Contains(a.Id))
                                   .ToList();

            // Calculer le montant total
            float montantTotal = articles.Sum(a => a.Prix);

            var commande = _mapper.Map<Commande>(commandeDto);
            commande.MontantTotal = montantTotal; // Set the total price

            _context.Commandes.Add(commande);
            _context.SaveChanges();

            return commande.Id;
        }



        public void UpdateCommande(int id, CommandeUpdateDto commandeDto)
        {
            var commande = _context.Commandes.Find(id);
            if (commande != null)
            {
                _mapper.Map(commandeDto, commande);
                _context.Commandes.Update(commande);
                _context.SaveChanges();
            }
        }

        public void DeleteCommande(int id)
        {
            var commande = _context.Commandes.Find(id);
            if (commande != null)
            {
                _context.Commandes.Remove(commande);
                _context.SaveChanges();
            }
        }
    }
}
