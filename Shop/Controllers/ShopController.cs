using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using System;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public ShopController(ICustomerService customerService, IProductService productService)
        {
            _customerService = customerService;
            _productService = productService;
        }

        [HttpGet("birthdays")]
        public async Task<IActionResult> GetBirthdayCustomers([FromQuery] DateTime date)
        {
            var customers = await _customerService.GetBirthdayCustomers(date);
            return Ok(customers);
        }

        [HttpGet("recent-customers")]
        public async Task<IActionResult> GetRecentCustomers([FromQuery] int days)
        {
            var customers = await _customerService.GetRecentCustomers(days);
            return Ok(customers);
        }

        [HttpGet("demanded-categories")]
        public async Task<IActionResult> GetDemandedCategories([FromQuery] int customerId)
        {
            var categories = await _productService.GetDemandedCategories(customerId);
            return Ok(categories);
        }
    }
}