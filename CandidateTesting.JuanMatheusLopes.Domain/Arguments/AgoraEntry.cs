namespace CandidateTesting.JuanMatheusLopes.Domain.Arguments
{
    public class AgoraEntry
    {
        public string Provider { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string UriPath { get; set; } = string.Empty;
        public int TimeTaken { get; set; }
        public int ResponseSize { get; set; }
        public string CacheStatus { get; set; } = string.Empty;
    }
}
