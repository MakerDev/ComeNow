using ComeNow.Application.Errors;
using ComeNow.Application.Interfaces;
using ComeNow.Domain;
using ComeNow.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComeNow.Application.User
{
    public class Register
    {
        public class Command : IRequest<UserDTO>
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserDTO>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<UserDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                bool isExisting = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();

                if (isExisting)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });
                }

                isExisting = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();

                if (isExisting)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });
                }

                AppUser newUser = new AppUser
                {
                    Email = request.Email,
                    UserName = request.Username,
                };

                var result = await _userManager.CreateAsync(newUser, request.Password);

                if (result.Succeeded)
                {
                    return new UserDTO
                    {
                        Email = newUser.Email,
                        Name = newUser.UserName,
                        Token = _jwtGenerator.CreateToken(newUser),
                    };
                }

                throw new Exception("Problem occured while Registering");
            }
        }
    }
}
