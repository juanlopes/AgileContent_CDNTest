using CandidateTesting.JuanMatheusLopes.Domain.Arguments;

namespace CandidateTesting.JuanMatheusLopes.Domain.Entities
{
    public class AgoraEntries
    {
        public decimal Version { get; set; }
        public DateTime Date { get; set; }
        public List<AgoraEntry> Entries { get; set; } = new List<AgoraEntry>();
    }
}
