using ComeNow.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
