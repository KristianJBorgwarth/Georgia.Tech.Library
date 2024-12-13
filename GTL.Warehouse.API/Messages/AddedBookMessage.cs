namespace Georgia.Tech.Library.Messages
{


    public class AddedBookMessage
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; }
        public int? Quantity { get; set; }
    };
}
