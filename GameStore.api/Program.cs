using GameStore.api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
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

// GET /games
app.MapGet("/games", () => games);

// GET /games/1
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame ) => 
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
});

// PUT /games
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => 
{
  var index = games.FindIndex(game => game.Id == id);
  games[index] = new GameDto(
    id,
    updatedGame.Name,
    updatedGame.Genre,
    updatedGame.Price,
    updatedGame.ReleaseDate
  );
  
  return Results.NoContent();
});


app.MapGet("/", () => "Hello World!");
app.Run();
