namespace CandidateTesting.JuanMatheusLopes.Domain.Arguments
{
    public class MinhaCDNEntry
    {
        public int ResponseSize { get; set; }
        public int StatusCode { get; set; }
        public string CacheStatus { get; set; } = string.Empty;
        public string RequestedPath { get; set; } = string.Empty;
        public decimal TimeTaken { get; set; }
    }
}
