using ProjetoFIAP.Api.Domain.Entities;
using ProjetoFIAP.Api.Domain.Enums;
using ProjetoFIAP.Api.Infra.Utils;

namespace ProjetoFIAP.Api.Infra.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedData(AppDbContext context)
        {
            await SeedUsers(context);
            await SeedGames(context);
        }

        private static async Task SeedUsers(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var adminUser = new User(
                    name: "Administrador",
                    email: "admin@fiap.com",
                    passwordHash: PasswordHasher.HashPassword("Admin@123456"),
                    role: UserRole.Admin
                );

                var normalUser = new User(
                    name: "Usuário",
                    email: "user@fiap.com",
                    passwordHash: PasswordHasher.HashPassword("User@123456"),
                    role: UserRole.User
                );

                context.Users.AddRange(adminUser, normalUser);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedGames(AppDbContext context)
        {
            if (!context.Games.Any())
            {
                var games = new List<Game>
                {
                    new Game(
                        title: "The Legend of Zelda: Breath of the Wild",
                        description: "Um jogo de aventura épica em um vasto reino de Hyrule.",
                        price: 299.90m,
                        releaseDate: new DateTime(2017, 3, 3),
                        developer: "Nintendo EPD",
                        publisher: "Nintendo"
                    ),
                    new Game(
                        title: "God of War Ragnarök",
                        description: "Continue a jornada de Kratos e Atreus pelos Nove Reinos.",
                        price: 349.90m,
                        releaseDate: new DateTime(2022, 11, 9),
                        developer: "Santa Monica Studio",
                        publisher: "Sony Interactive Entertainment"
                    ),
                    new Game(
                        title: "Red Dead Redemption 2",
                        description: "Uma epopeia do Velho Oeste sobre lealdade e sobrevivência.",
                        price: 249.90m,
                        releaseDate: new DateTime(2018, 10, 26),
                        developer: "Rockstar Games",
                        publisher: "Rockstar Games"
                    ),
                    new Game(
                        title: "Cyberpunk 2077",
                        description: "RPG de ação em um futuro distópico e tecnológico.",
                        price: 199.90m,
                        releaseDate: new DateTime(2020, 12, 10),
                        developer: "CD Projekt Red",
                        publisher: "CD Projekt"
                    ),
                    new Game(
                        title: "Elden Ring",
                        description: "Um RPG de ação em um vasto mundo criado por FromSoftware e George R. R. Martin.",
                        price: 299.90m,
                        releaseDate: new DateTime(2022, 2, 25),
                        developer: "FromSoftware",
                        publisher: "Bandai Namco"
                    )
                };

                context.Games.AddRange(games);
                await context.SaveChangesAsync();
            }
        }
    }
}