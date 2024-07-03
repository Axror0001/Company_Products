namespace Products.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Model { get; set; }
        public string Breand { get; set; }
        public List<Product> Products { get; } = new List<Product>();
    }
}
