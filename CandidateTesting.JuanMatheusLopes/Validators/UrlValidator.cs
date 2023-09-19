namespace CandidateTesting.JuanMatheusLopes.Application.Validators;

public class UrlValidator : IUrlValidator
{
    public bool IsValid(string url)
    {
        return Uri.IsWellFormedUriString(url, UriKind.Absolute);
    }
}
