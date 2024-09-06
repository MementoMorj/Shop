using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Models;

namespace Shop.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetBirthdayCustomers(DateTime date);
        Task<IEnumerable<CustomerLastPurchase>> GetRecentCustomers(int days);
    }

    public class CustomerLastPurchase
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime LastPurchaseDate { get; set; }
    }
}