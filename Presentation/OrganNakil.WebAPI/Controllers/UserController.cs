using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrganNakil.Application.Dtos.UserDtos;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Commands.UserCommands;
using OrganNakil.Domain.Entities;

namespace OrganNakil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailRepository _mailRepository;
        public UserController(IMediator mediator, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMailRepository mailRepository)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
            _mailRepository = mailRepository;
        }

        [HttpPost("register")]
       public async Task<IActionResult> RegisterUser(RegisterUserCommand register)
       {
           var value = await _mediator.Send(register);
           if (value.Code == "Success")
               return Ok(value);
           return Unauthorized(value);
       }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                UserStatusDto userStatusDto = new()
                {
                    Code = "User Not Found",
                    Description = "Kullanıcı Bilgisi Bulunamadı"
                };
                return Unauthorized(userStatusDto);
            }
            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true); // remember me bilgisini kullanıcıdan al
            if (result.Succeeded)
            {
                UserStatusDto userStatusDto = new()
                {
                    Code = "Success",
                    Description = "Giriş İşlemi Başarıyla Tamamlanmıştır",
                    UserId = user.Id
                };
                return Ok(userStatusDto);
            }
            else
            {
                UserStatusDto userStatusDto = new()
                {
                    Code = "Incorrect Password",
                    Description = "Şifreniz Yanlış"
                };
                return Unauthorized(userStatusDto);
            }
            
            
        }

        [HttpGet("generate-reset-token")]
        public async Task<IActionResult> PasswordReset()
        {
            return Ok();
        }

        [HttpPost("generate-reset-token")]
        public async Task<IActionResult> PasswordReset(PasswordResetDto passwordResetDto)
        {
            var hasUser = await _userManager.FindByEmailAsync(passwordResetDto.Email);
            if (hasUser == null)
            {
                UserStatusDto userStatusDto = new UserStatusDto()
                {
                    Code = "User Not Found",
                    Description = "Kullanıcı Bilgisi Bulunamadı"
                };
                return Unauthorized(userStatusDto);
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            var passwordResetLink = Url.Action("PasswordReset","User",new {userId = hasUser.Id,Token = passwordResetToken},HttpContext.Request.Scheme,"localhost");
            await _mailRepository.SendResetMailAsync(passwordResetLink, hasUser.Email);
            return Ok();
        }

    }
}

