using Microsoft.EntityFrameworkCore;
using MoneyFlow.Entities;

namespace MoneyFlow.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.UserId);
                e.Property(u => u.UserId).ValueGeneratedOnAdd();
                e.HasData(
                    new User()
                    {
                        UserId = 1,
                        FullName = "John Smith",
                        Email = "jhon@gmail.com",
                        Password = "password123"
                    });
            });

            modelBuilder.Entity<Service>(e =>
            {
                e.HasKey(u => u.ServiceId);
                e.Property(u => u.ServiceId).ValueGeneratedOnAdd();
                e.HasOne(e => e.User)
                 .WithMany(u => u.Services)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transaction>(e =>
            {
                e.HasKey(u => u.TransactionId);
                e.Property(u => u.TransactionId).ValueGeneratedOnAdd();
                e.HasOne(e => e.Service)
                 .WithMany()
                 .HasForeignKey(e => e.ServiceId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(e => e.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                e.Property(e => e.Date).HasColumnType("date");
                e.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            });
        }
    }
}
