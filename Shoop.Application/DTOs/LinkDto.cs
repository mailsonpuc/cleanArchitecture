namespace Shoop.Application.DTOs
{
    // Classe Link pura, sem necessidade de LinkDtoId, pois o Link é sobre a AÇÃO.
    public class LinkDto
    {
        public string? Href { get; set; } // O URL para a ação
        public string? Rel { get; set; }  // O relacionamento (self, update, delete)
        public string? Metodo { get; set; } // O método HTTP (GET, PUT, DELETE)

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