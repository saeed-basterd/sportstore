using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportStore.Models;

namespace SportStore.Data
{
    public class SportStoreDataContext : DbContext
    {
        public SportStoreDataContext(
            DbContextOptions<SportStoreDataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}