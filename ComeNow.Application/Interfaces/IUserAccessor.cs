using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Application.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUsername();
        string GetCurrentUserEmail();
    }
}
