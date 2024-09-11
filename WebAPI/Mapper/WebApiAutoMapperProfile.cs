using AutoMapper;
using Azure;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.Dtos;

namespace WebAPI.Mapper
{
    public class WebApiAutoMapperProfile : Profile
    {
        public WebApiAutoMapperProfile()
        {
            CreateMap<Song, SongDto>();
            CreateMap<SongDto, Song>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Performer, PerformerDto>();
            CreateMap<PerformerDto, Performer>();
        }
    }
}
