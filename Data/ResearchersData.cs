using System.Collections.Generic;

namespace Edison
{
    public static class ResearchersData
    {
        public static List<(Researchers id, string name, double startingPrice, double startingProduction, bool isHidden)> Data
        => new()
        {
            (Researchers.Inventor, "Self-taught inventor", 105, 2, false),
            (Researchers.PhD, "Engineering PhD", 255, 4, true),
            (Researchers.ResearchTeam, "Research team", 1255, 6, true),
            (Researchers.ResearchInstitute, "Research institute", 3333, 8, true),
            (Researchers.University, "Electrical engineering department", 6666, 10, true),
            (Researchers.RDTeam, "Research and development team", 14_000, 12, true),
            (Researchers.AIAssistant, "AI research assistant", 125_000, 14, true),
            (Researchers.QuantumAI, "Quantum AI inventor", 3_333_333, 16, true)
        };
    }
}
