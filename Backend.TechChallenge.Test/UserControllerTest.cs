using System.Threading.Tasks;
using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Api.Controllers.Entities;
using Backend.TechChallenge.Api.Exceptions;
using MediatR;
using Moq;
using Xunit;

namespace Backend.TechChallenge.Test
{
    [CollectionDefinition("UserControllerTest", DisableParallelization = true)]
    public class UserControllerTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GivenEmptyName_WhenCreateUser_ThenException(string name)
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var userController = new UsersController(mediator.Object);
            var req = GetRequest();
            req.Name = name;

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InvalidRequestException>(async () => await userController.CreateUser(req));

            Assert.Equal("Name cannot be null or empty", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GivenEmptyEmail_WhenCreateUser_ThenException(string email)
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var userController = new UsersController(mediator.Object);
            var req = GetRequest();
            req.Email = email;

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InvalidRequestException>(async () => await userController.CreateUser(req));

            Assert.Equal("Email cannot be null or empty", exception.Message);
        }

        [Fact]
        public async Task GivenInvalidEmail_WhenCreateUser_ThenException()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var userController = new UsersController(mediator.Object);
            var req = GetRequest();
            req.Email = "INVALID";

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InvalidRequestException>(async () => await userController.CreateUser(req));

            Assert.Equal("Email address value is not valid", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GivenEmptyAddress_WhenCreateUser_ThenException(string address)
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var userController = new UsersController(mediator.Object);
            var req = GetRequest();
            req.Address = address;

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InvalidRequestException>(async () => await userController.CreateUser(req));

            Assert.Equal("Address cannot be null or empty", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GivenEmptyPhone_WhenCreateUser_ThenException(string phone)
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var userController = new UsersController(mediator.Object);
            var req = GetRequest();
            req.Phone = phone;

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InvalidRequestException>(async () => await userController.CreateUser(req));

            Assert.Equal("Phone cannot be null or empty", exception.Message);
        }

        [Fact]
        public async Task GivenValidRequest_WhenCreateUser_ThenSuccess()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var userController = new UsersController(mediator.Object);
            var req = GetRequest();

            // Act && Assert
            var result = await userController.CreateUser(req);

            Assert.True(result.IsSuccess);
        }

        private CreateUserRequest GetRequest()
        {
            return new CreateUserRequest
            {
                Address = "Av. Juan G",
                Email = "Agustina@gmail.com",
                Money = 124m,
                Name = "Agustina",
                Phone = "+349 1122354215",
                Type = Api.Common.Models.UserType.Normal
            };
        }
    }
}
