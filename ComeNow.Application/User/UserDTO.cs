using ComeNow.Application.Receivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Application.User
{
    public class UserDTO
    {
        public string Email { get; set; }
        public List<ReceiverDTO> Receivers { get; set; }
    }
}
