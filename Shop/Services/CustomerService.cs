using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ShopContext _context;

        public CustomerService(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetBirthdayCustomers(DateTime date)
        {
            return await _context.Customers
                .Where(c => c.DateOfBirth.Month == date.Month && c.DateOfBirth.Day == date.Day)
                .Select(c => new Customer { Id = c.Id, FullName = c.FullName })
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerLastPurchase>> GetRecentCustomers(int days)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-days);

            return await _context.Purchases
                .Where(p => p.Date >= cutoffDate)
                .GroupBy(p => p.CustomerId)
                .Select(g => new CustomerLastPurchase
                {
                    Id = g.Key,
                    FullName = g.First().Customer.FullName,
                    LastPurchaseDate = g.Max(p => p.Date)
                })
                .ToListAsync();
        }
    }
}