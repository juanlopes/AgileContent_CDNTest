using CandidateTesting.JuanMatheusLopes.Application.Validators;
using CandidateTesting.JuanMatheusLopes.UnitTests.AutoFixture.Attributes;
using FluentAssertions;

namespace CandidateTesting.JuanMatheusLopes.UnitTests.Application.Validators
{
    public class UrlValidatorTests
    {
        [Theory, AutoNSubstituteData]
        public void UrlValidator_Should_Implement_IUrlValidator(UrlValidator sut)
        {
            sut.Should().BeAssignableTo<IUrlValidator>();
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("asf32fa3f3t", false)]
        [InlineData("www.data.com", false)]
        [InlineData("example.exm", false)]
        [InlineData("https://www.example.com", true)]
        public void UrlValidator_Should_Validate_Url(string url, bool expected)
        {
            var sut = new UrlValidator();

            var actual = sut.IsValid(url);

            actual.Should().Be(expected);
        }
    }
}
