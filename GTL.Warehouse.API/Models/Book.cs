namespace Georgia.Tech.Library.Models
{
    public class Book
    {
        public Guid id { get; set; }
        public required string title { get; set; }
        public required int quantity { get; set; }
        public required string price { get; set; }
    }
}
