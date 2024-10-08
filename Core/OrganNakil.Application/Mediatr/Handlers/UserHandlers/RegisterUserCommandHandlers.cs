using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OrganNakil.Application.Dtos.UserDtos;
using OrganNakil.Application.Mediatr.Commands.UserCommands;
using OrganNakil.Domain.Entities;

namespace OrganNakil.Application.Mediatr.Handlers.UserHandlers
{
    public class RegisterUserCommandHandlers : IRequestHandler<RegisterUserCommand, UserStatusDto>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterUserCommandHandlers(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserStatusDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            
            var value = await _userManager.CreateAsync(new()
            {
                UserName = request.Tc,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                PhoneNumber = request.Number

            }, request.Password);
            if (value.Succeeded)
            {
                return new() { Code = "Success", Description = "Kayıt İşlemi Başarıyla Tamamlanmıştır" };
            }
            var firstError = value.Errors.First();
            return new() { Code = firstError.Code, Description = firstError.Description };
        }
    }
}

