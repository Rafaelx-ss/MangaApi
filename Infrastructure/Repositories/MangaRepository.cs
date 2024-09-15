using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MangaApi.Models;

public class MangaRepository
{
    private readonly string _dataFilePath;
    private List<Manga> _mangaList = new();  
    
    public MangaRepository(IConfiguration configuration)
    {
        var dataBankPath = configuration["dataBank"];
        
        
        if (string.IsNullOrEmpty(dataBankPath))
        {
            throw new InvalidOperationException("La configuración 'dataBank' no está configurada correctamente en 'appsettings.json'.");
        }

        _dataFilePath = Path.Combine(AppContext.BaseDirectory, dataBankPath, "javerages.library.data.json");
        LoadData();
    }
    
    private void LoadData()
    {
        try
        {
            if (File.Exists(_dataFilePath))
            {
                var jsonData = File.ReadAllText(_dataFilePath);
                _mangaList = JsonConvert.DeserializeObject<List<Manga>>(jsonData) ?? new List<Manga>();
            }
            else
            {
                Console.WriteLine("Archivo JSON no encontrado en la ruta especificada.");
                _mangaList = new List<Manga>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar datos: {ex.Message}");
            _mangaList = new List<Manga>();
        }
    }

    
    private void SaveData()
    {
        var jsonData = JsonConvert.SerializeObject(_mangaList, Formatting.Indented);
        File.WriteAllText(_dataFilePath, jsonData);
    }

    public List<Manga> GetAll()
    {
        return _mangaList;
    }

    public Manga? GetById(int id)
    {
        return _mangaList.FirstOrDefault(m => m.Id == id);
    }

    public void Add(Manga manga)
    {
        manga.Id = _mangaList.Count > 0 ? _mangaList.Max(m => m.Id) + 1 : 1;
        _mangaList.Add(manga);
        SaveData();
    }

    public void Update(Manga manga)
    {
        var existingManga = GetById(manga.Id);
        if (existingManga != null)
        {
            existingManga.Title = manga.Title;
            existingManga.Author = manga.Author;
            existingManga.Genre = manga.Genre;
            existingManga.PublicationDate = manga.PublicationDate;
            existingManga.Description = manga.Description;

            SaveData();
        }
    }

    public void Delete(int id)
    {
        var mangaToDelete = GetById(id);
        if (mangaToDelete != null)
        {
            _mangaList.Remove(mangaToDelete);
            SaveData();
        }
    }
}