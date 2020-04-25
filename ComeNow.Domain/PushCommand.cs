using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Domain
{
    public class PushCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }

        //Many to many로 관계 바뀌어야함.
        public virtual ICollection<CommandReceiver> CommandReceivers { get; set; }
    }
}
