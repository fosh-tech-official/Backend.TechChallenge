using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;
using Backend.TechChallenge.Domain.Repositories.Contracts;
using Backend.TechChallenge.Domain.Services.Contracts;
using Backend.TechChallenge.Infrastructure.File.Helpers.Contracts;
using Backend.TechChallenge.Infrastructure.File.Mappers.Contracts;

namespace Backend.TechChallenge.Infrastructure.File.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly IUserFileMapper _userFileMapper;
    private readonly IUserFactory _userFactory;
    private readonly string _filePath;

    public UserRepository(IUserFileMapper userFileMapper, IUserFactory userFactory, IFilePathHelper filePathHelper)
    {
        _userFileMapper = userFileMapper ?? throw new ArgumentNullException(nameof(userFileMapper));
        _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
        _ = filePathHelper ?? throw new ArgumentNullException(nameof(filePathHelper));
        _filePath = filePathHelper.GetUserFilePath();
    }

    public async Task InsertAsync(User entity)
    {
        var userFile = _userFileMapper.Map(entity);
        var userLine = _userFileMapper.Map(userFile);

        await using var writer = new StreamWriter(_filePath, true);
        await writer.WriteLineAsync(userLine);
    }

    public async Task<List<User?>> GetAllAsync()
    {
        var result = new List<User?>();

        using var streamReader = new StreamReader(_filePath);
        while (!streamReader.EndOfStream)
        {
            var userLine = await streamReader.ReadLineAsync();
            if (!string.IsNullOrWhiteSpace(userLine))
            {
                const char separator = ',';
                var userFile = _userFileMapper.Map(userLine, separator);
                var userTypeIsValid = Enum.TryParse<UserType>(userFile?.UserType, out var userType);
                if (!userTypeIsValid)
                    continue;

                var user = _userFactory.Create(userFile?.Name, userFile?.Email, userFile?.Address, userFile?.Phone,
                    userFile?.Money, userType);
                result.Add(user);
            }
        }

        return result;
    }
}