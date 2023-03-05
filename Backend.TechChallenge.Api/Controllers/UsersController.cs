using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Backend.TechChallenge.Api.Controllers.Entities;
using Backend.TechChallenge.Api.Domain.Handler;
using Backend.TechChallenge.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.TechChallenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<CreateUserResult> CreateUser(CreateUserRequest request)
        {            
            Validate(request);

            await _mediator.Send(new CreateUserHandlerRequest
            {
                User = Converter.ToDomainModel(request)
            });

            return new CreateUserResult()
            {
                IsSuccess = true
            };
        }

        // Validate errors
        private void Validate(CreateUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new InvalidRequestException("Name cannot be null or empty");
            if (string.IsNullOrEmpty(request.Address))
                throw new InvalidRequestException("Address cannot be null or empty");
            if (string.IsNullOrEmpty(request.Email))
                throw new InvalidRequestException("Email cannot be null or empty");
            if (!IsValid(request.Email))
                throw new InvalidRequestException("Email address value is not valid");
            if (string.IsNullOrEmpty(request.Phone))
                throw new InvalidRequestException("Phone cannot be null or empty");
        }

        private bool IsValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
