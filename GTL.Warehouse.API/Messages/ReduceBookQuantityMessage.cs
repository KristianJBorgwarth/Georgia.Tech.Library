namespace Georgia.Tech.Library.Messages
{
    public class ReduceBookQuantityMessage
    {
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}
