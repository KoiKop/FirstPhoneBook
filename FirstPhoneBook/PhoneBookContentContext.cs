using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FirstPhoneBook
{
    public partial class PhoneBookContentContext : DbContext
    {
        private string connectionString;

        public PhoneBookContentContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public PhoneBookContentContext(DbContextOptions<PhoneBookContentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PhoneBookContent> PhoneBookContent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString); //connection string  "Server=ponikiewskipc\\sqlexpress;Database=PhoneBookContent;Trusted_Connection=True;"
                //optionsBuilder.UseSqlServer("Server=ponikiewskipc\\sqlexpress;Database=PhoneBookContentTests;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<PhoneBookContent>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__PhoneBoo__1788CC4CCBC4883B");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
