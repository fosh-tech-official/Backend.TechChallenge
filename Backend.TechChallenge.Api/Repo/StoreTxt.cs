using System.Collections.Generic;
using System.IO;
using Backend.TechChallenge.Api.Repo.Entities;

namespace Backend.TechChallenge.Api.Repo
{
    public class StoreTxt : IStore
    {
        public IList<User> GetUsers()
        {
            var _users = new List<User>();

            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    Type = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();

            return _users;
        }

        public void Save(User newUser)
        {
            WriteUserToFile(newUser);
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        private void WriteUserToFile(User user)
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            using (StreamWriter outputFile = new StreamWriter(path, append: true))
            {
                outputFile.WriteLine(user.Name + "," + user.Email + "," + user.Phone + "," + user.Address + "," + user.Type + "," + user.Money);
            }
        }
    }
}
