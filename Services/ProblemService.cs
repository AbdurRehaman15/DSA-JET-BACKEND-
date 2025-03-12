using System;
using DsaJet.Api.Data;
using DsaJet.Api.Dto;
using Microsoft.EntityFrameworkCore;

namespace DsaJet.Api.Services;

public static class ProblemService
{
    public static void AddProblemEndpoints(this WebApplication app ){
            
        app.MapGet("/getAllProblems", async (AppDbContext db) =>
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
        }).RequireAuthorization();;

        app.MapGet("/getProblemsByTag/{tag}", async (AppDbContext db, string tag) => {
            var problems = await db.Problems
                        .Where(p => p.Tag == tag)
                        .Include(s => s.Solutions)
                        .Include(pr => pr.Prerequisites)
                        .Select(d => new GetProblemsDto 
                        {
                            Name = d.Name,
                            Description = d.Description,
                            Difficulty = d.Difficulty,
                            Tag = d.Tag,
                        }).ToListAsync();

            return Results.Ok(problems);
        }).RequireAuthorization();;

        app.MapGet("getProblemByName/{Name}", async (AppDbContext db, string Name) => {
            
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
        
        }).RequireAuthorization();;

        app.MapGet("/getProblemSolution/{Name}/{Language}", async (AppDbContext db, string Name, string Language) => {
            var solution = await db.Solutions.Where(s => s.Problem_Name == Name && s.Language == Language).Select(s => s.SolutionCode).FirstOrDefaultAsync();;

            if(solution == null){
                return Results.NotFound();
            }

            return Results.Ok(new {Solution = solution});
        }).RequireAuthorization();;
    }
}
