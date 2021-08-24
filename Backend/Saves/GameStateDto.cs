using System.Collections.Generic;

public class GameStateDto
{
    public long LastTick { get; set; }
    public double LastDiff { get; set; }
    public double Cash { get; set; }
    public int GridSize { get; set; }
    public List<GeneratorDto> Generators { get; set; }
    public List<ExtenderDto> Extenders { get; set; }
}
