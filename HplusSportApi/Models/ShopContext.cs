using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HplusSport.Api.Models;

namespace HplusSportApi.Models
{
    public class ShopContext : DbContext
    {
        /// <summary>
        /// I have to build a constructor and then tell the frammework the classes I will use
        /// </summary>
        ///
        //get in the model data of the different classes that are on my mode I have to set and get the classes

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {}

        public object DataBase { get; internal set; }
        public object Products { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //the model to get the order category and user using this wil get the ID key for the cathegories.

            modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(a => a.Category).HasForeignKey(a => a.Category.Id);
            modelBuilder.Entity<Order>().HasMany(o => o.Products);
            modelBuilder.Entity<Order>().HasOne(o => o.User);
            modelBuilder.Entity<Users>().HasMany(u => u.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId);

            modelBuilder.Seed();

        }

    }
}
