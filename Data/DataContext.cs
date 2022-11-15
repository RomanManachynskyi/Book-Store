using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Models.Product_Entities.Book> Book { get; set; }
        public DbSet<Models.Product_Entities.Author> Author { get; set; }
        public DbSet<Models.Product_Entities.ProductCategory> Categories { get; set; }
        public DbSet<Models.User.User> User { get; set; }
        public DbSet<Models.Orders.Orders> Orders { get; set; }
        public DbSet<Models.Bucket.Bucket> Bucket { get; set; }
   }
}