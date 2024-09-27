using System.Diagnostics.CodeAnalysis;

namespace Mangas.Domain.Entities;

public class Manga 
{
    public int Id {get;set;}

    public required string Title {get;set;}

    public required string Author {get;set;}

    public DateTime PublicationDate {get;set;}
}