using ComeNow.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeNow.Persistance
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<PushCommand> PushCommands { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Receiver>()
                .HasOne(r => r.Owner)
                .WithMany(au => au.Receivers)
                .HasForeignKey(r => r.OwnerId);

            builder.Entity<CommandReceiver>(x => x.HasKey(cr => new { cr.CommandId, cr.ReceiverId }));

            builder.Entity<CommandReceiver>()
                .HasOne(cr => cr.Receiver)
                .WithMany(r => r.CommandReceivers)
                .HasForeignKey(cr => cr.ReceiverId);

            builder.Entity<CommandReceiver>()
                .HasOne(cr => cr.PushCommand)
                .WithMany(p => p.CommandReceivers)
                .HasForeignKey(cr => cr.CommandId);
        }
    }
}
