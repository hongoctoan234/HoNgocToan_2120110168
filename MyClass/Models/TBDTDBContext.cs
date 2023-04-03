using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class TBDTDBContext : DbContext
    {
        public TBDTDBContext() : base("name=StrConnect") { }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DetailPrd> DetailPrds { get; set; }
        public DbSet<ProductIMG> ProductIMGs { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
