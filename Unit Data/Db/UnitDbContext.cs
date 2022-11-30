using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Unit_Data.Models;
using Unit_Data.Models.Models;

namespace Unit_Data.Db
{
    public class UnitDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }


        //public DbSet<AppUser> appUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AppUserVM> UserVMs { get; set; }
        public DbSet<MessageUserVM> MessageUserVMs { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AppUserContact> AppUserContacts { get; set; }
        //public DbSet<AppUserAppUserContact> AppUserAppUserContact { get; set; }
    }
}
