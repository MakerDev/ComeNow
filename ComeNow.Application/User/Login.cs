using ComeNow.Application.Errors;
using ComeNow.Application.Interfaces;
using ComeNow.Domain;
using ComeNow.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComeNow.Application.User
{
    public class Login
    {
        public class Command : IRequest<UserDTO>
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserDTO>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<UserDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                AppUser user = await _userManager.FindByEmailAsync(request.Email);

                if(user == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { User = "No such email is found" });
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if(result.Succeeded)
                {
                    return new UserDTO
                    {
                        Email = request.Email,
                        Name = user.UserName,
                        Token = _jwtGenerator.CreateToken(user),
                    };
                }

                throw new RestException(HttpStatusCode.Unauthorized);             
            }
        }
    }
}
