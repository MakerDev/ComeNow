using ComeNow.Application.Receivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Application.PushCommands
{
    public class PushCommandDTO
    {
        public int Id { get; set; }
        public string CommandName { get; set; }
        public string Message { get; set; }
        public List<ReceiverDTO> Receivers { get; set; }
    }
}
