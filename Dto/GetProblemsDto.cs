using System;
using DsaJet.Api.Entities;

namespace DsaJet.Api.Dto;

public class GetProblemsDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Difficulty { get; set; }
    public required string Tag { get; set; }

}
