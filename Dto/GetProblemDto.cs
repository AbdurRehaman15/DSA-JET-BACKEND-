using System;

namespace DsaJet.Api.Dto;

public class GetProblemDto
{
     public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Difficulty { get; set; }
    public required string Tag { get; set; }
    public required string VideoSolutionUrl { get; set; }
    public List<string> Solutions { get; set; } = new();
    public required List<string> Prerequisites { get; set; }

}
