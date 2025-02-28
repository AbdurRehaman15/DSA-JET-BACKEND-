using System;

namespace DsaJet.Api.Entities;

public class Solution
{
    public int Id { get; set; }
    public int ProblemId { get; set; }
    public required string Language { get; set; }
    public required string SolutionCode { get; set; }
    public Problem Problem { get; set; } = null!;
}
