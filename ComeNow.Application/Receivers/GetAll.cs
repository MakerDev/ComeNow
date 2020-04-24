using ComeNow.Application.Interfaces;
using ComeNow.Domain;
using ComeNow.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComeNow.Application.Receivers
{
    public class GetAll
    {
        public class Query : IRequest<List<ReceiverDTO>>
        {

        }

        public class Hander : IRequestHandler<Query, List<ReceiverDTO>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Hander(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<List<ReceiverDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                AppUser user = await _context.Users
                        .SingleOrDefaultAsync(u => u.Email == _userAccessor.GetCurrentUserEmail());

                var receivers = user.Receivers;
                List<ReceiverDTO> receiverDTOs = new List<ReceiverDTO>();

                if (receivers != null)
                {
                    foreach (var receiver in receivers)
                    {
                        receiverDTOs.Add(new ReceiverDTO
                        {
                            Email = receiver.ReceivingUser.Email,
                            DisplayName = receiver.DisplayName,
                            CanReceiveData = receiver.ReceivingUser.CanReceiveMessage,
                        });
                    }
                }                

                return receiverDTOs;
            }
        }
    }
}
