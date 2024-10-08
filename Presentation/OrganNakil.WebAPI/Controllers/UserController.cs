using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrganNakil.Application.Dtos.UserDtos;
using OrganNakil.Application.Mediatr.Commands.UserCommands;
using OrganNakil.Domain.Entities;

namespace OrganNakil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public UserController(IMediator mediator, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
       public async Task<IActionResult> RegisterUser(RegisterUserCommand register)
        {
            
            return Ok(await _mediator.Send(register));
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
                return Ok(userStatusDto);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true); // remember me bilgisini kullanıcıdan al
            if (result.Succeeded)
            {
                UserStatusDto userStatusDto = new()
                {
                    Code = "Success",
                    Description = "Giriş İşlemi Başarıyla Tamamlanmıştır"
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
                return Ok(userStatusDto);
            }
            
            
        }

    }
}

