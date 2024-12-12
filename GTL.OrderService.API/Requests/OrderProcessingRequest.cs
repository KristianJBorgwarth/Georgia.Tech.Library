// ReSharper disable InconsistentNaming
namespace GTL.OrderService.API.Requests;

public sealed record OrderProcessingRequest
{
    public Guid CustomerId { get; init; }
    public Guid OrderId { get; init; }
    public required string CardNumber { get; init; }
    public required string ExpirationDate { get; init; }
    public required int CVC { get; init; }
}