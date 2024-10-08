using System;
using MediatR;
using OrganNakil.Application.Dtos.UserDtos;

namespace OrganNakil.Application.Mediatr.Commands.UserCommands
{
	public class RegisterUserCommand : IRequest<UserStatusDto>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Number { get; set; }
        public string Tc { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
      
    }
}

