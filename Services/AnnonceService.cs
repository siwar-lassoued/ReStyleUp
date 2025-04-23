using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.DTOs.Annonce;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Services
{
    public class AnnonceService : IAnnonceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        // Injection de IMapper pour AutoMapper
        public AnnonceService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<AnnonceReadDto> GetAllAnnonces()
        {
            var annonces = _context.Annonces.Include(a => a.Utilisateur).ToList();
            // Mapping de la liste des annonces vers les DTOs de lecture
            return _mapper.Map<IEnumerable<AnnonceReadDto>>(annonces);
        }

        public AnnonceReadDto GetAnnonceById(int id)
        {
            var annonce = _context.Annonces.Include(a => a.Utilisateur).FirstOrDefault(a => a.Id == id);
            // Mapping de l'annonce vers le DTO de lecture
            return _mapper.Map<AnnonceReadDto>(annonce);
        }

        public IEnumerable<AnnonceReadDto> GetAnnoncesByUtilisateurId(int userId)
        {
            var annonces = _context.Annonces
                                    .Where(a => a.UtilisateurId == userId)
                                    .Include(a => a.Utilisateur)
                                    .ToList();
            // Mapping des annonces vers les DTOs de lecture
            return _mapper.Map<IEnumerable<AnnonceReadDto>>(annonces);
        }

        public int AddAnnonce(AnnonceCreateDto annonceCreateDto)
        {
            // Vérifier que l'utilisateur existe
            var userExists = _context.Utilisateurs.Any(u => u.Id == annonceCreateDto.UtilisateurId);
            if (!userExists)
            {
                throw new ArgumentException($"L'utilisateur avec l'ID {annonceCreateDto.UtilisateurId} n'existe pas");
            }

            var annonce = _mapper.Map<Annonce>(annonceCreateDto);
            _context.Annonces.Add(annonce);
            _context.SaveChanges();
            return annonce.Id;
        }


        public void UpdateAnnonce(int id, AnnonceUpdateDto annonceUpdateDto)
        {
            var annonce = _context.Annonces.FirstOrDefault(a => a.Id == id);
            if (annonce != null)
            {
                // Mapping du DTO de mise à jour vers l'entité Annonce
                _mapper.Map(annonceUpdateDto, annonce);
                _context.Annonces.Update(annonce);
                _context.SaveChanges();
            }
        }

        public void DeleteAnnonce(int id)
        {
            var annonce = _context.Annonces.Find(id);
            if (annonce != null)
            {
                _context.Annonces.Remove(annonce);
                _context.SaveChanges();
            }
        }
    }
}
