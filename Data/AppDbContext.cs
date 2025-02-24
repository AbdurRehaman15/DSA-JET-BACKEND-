
using DsaJet.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DsaJet.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Problem> Problems { get; set; }
    public DbSet<Solution> Solutions { get; set; }
}