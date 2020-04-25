using AutoMapper;
using ComeNow.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Application.Receivers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Receiver, ReceiverDTO>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.ReceivingUser.Email))
                .ForMember(d => d.CanReceiveMessage, o => o.MapFrom(s => s.ReceivingUser.CanReceiveMessage))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.DisplayName));
        }
    }
}
