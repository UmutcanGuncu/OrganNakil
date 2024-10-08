using System;
using MediatR;
using OrganNakil.Application.Dtos.UserDtos;

namespace OrganNakil.Application.Mediatr.Commands.UserCommands
{
	public class RegisterUserCommand : IRequest<UserStatusDto>
	{
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

