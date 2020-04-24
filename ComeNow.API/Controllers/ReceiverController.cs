using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeNow.Application.Receivers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComeNow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReceiverController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReceiverDTO>>> GetAll()
        {
            return await _mediator.Send(new GetAll.Query());
        }
    }
}