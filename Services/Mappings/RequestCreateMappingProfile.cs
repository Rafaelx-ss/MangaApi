using AutoMapper;
using Mangas.Domain.Dtos;
using Mangas.Domain.Entities;

namespace Mangas.Services.MappingsM;

public class RequestCreateMappingProfile : Profile
{
    public RequestCreateMappingProfile(){

        CreateMap<MangaCreateDTO,Manga>()
        .AfterMap

        (
            (src ,dest) =>{
                dest.PublicationDate = DateTime.Now;
            }


        );




    }

}