using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class KorytoDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=(local)\SQLEXPRESS;Initial Catalog=KorytoDatabase1;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Order> Orders { set; get; }

        public DbSet<Client> Clients { set; get; }

        public DbSet<Request> Requests { set; get; }

        public DbSet<Detail> Details { set; get; }

        public DbSet<Car> Cars { set; get; }

        public DbSet<OrderCar> OrderCars { set; get; }

        public DbSet<CarDetail> CarDetails { set; get; }

        public DbSet<DetailRequest> DetailRequests { set; get; }
    }
}
