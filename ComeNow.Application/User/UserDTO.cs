using ComeNow.Application.Receivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Application.User
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }        
    }
}
