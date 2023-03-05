using System.Collections.Generic;

namespace Backend.TechChallenge.Api.Dtos.Common
{
    public class ErrorResponseDto
    {
        public IEnumerable<string> Errors { get; }

        public ErrorResponseDto(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}