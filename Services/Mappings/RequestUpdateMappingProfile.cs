using AutoMapper;
using Mangas.Domain.Dtos;
using Mangas.Domain.Entities;
/*Parte del reto creo un nuevo archivo llamado RequestUpdateMappingProfile.cs.
Esta clase debe heredar de Profile, al igual que los otros perfiles de mapeo.

Configurar el mapeo en el nuevo perfil:

En el constructor de RequestUpdateMappingProfile, configura el mapeo de MangaUpdateDTO a Manga.
Utiliza el método ForMember() o AfterMap() para las configuraciones especiales para algunas propiedades.

*/
public class RequestUpdateMappingProfile : Profile
{
    public RequestUpdateMappingProfile()
    {
        CreateMap<MangaUpdateDTO, Manga>()
            .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => new DateTime(src.PublicationYear, 1, 1)))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}