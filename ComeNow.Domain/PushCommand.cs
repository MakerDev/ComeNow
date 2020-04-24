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

        public virtual ICollection<Receiver> Receivers { get; set; }
    }
}
