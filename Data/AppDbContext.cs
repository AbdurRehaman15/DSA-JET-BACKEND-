
using DsaJet.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DsaJet.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Problem> Problems { get; set; }
    public DbSet<Solution> Solutions { get; set; }
    public DbSet<Prerequisite> Prerequisites { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          modelBuilder.Entity<Problem>()
            .HasIndex(p => p.Name)
            .IsUnique();
        
         modelBuilder.Entity<Solution>()
            .HasOne(s => s.Problem)
            .WithMany(p => p.Solutions)
            .HasForeignKey(s => s.Problem_Name)  
            .HasPrincipalKey(p => p.Name)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Problem>()
            .HasMany(p => p.Prerequisites)
            .WithOne(pr => pr.Problem)
            .HasForeignKey(pr => pr.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>().HasMany(r => r.RefreshTokens).WithOne(u => u.User).HasForeignKey(r => r.UserId);
    }
}