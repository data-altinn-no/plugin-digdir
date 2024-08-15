namespace Altinn.Dan.Plugin.Digdir.Models
{
    public class RichEvidence
    {
        public string Name { get; init; }

        public int Number { get; init; }

        public bool TrueOrFalse { get; init; }

        public SubEvidence SubEvidence { get; init; }
    }

    public class SubEvidence
    {
        public string SubName { get; init; }

        public double SubNumber { get; init; }
    }
}