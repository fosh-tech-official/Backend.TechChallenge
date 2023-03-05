using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.TechChallenge.Api.Common.Models;
using Backend.TechChallenge.Api.Domain.Handler;
using Backend.TechChallenge.Api.Domain.Model;
using Backend.TechChallenge.Api.Exceptions;
using Backend.TechChallenge.Api.Repo;
using Backend.TechChallenge.Api.Repo.Entities;
using Moq;
using Xunit;

namespace Backend.TechChallenge.Test
{
    public class CreateUserHandlerTest
    {
        [Fact]
        public async Task GivenNoStoredUsers_WhenCreateUser_ThenSuccess()
        {
            // Arrange
            var store = new Mock<IStore>();
            store.Setup(x => x.GetUsers()).Returns(new List<User>());
            var handler = new CreateUserHandler(store.Object);
            var req = GetRequest();

            // Act 
            var result = await handler.Handle(req, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        // Same email
        [InlineData("Agustina@gmail.com", "666 666 666", "Paula", "Av. Juan F")]
        // Same phone
        [InlineData("different@gmail.com", "+349 1122354215", "Paula", "Av. Juan F")]
        // Same name and address
        [InlineData("different@gmail.com", "+349 1122354215", "Agustina", "Av. Juan G")]
        public async Task GivenSameStoredUser_WhenCreateUser_ThenException(string email, string phone, string name, string address)
        {
            // Arrange
            var store = new Mock<IStore>();
            store.Setup(x => x.GetUsers()).Returns(new List<User> {
                new User { Email = "Agustina@gmail.com", Phone = "+349 1122354215", Name = "Agustina", Address = "Av. Juan G" } });
            var req = new CreateUserHandlerRequest
            {
                User = new NormalUser
                {
                    Address = address,
                    Email = email,
                    Money = 124m,
                    Name = name,
                    Phone = phone
                }
            };
            var handler = new CreateUserHandler(store.Object);

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InvalidRequestException>(async () => await handler.Handle(req, new System.Threading.CancellationToken()));

            Assert.Equal("User is duplicated", exception.Message);
        }

        [Theory]
        [InlineData(UserType.Normal, 100, 100)]
        [InlineData(UserType.Normal, 110, 123.2)]
        [InlineData(UserType.Normal, 50, 90)]
        [InlineData(UserType.Normal, 5, 5)]
        
        [InlineData(UserType.SuperUser, 110, 132)]
        [InlineData(UserType.SuperUser, 100, 100)]
        
        [InlineData(UserType.Premium, 110, 330)]
        [InlineData(UserType.Premium, 100, 100)]
        public void GivenInputData_WhenApplyGift_ThenGiftApplied(UserType type , decimal initialMoney, decimal finalMoney)
        {
            // Arrange
            var store = new Mock<IStore>();
            var handler = new CreateUserHandler(store.Object);
            var user = GetUser(type, initialMoney);

            // Act 
            handler.ApplyGift(user);

            // Assert
            Assert.Equal(finalMoney,user.Money);
        }

        private CreateUserHandlerRequest GetRequest()
        {
            return new CreateUserHandlerRequest
            {
                User = GetUser(UserType.Normal, 124m)
            };
        }

        private UserBase GetUser(UserType type, decimal money)
        {
            UserBase target = null;

            switch (type)
            {
                case UserType.Normal: target = new NormalUser(); break;
                case UserType.SuperUser: target = new SuperUser(); break;
                case UserType.Premium: target = new PremiumUser(); break;
            }

            target.Address = "Av. Juan G";
            target.Phone = "+349 1122354215";
            target.Email = "Agustina@gmail.com";
            target.Money = money;
            target.Name = "Agustina";

            return target;
        }
    }
}
