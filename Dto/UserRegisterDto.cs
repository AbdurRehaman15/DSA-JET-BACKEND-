using System;

namespace DsaJet.Api.Dto;

public class UserRegisterDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
