using CandidateTesting.JuanMatheusLopes.Domain.Arguments;
using CandidateTesting.JuanMatheusLopes.Domain.Entities;

namespace CandidateTesting.JuanMatheusLopes.Domain.Mappers;

public interface IAgoraCDNMapping
{
    AgoraEntries MapFromMinhaCDN(MinhaCDNEntries entries);
}
