namespace ProjetoFIAP.Api.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public string Developer { get; private set; } = string.Empty;
        public string Publisher { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        protected Game() { } // Para EF Core

        public Game(string title, string description, decimal price, 
            DateTime releaseDate, string developer, string publisher)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Price = price;
            ReleaseDate = releaseDate;
            Developer = developer;
            Publisher = publisher;
            CreatedAt = DateTime.UtcNow;
        }
    }
}