using System;

namespace DsaJet.Api.Entities;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public List<RefreshToken> RefreshTokens {get; set;} = new();
}
