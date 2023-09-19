using CandidateTesting.JuanMatheusLopes.Domain.Arguments;

namespace CandidateTesting.JuanMatheusLopes.Domain.Mappers;

public interface IMinhaCDNMapping
{
    MinhaCDNEntries MapEntriesFromStream(Stream stream);
}
