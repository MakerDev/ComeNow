using ComeNow.Application.Interfaces;
using ComeNow.Domain;
using ComeNow.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComeNow.Application.Receivers
{
    public class AddReceiver
    {
        public class Command : IRequest
        {
            public string DisplayName { get; set; }
            public string Email { get; set; }
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
                var user = _context.Users
                    .SingleOrDefault(u => u.Email == _userAccessor.GetCurrentUserEmail());
                var receivingUser = _context.Users
                    .SingleOrDefault(u => u.Email == request.Email);

                Receiver receiver = new Receiver
                {
                    Owner = user,
                    ReceivingUser = receivingUser,
                    DisplayName = request.DisplayName,
                };

                _context.Receivers.Add(receiver);

                bool success = (await _context.SaveChangesAsync() > 0);

                if (success == true)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem Saving Changes Adding receiver");
            }
        }
    }
}
