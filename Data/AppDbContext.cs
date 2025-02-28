
using DsaJet.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DsaJet.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Problem> Problems { get; set; }
    public DbSet<Solution> Solutions { get; set; }
    public DbSet<Prerequisite> Prerequisites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Problem>().HasMany(p => p.Solutions).WithOne(s => s.Problem).HasForeignKey(s => s.ProblemId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Problem>().HasMany(p => p.Prerequisites).WithOne(pr => pr.Problem).HasForeignKey(pr => pr.ProblemId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Problem>().HasData(
        new Problem
        {
            Id = 1,
            Name = "Find the sum of two numbers",
            Description = "Given two integer numbers find there sum",
            Difficulty = "Easy",
            Tag = "Math",
            VideoSolutionUrl = "https://example.com/sum",
            Prerequisites = new() 
        },
        new Problem
        {
            Id = 2,
            Name = "Implement Binary Search",
            Description = "Given an array of size n apply the binaru search algorithm on it",
            Difficulty = "Medium",
            Tag = "Search",
            VideoSolutionUrl = "https://example.com/binary-search",
            Prerequisites = new()  
        }
    );

    modelBuilder.Entity<Prerequisite>().HasData(
        new Prerequisite
        {
            Id = 1,
            ProblemId = 2,  
            Prereq = "Sorting Algorithms"
        },
        new Prerequisite
        {
            Id = 2,
            ProblemId = 2,
            Prereq = "Arrays"
        }
    );

    modelBuilder.Entity<Solution>().HasData(
        new Solution
        {
            Id = 1,
            ProblemId = 1,  
            Language = "Python",
            SolutionCode = "def sum(a, b): return a + b"
        },
        new Solution
        {
            Id = 2,
            ProblemId = 1,
            Language = "C++",
            SolutionCode = "int sum(int a, int b) { return a + b; }"
        },
        new Solution
        {
            Id = 3,
            ProblemId = 2,  
            Language = "Java",
            SolutionCode = "int binarySearch(int[] arr, int x) { /* code */ }"
           
        }
    );
    }
}