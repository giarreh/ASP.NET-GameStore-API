using GameStore.api.Dtos;

namespace GameStore.api.Endpoints;

public static class GamesEndpoints
{
  
const string GetGameEndpointName = "GetGame";

private static readonly List<GameDto> games = [
    new GameDto(1, "Super Mario Bros", "Platformer", 29.99m, new DateOnly(1985, 9, 13)),
    new GameDto(2, "The Legend of Zelda", "Action-adventure", 49.99m, new DateOnly(1986, 2, 21)),
    new GameDto(3, "Minecraft", "Sandbox", 19.99m, new DateOnly(2011, 11, 18)),
    new GameDto(4, "Among Us", "Social deduction", 4.99m, new DateOnly(2018, 6, 15)),
    new GameDto(5, "Cyberpunk 2077", "Action role-playing", 59.99m, new DateOnly(2020, 12, 10)),
    new GameDto(6, "The Witcher 3: Wild Hunt", "Action role-playing", 39.99m, new DateOnly(2015, 5, 19)),
    new GameDto(7, "Grand Theft Auto V", "Action-adventure", 29.99m, new DateOnly(2013, 9, 17)),
    new GameDto(8, "Red Dead Redemption 2", "Action-adventure", 39.99m, new DateOnly(2018, 10, 26)),
    new GameDto(9, "The Elder Scrolls V: Skyrim", "Action role-playing", 39.99m, new DateOnly(2011, 11, 11)),
    new GameDto(10, "Fortnite", "Battle royale", 0.00m, new DateOnly(2017, 7, 25))
  ];

  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("games");

    // GET /games
    group.MapGet("/", () => games);

    // GET /games/1
    group.MapGet("/{id}", (int id) => {
      GameDto? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);

    })
    .WithName(GetGameEndpointName);

    // POST /games
    group.MapPost("/", (CreateGameDto newGame ) => 
    {
      GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
      );

      games.Add(game);
      return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
    })
    .WithParameterValidation();

    // PUT /games
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => 
    {
      var index = games.FindIndex(game => game.Id == id);
      if (index == -1){
        return Results.NotFound();
      }

      games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
      );
      
      return Results.NoContent();
    });

    // DELETE /games/1
    group.MapDelete("/{id}", (int id) => 
    {
      var index = games.FindIndex(game => game.Id == id);
      Console.WriteLine($"Found {games[index].Name} and removed it.");
      games.RemoveAt(index);
      return Results.NoContent();
    });

    return group;
  }
}
