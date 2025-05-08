using Battleship.Services;
using Battleship.Enums;
using static System.Collections.Specialized.BitVector32;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<JsonPlacement>();
builder.Services.AddSingleton<RandomPlacement>();
builder.Services.AddSingleton<IPlacementStrategy>(ps =>
{
    var cfg = ps.GetRequiredService<IConfiguration>();
    var type = cfg["ShipPlacement:Method"];

    if (type == PlacemenStrategyEnum.Json.ToString())
    {
        return ps.GetRequiredService<JsonPlacement>();
    }
    else if (type == PlacemenStrategyEnum.Json.ToString())
    {
        return ps.GetRequiredService<RandomPlacement>();
    }
    else
    {
        throw new Exception("Wrong or Missing Placement Strategy from appsettings");
    }
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
