using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComeNow.Domain
{
    public class Receiver
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }
        public virtual AppUser Owner { get; set; }
        public string DisplayName { get; set; }
        public virtual AppUser ReceivingUser { get; set; }

        public virtual ICollection<CommandReceiver> CommandReceivers { get; set; }
    }
}
