using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using wbl.Controllers;
using wbl.Models;

namespace wbl.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Admin> tb_Admin { get; set; }
        public DbSet<Announcement> tb_Announcement { get; set; }
        public DbSet<Therapist> tb_Therapist { get; set; }
        public DbSet<Parent1> tb_parent1 { get; set; }
        public DbSet<Parent2> tb_parent2 { get; set; }
        public DbSet<Child> tb_child { get; set; }
        public DbSet<Programs> tb_Program { get; set; }
        public DbSet<CustomerService> CustomerServices { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Availibility> Availabilities { get; set; }
        public DbSet<SessionTime> SessionTimes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<TreatmentHistory> TreatmentHistories { get; set; }

        public DbSet<Invoices> Invoicess { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var Admin = new IdentityRole("Admin");
            Admin.NormalizedName = "Admin";

            var Parent = new IdentityRole("Parent");
            Parent.NormalizedName = "Parent";

            var CustomerService = new IdentityRole("CustomerService");
            CustomerService.NormalizedName = "CustomerService";

            var Therapist = new IdentityRole("Therapist");
            Therapist.NormalizedName = "Therapist";

            builder.Entity<IdentityRole>().HasData(Admin, Parent, CustomerService, Therapist);

            builder.Entity<Child>().ToTable("tb_child");
            builder.Entity<Parent1>().ToTable("tb_parent1");
            builder.Entity<Parent2>().ToTable("tb_parent2");

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Therapist)
                .WithOne(b => b.ApplicationUser)
                .HasForeignKey<Therapist>(b => b.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Parent1)
                .WithOne(b => b.ApplicationUser)
                .HasForeignKey<Parent1>(b => b.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Child>()
                .HasOne(c => c.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(c => c.FatherIc)
                .HasPrincipalKey(p => p.FatherIc);

            builder.Entity<CustomerService>()
                .HasOne(cs => cs.ApplicationUser)
                .WithOne(u => u.CustomerService)
                .HasForeignKey<CustomerService>(cs => cs.ApplicationUserId);

            builder.Entity<Report>()
                .HasOne(r => r.Session)
                .WithMany()
                .HasForeignKey(r => r.SessionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Session>()
                .HasOne(s => s.Therapist)
                .WithMany()
                .HasForeignKey(s => s.TherapistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Session>()
                .HasOne(s => s.Child)
                .WithMany()
                .HasForeignKey(s => s.ChildId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Session>()
                .HasOne(s => s.CustomerService)
                .WithMany()
                .HasForeignKey(s => s.CustomerServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TreatmentHistory>()
                .HasOne(th => th.Child)
                .WithMany(c => c.TreatmentHistories)
                .HasForeignKey(th => th.ChildId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Programs>(entity =>
            {
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });

            builder.Entity<Invoices>()
                .HasOne(i => i.Child)
                .WithMany()
                .HasForeignKey(i => i.ChildId)
                .OnDelete(DeleteBehavior.Restrict);



        }

    }
}
