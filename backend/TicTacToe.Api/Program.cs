using TicTacToe.Application.Services;
using TicTacToe.Core.Interfaces;
using TicTacToe.Repository.Repositories;
using TicTacToe.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddSingleton<GameRepository>();

builder.Services.AddSingleton<ScoreboardRepository>();

builder.Services.AddSingleton<IComputerMoveService,
                              ComputerMoveService>();

builder.Services.AddSingleton<IScoreboardService,
                              ScoreboardService>();

builder.Services.AddSingleton<IGameService,
                              GameService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("Angular");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();