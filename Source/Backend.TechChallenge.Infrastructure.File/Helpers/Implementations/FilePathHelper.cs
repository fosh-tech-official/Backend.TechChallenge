using Backend.TechChallenge.Infrastructure.File.Helpers.Contracts;
using Microsoft.Extensions.Configuration;

namespace Backend.TechChallenge.Infrastructure.File.Helpers.Implementations;

public class FilePathHelper : IFilePathHelper
{
    private readonly IConfiguration _configuration;

    public FilePathHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetUserFilePath()
    {
        var fileName = _configuration.GetValue<string>("UsersFileName");
        var result =
            @$"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}{Path.DirectorySeparatorChar}Files{Path.DirectorySeparatorChar}{fileName}.txt";

        return result;
    }
}