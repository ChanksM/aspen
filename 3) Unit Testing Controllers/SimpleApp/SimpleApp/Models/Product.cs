namespace SimpleApp.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public string? ProductType { get; set; }
       
    }
    public class ProductDataSource : IDataSource
    {
        public IEnumerable<Product> Products =>
        new Product[] {
        new Product { Name = "Watermelon", Price = 10.6M , ProductType = "Food"},
        new Product { Name = "Basketball", Price = 103M , ProductType = "Sport"},
        new Product { Name = "Transistor", Price = 8.2M , ProductType = "Electronics"}
        };
    }
}