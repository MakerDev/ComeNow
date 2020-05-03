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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComeNow.Application.PushCommands
{
    public class AddPushCommand
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
            public string Message { get; set; }
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
                List<Receiver> receivers = new List<Receiver>();

                foreach (var receiverEmail in request.ReceiverEmails)
                {
                    Receiver receiver = await _context.Receivers.FirstOrDefaultAsync(
                        r => r.ReceivingUser.Email == receiverEmail);

                    if (receiver == null)
                    {
                        throw new RestException(HttpStatusCode.BadRequest,
                            new { Receiver = $"{receiverEmail} is not registerd" });
                    }

                    receivers.Add(receiver);
                }

                var user = await _context.Users.SingleOrDefaultAsync(
                    x => x.Email == _userAccessor.GetCurrentUserEmail());

                PushCommand pushCommand = new PushCommand
                {
                    Name = request.Name,
                    Message = request.Message,
                    CommandReceivers = new List<CommandReceiver>(),
                };

                foreach (var receiver in receivers)
                {
                    CommandReceiver commandReceiver = new CommandReceiver
                    {
                        PushCommand = pushCommand,
                        Receiver = receiver,
                    };

                    pushCommand.CommandReceivers.Add(commandReceiver);
                }

                user.PushCommands.Add(pushCommand);

                bool success = (await _context.SaveChangesAsync() > 0);

                if (success == true)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem Saving Changes");
            }
        }
    }
}
