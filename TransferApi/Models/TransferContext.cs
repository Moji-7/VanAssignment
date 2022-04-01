
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;



namespace TransferApi.Models
{
    public class TransferContext : DbContext
    {
        public TransferContext(DbContextOptions<TransferContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            
            // modelBuilder.Entity<Cart>()
            // .HasOne(a => a.CartInfo)
            // .WithOne(a => a.CardNumber)
            // .HasForeignKey<CartInfo>(c => c.cardNumber);
            // base.OnModelCreating(modelBuilder);
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferTransaction> TransferTransactions { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Course>().ToTable("Course");
        //     modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        //     modelBuilder.Entity<Student>().ToTable("Student");
        //     modelBuilder.Entity<Student>().ToTable("Student");
        // }
    }
}