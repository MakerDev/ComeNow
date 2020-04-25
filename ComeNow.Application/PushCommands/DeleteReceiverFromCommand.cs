using ComeNow.Application.Errors;
using ComeNow.Application.Interfaces;
using ComeNow.Domain;
using ComeNow.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComeNow.Application.PushCommands
{
    public class DeleteReceiverFromCommand
    {
        public class Command : IRequest
        {
            //TODO : 나중에 유저 이름도 가능하게 하기
            public int CommandId { get; set; }
            public List<string> ReceiverEmails { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .SingleOrDefaultAsync(u => u.Email == _userAccessor.GetCurrentUserEmail());

                PushCommand pushCommand = user.PushCommands.FirstOrDefault(p => p.Id == request.CommandId);

                if (pushCommand == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { pushcommand = "No such command is found" });
                }

                List<CommandReceiver> receiversToRemove = new List<CommandReceiver>();
                List<CommandReceiver> commandReceivers = pushCommand.CommandReceivers.ToList();


                foreach (var receiverEmail in request.ReceiverEmails)
                {
                    var receiver = commandReceivers.FirstOrDefault(x => x.Receiver.ReceivingUser.Email == receiverEmail);

                    if (receiver == null)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, new { Receiver = "One or more receivers are not valid" });
                    }

                    receiversToRemove.Add(receiver);
                }

                foreach (var receiverToRemove in receiversToRemove)
                {
                    pushCommand.CommandReceivers.Remove(receiverToRemove);
                }

                bool success = (await _context.SaveChangesAsync() > 0);

                if (success == true)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem Saving Changes Removing receiver from command");
            }
        }
    }
}
