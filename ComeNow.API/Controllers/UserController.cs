using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeNow.Application.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComeNow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            return await _mediator.Send(new GetCurrnetUser.Query());
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<UserDTO> Login(Login.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<UserDTO> Register(Register.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}