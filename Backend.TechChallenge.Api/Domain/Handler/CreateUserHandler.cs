using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Backend.TechChallenge.Api.Common.Models;
using Backend.TechChallenge.Api.Domain.Model;
using Backend.TechChallenge.Api.Exceptions;
using Backend.TechChallenge.Api.Helper;
using Backend.TechChallenge.Api.Repo;
using Backend.TechChallenge.Api.Repo.Entities;
using MediatR;

namespace Backend.TechChallenge.Api.Domain.Handler
{
    public class CreateUserHandlerRequest : IRequest<CreateUserHandlerResult>
    {
        public UserBase User { get; set; }
    }

    public class CreateUserHandlerResult
    {
    }
    public class CreateUserHandler : IRequestHandler<CreateUserHandlerRequest, CreateUserHandlerResult>
    {
        private readonly IStore _store;

        public CreateUserHandler(IStore store)
        {
            _store = store;
        }

        public async Task<CreateUserHandlerResult> Handle(CreateUserHandlerRequest request, CancellationToken cancellationToken)
        {
            var existingUsers = _store.GetUsers();

            // We first normalize before assert
            UserHelper.NormalizeEmail(request.User);
            AssertDuplicatedUser(request.User, existingUsers);
            // Some business logic
            ApplyGift(request.User);

            _store.Save(Converter.ToStoreModel(request.User));

            return new CreateUserHandlerResult();
        }

        private void AssertDuplicatedUser(UserBase newUser, IEnumerable<User> existingUsers)
        {
            foreach (var user in existingUsers)
            {
                var isDuplicated = user.Email == newUser.Email
                            || user.Phone == newUser.Phone
                            || (user.Name == newUser.Name && user.Address == newUser.Address);

                if (isDuplicated)
                    throw new InvalidRequestException("User is duplicated");
            }
        }

        public void ApplyGift(UserBase newUser)
        {
            var percentage = 0m;

            switch (newUser.Type)
            {
                case UserType.Normal:
                    if (newUser.Money > 100m)
                        percentage = 0.12m;
                    else if (newUser.Money < 100m && newUser.Money > 10m)
                        percentage = 0.8m;
                    break;
                case UserType.SuperUser:
                    if (newUser.Money > 100)
                        percentage = 0.20m;
                    break;
                case UserType.Premium:
                    if (newUser.Money > 100)
                        percentage = 0.8m;
                    break;
            }

            var gift = newUser.Money * percentage;
            newUser.Money = newUser.Money + gift;
        }
    }
}
