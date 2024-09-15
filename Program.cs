using MangaApi.Models;
using MangaApi.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MangaRepository>();

builder.Services.AddScoped<MangaService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/mangas", (MangaService mangaService) =>
{
    return Results.Ok(mangaService.GetAll());
})
.WithName("GetAllMangas")
.WithOpenApi();

app.MapGet("/mangas/{id}", (int id, MangaService mangaService) =>
{
    var manga = mangaService.GetById(id);
    return manga is not null ? Results.Ok(manga) : Results.NotFound();
})
.WithName("GetMangaById")
.WithOpenApi();

app.MapPost("/mangas", (Manga manga, MangaService mangaService) =>
{
    mangaService.Add(manga);
    return Results.Created($"/mangas/{manga.Id}", manga);
})
.WithName("AddManga")
.WithOpenApi();

app.MapPut("/mangas/{id}", (int id, Manga updatedManga, MangaService mangaService) =>
{
    var success = mangaService.Update(id, updatedManga);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("UpdateManga")
.WithOpenApi();

app.MapDelete("/mangas/{id}", (int id, MangaService mangaService) =>
{
    var success = mangaService.Delete(id);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteManga")
.WithOpenApi();

app.Run();
