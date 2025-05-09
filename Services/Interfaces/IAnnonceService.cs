using ReStyleUp.DTOs.Annonce;
using System;
using System.Collections.Generic;

namespace ReStyleUp.Services.Interfaces
{
    public interface IAnnonceService
    {
        IEnumerable<AnnonceReadDto> GetAllAnnonces();
        AnnonceReadDto GetAnnonceById(int id);
        IEnumerable<AnnonceReadDto> GetAnnoncesByUtilisateurId(Guid utilisateurId);
        int AddAnnonce(AnnonceCreateDto annonceCreateDto);
        void UpdateAnnonce(int id, AnnonceUpdateDto annonceUpdateDto);
        void DeleteAnnonce(int id);
    }
}