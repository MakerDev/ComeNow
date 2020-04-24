using ComeNow.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComeNow.Application.Receivers
{
    public class AddReceiver
    {
        public class Command : IRequest
        {
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

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
