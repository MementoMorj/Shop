using Shop.Models;

namespace Shop.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ShopContext context)
        {
            // Перевіряємо, чи база даних вже містить дані
            if (context.Customers.Any() || context.Products.Any() || context.Purchases.Any())
            {
                return; // База даних вже заповнена
            }

            // Додаємо клієнтів
            var customers = new List<Customer>
            {
                new Customer { FullName = "Віталій Забродський", DateOfBirth = new DateTime(2002, 12, 8), RegistrationDate = DateTime.Now.AddYears(-2) },
                new Customer { FullName = "Ілон Маск", DateOfBirth = new DateTime(1990, 8, 21), RegistrationDate = DateTime.Now.AddYears(-1) },
                new Customer { FullName = "Біл Гейтс", DateOfBirth = new DateTime(1988, 3, 10), RegistrationDate = DateTime.Now.AddMonths(-6) }
            };
            context.Customers.AddRange(customers);

            // Додаємо продукти
            var products = new List<Product>
            {
                new Product { Name = "Айфон", Category = "Електроніка", ArticleNumber = "E001", Price = 15000 },
                new Product { Name = "МакБук", Category = "Електроніка", ArticleNumber = "E002", Price = 25000 },
                new Product { Name = "Футболка Найк", Category = "Одяг", ArticleNumber = "C001", Price = 500 },
                new Product { Name = "Кросівки Адідас", Category = "Одяг", ArticleNumber = "C002", Price = 1200 }
            };
            context.Products.AddRange(products);

            // Зберігаємо зміни, щоб отримати ID для клієнтів та продуктів
            context.SaveChanges();

            // Додаємо покупки
            var purchases = new List<Purchase>
            {
                new Purchase
                {
                    Number = 1001,
                    Date = DateTime.Now.AddDays(-10),
                    CustomerId = customers[0].Id,
                    Items = new List<PurchaseItem>
                    {
                        new PurchaseItem { ProductId = products[0].Id, Quantity = 1 },
                        new PurchaseItem { ProductId = products[2].Id, Quantity = 2 }
                    }
                },
                new Purchase
                {
                    Number = 1002,
                    Date = DateTime.Now.AddDays(-5),
                    CustomerId = customers[1].Id,
                    Items = new List<PurchaseItem>
                    {
                        new PurchaseItem { ProductId = products[1].Id, Quantity = 1 }
                    }
                },
                new Purchase
                {
                    Number = 1003,
                    Date = DateTime.Now.AddDays(-2),
                    CustomerId = customers[2].Id,
                    Items = new List<PurchaseItem>
                    {
                        new PurchaseItem { ProductId = products[2].Id, Quantity = 1 },
                        new PurchaseItem { ProductId = products[3].Id, Quantity = 1 }
                    }
                }
            };

            // Обчислюємо загальну вартість для кожної покупки
            foreach (var purchase in purchases)
            {
                purchase.TotalCost = purchase.Items.Sum(item => item.Quantity * products.First(p => p.Id == item.ProductId).Price);
            }

            context.Purchases.AddRange(purchases);

            // Зберігаємо всі зміни
            context.SaveChanges();
        }
    }
}
