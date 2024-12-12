namespace GTL.SearchService.API.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AmountInStock { get; set; }
    }
}