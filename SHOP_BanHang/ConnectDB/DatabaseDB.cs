using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SHOP_BanHang.ConnectDB
{
    public partial class DatabaseDB : DbContext
    {
        public DatabaseDB()
            : base("name=DatabaseDB")
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .Property(e => e.Images)
                .IsFixedLength();

            //modelBuilder.Entity<Brand>()
            //    .Property(e => e.Slug)
            //    .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Slug)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.NameBrand)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
