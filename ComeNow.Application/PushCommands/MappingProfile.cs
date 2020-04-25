using AutoMapper;
using ComeNow.Application.Receivers;
using ComeNow.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Application.PushCommands
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PushCommand, PushCommandDTO>();
            CreateMap<CommandReceiver, ReceiverDTO>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Receiver.ReceivingUser.Email))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Receiver.DisplayName))
                .ForMember(d => d.CanReceiveMessage, o => o.MapFrom(s => s.Receiver.ReceivingUser.CanReceiveMessage));
        }
    }
}
