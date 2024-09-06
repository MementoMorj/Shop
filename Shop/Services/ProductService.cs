using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Services;

namespace Shop.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopContext _context;

        public ProductService(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDemand>> GetDemandedCategories(int customerId)
        {
            return await _context.PurchaseItems
                .Where(pi => pi.Purchase.CustomerId == customerId)
                .GroupBy(pi => pi.Product.Category)
                .Select(g => new CategoryDemand
                {
                    Category = g.Key,
                    TotalQuantity = g.Sum(pi => pi.Quantity)
                })
                .ToListAsync();
        }
    }
}