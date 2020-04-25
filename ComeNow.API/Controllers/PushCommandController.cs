using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeNow.Application.PushCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComeNow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PushCommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PushCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<PushCommandDTO>> GetAll()
        {
            return await _mediator.Send(new GetAll.Query());
        }

        [HttpDelete("{id}/delete/receivers")]
        public async Task<Unit> RemoveReceiversFromCommand(int id, DeleteReceiverFromCommand.Command command)
        {
            command.CommandId = id;

            return await _mediator.Send(command);
        }
    }
}