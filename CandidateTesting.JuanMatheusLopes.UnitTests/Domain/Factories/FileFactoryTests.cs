using CandidateTesting.JuanMatheusLopes.Infrastructure.Factories;
using CandidateTesting.JuanMatheusLopes.UnitTests.AutoFixture.Attributes;
using FluentAssertions;

namespace CandidateTesting.JuanMatheusLopes.UnitTests.Domain.Factories
{
    public class FileFactoryTests
    {
        [Theory, AutoNSubstituteData]
        public void FileFactory_Should_Implement_IFileFactory(FileFactory sut)
        {
            sut.Should().BeAssignableTo<IFileFactory>();
        }

        [Theory, AutoNSubstituteData]
        public void Create_Should_Write_File_With_Content(string content)
        {
            var fileFactory = new FileFactory();
            var path = $"{Guid.NewGuid()}.txt";

            var filePath = fileFactory.Create(path, content);

            File.Exists(filePath).Should().BeTrue();

            var fileContent = File.ReadAllText(filePath);
            fileContent.Should().Be(content);
        }

        [Theory, AutoNSubstituteData]
        public void Create_Should_Create_Directory_If_Not_Exists(string directory, string content)
        {
            var fileFactory = new FileFactory();
            var path = Path.Combine(directory, "test.txt");

            var filePath = fileFactory.Create(path, content);

            File.Exists(filePath).Should().BeTrue();
            Directory.Exists(directory).Should().BeTrue();
        }
    }
}
