namespace Game.RuleEngine
{
    public class EvolutionEngineActionResult
    {
        public bool EvolutionEnded { get; private set; }
        public int GenerationNumber { get; private set; }

        public EvolutionEngineActionResult(bool evolutionEnded)
        {
            EvolutionEnded = evolutionEnded;
        }
    }
}
