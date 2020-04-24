using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Application.Receivers
{
    public class ReceiverDTO
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool CanReceiveData { get; set; }
    }
}
