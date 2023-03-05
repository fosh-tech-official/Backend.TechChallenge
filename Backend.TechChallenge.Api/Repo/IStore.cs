using Backend.TechChallenge.Api.Repo.Entities;
using System.Collections.Generic;

namespace Backend.TechChallenge.Api.Repo
{
    public interface IStore
    {
        IList<User> GetUsers();
        void Save(User newUser);
    }
}
