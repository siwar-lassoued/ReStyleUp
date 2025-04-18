using ReStyleUp.DTOs.Annonce;
using ReStyleUp.Models;

namespace ReStyleUp.Services.Interfaces
{
    public interface IAnnonceService
    {
        public IEnumerable<AnnonceReadDto> GetAllAnnonces();
        public AnnonceReadDto GetAnnonceById(int id);
        public IEnumerable<AnnonceReadDto> GetAnnoncesByUtilisateurId(int utilisateurId);
        public void AddAnnonce(AnnonceCreateDto annonce);
        public void UpdateAnnonce(int id, AnnonceUpdateDto annonceUpdateDto);
        public void DeleteAnnonce(int id);
    }
}
