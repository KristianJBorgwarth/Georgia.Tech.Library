namespace Georgia.Tech.Library.Messages
{
    public class ProcessOrderFailedMessage
    {
        public Guid OrderId { get; set; }
        public List<Guid>? BookIds { get; set; }
        public List<int>? Quantities { get; set; }
    }
}