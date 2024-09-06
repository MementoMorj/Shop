namespace Shop.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalCost { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<PurchaseItem> Items { get; set; }
    }

}
