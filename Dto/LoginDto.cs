using System;

namespace DsaJet.Api.Dto;

public class LoginDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
