using System.Text.Json;
using System.Text.Json.Serialization;
using Mangas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Mangas.Infraestructure.Repositories;

public class MangaRepository
{
    private List<Manga> _mangas1;

    private string _filePath;

    public MangaRepository(IConfiguration configuration){
        _filePath = configuration.GetValue<string>("dataBank") ?? string.Empty;
        _mangas1 = LoadData();
    }

    private string GetCurrentFilePath(){
        var CurrentDirectory = Directory.GetCurrentDirectory();
        var currentFilePath = Path.Combine(CurrentDirectory,_filePath);

        return currentFilePath;
    }

    private List<Manga> LoadData(){
        var currentFilePath = GetCurrentFilePath();

        if(File.Exists(currentFilePath)){
            var jsonData = File.ReadAllText(currentFilePath);
            return JsonSerializer.Deserialize<List<Manga>>(jsonData);
        }

        return new List<Manga>();
    }

    public IEnumerable<Manga> GetAll(){
        return _mangas1;
    }

    public Manga GetById(int id){
        return _mangas1.FirstOrDefault(manga => manga.Id == id)
        ?? new Manga{
            Title = string.Empty,
            Author = string.Empty
        };
    }


    public void Add(Manga manga){
        var currentFilePath = GetCurrentFilePath();
        if(!File.Exists(currentFilePath))
        return;

        _mangas1.Add(manga);
        File.WriteAllText(_filePath,JsonSerializer.Serialize(_mangas1));
    }

    public void Update(Manga updateManga){
        var currentFilePath = GetCurrentFilePath();
        if(!File.Exists(currentFilePath))
        return;
        

        var index = _mangas1.FindIndex(m => m.Id == updateManga.Id);

        if(index != 1){
            _mangas1[index] = updateManga;
            File.WriteAllText(_filePath,JsonSerializer.Serialize(_mangas1));
        }
    }


    public void Delete(int id){
        var currentFilePath = GetCurrentFilePath();
        if(!File.Exists(currentFilePath))
        return;

        _mangas1.RemoveAll(m => m.Id == id);
        File.WriteAllText(_filePath,JsonSerializer.Serialize(_mangas1));

    }


}