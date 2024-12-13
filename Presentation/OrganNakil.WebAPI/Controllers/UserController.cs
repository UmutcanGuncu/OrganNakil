using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrganNakil.Application.Abstractions.Token;
using OrganNakil.Application.Dtos.TokenDtos;
using OrganNakil.Application.Dtos.UserDtos;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Commands.UserCommands;
using OrganNakil.Application.Mediatr.Queries.RefreshTokenQueries;
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
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserRepository _userRepository;
        public UserController(IMediator mediator, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMailRepository mailRepository, ITokenHandler tokenHandler, IUserRepository userRepository)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
            _mailRepository = mailRepository;
            _tokenHandler = tokenHandler;
            _userRepository = userRepository;
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
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true); // remember me bilgisini kullanıcıdan al
            if (result.Succeeded)
            {
                Token token= _tokenHandler.CreateAccessToken(20);
                await _userRepository.UpdateRefreshToken(token.RefreshToken, user, token.Expiration,2);
                UserStatusDto userStatusDto = new()
                {
                    Code = "Success",
                    Description = "Giriş İşlemi Başarıyla Tamamlanmıştır",
                    UserId = user.Id
                };
                return Ok(token);
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

        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromForm] RefreshTokenLoginQuery query)
        {
            return Ok(await _mediator.Send(query));
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

