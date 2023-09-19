using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace CandidateTesting.JuanMatheusLopes.UnitTests.AutoFixture.Attributes;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute()
        : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
    { }
}
