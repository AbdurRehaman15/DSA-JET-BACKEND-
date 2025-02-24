using System;

namespace DsaJet.Api.Entities;

public class Prerequisite
{
    public int Id { get; set; }
    public int ProblemId { get; set; }
    public required string Prereq { get; set; }

    public Problem Problem { get; set; } = null!;
}
