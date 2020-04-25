using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Domain
{
    public class CommandReceiver
    {
        public int CommandId { get; set; }
        public virtual PushCommand PushCommand { get; set; }
        public int ReceiverId { get; set; }
        public virtual Receiver Receiver { get; set; }
    }
}
