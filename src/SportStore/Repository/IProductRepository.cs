using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Models;

namespace SportStore.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}