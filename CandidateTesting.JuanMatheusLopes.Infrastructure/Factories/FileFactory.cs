namespace CandidateTesting.JuanMatheusLopes.Infrastructure.Factories;

public class FileFactory : IFileFactory
{
    public string Create(string path, string content)
    {
        CreateDirectoryIfNotExists(path);

        File.WriteAllText(path, content);
        Console.WriteLine($"[{DateTime.Now}] - File created successfully.");

        var formattedPath = new FileInfo(path);

        return formattedPath.FullName;
    }

    private string CreateDirectoryIfNotExists(string path)
    {
        Console.WriteLine($"[{DateTime.Now}] - Verifying Directory existence.");
        var directory = new DirectoryInfo(path).Parent!.FullName;

        var directoryExists = Directory.Exists(directory);

        if (!directoryExists)
        {
            Console.WriteLine($"[{DateTime.Now}] - Creating Directory.");
            Directory.CreateDirectory(directory);

            return directory;
        }

        Console.WriteLine($"[{DateTime.Now}] - Direactory already exists.");

        return directory;
    }
}
