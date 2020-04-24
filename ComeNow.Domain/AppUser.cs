using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Domain
{
    public class AppUser : IdentityUser
    {
        public bool CanReceiveMessage { get; set; }
        public string DisplayName { get; set; }
        public virtual ICollection<Receiver> Receivers { get; set; }
    }
}
