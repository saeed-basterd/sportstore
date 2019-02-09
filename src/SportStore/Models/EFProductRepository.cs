using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Data;
using SportStore.Repository;

namespace SportStore.Models
{
    public class EfProductRepository : IProductRepository
    {
        private readonly SportStoreDataContext _context;

        public EfProductRepository(SportStoreDataContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}