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
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<PacketsActivities> PacketsActivities { get; set; }
        public DbSet<ClassEnrollment> ClassEnrollments { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        
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

            modelBuilder.Entity<Payment>()
                        .HasOne(typeof(Packet))
                        .WithMany()
                        .HasForeignKey("PacketId")
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                        .HasOne(typeof(User))
                        .WithMany()
                        .HasForeignKey("UserId");

            modelBuilder.Entity<Subscription>()
                        .HasOne(typeof(Packet))
                        .WithMany()
                        .HasForeignKey("PacketId");

            modelBuilder.Entity<Announcement>()
                        .HasOne(typeof(User))
                        .WithMany()
                        .HasForeignKey("CreatedBy");
            //ManyToMany relations

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

            //ClassEnrollments
            modelBuilder.Entity<ClassEnrollment>()
                .HasKey(t => new { t.UserId, t.ClassId });

            modelBuilder.Entity<ClassEnrollment>()
                .HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("UserId");

            modelBuilder.Entity<ClassEnrollment>()
                .HasOne(typeof(Class))
                .WithMany()
                .HasForeignKey("ClassId");
        }
    }
}
