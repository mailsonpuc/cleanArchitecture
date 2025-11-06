namespace Shoop.Application.DTOs
{

    public class LinkDto
    {
        public string? Href { get; set; }
        public string? Rel { get; set; }  
        public string? Metodo { get; set; }

        public LinkDto(string href, string rel, string metodo)
        {
            Href = href;
            Rel = rel;
            Metodo = metodo;
        }
    }

    // Classe base para todos os DTOs que suportam HATEOAS
    public abstract class LinksHATEOAS
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}