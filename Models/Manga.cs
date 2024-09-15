namespace MangaApi.Models;

public class Manga
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public string Description { get; set; } = string.Empty;
}
