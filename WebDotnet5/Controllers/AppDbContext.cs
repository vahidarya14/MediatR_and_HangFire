using Microsoft.EntityFrameworkCore;
using System;

namespace WebDotnet5.Controllers
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }




    }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public TimeSpan DateTime { get; set; } = DateTimeOffset.Now.TimeOfDay;
    }
}
