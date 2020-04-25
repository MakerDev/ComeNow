using AutoMapper;
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
            private readonly IMapper _mapper;

            public Hander(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<List<ReceiverDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                AppUser user = await _context.Users
                        .SingleOrDefaultAsync(u => u.Email == _userAccessor.GetCurrentUserEmail());

                var receivers = user.Receivers?.ToList();

                List<ReceiverDTO> receiverDTOs = new List<ReceiverDTO>();

                if (receivers == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Receivers = "No receiver found" });
                }


                //foreach (var receiver in receivers)
                //{
                //    receiverDTOs.Add(new ReceiverDTO
                //    {
                //        Email = receiver.ReceivingUser.Email,
                //        DisplayName = receiver.DisplayName,
                //        CanReceiveMessage = receiver.ReceivingUser.CanReceiveMessage,
                //    });
                //}

                return _mapper.Map<List<Receiver>, List<ReceiverDTO>>(receivers);
            }
        }
    }
}
