using AutoMapper;
using ReStyleUp.Models;
using ReStyleUp.DTOs.Annonce;
using ReStyleUp.DTOs.Article;
using ReStyleUp.DTOs.Commande;
using ReStyleUp.DTOs.Image;
using ReStyleUp.DTOs.Utilisateur;



namespace ReStyleUp.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mappage entre Annonce et AnnonceReadDto
            CreateMap<Annonce, AnnonceReadDto>()
                .ForMember(dest => dest.UtilisateurNom, opt => opt.MapFrom(src => src.Utilisateur.Nom))
                .ForMember(dest => dest.ImagesUrls, opt => opt.MapFrom(src => src.Images.Select(i => i.Url).ToList()));

            // Mappage entre AnnonceCreateDto et Annonce
            CreateMap<AnnonceCreateDto, Annonce>()
                .ForMember(dest => dest.DatePublication, opt => opt.MapFrom(src => DateTime.Now));

            // Mappage entre AnnonceUpdateDto et Annonce
            CreateMap<AnnonceUpdateDto, Annonce>();



            CreateMap<Article, ArticleReadDto>()
     .ForMember(dest => dest.Categorie, opt => opt.MapFrom(src => src.Categorie.ToString()))
     .ForMember(dest => dest.Etat, opt => opt.MapFrom(src => src.Etat.ToString()))
     .ForMember(dest => dest.AnnonceTitre, opt => opt.MapFrom(src => src.Annonce.Titre));

            CreateMap<ArticleCreateDto, Article>()
                .ForMember(dest => dest.Categorie, opt => opt.MapFrom(src => Enum.Parse<CategorieArticle>(src.Categorie)));

            CreateMap<ArticleUpdateDto, Article>()
                .ForMember(dest => dest.Categorie, opt => opt.MapFrom(src => Enum.Parse<CategorieArticle>(src.Categorie)));





            // Mappage entre CommandeReadDto et Commande
            CreateMap<Commande, CommandeReadDto>()
                .ForMember(dest => dest.UtilisateurNom, opt => opt.MapFrom(src => src.Utilisateur.Nom))
                .ForMember(dest => dest.ArticlesIds, opt => opt.MapFrom(src => src.Articles.Select(a => a.Id).ToList()));
            
            // Mappage entre CommandeCreateDto et Commande
            CreateMap<CommandeCreateDto, Commande>();

            // Mappage entre CommandeUpdateDto et Commande
            CreateMap<CommandeUpdateDto, Commande>();



            //Mappage entre ImageReadDto et Image
            CreateMap<Image, ImageReadDto>();

            //Mappage entre ImageCreateDto et Image
            CreateMap<ImageCreateDto, Image>();

            //Mappage entre ImageUpdateDto et Image
            CreateMap<ImageUpdateDto, Image>();




            // Mappage entre Utilisateur et UtilisateurReadDto
            CreateMap<Utilisateur, UtilisateurReadDto>()
     .ForMember(dest => dest.CommandeIds,
                opt => opt.MapFrom(src => src.Commandes.Select(c => c.Id)));


            // Mappage entre UtilisateurCreateDto et Utilisateur
            CreateMap<UtilisateurCreateDto, Utilisateur>();

            // Mappage entre UtilisateurUpdateDto et Utilisateur
            CreateMap<UtilisateurUpdateDto, Utilisateur>();
        }

    }
}
