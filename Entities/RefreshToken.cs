using System;

namespace DsaJet.Api.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string JwtRefresher { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public User User { get; set; } = null!;
}
