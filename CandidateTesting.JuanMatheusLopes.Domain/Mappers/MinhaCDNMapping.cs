using CandidateTesting.JuanMatheusLopes.Domain.Arguments;
using System.Globalization;

namespace CandidateTesting.JuanMatheusLopes.Domain.Mappers
{
    public class MinhaCDNMapping : IMinhaCDNMapping
    {
        public MinhaCDNEntries MapEntriesFromStream(Stream stream)
        {
            using var reader = new StreamReader(stream);

            var stringContent = reader.ReadToEnd();

            Console.WriteLine(
                $"[{DateTime.Now}] - Received Response: \r\n" +
                $"--------------------------------------------------- \r\n " +
                $"{stringContent} \r\n" +
                $"---------------------------------------------------");


            var contentSplitted = stringContent.Split("\r\n");

            var minhaCDNEntries = new MinhaCDNEntries();

            foreach (var log in contentSplitted)
            {
                var fields = log.Split('|');

                if (fields.Length == 5)
                {
                    var entry = new MinhaCDNEntry
                    {
                        ResponseSize = GetResponseSize(fields[0]),
                        StatusCode = GetStatusCode(fields[1]),
                        CacheStatus = GetCacheStatus(fields[2]),
                        RequestedPath = GetRequestedPath(fields[3]),
                        TimeTaken = GetTimeTaken(fields[4])
                    };

                    minhaCDNEntries.Entries.Add(entry);
                }
            }

            return minhaCDNEntries;
        }

        private static int GetResponseSize(string responseSize)
        {
            return int.Parse(responseSize);
        }

        private static int GetStatusCode(string statusCode)
        {
            return int.Parse(statusCode);
        }

        private static string GetCacheStatus(string cacheStatus)
        {
            return cacheStatus;
        }

        private static string GetRequestedPath(string requestedPath)
        {
            return requestedPath;
        }

        private static decimal GetTimeTaken(string timeTaken)
        {
            var cultureInfo = new CultureInfo("en-US");

            return decimal.Parse(timeTaken, cultureInfo);
        }

    }
}
