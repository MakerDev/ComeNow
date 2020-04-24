using ComeNow.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Instrastucture.Security
{
    public class MockUserAccessor : IUserAccessor
    {
        public string GetCurrentUserEmail()
        {
            return "raki2001@naver.com";
        }

        public string GetCurrentUsername()
        {
            return "Yujin";
        }
    }
}
