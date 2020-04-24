using ComeNow.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeNow.Persistance
{
    public class Seed
    {
        public static async Task SeedDataAsync(DataContext context, UserManager<AppUser> userManager)
        {

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = "user1",
                        Email = "raki2001@naver.com",
                        UserName = "Yujin",
                        CanReceiveMessage = true,
                    },
                    new AppUser
                    {
                        Id = "user2",
                        Email = "raki2002@naver.com",
                        UserName = "park",
                        CanReceiveMessage = true,
                    },
                    new AppUser
                    {
                        Id = "user3",
                        Email = "raki2003@naver.com",
                        UserName = "makerdev",
                        CanReceiveMessage = true,
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Dbwls6683!");
                }

                context.SaveChanges();
            };
        }
    }
}
