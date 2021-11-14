using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCMS.API.Models;

namespace SCMS.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Packet> Packets { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PacketsUsers> PacketsUsers { get; set; }
        public DbSet<PacketsActivities> PacketsActivities { get; set; }
        public DbSet<PacketsPayments> PacketsPayments { get; set; }
        public DbSet<UsersClasses> UsersClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // create tables for Identity

            modelBuilder.Entity<Class>()
                        .HasOne(typeof(Activity))
                        .WithMany()
                        .HasForeignKey("ActivityId");

            modelBuilder.Entity<Class>()
                        .HasOne(typeof(User))
                        .WithMany()
                        .HasForeignKey("TrainerUserId");

            modelBuilder.Entity<Payment>()
                        .HasOne(typeof(User))
                        .WithMany()
                        .HasForeignKey("UserId");

            //ManyToMany relations

            //PacketsUsers
            modelBuilder.Entity<PacketsUsers>()
                .HasKey(t => new { t.PacketId, t.UserId });

            modelBuilder.Entity<PacketsUsers>()
                .HasOne(typeof(Packet))
                .WithMany()
                .HasForeignKey("PacketId");

            modelBuilder.Entity<PacketsUsers>()
                .HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("UserId");

            //PacketsActivities
            modelBuilder.Entity<PacketsActivities>()
                .HasKey(t => new { t.PacketId, t.ActivityId });

            modelBuilder.Entity<PacketsActivities>()
                .HasOne(typeof(Packet))
                .WithMany()
                .HasForeignKey("PacketId");

            modelBuilder.Entity<PacketsActivities>()
                .HasOne(typeof(Activity))
                .WithMany()
                .HasForeignKey("ActivityId");

            //PacketsPayments
            modelBuilder.Entity<PacketsPayments>()
                .HasKey(t => new { t.PacketId, t.PaymentId });

            modelBuilder.Entity<PacketsPayments>()
                .HasOne(typeof(Packet))
                .WithMany()
                .HasForeignKey("PacketId");

            modelBuilder.Entity<PacketsPayments>()
                .HasOne(typeof(Payment))
                .WithMany()
                .HasForeignKey("PaymentId");

            //UsersClasses
            modelBuilder.Entity<UsersClasses>()
                .HasKey(t => new { t.UserId, t.ClassId });

            modelBuilder.Entity<UsersClasses>()
                .HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("UserId");

            modelBuilder.Entity<UsersClasses>()
                .HasOne(typeof(Class))
                .WithMany()
                .HasForeignKey("ClassId");
        }
    }
}
