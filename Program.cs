using DsaJet.Api.Data;
using DsaJet.Api.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultString"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultString"))
    ));


var app = builder.Build();

app.MapGet("/GetAllProblems", async (AppDbContext db) =>
{
    var problems = await db.Problems
        .Include(p => p.Prerequisites)
        .Select(d => new GetProblemsDto
        {
            Name = d.Name,
            Description = d.Description,
            Difficulty = d.Difficulty,
            Tag = d.Tag,
        })
        .ToListAsync();

    return Results.Ok(problems);
});

app.MapGet("/GetProblemsByTag/{tag}", async (AppDbContext db, string tag) => {
    var problems = await db.Problems.Where(p => p.Tag == tag).Include(s => s.Solutions).Include(pr => pr.Prerequisites).Select(d => new GetProblemsDto {
          Name = d.Name,
          Description = d.Description,
          Difficulty = d.Difficulty,
          Tag = d.Tag,
    }).ToListAsync();

    return Results.Ok(problems);
});

app.MapGet("GetProblemByName/{Name}", async (AppDbContext db, string Name) => {
    
    var problem = await db.Problems.Where(p => p.Name == Name).Select(d => new GetProblemDto {
        Name = d.Name,
        Description = d.Description,
        Difficulty = d.Difficulty,
        Tag = d.Tag,
        VideoSolutionUrl = d.VideoSolutionUrl,
        Solutions = d.Solutions.Select(s => s.SolutionCode).ToList(),
        Prerequisites = d.Prerequisites.Select(pr => pr.Prereq).ToList()
    }).ToListAsync();

    return problem.Count == 0? Results.NotFound(new {message = "No problem with that name exists"}) : Results.Ok(problem);
 
});
app.Run();
