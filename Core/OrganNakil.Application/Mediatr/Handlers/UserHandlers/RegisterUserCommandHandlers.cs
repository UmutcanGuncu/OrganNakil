﻿using System;
using System.Globalization;
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
        private readonly RoleManager<AppRole> _roleManager;
        public RegisterUserCommandHandlers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserStatusDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            
            var value = await _userManager.CreateAsync(new()
            {
                UserName = request.Tc,
                Email = request.Email,
                City = request.City.ToLower(new CultureInfo("tr-TR")),
                Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Name.ToLower()),
                Surname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Surname.ToLower()),
                PhoneNumber = request.Number,
                BloodGroup = request.BloodGroup
                

            }, request.Password);
            if (value.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                await _userManager.AddToRoleAsync(user, "User");
                return new() { Code = "Success", Description = "Kayıt İşlemi Başarıyla Tamamlanmıştır" };
            }
            var firstError = value.Errors.First();
            return new() { Code = firstError.Code, Description = firstError.Description };
        }
    }
}

