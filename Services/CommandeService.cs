using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.Services.Interfaces;
using ReStyleUp.DTOs.Commande;
using AutoMapper;

namespace ReStyleUp.Services
{
    public class CommandeService : ICommandeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CommandeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public IEnumerable<CommandeReadDto> GetCommandesByUtilisateurId(Guid utilisateurId)
        {
            var commandes = _context.Commandes
                                    .Where(c => c.UtilisateurId == utilisateurId) // Filtrer par UtilisateurId
                                    .Include(c => c.Utilisateur)  // Inclure les informations de l'utilisateur
                                    .ToList();

            return _mapper.Map<IEnumerable<CommandeReadDto>>(commandes);
        }

        public void AddCommande(CommandeCreateDto commandeDto)
        {
            // Verify user exists first
            if (!_context.Utilisateurs.Any(u => u.Id == commandeDto.UtilisateurId))
            {
                throw new ArgumentException($"User with ID {commandeDto.UtilisateurId} not found");
            }

            var commande = _mapper.Map<Commande>(commandeDto);
            _context.Commandes.Add(commande);
            _context.SaveChanges();
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
