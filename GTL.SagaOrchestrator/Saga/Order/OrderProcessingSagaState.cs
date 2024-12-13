using GTL.SagaOrchestrator.Abstractions;

namespace GTL.SagaOrchestrator.Saga.Order;

public class OrderProcessingSagaState : BaseState
{
    public Guid CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public List<Guid> BookIds { get; set; }
    public int Price { get; set; }
    public string CardNumber { get; set; } = null!;
    public string ExpirationDate { get; set; } = null!;
    public int CVC { get; set; }
}