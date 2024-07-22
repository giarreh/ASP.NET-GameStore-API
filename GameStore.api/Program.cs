using GameStore.api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
app.MapGet("/games", () => games);

app.MapGet("/", () => "Hello World!");
app.Run();
