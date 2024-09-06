namespace MangaApi.Services;
using MangaApi.Models;

public class MangaService
{
    private readonly List<Manga> _mangas = new();

    public IEnumerable<Manga> GetAll() => _mangas;

    public Manga? GetById(int id) => _mangas.FirstOrDefault(m => m.Id == id);

    public void Add(Manga manga) => _mangas.Add(manga);

    public bool Update(int id, Manga updatedManga)
    {
        var manga = GetById(id);
        if (manga is null) return false;

        manga.Title = updatedManga.Title;
        manga.Author = updatedManga.Author;
        return true;
    }

    public bool Delete(int id)
    {
        var manga = GetById(id);
        if (manga is null) return false;

        _mangas.Remove(manga);
        return true;
    }
}
