using AutoMapper;
using EgressProject.API.Models.InputModel;
using EgressProject.API.Models.Utils;

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
        }
    }
}