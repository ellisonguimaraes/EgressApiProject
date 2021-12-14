using AutoMapper;
using EgressProject.API.Models;
using EgressProject.API.Models.InputModel;
using EgressProject.API.Models.Utils;
using EgressProject.API.Models.ViewModel;

namespace EgressProject.API.Services.MapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TokenInputModel, Token>()
                .ForMember(dest => dest.AccessToken, map => map.MapFrom(src => src.AccessToken))
                .ForMember(dest => dest.RefreshToken, map => map.MapFrom(src => src.RefreshToken))
                .ReverseMap();
            
            CreateMap<RegisterInputModel, Login>()
                .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, map => map.MapFrom(src => src.Password))
                .ReverseMap();

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email))
                .ForMember(dest => dest.IsValidated, map => map.MapFrom(src => src.IsValidated))
                .ForMember(dest => dest.PersonId, map => map.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.Person, map => map.MapFrom(src => src.Person))
                .ForMember(dest => dest.JobAdvertisements, map => map.MapFrom(src => src.JobAdvertisements))
                .ForMember(dest => dest.News, map => map.MapFrom(src => src.News))
                .ForMember(dest => dest.Role, map => map.MapFrom(src => src.Role.ToString()))
                .ReverseMap();
            
            CreateMap<UserInputModel, User>()
                .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, map => map.MapFrom(src => src.Password))
                .ForMember(dest => dest.IsValidated, map => map.MapFrom(src => src.IsValidated))
                .ForMember(dest => dest.PersonId, map => map.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.Role, map => map.MapFrom(src => src.Role.ToString()))
                .ReverseMap();
        }
    }
}