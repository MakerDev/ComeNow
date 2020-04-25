using AutoMapper;
using ComeNow.Application.Errors;
using ComeNow.Application.Interfaces;
using ComeNow.Application.Receivers;
using ComeNow.Domain;
using ComeNow.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComeNow.Application.PushCommands
{
    public class GetAll
    {
        public class Query : IRequest<List<PushCommandDTO>>
        {

        }

        public class Hander : IRequestHandler<Query, List<PushCommandDTO>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Hander(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<List<PushCommandDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                AppUser user = await _context.Users
                    .SingleOrDefaultAsync(u => u.Email == _userAccessor.GetCurrentUserEmail());

                var pushCommands = user.PushCommands?.ToList();

                if (pushCommands == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { Commands = "No Command are found" });
                }

                List<PushCommandDTO> commandDTOs = new List<PushCommandDTO>();

                foreach (var command in pushCommands)
                {
                    commandDTOs.Add(new PushCommandDTO
                    {
                        Id = command.Id,
                        Message = command.Message,
                        CommandName = command.Name,
                        Receivers = _mapper.Map<List<CommandReceiver>, List<ReceiverDTO>>(command.CommandReceivers.ToList())
                    });
                }

                return commandDTOs;
            }
        }
    }
}
