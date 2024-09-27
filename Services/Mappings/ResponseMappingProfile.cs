using AutoMapper;
using Mangas.Domain.Dtos;
using Mangas.Domain.Entities;
using Mangas.Domain.Entities.Dtos;

namespace Mangas.Services.MappingsM;

public class ResponseMappingProfile : Profile
{
    public ResponseMappingProfile(){
        CreateMap<Manga,MangaDTO>()
        .ForMember(
            dest => dest.PublicationYear,
            opt => opt.MapFrom(src => src.PublicationDate.Date.Year)
        );


    }

}