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
                var users = new List<AppUser>()
                {
                    new AppUser
                    {
                        Id = "user1",
                        Email = "raki2001@naver.com",
                        UserName = "Yujin",
                        CanReceiveMessage = true,
                        PushCommands = new List<PushCommand>(),
                        Receivers = new List<Receiver>(),
                    },
                        new AppUser
                        {
                            Id = "user4",
                            Email = "raki2004@naver.com",
                            UserName = "Yujin4",
                            CanReceiveMessage = true,
                            PushCommands = new List<PushCommand>(),
                            Receivers = new List<Receiver>(),

                        },
                        new AppUser
                        {
                            Id = "user2",
                            Email = "raki2002@naver.com",
                            UserName = "park",
                            CanReceiveMessage = true,
                            PushCommands = new List<PushCommand>(),
                            Receivers = new List<Receiver>(),

                        },
                        new AppUser
                        {
                            Id = "user3",
                            Email = "raki2003@naver.com",
                            UserName = "makerdev",
                            CanReceiveMessage = true,
                            PushCommands = new List<PushCommand>(),
                            Receivers = new List<Receiver>(),
                        },

                };


                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Dbwls6683!");
                }

                context.SaveChanges();
            };

            if (!context.Receivers.Any())
            {
                var user2 = await context.Users.SingleOrDefaultAsync(u => u.Email == "raki2002@naver.com");
                var user4 = await context.Users.SingleOrDefaultAsync(u => u.Email == "raki2004@naver.com");


                var receivers = new List<Receiver>
                {
                    new Receiver
                    {
                        OwnerId = "user1",
                        DisplayName = "user4-dp",
                        ReceivingUser = user4,
                    },
                    new Receiver
                    {
                        OwnerId = "user1",
                        DisplayName = "user2-dp",
                        ReceivingUser = user2,
                    },
                    new Receiver
                    {
                        OwnerId = "user3",
                        DisplayName = "user2-dp",
                        ReceivingUser = user2,
                    },
                };

                context.Receivers.AddRange(receivers);
                context.SaveChanges();
            }

            if (!context.PushCommands.Any())
            {
                var user1 = await context.Users.SingleOrDefaultAsync(u => u.Email == "raki2001@naver.com");
                var user3 = await context.Users.SingleOrDefaultAsync(u => u.Email == "raki2003@naver.com");

                user1.PushCommands.Add(new PushCommand
                {
                    Message = "밥 먹어",
                    Name = "Comenow",

                    CommandReceivers = new List<CommandReceiver>
                    {
                        new CommandReceiver
                        {
                            Receiver = user1.Receivers.FirstOrDefault(r => r.ReceivingUser.Email == "raki2002@naver.com"),
                        },
                        new CommandReceiver
                        {
                            Receiver = user1.Receivers.FirstOrDefault(r => r.ReceivingUser.Email == "raki2004@naver.com"),
                        },
                    }
                });

                user1.PushCommands.Add(new PushCommand
                {
                    Message = "나가",
                    Name = "Getout",
                    CommandReceivers = new List<CommandReceiver>
                    {
                        new CommandReceiver
                        {
                            Receiver = user1.Receivers.FirstOrDefault(r => r.ReceivingUser.Email == "raki2002@naver.com"),
                        },
                    }
                });

                user3.PushCommands.Add(new PushCommand
                {
                    Message = "Come-from user3 to user 2",
                    Name = "Comenow",

                    CommandReceivers = new List<CommandReceiver>
                    {
                        new CommandReceiver
                        {
                            Receiver = user1.Receivers.FirstOrDefault(r => r.ReceivingUser.Email == "raki2002@naver.com"),
                        },
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
