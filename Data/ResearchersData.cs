using System.Collections.Generic;

namespace Edison
{
    public static class ResearchersData
    {
        public static List<(Researchers id, string name, double startingPrice, double startingProduction)> Data
        => new()
        {
            (Researchers.Inventor, "Self-taught inventor", 33, 2),
            (Researchers.PhD, "Engineering PhD", 255, 4),
            (Researchers.ResearchTeam, "Research team", 1255, 6),
            (Researchers.ResearchInstitute, "Research institute", 3333, 8),
            (Researchers.University, "Electrical engineering department", 6666, 10),
            (Researchers.RDTeam, "Research and development team", 14_000, 12),
            (Researchers.AIAssistant, "AI research assistant", 125_000, 14),
            (Researchers.QuantumAI, "Quantum AI inventor", 3_333_333, 16)
        };
    }
}
