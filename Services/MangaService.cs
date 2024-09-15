namespace MangaApi.Services;
using MangaApi.Models;
using System.Collections.Generic;

public class MangaService
{
    private readonly MangaRepository _mangaRepository;

    public MangaService(MangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public IEnumerable<Manga> GetAll() => _mangaRepository.GetAll();

    public Manga? GetById(int id) => _mangaRepository.GetById(id);

    public void Add(Manga manga) => _mangaRepository.Add(manga);

    public bool Update(int id, Manga updatedManga)
    {
        var manga = _mangaRepository.GetById(id);
        if (manga is null) return false;
        manga.Title = updatedManga.Title;
        manga.Author = updatedManga.Author;
        manga.Genre = updatedManga.Genre;
        manga.PublicationDate = updatedManga.PublicationDate;
        manga.Description = updatedManga.Description;
        _mangaRepository.Update(manga);
        return true;
    }

    public bool Delete(int id)
    {
        var manga = _mangaRepository.GetById(id);
        if (manga is null) return false;
        _mangaRepository.Delete(id);
        return true;
    }
}