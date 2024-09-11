using Administrator.ViewModels;
using AutoMapper;
using DAL.Models;

namespace Administrator.Mapper
{
    public class AdministratorAutoMapperProfile : Profile
    {
        public AdministratorAutoMapperProfile()
        {
            CreateMap<Song, VMSong>();
            CreateMap<VMSong, Song>();
            CreateMap<Performer, VMPerformer>();
            CreateMap<VMPerformer, Performer>();
            CreateMap<Genre, VMGenre>();
            CreateMap<VMGenre, Genre>();
            CreateMap<User, VMUser>();
            CreateMap<VMUser, User>();
        }
    }
}
