using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MyChurch.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Counseling> Counselings { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Leader> Leaders { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Ministry> Ministries { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sermon> Sermons { get; set; }
        public virtual DbSet<Testimony> Testimonies { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donation>()
                .Property(e => e.Amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Counselings)
                .WithOptional(e => e.Member1)
                .HasForeignKey(e => e.Member);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Testimonies)
                .WithOptional(e => e.Member1)
                .HasForeignKey(e => e.Member);

            modelBuilder.Entity<Ministry>()
                .HasMany(e => e.Leaders)
                .WithOptional(e => e.Ministry1)
                .HasForeignKey(e => e.Ministry);

            modelBuilder.Entity<Ministry>()
                .HasMany(e => e.Members)
                .WithOptional(e => e.Ministry1)
                .HasForeignKey(e => e.Ministry);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);
        }
    }
}
