using System;

namespace DsaJet.Api.Entities;

public class Problem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Difficulty { get; set; }
    public required string Tag { get; set; }
    public List<Solution> Solutions { get; set; } = new();
    public required string VideoSolutionUrl { get; set; }
    public required List<Prerequisite> Prerequisites { get; set; }

}
