using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            var line = _lineCollection
                .FirstOrDefault(p => p.Product.ProductID == product.ProductID);

            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quality = quantity
                });
            }
            else
                line.Quality += quantity;
        }

        public virtual void RemoveLine(Product product)
        {
            _lineCollection.RemoveAll(
                l => l.Product.ProductID == product.ProductID);
        }

        public virtual decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(l => l.Product.Price * l.Quality);
        }

        public virtual void Clear() => _lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => _lineCollection;
    }

    public class CartLine
    {
        public int CartLineId { get; set; }

        public Product Product { get; set; }

        public int Quality { get; set; }
    }
}