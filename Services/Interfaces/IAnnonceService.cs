using ReStyleUp.Models;
namespace ReStyleUp.Services.Interfaces
{
    public interface IAnnonceService
    {
        IEnumerable<Annonce> GetAllAnnonces();
        Annonce GetAnnonceById(int id);
        IEnumerable<Annonce> GetAnnoncesByUserId(int userId);
        IEnumerable<Annonce> GetAnnonceByUserName(string userName);
        IEnumerable<Annonce> GetAnnonceByDate(DateTime date);
        void AddAnnonce(Annonce annonce);
        void UpdateAnnonce(Annonce annonce);
        void DeleteAnnonce(int id);
    }
}
