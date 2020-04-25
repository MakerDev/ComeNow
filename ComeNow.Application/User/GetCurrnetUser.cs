using ComeNow.Application.Interfaces;
using ComeNow.Application.Receivers;
using ComeNow.Domain;
using ComeNow.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ComeNow.Application.User
{
    public class GetCurrnetUser
    {
        public class Query : IRequest<UserDTO>
        {
        }

        public class Hander : IRequestHandler<Query, UserDTO>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IJwtGenerator _jwtGenerator;

            public Hander(DataContext context, IUserAccessor userAccessor, IJwtGenerator jwtGenerator)
            {
                _context = context;
                _userAccessor = userAccessor;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<UserDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                AppUser user = await _context.Users
                    .SingleOrDefaultAsync(u => u.Email == _userAccessor.GetCurrentUserEmail());

                List<ReceiverDTO> receiverDTOs = new List<ReceiverDTO>();

                foreach (var receiver in user.Receivers)
                {
                    receiverDTOs.Add(new ReceiverDTO
                    {
                        DisplayName = receiver.DisplayName,
                        Email = receiver.ReceivingUser.Email,
                        CanReceiveMessage = receiver.ReceivingUser.CanReceiveMessage,
                    });
                }

                UserDTO userDTO = new UserDTO
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Token = _jwtGenerator.CreateToken(user),
                };

                return userDTO;
            }
        }
    }
}
