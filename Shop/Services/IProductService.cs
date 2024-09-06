using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Services
{
    public interface IProductService
    {
        Task<IEnumerable<CategoryDemand>> GetDemandedCategories(int customerId);
    }

    public class CategoryDemand
    {
        public string Category { get; set; }
        public int TotalQuantity { get; set; }
    }
}